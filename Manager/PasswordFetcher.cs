using System;
using System.Windows.Forms;

namespace Manager
{
    public partial class PasswordFetcher : Form
    {
        public string Password { get; set; }
        public PasswordFetcher()
        {
            InitializeComponent();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Password = textBoxPassword.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
