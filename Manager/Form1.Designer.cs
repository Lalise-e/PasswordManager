﻿
namespace Manager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			infoPanel = new System.Windows.Forms.Panel();
			textBoxInfoPassword = new System.Windows.Forms.TextBox();
			infoPasswordLabel = new System.Windows.Forms.Label();
			infoDomainLabel = new System.Windows.Forms.Label();
			textBoxInfoDomain = new System.Windows.Forms.TextBox();
			textBoxInfoAccount = new System.Windows.Forms.TextBox();
			infoAccountLabel = new System.Windows.Forms.Label();
			infoServiceLabel = new System.Windows.Forms.Label();
			textBoxInfoService = new System.Windows.Forms.TextBox();
			listViewPasswords = new System.Windows.Forms.ListView();
			columnHeaderService = new System.Windows.Forms.ColumnHeader();
			columnHeaderName = new System.Windows.Forms.ColumnHeader();
			buttonAdd = new System.Windows.Forms.Button();
			buttonEdit = new System.Windows.Forms.Button();
			buttonDelete = new System.Windows.Forms.Button();
			buttonCopy = new System.Windows.Forms.Button();
			checkBoxVisible = new System.Windows.Forms.CheckBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			toolStripMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemFont = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemBackground = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemIncognito = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemPassword = new System.Windows.Forms.ToolStripMenuItem();
			settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			buttonWebsite = new System.Windows.Forms.Button();
			tabControl = new System.Windows.Forms.TabControl();
			tabPagePasswords = new System.Windows.Forms.TabPage();
			tabPageTextEntries = new System.Windows.Forms.TabPage();
			tabPageFiles = new System.Windows.Forms.TabPage();
			listViewFiles = new System.Windows.Forms.ListView();
			infoPanel.SuspendLayout();
			menuStrip1.SuspendLayout();
			tabControl.SuspendLayout();
			tabPagePasswords.SuspendLayout();
			tabPageFiles.SuspendLayout();
			SuspendLayout();
			// 
			// infoPanel
			// 
			infoPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			infoPanel.Controls.Add(textBoxInfoPassword);
			infoPanel.Controls.Add(infoPasswordLabel);
			infoPanel.Controls.Add(infoDomainLabel);
			infoPanel.Controls.Add(textBoxInfoDomain);
			infoPanel.Controls.Add(textBoxInfoAccount);
			infoPanel.Controls.Add(infoAccountLabel);
			infoPanel.Controls.Add(infoServiceLabel);
			infoPanel.Controls.Add(textBoxInfoService);
			infoPanel.Location = new System.Drawing.Point(428, 5);
			infoPanel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			infoPanel.Name = "infoPanel";
			infoPanel.Size = new System.Drawing.Size(252, 173);
			infoPanel.TabIndex = 0;
			// 
			// textBoxInfoPassword
			// 
			textBoxInfoPassword.Location = new System.Drawing.Point(-1, 149);
			textBoxInfoPassword.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			textBoxInfoPassword.Name = "textBoxInfoPassword";
			textBoxInfoPassword.PasswordChar = '*';
			textBoxInfoPassword.ReadOnly = true;
			textBoxInfoPassword.Size = new System.Drawing.Size(252, 27);
			textBoxInfoPassword.TabIndex = 7;
			// 
			// infoPasswordLabel
			// 
			infoPasswordLabel.AutoSize = true;
			infoPasswordLabel.Location = new System.Drawing.Point(-1, 131);
			infoPasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			infoPasswordLabel.Name = "infoPasswordLabel";
			infoPasswordLabel.Size = new System.Drawing.Size(70, 20);
			infoPasswordLabel.TabIndex = 6;
			infoPasswordLabel.Text = "Password";
			// 
			// infoDomainLabel
			// 
			infoDomainLabel.AutoSize = true;
			infoDomainLabel.Location = new System.Drawing.Point(-1, 88);
			infoDomainLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			infoDomainLabel.Name = "infoDomainLabel";
			infoDomainLabel.Size = new System.Drawing.Size(62, 20);
			infoDomainLabel.TabIndex = 5;
			infoDomainLabel.Text = "Domain";
			// 
			// textBoxInfoDomain
			// 
			textBoxInfoDomain.Location = new System.Drawing.Point(-1, 105);
			textBoxInfoDomain.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			textBoxInfoDomain.Name = "textBoxInfoDomain";
			textBoxInfoDomain.ReadOnly = true;
			textBoxInfoDomain.Size = new System.Drawing.Size(252, 27);
			textBoxInfoDomain.TabIndex = 4;
			// 
			// textBoxInfoAccount
			// 
			textBoxInfoAccount.Location = new System.Drawing.Point(-1, 61);
			textBoxInfoAccount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			textBoxInfoAccount.Name = "textBoxInfoAccount";
			textBoxInfoAccount.ReadOnly = true;
			textBoxInfoAccount.Size = new System.Drawing.Size(252, 27);
			textBoxInfoAccount.TabIndex = 3;
			// 
			// infoAccountLabel
			// 
			infoAccountLabel.AutoSize = true;
			infoAccountLabel.Location = new System.Drawing.Point(-1, 42);
			infoAccountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			infoAccountLabel.Name = "infoAccountLabel";
			infoAccountLabel.Size = new System.Drawing.Size(104, 20);
			infoAccountLabel.TabIndex = 2;
			infoAccountLabel.Text = "Account name";
			// 
			// infoServiceLabel
			// 
			infoServiceLabel.AutoSize = true;
			infoServiceLabel.Location = new System.Drawing.Point(-1, -1);
			infoServiceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			infoServiceLabel.Name = "infoServiceLabel";
			infoServiceLabel.Size = new System.Drawing.Size(56, 20);
			infoServiceLabel.TabIndex = 1;
			infoServiceLabel.Text = "Service";
			// 
			// textBoxInfoService
			// 
			textBoxInfoService.Location = new System.Drawing.Point(-1, 18);
			textBoxInfoService.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			textBoxInfoService.Name = "textBoxInfoService";
			textBoxInfoService.ReadOnly = true;
			textBoxInfoService.Size = new System.Drawing.Size(252, 27);
			textBoxInfoService.TabIndex = 0;
			// 
			// listViewPasswords
			// 
			listViewPasswords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeaderService, columnHeaderName });
			listViewPasswords.ForeColor = System.Drawing.SystemColors.MenuText;
			listViewPasswords.FullRowSelect = true;
			listViewPasswords.LabelEdit = true;
			listViewPasswords.Location = new System.Drawing.Point(4, 5);
			listViewPasswords.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			listViewPasswords.MultiSelect = false;
			listViewPasswords.Name = "listViewPasswords";
			listViewPasswords.ShowGroups = false;
			listViewPasswords.Size = new System.Drawing.Size(416, 427);
			listViewPasswords.Sorting = System.Windows.Forms.SortOrder.Ascending;
			listViewPasswords.TabIndex = 1;
			listViewPasswords.UseCompatibleStateImageBehavior = false;
			listViewPasswords.View = System.Windows.Forms.View.Details;
			listViewPasswords.ColumnWidthChanging += listViewPasswords_ColumnWidthChanging;
			listViewPasswords.ItemActivate += ListViewEntries_ItemActivate;
			// 
			// columnHeaderService
			// 
			columnHeaderService.Text = "Service";
			columnHeaderService.Width = 120;
			// 
			// columnHeaderName
			// 
			columnHeaderName.Text = "Account Name";
			columnHeaderName.Width = 292;
			// 
			// buttonAdd
			// 
			buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonAdd.Location = new System.Drawing.Point(428, 402);
			buttonAdd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new System.Drawing.Size(252, 30);
			buttonAdd.TabIndex = 4;
			buttonAdd.Text = "Add";
			buttonAdd.UseVisualStyleBackColor = true;
			buttonAdd.Click += ButtonAdd_Click;
			// 
			// buttonEdit
			// 
			buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonEdit.Location = new System.Drawing.Point(428, 280);
			buttonEdit.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			buttonEdit.Name = "buttonEdit";
			buttonEdit.Size = new System.Drawing.Size(252, 30);
			buttonEdit.TabIndex = 5;
			buttonEdit.Text = "Edit";
			buttonEdit.UseVisualStyleBackColor = true;
			buttonEdit.Click += ButtonEdit_Click;
			// 
			// buttonDelete
			// 
			buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonDelete.Location = new System.Drawing.Point(428, 314);
			buttonDelete.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(252, 30);
			buttonDelete.TabIndex = 6;
			buttonDelete.Text = "Delete";
			buttonDelete.UseVisualStyleBackColor = true;
			buttonDelete.Click += ButtonDelete_Click;
			// 
			// buttonCopy
			// 
			buttonCopy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonCopy.Location = new System.Drawing.Point(428, 210);
			buttonCopy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			buttonCopy.Name = "buttonCopy";
			buttonCopy.Size = new System.Drawing.Size(252, 30);
			buttonCopy.TabIndex = 7;
			buttonCopy.Text = "Copy password to clipboard";
			buttonCopy.UseVisualStyleBackColor = true;
			buttonCopy.Click += buttonCopy_Click;
			// 
			// checkBoxVisible
			// 
			checkBoxVisible.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			checkBoxVisible.AutoSize = true;
			checkBoxVisible.Location = new System.Drawing.Point(428, 182);
			checkBoxVisible.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			checkBoxVisible.Name = "checkBoxVisible";
			checkBoxVisible.Size = new System.Drawing.Size(138, 24);
			checkBoxVisible.TabIndex = 8;
			checkBoxVisible.Text = "Password visible";
			checkBoxVisible.UseVisualStyleBackColor = true;
			checkBoxVisible.CheckedChanged += checkBoxVisible_CheckedChanged;
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuSettings });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(719, 28);
			menuStrip1.TabIndex = 9;
			menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuSettings
			// 
			toolStripMenuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemFont, toolStripMenuItemBackground, toolStripMenuItemIncognito, toolStripMenuItemPassword, settingsToolStripMenuItem });
			toolStripMenuSettings.Name = "toolStripMenuSettings";
			toolStripMenuSettings.Size = new System.Drawing.Size(76, 24);
			toolStripMenuSettings.Text = "Settings";
			// 
			// toolStripMenuItemFont
			// 
			toolStripMenuItemFont.Name = "toolStripMenuItemFont";
			toolStripMenuItemFont.Size = new System.Drawing.Size(207, 26);
			toolStripMenuItemFont.Text = "Font";
			// 
			// toolStripMenuItemBackground
			// 
			toolStripMenuItemBackground.Name = "toolStripMenuItemBackground";
			toolStripMenuItemBackground.Size = new System.Drawing.Size(207, 26);
			toolStripMenuItemBackground.Text = "Background";
			// 
			// toolStripMenuItemIncognito
			// 
			toolStripMenuItemIncognito.CheckOnClick = true;
			toolStripMenuItemIncognito.Name = "toolStripMenuItemIncognito";
			toolStripMenuItemIncognito.Size = new System.Drawing.Size(207, 26);
			toolStripMenuItemIncognito.Text = "Start in incognito";
			// 
			// toolStripMenuItemPassword
			// 
			toolStripMenuItemPassword.Name = "toolStripMenuItemPassword";
			toolStripMenuItemPassword.Size = new System.Drawing.Size(207, 26);
			toolStripMenuItemPassword.Text = "Change Password";
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
			settingsToolStripMenuItem.Text = "Settings";
			// 
			// buttonWebsite
			// 
			buttonWebsite.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonWebsite.Location = new System.Drawing.Point(428, 245);
			buttonWebsite.Name = "buttonWebsite";
			buttonWebsite.Size = new System.Drawing.Size(252, 30);
			buttonWebsite.TabIndex = 10;
			buttonWebsite.Text = "Open website";
			buttonWebsite.UseVisualStyleBackColor = true;
			buttonWebsite.Click += buttonWebsite_Click;
			// 
			// tabControl
			// 
			tabControl.Controls.Add(tabPagePasswords);
			tabControl.Controls.Add(tabPageTextEntries);
			tabControl.Controls.Add(tabPageFiles);
			tabControl.Location = new System.Drawing.Point(12, 31);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(695, 470);
			tabControl.TabIndex = 11;
			// 
			// tabPagePasswords
			// 
			tabPagePasswords.BackColor = System.Drawing.SystemColors.Control;
			tabPagePasswords.Controls.Add(listViewPasswords);
			tabPagePasswords.Controls.Add(infoPanel);
			tabPagePasswords.Controls.Add(buttonWebsite);
			tabPagePasswords.Controls.Add(checkBoxVisible);
			tabPagePasswords.Controls.Add(buttonAdd);
			tabPagePasswords.Controls.Add(buttonCopy);
			tabPagePasswords.Controls.Add(buttonEdit);
			tabPagePasswords.Controls.Add(buttonDelete);
			tabPagePasswords.Location = new System.Drawing.Point(4, 29);
			tabPagePasswords.Name = "tabPagePasswords";
			tabPagePasswords.Padding = new System.Windows.Forms.Padding(3);
			tabPagePasswords.Size = new System.Drawing.Size(687, 437);
			tabPagePasswords.TabIndex = 0;
			tabPagePasswords.Text = "Login Details";
			// 
			// tabPageTextEntries
			// 
			tabPageTextEntries.BackColor = System.Drawing.SystemColors.Control;
			tabPageTextEntries.Location = new System.Drawing.Point(4, 29);
			tabPageTextEntries.Name = "tabPageTextEntries";
			tabPageTextEntries.Padding = new System.Windows.Forms.Padding(3);
			tabPageTextEntries.Size = new System.Drawing.Size(687, 437);
			tabPageTextEntries.TabIndex = 1;
			tabPageTextEntries.Text = "Text Entries";
			// 
			// tabPageFiles
			// 
			tabPageFiles.Controls.Add(listViewFiles);
			tabPageFiles.Location = new System.Drawing.Point(4, 29);
			tabPageFiles.Name = "tabPageFiles";
			tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
			tabPageFiles.Size = new System.Drawing.Size(687, 437);
			tabPageFiles.TabIndex = 2;
			tabPageFiles.Text = "Files";
			tabPageFiles.UseVisualStyleBackColor = true;
			// 
			// listViewFiles
			// 
			listViewFiles.Location = new System.Drawing.Point(6, 6);
			listViewFiles.Name = "listViewFiles";
			listViewFiles.Size = new System.Drawing.Size(675, 359);
			listViewFiles.TabIndex = 0;
			listViewFiles.UseCompatibleStateImageBehavior = false;
			listViewFiles.View = System.Windows.Forms.View.SmallIcon;
			// 
			// Form1
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(719, 513);
			Controls.Add(tabControl);
			Controls.Add(menuStrip1);
			Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			MaximizeBox = false;
			Name = "Form1";
			Text = "Generic password manager";
			Load += Form1_Load;
			infoPanel.ResumeLayout(false);
			infoPanel.PerformLayout();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			tabControl.ResumeLayout(false);
			tabPagePasswords.ResumeLayout(false);
			tabPagePasswords.PerformLayout();
			tabPageFiles.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TextBox textBoxInfoPassword;
        private System.Windows.Forms.Label infoPasswordLabel;
        private System.Windows.Forms.Label infoDomainLabel;
        private System.Windows.Forms.TextBox textBoxInfoDomain;
        private System.Windows.Forms.TextBox textBoxInfoAccount;
        private System.Windows.Forms.Label infoAccountLabel;
        private System.Windows.Forms.Label infoServiceLabel;
        private System.Windows.Forms.TextBox textBoxInfoService;
        private System.Windows.Forms.ListView listViewPasswords;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.CheckBox checkBoxVisible;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFont;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBackground;
        private System.Windows.Forms.Button buttonWebsite;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIncognito;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPassword;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPagePasswords;
		private System.Windows.Forms.TabPage tabPageTextEntries;
		private System.Windows.Forms.TabPage tabPageFiles;
		private System.Windows.Forms.ColumnHeader columnHeaderService;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ListView listViewFiles;
	}
}

