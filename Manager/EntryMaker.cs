using System;
using System.Drawing;
using System.Windows.Forms;
using Password;

namespace Manager
{
    public partial class EntryMaker : Form
    {
        public PasswordEntry Entry { get; set; }
        private byte[] Key { get; set; }
        public EntryMaker(byte[] key)
        {
            InitializeComponent();
            Key = key;
            this.ApplySettings();
        }
        public EntryMaker(byte[] key, PasswordEntry entry) : this(key)
        {
            textBoxAccount.Text = entry.AccountName;
            if (entry.Domain != null)
                textBoxDomain.Text = entry.Domain.AbsoluteUri;
            textBoxPassword.Text = entry.Password;
            textBoxService.Text = entry.Service;
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxAccount.Text.Length == 0)
            {
                DisplayMessage("Account name can't be left empty.");
                return;
            }
            if (!Uri.TryCreate(textBoxDomain.Text, UriKind.Absolute, out Uri url))
            {
                if (textBoxDomain.Text.Length == 0)
                    url = null;
                else
                {
                    DisplayMessage($"{textBoxDomain.Text} is not a valid url.");
                    return;
                }
            }
            if (textBoxPassword.Text.Length == 0)
            {
                DisplayMessage("Password can't be left empty.");
                return;
            }
            if (textBoxService.Text.Length == 0)
            {
                DisplayMessage("Service can't be left empty.");
                return;
            }
            Entry = new(Key)
            {
                AccountName = textBoxAccount.Text,
                Domain = url,
                Password = textBoxPassword.Text,
                Service = textBoxService.Text
            };
            DialogResult = DialogResult.OK;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void DisplayMessage(string message) => MessageBox.Show(message, "Uh oh.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        private void buttonRandomPassword_Click(object sender, EventArgs e)
        {
            using(PasswordGenerator generator = new())
            {
                if (generator.ShowDialog() == DialogResult.OK)
                    textBoxPassword.Text = generator.Password;
            }
        }
    }
}
