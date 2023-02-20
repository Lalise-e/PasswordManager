using System;
using System.Text;
using System.Windows.Forms;
using Password;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Diagnostics;

namespace Manager
{
    public partial class Form1 : Form
    {
        internal static string BackgroundDirectory { get { return $"{Directory.GetCurrentDirectory()}\\Images"; } }
        internal static Settings settings { get; set; }
        //internal static string BackgroundLocation { get { return $"{Directory.GetCurrentDirectory()}\\bg{settings.FileExtension}"; } }
        private string SettingsFile { get { return $"{Directory.GetCurrentDirectory()}\\settings.json"; } }
        private string EntryDirectory { get; set; } = $"{Directory.GetCurrentDirectory()}\\Entries";
        private string MasterPassword { get; set; }
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(EntryDirectory);
            Directory.CreateDirectory(BackgroundDirectory);
            PasswordEntry.EntryDirectory = EntryDirectory;
            if (File.Exists(SettingsFile))
            {
                settings = Settings.GetSettings(SettingsFile);
                this.ApplySettings();
                toolStripMenuItemIncognito.Checked = settings.Incognito;
            }
            else
                settings = new();
            toolStripMenuItemFont.Click += ToolStripMenuItemFont_Click;
            toolStripMenuItemBackground.Click += ToolStripMenuItemBackground_Click;
            toolStripMenuItemIncognito.Click += ToolStripMenuItemIncognito_Click;
            toolStripMenuItemPassword.Click += ToolStripMenuItemPassword_Click;
            using(PasswordFetcher fetcher = new())
            {
                DialogResult result = fetcher.ShowDialog();
                if (result == DialogResult.OK)
                    MasterPassword = fetcher.Password;
            }
            try { PasswordPatcher.OneToTwoPatcher(EntryDirectory, GetHash(MasterPassword)); }
            catch (CryptographicException) { DisplayError("Wrong master password."); Environment.Exit(0);}
            LoadListItems();
        }
        private void ToolStripMenuItemIncognito_Click(object sender, EventArgs e)
        {
            settings.Incognito = toolStripMenuItemIncognito.Checked;
            Settings.SaveSettings(SettingsFile, settings);
        }
        private void ToolStripMenuItemBackground_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new())
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
                    settings.BackgroundLocation = file;
                    Settings.SaveSettings(SettingsFile, settings);
                    this.ApplySettings();
                }
            }
        }
        private void ToolStripMenuItemFont_Click(object sender, EventArgs e)
        {
            using(FontDialog dialog = new())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    settings.FontName = dialog.Font.FontFamily.Name;
                    Settings.SaveSettings(SettingsFile, settings);
                    this.ApplySettings();
                }
            }
        }
        private void ToolStripMenuItemPassword_Click(object sender, EventArgs e)
        {
            string newPassword = null;
            using(PasswordChanger changer = new())
            {
                changer.Tag = MasterPassword;
                DialogResult dialogResult = changer.ShowDialog();
                if (dialogResult != DialogResult.OK)
                    return;
                newPassword = changer.Tag.ToString();
            }
            string[] files = Directory.GetFiles(EntryDirectory);
            foreach(string file in files)
            {
                if (!File.Exists(file))
                    continue;
                PasswordEntry oldEntry = null;
                try { oldEntry = new(GetHash(MasterPassword), file); }
                catch
                {
                    DisplayError($"File: {file} is corrupt.{Environment.NewLine} Either that or I suck at coding, no idea which it is.");
                    File.Delete(file);
                    continue;
                }
                PasswordEntry newEntry = new(GetHash(newPassword))
                {
                    Password = oldEntry.Password,
                    Domain = oldEntry.Domain,
                    Service = oldEntry.Service,
                    AccountName = oldEntry.AccountName
                };
                oldEntry.Delete();
                newEntry.Save();
            }
            MasterPassword = newPassword;
            LoadListItems();
        }
        private void ListViewEntries_ItemActivate(object sender, EventArgs e)
        {
            PasswordEntry entry;
            try { entry = GetActiveEntry(); }
            catch { return; }
            LoadEntry(entry);
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (EntryMaker maker = new(GetHash(MasterPassword)))
            {
                handleMaker(maker);
            }
        }
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            PasswordEntry entry;
            try { entry = GetActiveEntry(); }
            catch { return; }
            using(EntryMaker maker = new(GetHash(MasterPassword), entry))
            {
                DialogResult result = handleMaker(maker);
                if (result == DialogResult.OK)
                {
                    if (maker.Entry.Filename != entry.Filename)
                        entry.Delete();
                    LoadListItems();
                }
            }
        }
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            PasswordEntry entry;
            try { entry = GetActiveEntry(); }
            catch { return; }
            entry.Delete();
            ClearInfo();
            LoadListItems();
        }
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (textBoxInfoPassword.Text == null)
                return;
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
            if (settings.Incognito)
            {
                info.Arguments += $"-private-window {textBoxInfoDomain.Text}";
            }
            else
            {
                info.Arguments += $"{textBoxInfoDomain.Text}";
            }
            try { Process.Start(info); }
            catch(Exception ex) { DisplayError(ex); }
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
        private void LoadEntry(PasswordEntry entry)
        {
            textBoxInfoAccount.Text = entry.AccountName;
            if (entry.Domain != null)
                textBoxInfoDomain.Text = entry.Domain.OriginalString;
            else
                textBoxInfoDomain.Text = null;
            textBoxInfoPassword.Text = entry.Password;
            textBoxInfoService.Text = entry.Service;
        }
        private void LoadListItems()
        {
            listViewEntries.Items.Clear();
            foreach(string path in Directory.GetFiles(EntryDirectory))
            {
                PasswordEntry entry;
                ListViewItem item;
                try
                {
                    entry = new(GetHash(MasterPassword), path);
                    item = new()
                    {
                        Text = $"{entry.Service} - {entry.AccountName}",
                        Tag = entry,
                    };
                }
                catch(CryptographicException) { DisplayError("Wrong master password."); Environment.Exit(0); return; }
                catch(FileNotFoundException e) { DisplayError($"File {e.FileName} not found."); continue; }
                catch(FileLoadException e) { DisplayError($"File {e.FileName} was corrupt"); continue; }
                catch(Exception e) { DisplayError(e); continue; }
                listViewEntries.Items.Add(item);
            }
        }
        private void ClearInfo()
        {
            foreach (object e in infoPanel.Controls)
                if (e.GetType() == typeof(TextBox))
                    (e as TextBox).Text = "";
        }
        private PasswordEntry GetActiveEntry()
        {
            PasswordEntry entry;
            try { entry = listViewEntries.SelectedItems[0].Tag as PasswordEntry; }
            catch (ArgumentOutOfRangeException) { DisplayError("Nothing is selected."); throw new Exception(); }
            catch (Exception) { DisplayError("Something went wrong."); throw; }
            return entry;
        }
        private DialogResult handleMaker(EntryMaker maker)
        {
            DialogResult result = maker.ShowDialog();
            if (result == DialogResult.OK)
            {
                maker.Entry.Delete();
                maker.Entry.Save();
                LoadEntry(maker.Entry);
                ListViewItem item = new()
                {
                    Text = $"{maker.Entry.Service} - {maker.Entry.AccountName}",
                    Tag = maker.Entry
                };
                listViewEntries.Items.Add(item);
            }
            return result;
        }
        internal static void DisplayError(Exception e) => DisplayError(e.Message);
        internal static void DisplayError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        internal static byte[] GetHash(string text)=> SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(text));
        internal static string GetHexString(byte[] bytes)
        {
            StringBuilder builder = new(bytes.Length * 2);
            foreach (byte b in bytes)
                builder.AppendFormat("{0:x2}", b);
            return builder.ToString();
        }
    }
}
