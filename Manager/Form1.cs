using System;
using System.Text;
using System.Windows.Forms;
using Password;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;

namespace Manager
{
	public partial class Form1 : Form
	{
		internal static string BackgroundDirectory { get { return $"{Directory.GetCurrentDirectory()}\\Images"; } }
		/// <summary>
		/// Don't put more stuff in this, I am eventually gonna replace this<br></br>
		/// Use <see cref="_settings"/> instead.
		/// </summary>
		internal static Settings settings_old { get; set; }
		/// <summary>
		/// Contains various settings and other information that needs to be saved.
		/// </summary>
		private MetaEntry _settings {  get; set; }
		//internal static string BackgroundLocation { get { return $"{Directory.GetCurrentDirectory()}\\bg{settings.FileExtension}"; } }
		private string SettingsFile { get { return $"{Directory.GetCurrentDirectory()}\\settings.json"; } }
		private string EntryDirectory { get; set; } = $"{Directory.GetCurrentDirectory()}\\Entries";
		private string MasterPassword { get; set; }
		private List<PasswordEntry> PasswordEntries { get; set; } = new List<PasswordEntry>();
		private List<TextEntry> TextEntries { get; set; } = new List<TextEntry>();
		private List<FileEntry> FileEntries { get; set; } = new List<FileEntry>();
		public Form1()
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			Directory.CreateDirectory(EntryDirectory);
			Directory.CreateDirectory(BackgroundDirectory);
			EncryptedFile.FileDirectory = EntryDirectory;
			if (File.Exists(SettingsFile))
			{
				settings_old = Settings.GetSettings(SettingsFile);
				this.ApplySettings();
				toolStripMenuItemIncognito.Checked = settings_old.Incognito;
			}
			else
				settings_old = new();
			toolStripMenuItemFont.Click += ToolStripMenuItemFont_Click;
			toolStripMenuItemBackground.Click += ToolStripMenuItemBackground_Click;
			toolStripMenuItemIncognito.Click += ToolStripMenuItemIncognito_Click;
			toolStripMenuItemPassword.Click += ToolStripMenuItemPassword_Click;
			using (PasswordFetcher fetcher = new())
			{
				DialogResult result = fetcher.ShowDialog();
				if (result == DialogResult.OK)
					MasterPassword = fetcher.Password;
			}
			try { PasswordPatcher.TwoToThreePatcher(EntryDirectory, GetHash(MasterPassword)); }
			catch (CryptographicException) { DisplayError("Wrong master password."); Environment.Exit(0); }
			LoadListItems();
		}
		private void LoadListItems()
		{
			string[] paths = Directory.GetFiles(EntryDirectory);
			for (int i = 0; i < paths.Length; i++)
			{
				EncryptedFile file;
				try { file = EncryptedFile.GetFile(GetHash(MasterPassword), paths[i]); }
				catch (CryptographicException) { DisplayError("Wrong master password."); Environment.Exit(0); return; }
				catch (FileNotFoundException e) { DisplayError($"File {e.FileName} not found."); continue; }
				catch (FileLoadException e) { DisplayError($"File {e.FileName} was corrupt"); continue; }
				catch (Exception e) { DisplayError(e); continue; }
				if (file.GetType() == typeof(PasswordEntry))
				{
					AddPasswordEntry(file as PasswordEntry);
					continue;
				}
				if (file.GetType() == typeof(FileEntry))
				{
					AddFileEntry(file as FileEntry);
					continue;
				}
				if (file.GetType() == typeof(TextEntry))
				{
					AddTextEntry(file as TextEntry);
					continue;
				}
				if(file.GetType() == typeof(Metafile))
				{
					_settings = file as MetaEntry;
					continue;
				}
				throw new UnhandledTypeException("File type is not handled", file.GetType());
			}
			if (_settings == null)
				_settings = new(GetHash(MasterPassword));
		}
		private void ToolStripMenuItemIncognito_Click(object sender, EventArgs e)
		{
			settings_old.Incognito = toolStripMenuItemIncognito.Checked;
			Settings.SaveSettings(SettingsFile, settings_old);
		}
		private void ToolStripMenuItemBackground_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new())
			{
				dialog.Multiselect = false;
				dialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.PNG;*.TIFF;*.TIF;*.GIF)|*.BMP;*.JPG;*.JPEG;*.PNG;*.TIFF;*.TIF;*.GIF";
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					byte[] hash = MD5.Create().ComputeHash(File.ReadAllBytes(dialog.FileName));
					string file = $"{BackgroundDirectory}\\{GetHexString(hash)}{Path.GetExtension(dialog.FileName)}";
					if (!File.Exists(file))
						File.Copy(dialog.FileName, file);
					settings_old.BackgroundLocation = file;
					Settings.SaveSettings(SettingsFile, settings_old);
					this.ApplySettings();
				}
			}
		}
		private void ToolStripMenuItemFont_Click(object sender, EventArgs e)
		{
			using (FontDialog dialog = new())
			{
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					settings_old.FontName = dialog.Font.FontFamily.Name;
					Settings.SaveSettings(SettingsFile, settings_old);
					this.ApplySettings();
				}
			}
		}
		private void ToolStripMenuItemPassword_Click(object sender, EventArgs e)
		{
			string newPassword = null;
			using (PasswordChanger changer = new())
			{
				changer.Tag = MasterPassword;
				DialogResult dialogResult = changer.ShowDialog();
				if (dialogResult != DialogResult.OK)
					return;
				newPassword = changer.Tag.ToString();
			}
			List<EncryptedFile> files = new(PasswordEntries);
			files.AddRange(FileEntries);
			files.AddRange(TextEntries);
			files.Add(_settings);
			//I swear to god if I have to go back at some point and make this into a for loop I will never make a
			//foreach loop again.
			foreach (EncryptedFile file in files)
			{
				file.ChangeKey(GetHash(newPassword));
			}
			MasterPassword = newPassword;
		}
		#region PasswordStuff
		private void AddPasswordEntry(PasswordEntry entry)
		{
			PasswordEntries.Add(entry);
			ListViewItem item = MakePasswordListItem(entry);
			listViewPasswords.Items.Add(item);
		}
		private static ListViewItem MakePasswordListItem(PasswordEntry entry)
		{
			ListViewItem item = new ListViewItem()
			{
				Text = entry.Service,
				Tag = entry,
				Name = entry.ID.ToString()
			};
			item.SubItems.Add(entry.AccountName);
			return item;
		}
		private void ListViewEntries_ItemActivate(object sender, EventArgs e)
		{
			PasswordEntry entry;
			try { entry = GetActivePasswordEntry(); }
			catch { return; }
			LoadPasswordEntry(entry);
		}
		/// <summary>
		/// This is needed so the stack won't overflow when resizing the columns, just ignore it.
		/// </summary>
		private bool ignoreResize = false;
		private void listViewDetails_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			if (ignoreResize)
				return;
			ignoreResize = true;
			ListView list = sender as ListView;
			int totalWdith = list.ClientSize.Width, index;
			if (e.ColumnIndex == 0)
				index = 1;
			else
				index = 0;
			list.Columns[index].Width = totalWdith - list.Columns[e.ColumnIndex].Width;
			ignoreResize = false;
		}
		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			using (PasswordEntryMaker maker = new(GetHash(MasterPassword)))
			{
				handlePasswordMaker(maker);
			}
		}
		private void ButtonEdit_Click(object sender, EventArgs e)
		{
			PasswordEntry entry;
			try { entry = GetActivePasswordEntry(); }
			catch { return; }
			using (PasswordEntryMaker maker = new(GetHash(MasterPassword), entry))
			{
				DialogResult result = handlePasswordMaker(maker);
				if (result == DialogResult.OK)
				{

				}
			}
		}
		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			PasswordEntry entry;
			try { entry = GetActivePasswordEntry(); }
			catch { return; }
			entry.Delete();
			ClearInfo();
			listViewPasswords.Items.RemoveByKey(entry.ID.ToString());
			entry.ReleaseID();
		}
		private void buttonCopy_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxInfoPassword.Text))
			{
				DisplayError("Nothing to copy");
				return;
			}
			Clipboard.SetText(textBoxInfoPassword.Text);
		}
		private void buttonWebsite_Click(object sender, EventArgs e)
		{
			if (textBoxInfoDomain.Text.Length == 0)
			{
				DisplayError("No website is available.");
				return;
			}
			ProcessStartInfo info = new()
			{
				FileName = "firefox",
				UseShellExecute = true
			};
			if (settings_old.Incognito)
			{
				info.Arguments += "-private-window";
			}
			info.Arguments += $" {textBoxInfoDomain.Text}";
			try { Process.Start(info); }
			catch (Exception ex) { DisplayError(ex); }
		}
		private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxVisible.Checked)
			{
				textBoxInfoPassword.PasswordChar = char.MinValue;
				return;
			}
			textBoxInfoPassword.PasswordChar = '*';
		}
		private void LoadPasswordEntry(PasswordEntry entry)
		{
			textBoxInfoAccount.Text = entry.AccountName;
			if (entry.Domain != null)
				textBoxInfoDomain.Text = entry.Domain.OriginalString;
			else
				textBoxInfoDomain.Text = null;
			textBoxInfoPassword.Text = entry.Password;
			textBoxInfoService.Text = entry.Service;
		}
		private void ClearInfo()
		{
			foreach (object e in infoPanel.Controls)
				if (e.GetType() == typeof(TextBox))
					(e as TextBox).Text = "";
		}
		private PasswordEntry GetActivePasswordEntry()
		{
			PasswordEntry entry;
			try { entry = listViewPasswords.SelectedItems[0].Tag as PasswordEntry; }
			catch (ArgumentOutOfRangeException) { DisplayError("Nothing is selected."); throw new Exception(); }
			catch (Exception) { DisplayError("Something went wrong."); throw; }
			return entry;
		}
		private DialogResult handlePasswordMaker(PasswordEntryMaker maker)
		{
			DialogResult result = maker.ShowDialog();
			if (result == DialogResult.OK)
			{
				maker.Entry.Save();
				LoadPasswordEntry(maker.Entry);
				listViewPasswords.Items.RemoveByKey(maker.Entry.ID.ToString());
				listViewPasswords.Items.Add(MakePasswordListItem(maker.Entry));
			}
			return result;
		}
		#endregion
		#region TextStuff
		private TextEntry currentlyLoadedTextEntry;
		private void PromptSave()
		{
			DialogResult r = MessageBox.Show($"You have unsaved changes.{Environment.NewLine}Do you want to save?",
				"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (r == DialogResult.No)
				return;
			currentlyLoadedTextEntry.Text = textBoxBody.Text;
			currentlyLoadedTextEntry.Title = textBoxTitle.Text;
			currentlyLoadedTextEntry.Save();
			listViewText.Items.RemoveByKey(currentlyLoadedTextEntry.ID.ToString());
			listViewText.Items.Add(MakeTextListItem(currentlyLoadedTextEntry));
		}
		private bool NeedSavePrompt()
		{
			if (currentlyLoadedTextEntry == null)
				return false;
			if (currentlyLoadedTextEntry.Text != textBoxBody.Text)
				return true;
			if (currentlyLoadedTextEntry.Title != textBoxTitle.Text)
				return true;
			return false;
		}
		private void AddTextEntry(TextEntry entry)
		{
			TextEntries.Add(entry);
			listViewText.Items.Add(MakeTextListItem(entry));
		}
		private ListViewItem MakeTextListItem(TextEntry entry)
		{
			ListViewItem item = new ListViewItem()
			{
				Name = entry.ID.ToString(),
				Tag = entry,
				Text = entry.Title
			};
			return item;
		}
		private void buttonNewText_Click(object sender, EventArgs e)
		{
			TextEntry entry = new(GetHash(MasterPassword))
			{
				Title = "Placeholder Title"
			};
			if (NeedSavePrompt())
				PromptSave();
			currentlyLoadedTextEntry = entry;
			AddTextEntry(entry);
			entry.Save();
			textBoxBody.Text = "";
			textBoxBody.ReadOnly = false;
			textBoxTitle.Text = "Placeholder Title";
			textBoxTitle.ReadOnly = false;
		}
		#endregion
		#region FileStuff
		private FileEntry GetActiveFileEntry()
		{
			FileEntry entry;
			try { entry = listViewFiles.SelectedItems[0].Tag as FileEntry; }
			catch (ArgumentOutOfRangeException) { DisplayError("Nothing is selected."); throw new Exception(); }
			catch (Exception) { DisplayError("Something went wrong."); throw; }
			return entry;
		}
		private void AddFileEntry(FileEntry entry)
		{
			FileEntries.Add(entry);
			listViewFiles.Items.Add(MakeFileListItem(entry));
		}
		private ListViewItem MakeFileListItem(FileEntry entry)
		{
			ListViewItem item = new ListViewItem()
			{
				Name = entry.ID.ToString(),
				Tag = entry,
				Text = entry.OriginalFileName
			};
			item.SubItems.Add(entry.EncryptedSize);
			return item;
		}
		private void buttonImportFile_Click(object sender, EventArgs e)
		{
			DialogResult r = dialogImport.ShowDialog();
			if (r != DialogResult.OK)
				return;
			FileEntry entry = new(GetHash(MasterPassword));
			entry.Import(dialogImport.FileName);
			entry.Save();
			AddFileEntry(entry);
		}
		private void buttonExportFile_Click(object sender, EventArgs e)
		{
			FileEntry entry;
			try { entry = GetActiveFileEntry(); }
			catch { return; }
			dialogExport.FileName = entry.OriginalFileName;
			DialogResult r = dialogExport.ShowDialog();
			if (r != DialogResult.OK)
				return;
			entry.Export(dialogExport.FileName);
		}
		private void buttonDeleteFile_Click(object sender, EventArgs e)
		{
			ListViewItem[] items = new ListViewItem[listViewFiles.SelectedItems.Count];
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = listViewFiles.SelectedItems[i];
			}
			FileEntry entry;
			for (int i = 0; i < items.Length; i++)
			{
				entry = items[i].Tag as FileEntry;
				listViewFiles.Items.RemoveByKey(entry.ID.ToString());
				entry.Delete();
				entry.ReleaseID();
			}
		}
		private void listViewFiles_ItemActivate(object sender, EventArgs e)
		{
			FileEntry entry;
			try { entry = GetActiveFileEntry(); }
			catch { return; }
			textBoxFileSize.Text = entry.OriginalSize.ToString();
		}
		private void listViewFiles_DragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
				return;
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			for (int i = 0; i < files.Length; i++)
			{
				string file = files[i];
				FileEntry entry = new(GetHash(MasterPassword));
				entry.Import(file);
				entry.Save();
				AddFileEntry(entry);
			}
		}
		private void listViewFiles_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}
		#endregion
		internal static void DisplayError(Exception e) => DisplayError(e.Message);
		internal static void DisplayError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
		internal static byte[] GetHash(string text) => SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(text));
		internal static string GetHexString(byte[] bytes)
		{
			StringBuilder builder = new(bytes.Length * 2);
			foreach (byte b in bytes)
				builder.AppendFormat("{0:x2}", b);
			return builder.ToString();
		}
	}
}

