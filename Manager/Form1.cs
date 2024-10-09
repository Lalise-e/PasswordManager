using System;
using System.Text;
using System.Windows.Forms;
using Password;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;

namespace Manager
{
	public partial class Form1 : Form
	{
		internal static string BackgroundDirectory { get { return $"{Directory.GetCurrentDirectory()}\\Images"; } }
		/// <summary>
		/// Don't put more stuff in this, I am eventually gonna replace this<br></br>
		/// Use <see cref="_settings"/> instead.
		/// </summary>
		internal static Settings_old settings_old { get; set; }
		/// <summary>
		/// Contains various settings and other information that needs to be saved.
		/// </summary>
		private MetaEntry _settings { get; set; }
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
				settings_old = Settings_old.GetSettings(SettingsFile);
				this.ApplySettings();
				toolStripMenuItemIncognito.Checked = settings_old.Incognito;
			}
			else
				settings_old = new();
			toolStripMenuItemFont.Click += ToolStripMenuItemFont_Click;
			toolStripMenuItemBackground.Click += ToolStripMenuItemBackground_Click;
			toolStripMenuItemIncognito.Click += ToolStripMenuItemIncognito_Click;
			toolStripMenuItemPassword.Click += ToolStripMenuItemPassword_Click;
			toolStripMenuItemDeleteImport.Click += ToolStripMenuItemDeleteImport_Click;
			toolStripMenuItemDeleteExport.Click += ToolStripMenuItemDeleteExport_Click;
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
				if (file.GetType() == typeof(MetaEntry))
				{
					_settings = file as MetaEntry;
					continue;
				}
				throw new UnhandledTypeException("File type is not handled", file.GetType());
			}
			if (_settings == null)
				_settings = new(GetHash(MasterPassword));
			LoadSettings();
		}
		private void LoadSettings()
		{
			toolStripMenuItemDeleteImport.Checked = _settings.DeleteImport;
			toolStripMenuItemDeleteExport.Checked = _settings.DeleteExport;
			if (_settings.ListViewWidthFiles != 0)
				listViewFiles.Columns[0].Width = _settings.ListViewWidthFiles;
			if (_settings.ListViewWidthPassword != 0)
				listViewPasswords.Columns[0].Width = _settings.ListViewWidthPassword;
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			if (Directory.Exists(tempFolder))
				Directory.Delete(tempFolder, true);
			base.OnClosing(e);
		}
		private void ToolStripMenuItemIncognito_Click(object sender, EventArgs e)
		{
			settings_old.Incognito = toolStripMenuItemIncognito.Checked;
			Settings_old.SaveSettings(SettingsFile, settings_old);
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
					Settings_old.SaveSettings(SettingsFile, settings_old);
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
					Settings_old.SaveSettings(SettingsFile, settings_old);
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
		private void ToolStripMenuItemDeleteImport_Click(object sender, EventArgs e)
		{
			toolStripMenuItemDeleteImport.Checked = !toolStripMenuItemDeleteImport.Checked;
			_settings.DeleteImport = toolStripMenuItemDeleteImport.Checked;
			_settings.Save();
		}
		private void ToolStripMenuItemDeleteExport_Click(object sender, EventArgs e)
		{
			toolStripMenuItemDeleteExport.Checked = !toolStripMenuItemDeleteExport.Checked;
			_settings.DeleteExport = toolStripMenuItemDeleteExport.Checked;
			_settings.Save();
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
			if (sender == listViewFiles)
				_settings.ListViewWidthFiles = list.Columns[0].Width;
			if (sender == listViewPasswords)
				_settings.ListViewWidthPassword = list.Columns[0].Width;
			_settings.Save();
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
			textBoxInfoEmail.Text = entry.Email;
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
			listViewText.Items[currentlyLoadedTextEntry.ID.ToString()].Text = currentlyLoadedTextEntry.Title;
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
		private ListViewItem GetActiveText()
		{
			if (listViewText.SelectedItems.Count == 0)
				return null;
			return listViewText.SelectedItems[0];
		}
		private void LoadTextEntry(TextEntry entry)
		{
			currentlyLoadedTextEntry = entry;
			if (currentlyLoadedTextEntry == null)
			{
				textBoxTitle.ReadOnly = true;
				textBoxTitle.Text = "";
				textBoxBody.ReadOnly = true;
				textBoxBody.Text = "";
				return;
			}
			textBoxTitle.ReadOnly = false;
			textBoxTitle.Text = currentlyLoadedTextEntry.Title;
			textBoxBody.ReadOnly = false;
			textBoxBody.Text = currentlyLoadedTextEntry.Text;
		}
		private void buttonNewText_Click(object sender, EventArgs e)
		{
			TextEntry entry = new(GetHash(MasterPassword))
			{
				Title = "Placeholder Title"
			};
			if (NeedSavePrompt())
				PromptSave();
			AddTextEntry(entry);
			entry.Save();
			LoadTextEntry(entry);
			foreach(ListViewItem l in  listViewText.SelectedItems)
			{
				l.Selected = false;
			}
			listViewText.Items[entry.ID.ToString()].Selected = true;
		}
		private void buttonSaveText_Click(object sender, EventArgs e)
		{
			if (currentlyLoadedTextEntry == null)
				return;
			currentlyLoadedTextEntry.Text = textBoxBody.Text;
			currentlyLoadedTextEntry.Title = textBoxTitle.Text;
			currentlyLoadedTextEntry.Save();
			listViewText.Items[currentlyLoadedTextEntry.ID.ToString()].Text = currentlyLoadedTextEntry.Title;
		}
		private void buttonDeleteText_Click(object sender, EventArgs e)
		{
			ListViewItem item = GetActiveText();
			TextEntry entry = item.Tag as TextEntry;
			if (item == null)
				return;
			DialogResult r = MessageBox.Show($"Are you sure you want to delete {entry.Title}?",
				"Delete?", MessageBoxButtons.YesNo);
			if (r == DialogResult.No)
				return;
			listViewText.Items.Remove(item);
			if (currentlyLoadedTextEntry == entry)
				LoadTextEntry(null);
			entry.Delete();
			entry.ReleaseID();
		}
		private void listViewText_ItemActivate(object sender, EventArgs e)
		{
			ListViewItem selected = GetActiveText();
			if (selected == null || selected.Tag == currentlyLoadedTextEntry)
				return;
			if (NeedSavePrompt())
				PromptSave();
			LoadTextEntry(selected.Tag as TextEntry);
		}
		#endregion
		#region FileStuff
		private string tempFolder { get { return $"{Directory.GetCurrentDirectory()}\\temp"; } }
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
			if (_settings.DeleteImport)
				File.Delete(dialogImport.FileName);
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
			if (_settings.DeleteExport)
			{
				listViewFiles.Items.RemoveByKey(entry.ID.ToString());
				entry.Delete();
				entry.ReleaseID();
			}
		}
		private void buttonDeleteFile_Click(object sender, EventArgs e)
		{
			deleteSelectedFiles();
		}
		private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			long size = 0;
			for (int i = 0; i < listViewFiles.SelectedItems.Count; i++)
			{
				size += (listViewFiles.SelectedItems[i].Tag as FileEntry).InnerSize;
			}
			if (size == 0)
			{
				textBoxFileSize.Text = string.Empty;
				return;
			}
			textBoxFileSize.Text = FileEntry.GetFileSize(size);
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
				if (_settings.DeleteImport)
				{
					File.Delete(files[i]);
				}
			}
		}
		private void listViewFiles_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}
		private void listViewFiles_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DataObject dObject = GetSelectedFiles(!_settings.DeleteExport, out FileEntry[] files);
			if (dObject == null)
				return;
			DragDropEffects s = DoDragDrop(dObject, DragDropEffects.Move);
			if (_settings.DeleteExport)
			{
				for (int i = 0; i < files.Length; i++)
				{
					listViewFiles.Items.RemoveByKey(files[i].ID.ToString());
					files[i].Delete();
					files[i].ReleaseID();
				}
			}
		}
		private void listViewFiles_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				e.SuppressKeyPress = true;
				switch (e.KeyCode)
				{
					case Keys.A:
						selectAllItems(sender as ListView);
						break;
					case Keys.C:
						copyFileOut();
						break;
					case Keys.V:
						pasteFilesIn();
						break;
				}
			}
			else if (!e.Shift && !e.Alt && e.KeyCode == Keys.Delete)
				deleteSelectedFiles();
		}
		private void copyFileOut()
		{
			DataObject dObject = GetSelectedFiles(true, out FileEntry[] dump);
			if (dObject == null)
				return;
			System.Collections.Specialized.StringCollection strings = new();
			for (int i = 0; i < dump.Length; i++)
			{
				strings.Add($"{tempFolder}\\{dump[i].OriginalFileName}");
			}
			Clipboard.SetFileDropList(strings);
		}
		private void pasteFilesIn()
		{
			if (!Clipboard.ContainsFileDropList())
				return;
			System.Collections.Specialized.StringCollection strings = Clipboard.GetFileDropList();
			for (int i = 0; i < strings.Count; i++)
			{
				FileEntry entry = new(GetHash(MasterPassword));
				entry.FileSource = strings[i];
				entry.Import();
				entry.Save();
				AddFileEntry(entry);
			}
		}
		private void selectAllItems(ListView view)
		{
			for (int i = 0; i < view.Items.Count; i++)
			{
				view.Items[i].Selected = true;
			}
		}
		private void deleteSelectedFiles()
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
		private DataObject GetSelectedFiles(bool copy, out FileEntry[] selectedFiles)
		{
			selectedFiles = null;
			int selected = listViewFiles.SelectedItems.Count;
			if (selected == 0)
				return null;
			if (Directory.Exists(tempFolder))
				Directory.Delete(tempFolder, true);
			Directory.CreateDirectory(tempFolder);
			DirectoryInfo info = new DirectoryInfo(tempFolder);
			info.Attributes = FileAttributes.Hidden;
			selectedFiles = new FileEntry[selected];
			string[] files = new string[selected];
			for (int i = 0; i < selected; i++)
			{
				selectedFiles[i] = listViewFiles.SelectedItems[i].Tag as FileEntry;
				files[i] = $"{tempFolder}\\{selectedFiles[i].OriginalFileName}";
				selectedFiles[i].Export(files[i]);
			}
			return new(DataFormats.FileDrop, files);
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

