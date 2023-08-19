
namespace Manager
{
    partial class PasswordFetcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            textBoxPassword = new System.Windows.Forms.TextBox();
            buttonOk = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(187, 20);
            label1.TabIndex = 3;
            label1.Text = "Enter the master password.";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new System.Drawing.Point(14, 36);
            textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new System.Drawing.Size(263, 27);
            textBoxPassword.TabIndex = 0;
            // 
            // buttonOk
            // 
            buttonOk.Location = new System.Drawing.Point(14, 75);
            buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(264, 32);
            buttonOk.TabIndex = 1;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // PasswordFetcher
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(291, 116);
            ControlBox = false;
            Controls.Add(buttonOk);
            Controls.Add(textBoxPassword);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "PasswordFetcher";
            Text = "PasswordFetcher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonOk;
    }
}