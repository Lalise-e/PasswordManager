
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
            this.infoPanel = new System.Windows.Forms.Panel();
            this.textBoxInfoPassword = new System.Windows.Forms.TextBox();
            this.infoPasswordLabel = new System.Windows.Forms.Label();
            this.infoDomainLabel = new System.Windows.Forms.Label();
            this.textBoxInfoDomain = new System.Windows.Forms.TextBox();
            this.textBoxInfoAccount = new System.Windows.Forms.TextBox();
            this.infoAccountLabel = new System.Windows.Forms.Label();
            this.infoServiceLabel = new System.Windows.Forms.Label();
            this.textBoxInfoService = new System.Windows.Forms.TextBox();
            this.listViewEntries = new System.Windows.Forms.ListView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.checkBoxVisible = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFont = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemIncognito = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonWebsite = new System.Windows.Forms.Button();
            this.infoPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoPanel.Controls.Add(this.textBoxInfoPassword);
            this.infoPanel.Controls.Add(this.infoPasswordLabel);
            this.infoPanel.Controls.Add(this.infoDomainLabel);
            this.infoPanel.Controls.Add(this.textBoxInfoDomain);
            this.infoPanel.Controls.Add(this.textBoxInfoAccount);
            this.infoPanel.Controls.Add(this.infoAccountLabel);
            this.infoPanel.Controls.Add(this.infoServiceLabel);
            this.infoPanel.Controls.Add(this.textBoxInfoService);
            this.infoPanel.Location = new System.Drawing.Point(191, 26);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(252, 173);
            this.infoPanel.TabIndex = 0;
            // 
            // textBoxInfoPassword
            // 
            this.textBoxInfoPassword.Location = new System.Drawing.Point(-1, 149);
            this.textBoxInfoPassword.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textBoxInfoPassword.Name = "textBoxInfoPassword";
            this.textBoxInfoPassword.PasswordChar = '*';
            this.textBoxInfoPassword.ReadOnly = true;
            this.textBoxInfoPassword.Size = new System.Drawing.Size(252, 23);
            this.textBoxInfoPassword.TabIndex = 7;
            // 
            // infoPasswordLabel
            // 
            this.infoPasswordLabel.AutoSize = true;
            this.infoPasswordLabel.Location = new System.Drawing.Point(-1, 131);
            this.infoPasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.infoPasswordLabel.Name = "infoPasswordLabel";
            this.infoPasswordLabel.Size = new System.Drawing.Size(57, 15);
            this.infoPasswordLabel.TabIndex = 6;
            this.infoPasswordLabel.Text = "Password";
            // 
            // infoDomainLabel
            // 
            this.infoDomainLabel.AutoSize = true;
            this.infoDomainLabel.Location = new System.Drawing.Point(-1, 88);
            this.infoDomainLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.infoDomainLabel.Name = "infoDomainLabel";
            this.infoDomainLabel.Size = new System.Drawing.Size(49, 15);
            this.infoDomainLabel.TabIndex = 5;
            this.infoDomainLabel.Text = "Domain";
            // 
            // textBoxInfoDomain
            // 
            this.textBoxInfoDomain.Location = new System.Drawing.Point(-1, 105);
            this.textBoxInfoDomain.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textBoxInfoDomain.Name = "textBoxInfoDomain";
            this.textBoxInfoDomain.ReadOnly = true;
            this.textBoxInfoDomain.Size = new System.Drawing.Size(252, 23);
            this.textBoxInfoDomain.TabIndex = 4;
            // 
            // textBoxInfoAccount
            // 
            this.textBoxInfoAccount.Location = new System.Drawing.Point(-1, 61);
            this.textBoxInfoAccount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textBoxInfoAccount.Name = "textBoxInfoAccount";
            this.textBoxInfoAccount.ReadOnly = true;
            this.textBoxInfoAccount.Size = new System.Drawing.Size(252, 23);
            this.textBoxInfoAccount.TabIndex = 3;
            // 
            // infoAccountLabel
            // 
            this.infoAccountLabel.AutoSize = true;
            this.infoAccountLabel.Location = new System.Drawing.Point(-1, 42);
            this.infoAccountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.infoAccountLabel.Name = "infoAccountLabel";
            this.infoAccountLabel.Size = new System.Drawing.Size(85, 15);
            this.infoAccountLabel.TabIndex = 2;
            this.infoAccountLabel.Text = "Account name";
            // 
            // infoServiceLabel
            // 
            this.infoServiceLabel.AutoSize = true;
            this.infoServiceLabel.Location = new System.Drawing.Point(-1, -1);
            this.infoServiceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.infoServiceLabel.Name = "infoServiceLabel";
            this.infoServiceLabel.Size = new System.Drawing.Size(44, 15);
            this.infoServiceLabel.TabIndex = 1;
            this.infoServiceLabel.Text = "Service";
            // 
            // textBoxInfoService
            // 
            this.textBoxInfoService.Location = new System.Drawing.Point(-1, 18);
            this.textBoxInfoService.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textBoxInfoService.Name = "textBoxInfoService";
            this.textBoxInfoService.ReadOnly = true;
            this.textBoxInfoService.Size = new System.Drawing.Size(252, 23);
            this.textBoxInfoService.TabIndex = 0;
            // 
            // listViewEntries
            // 
            this.listViewEntries.HideSelection = false;
            this.listViewEntries.Location = new System.Drawing.Point(12, 26);
            this.listViewEntries.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listViewEntries.MultiSelect = false;
            this.listViewEntries.Name = "listViewEntries";
            this.listViewEntries.ShowGroups = false;
            this.listViewEntries.Size = new System.Drawing.Size(173, 402);
            this.listViewEntries.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewEntries.TabIndex = 1;
            this.listViewEntries.UseCompatibleStateImageBehavior = false;
            this.listViewEntries.View = System.Windows.Forms.View.List;
            this.listViewEntries.ItemActivate += new System.EventHandler(this.ListViewEntries_ItemActivate);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(191, 406);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(252, 22);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(191, 285);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(252, 22);
            this.buttonEdit.TabIndex = 5;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(191, 311);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(252, 22);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(191, 230);
            this.buttonCopy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(252, 22);
            this.buttonCopy.TabIndex = 7;
            this.buttonCopy.Text = "Copy password to clipboard";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // checkBoxVisible
            // 
            this.checkBoxVisible.AutoSize = true;
            this.checkBoxVisible.Location = new System.Drawing.Point(191, 205);
            this.checkBoxVisible.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.checkBoxVisible.Name = "checkBoxVisible";
            this.checkBoxVisible.Size = new System.Drawing.Size(112, 19);
            this.checkBoxVisible.TabIndex = 8;
            this.checkBoxVisible.Text = "Password visible";
            this.checkBoxVisible.UseVisualStyleBackColor = true;
            this.checkBoxVisible.CheckedChanged += new System.EventHandler(this.checkBoxVisible_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(455, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuSettings
            // 
            this.toolStripMenuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFont,
            this.toolStripMenuItemBackground,
            this.toolStripMenuItemIncognito});
            this.toolStripMenuSettings.Name = "toolStripMenuSettings";
            this.toolStripMenuSettings.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuSettings.Text = "Settings";
            // 
            // toolStripMenuItemFont
            // 
            this.toolStripMenuItemFont.Name = "toolStripMenuItemFont";
            this.toolStripMenuItemFont.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemFont.Text = "Font";
            // 
            // toolStripMenuItemBackground
            // 
            this.toolStripMenuItemBackground.Name = "toolStripMenuItemBackground";
            this.toolStripMenuItemBackground.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemBackground.Text = "Background";
            // 
            // toolStripMenuItemIncognito
            // 
            this.toolStripMenuItemIncognito.CheckOnClick = true;
            this.toolStripMenuItemIncognito.Name = "toolStripMenuItemIncognito";
            this.toolStripMenuItemIncognito.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemIncognito.Text = "Start in incognito";
            // 
            // buttonWebsite
            // 
            this.buttonWebsite.Location = new System.Drawing.Point(191, 257);
            this.buttonWebsite.Name = "buttonWebsite";
            this.buttonWebsite.Size = new System.Drawing.Size(252, 23);
            this.buttonWebsite.TabIndex = 10;
            this.buttonWebsite.Text = "Open website";
            this.buttonWebsite.UseVisualStyleBackColor = true;
            this.buttonWebsite.Click += new System.EventHandler(this.buttonWebsite_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 440);
            this.Controls.Add(this.buttonWebsite);
            this.Controls.Add(this.checkBoxVisible);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listViewEntries);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Generic password manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

