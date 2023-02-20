using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager
{
    public partial class PasswordChanger : Form
    {
        public PasswordChanger()
        {
            InitializeComponent();
        }

        private void PasswordChanger_Load(object sender, EventArgs e)
        {
            this.ApplySettings();
        }
        internal static byte[] GetHash(string text) => SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(text));

        private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if (c.GetType() != typeof(TextBox))
                    continue;
                TextBox t = c as TextBox;
                if (checkBoxVisible.Checked)
                    t.PasswordChar = (char)0;
                else
                    t.PasswordChar = '*';
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (Tag.ToString() != textBoxOld.Text)
            {
                Form1.DisplayError("Old password is incorrect.");
                return;
            }
            if(textBoxNew.Text != textBoxConfirm.Text)
            {
                Form1.DisplayError("The passwords to not match");
                return;
            }
            Tag = textBoxNew.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
