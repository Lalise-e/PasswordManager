namespace Manager
{
    partial class PasswordChanger
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
			labelOld = new System.Windows.Forms.Label();
			labelNew = new System.Windows.Forms.Label();
			labelConfirm = new System.Windows.Forms.Label();
			textBoxNew = new System.Windows.Forms.TextBox();
			textBoxOld = new System.Windows.Forms.TextBox();
			textBoxConfirm = new System.Windows.Forms.TextBox();
			checkBoxVisible = new System.Windows.Forms.CheckBox();
			buttonCancel = new System.Windows.Forms.Button();
			buttonOk = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// labelOld
			// 
			labelOld.AutoSize = true;
			labelOld.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			labelOld.Location = new System.Drawing.Point(14, 12);
			labelOld.Name = "labelOld";
			labelOld.Size = new System.Drawing.Size(130, 28);
			labelOld.TabIndex = 0;
			labelOld.Text = "Old Password";
			// 
			// labelNew
			// 
			labelNew.AutoSize = true;
			labelNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			labelNew.Location = new System.Drawing.Point(14, 87);
			labelNew.Name = "labelNew";
			labelNew.Size = new System.Drawing.Size(137, 28);
			labelNew.TabIndex = 1;
			labelNew.Text = "New Password";
			// 
			// labelConfirm
			// 
			labelConfirm.AutoSize = true;
			labelConfirm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			labelConfirm.Location = new System.Drawing.Point(14, 161);
			labelConfirm.Name = "labelConfirm";
			labelConfirm.Size = new System.Drawing.Size(168, 28);
			labelConfirm.TabIndex = 2;
			labelConfirm.Text = "Confirm Password";
			// 
			// textBoxNew
			// 
			textBoxNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			textBoxNew.Location = new System.Drawing.Point(14, 119);
			textBoxNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxNew.Name = "textBoxNew";
			textBoxNew.PasswordChar = '*';
			textBoxNew.Size = new System.Drawing.Size(194, 34);
			textBoxNew.TabIndex = 1;
			// 
			// textBoxOld
			// 
			textBoxOld.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			textBoxOld.Location = new System.Drawing.Point(14, 44);
			textBoxOld.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxOld.Name = "textBoxOld";
			textBoxOld.PasswordChar = '*';
			textBoxOld.Size = new System.Drawing.Size(194, 34);
			textBoxOld.TabIndex = 0;
			// 
			// textBoxConfirm
			// 
			textBoxConfirm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			textBoxConfirm.Location = new System.Drawing.Point(14, 193);
			textBoxConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textBoxConfirm.Name = "textBoxConfirm";
			textBoxConfirm.PasswordChar = '*';
			textBoxConfirm.Size = new System.Drawing.Size(194, 34);
			textBoxConfirm.TabIndex = 2;
			// 
			// checkBoxVisible
			// 
			checkBoxVisible.AutoSize = true;
			checkBoxVisible.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			checkBoxVisible.Location = new System.Drawing.Point(14, 240);
			checkBoxVisible.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			checkBoxVisible.Name = "checkBoxVisible";
			checkBoxVisible.Size = new System.Drawing.Size(187, 32);
			checkBoxVisible.TabIndex = 3;
			checkBoxVisible.Text = "Visible Characters";
			checkBoxVisible.UseVisualStyleBackColor = true;
			checkBoxVisible.CheckedChanged += checkBoxVisible_CheckedChanged;
			// 
			// buttonCancel
			// 
			buttonCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			buttonCancel.Location = new System.Drawing.Point(14, 336);
			buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(194, 47);
			buttonCancel.TabIndex = 5;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += buttonCancel_Click;
			// 
			// buttonOk
			// 
			buttonOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			buttonOk.Location = new System.Drawing.Point(14, 281);
			buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new System.Drawing.Size(194, 47);
			buttonOk.TabIndex = 4;
			buttonOk.Text = "Ok";
			buttonOk.UseVisualStyleBackColor = true;
			buttonOk.Click += buttonOk_Click;
			// 
			// PasswordChanger
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(219, 393);
			ControlBox = false;
			Controls.Add(buttonOk);
			Controls.Add(buttonCancel);
			Controls.Add(checkBoxVisible);
			Controls.Add(textBoxConfirm);
			Controls.Add(textBoxOld);
			Controls.Add(textBoxNew);
			Controls.Add(labelConfirm);
			Controls.Add(labelNew);
			Controls.Add(labelOld);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			HelpButton = true;
			Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			Name = "PasswordChanger";
			Text = "PasswordChanger";
			Load += PasswordChanger_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label labelOld;
        private System.Windows.Forms.Label labelNew;
        private System.Windows.Forms.Label labelConfirm;
        private System.Windows.Forms.TextBox textBoxNew;
        private System.Windows.Forms.TextBox textBoxOld;
        private System.Windows.Forms.TextBox textBoxConfirm;
        private System.Windows.Forms.CheckBox checkBoxVisible;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}