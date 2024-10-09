using System;
using System.Drawing;
using System.Windows.Forms;
using Password;

namespace Manager
{
   public partial class PasswordEntryMaker : Form
   {
      public PasswordEntry Entry { get; set; }
      private byte[] Key { get; set; }
      public PasswordEntryMaker(byte[] key)
      {
         InitializeComponent();
         Key = key;
         this.ApplySettings();
      }
      public PasswordEntryMaker(byte[] key, PasswordEntry entry) : this(key)
      {
         textBoxAccount.Text = entry.AccountName;
         if (entry.Domain != null)
            textBoxDomain.Text = entry.Domain.AbsoluteUri;
         textBoxPassword.Text = entry.Password;
         textBoxService.Text = entry.Service;
         textBoxEmail.Text = entry.Email;
         Entry = entry;
      }
      private void buttonOk_Click(object sender, EventArgs e)
      {
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
         if (Entry == null)
            Entry = new(Key);
         Entry.AccountName = textBoxAccount.Text;
         Entry.Password = textBoxPassword.Text;
         Entry.Service = textBoxService.Text;
         Entry.Domain = url;
         Entry.Email = textBoxEmail.Text;
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
         using (PasswordGenerator generator = new())
         {
            if (generator.ShowDialog() == DialogResult.OK)
               textBoxPassword.Text = generator.Password;
         }
      }
   }
}
