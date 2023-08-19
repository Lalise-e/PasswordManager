using System;
using System.Windows.Forms;
using Password;

namespace Manager
{
    public partial class PasswordGenerator : Form
    {
        public string Password { get; set; }
        public PasswordGenerator()
        {
            InitializeComponent();
            this.ApplySettings();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 10)
                return;
            Password = textBox1.Text;
            DialogResult = DialogResult.OK;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = PasswordEntry.GetRandomPassword((byte)numericUpDown1.Value,
                    checkBoxCapital.Checked,
                    checkBoxNumeral.Checked,
                    checkBoxSpecial.Checked,
                    textBoxBanned.Text.ToCharArray());
            }
            catch (ArgumentException error)
            {
                Form1.DisplayError(error);
            }
        }
    }
}
