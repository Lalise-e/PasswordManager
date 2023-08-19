
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
            listViewEntries = new System.Windows.Forms.ListView();
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
            buttonWebsite = new System.Windows.Forms.Button();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            infoPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            infoPanel.Location = new System.Drawing.Point(191, 26);
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
            // listViewEntries
            // 
            listViewEntries.Location = new System.Drawing.Point(12, 26);
            listViewEntries.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            listViewEntries.MultiSelect = false;
            listViewEntries.Name = "listViewEntries";
            listViewEntries.ShowGroups = false;
            listViewEntries.Size = new System.Drawing.Size(173, 402);
            listViewEntries.Sorting = System.Windows.Forms.SortOrder.Ascending;
            listViewEntries.TabIndex = 1;
            listViewEntries.UseCompatibleStateImageBehavior = false;
            listViewEntries.View = System.Windows.Forms.View.List;
            listViewEntries.ItemActivate += ListViewEntries_ItemActivate;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new System.Drawing.Point(190, 398);
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
            buttonEdit.Location = new System.Drawing.Point(191, 303);
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
            buttonDelete.Location = new System.Drawing.Point(190, 337);
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
            buttonCopy.Location = new System.Drawing.Point(191, 233);
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
            checkBoxVisible.AutoSize = true;
            checkBoxVisible.Location = new System.Drawing.Point(191, 205);
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
            menuStrip1.Size = new System.Drawing.Size(455, 28);
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
            toolStripMenuItemFont.Size = new System.Drawing.Size(224, 26);
            toolStripMenuItemFont.Text = "Font";
            // 
            // toolStripMenuItemBackground
            // 
            toolStripMenuItemBackground.Name = "toolStripMenuItemBackground";
            toolStripMenuItemBackground.Size = new System.Drawing.Size(224, 26);
            toolStripMenuItemBackground.Text = "Background";
            // 
            // toolStripMenuItemIncognito
            // 
            toolStripMenuItemIncognito.CheckOnClick = true;
            toolStripMenuItemIncognito.Name = "toolStripMenuItemIncognito";
            toolStripMenuItemIncognito.Size = new System.Drawing.Size(224, 26);
            toolStripMenuItemIncognito.Text = "Start in incognito";
            // 
            // toolStripMenuItemPassword
            // 
            toolStripMenuItemPassword.Name = "toolStripMenuItemPassword";
            toolStripMenuItemPassword.Size = new System.Drawing.Size(224, 26);
            toolStripMenuItemPassword.Text = "Change Password";
            // 
            // buttonWebsite
            // 
            buttonWebsite.Location = new System.Drawing.Point(191, 268);
            buttonWebsite.Name = "buttonWebsite";
            buttonWebsite.Size = new System.Drawing.Size(252, 30);
            buttonWebsite.TabIndex = 10;
            buttonWebsite.Text = "Open website";
            buttonWebsite.UseVisualStyleBackColor = true;
            buttonWebsite.Click += buttonWebsite_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(455, 440);
            Controls.Add(buttonWebsite);
            Controls.Add(checkBoxVisible);
            Controls.Add(buttonCopy);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(listViewEntries);
            Controls.Add(infoPanel);
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
        private System.Windows.Forms.ListView listViewEntries;
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
    }
}

