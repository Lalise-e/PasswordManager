
namespace Manager
{
    partial class PasswordEntryMaker
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
			buttonOk = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			textBoxPassword = new System.Windows.Forms.TextBox();
			textBoxService = new System.Windows.Forms.TextBox();
			textBoxAccount = new System.Windows.Forms.TextBox();
			textBoxDomain = new System.Windows.Forms.TextBox();
			labelService = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelPassword = new System.Windows.Forms.Label();
			buttonRandomPassword = new System.Windows.Forms.Button();
			labelEmail = new System.Windows.Forms.Label();
			textBoxEmail = new System.Windows.Forms.TextBox();
			SuspendLayout();
			// 
			// buttonOk
			// 
			buttonOk.Location = new System.Drawing.Point(12, 345);
			buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new System.Drawing.Size(86, 31);
			buttonOk.TabIndex = 5;
			buttonOk.Text = "Ok";
			buttonOk.UseVisualStyleBackColor = true;
			buttonOk.Click += buttonOk_Click;
			// 
			// buttonCancel
			// 
			buttonCancel.Location = new System.Drawing.Point(104, 345);
			buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(86, 31);
			buttonCancel.TabIndex = 6;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += buttonCancel_Click;
			// 
			// textBoxPassword
			// 
			textBoxPassword.Location = new System.Drawing.Point(14, 260);
			textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(178, 27);
			textBoxPassword.TabIndex = 3;
			// 
			// textBoxService
			// 
			textBoxService.Location = new System.Drawing.Point(14, 36);
			textBoxService.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxService.Name = "textBoxService";
			textBoxService.Size = new System.Drawing.Size(178, 27);
			textBoxService.TabIndex = 0;
			// 
			// textBoxAccount
			// 
			textBoxAccount.Location = new System.Drawing.Point(14, 95);
			textBoxAccount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxAccount.Name = "textBoxAccount";
			textBoxAccount.Size = new System.Drawing.Size(178, 27);
			textBoxAccount.TabIndex = 1;
			// 
			// textBoxDomain
			// 
			textBoxDomain.Location = new System.Drawing.Point(12, 205);
			textBoxDomain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxDomain.Name = "textBoxDomain";
			textBoxDomain.Size = new System.Drawing.Size(178, 27);
			textBoxDomain.TabIndex = 2;
			// 
			// labelService
			// 
			labelService.AutoSize = true;
			labelService.Location = new System.Drawing.Point(14, 12);
			labelService.Name = "labelService";
			labelService.Size = new System.Drawing.Size(56, 20);
			labelService.TabIndex = 10;
			labelService.Text = "Service";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 71);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(104, 20);
			label2.TabIndex = 7;
			label2.Text = "Account name";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(14, 181);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(62, 20);
			label3.TabIndex = 8;
			label3.Text = "Domain";
			// 
			// labelPassword
			// 
			labelPassword.AutoSize = true;
			labelPassword.Location = new System.Drawing.Point(14, 236);
			labelPassword.Name = "labelPassword";
			labelPassword.Size = new System.Drawing.Size(70, 20);
			labelPassword.TabIndex = 9;
			labelPassword.Text = "Password";
			// 
			// buttonRandomPassword
			// 
			buttonRandomPassword.Location = new System.Drawing.Point(12, 295);
			buttonRandomPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			buttonRandomPassword.Name = "buttonRandomPassword";
			buttonRandomPassword.Size = new System.Drawing.Size(178, 43);
			buttonRandomPassword.TabIndex = 4;
			buttonRandomPassword.Text = "Get a random password";
			buttonRandomPassword.UseVisualStyleBackColor = true;
			buttonRandomPassword.Click += buttonRandomPassword_Click;
			// 
			// labelEmail
			// 
			labelEmail.AutoSize = true;
			labelEmail.Location = new System.Drawing.Point(12, 126);
			labelEmail.Name = "labelEmail";
			labelEmail.Size = new System.Drawing.Size(46, 20);
			labelEmail.TabIndex = 12;
			labelEmail.Text = "Email";
			// 
			// textBoxEmail
			// 
			textBoxEmail.Location = new System.Drawing.Point(12, 150);
			textBoxEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(178, 27);
			textBoxEmail.TabIndex = 11;
			// 
			// PasswordEntryMaker
			// 
			AcceptButton = buttonOk;
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(206, 389);
			ControlBox = false;
			Controls.Add(labelEmail);
			Controls.Add(textBoxEmail);
			Controls.Add(buttonRandomPassword);
			Controls.Add(labelPassword);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(labelService);
			Controls.Add(textBoxDomain);
			Controls.Add(textBoxAccount);
			Controls.Add(textBoxService);
			Controls.Add(textBoxPassword);
			Controls.Add(buttonCancel);
			Controls.Add(buttonOk);
			Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			Name = "PasswordEntryMaker";
			Text = "EntryMaker";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxService;
        private System.Windows.Forms.TextBox textBoxAccount;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.Label labelService;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonRandomPassword;
		private System.Windows.Forms.Label labelEmail;
		private System.Windows.Forms.TextBox textBoxEmail;
	}
}