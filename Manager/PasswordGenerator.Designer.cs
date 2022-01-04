
namespace Manager
{
    partial class PasswordGenerator
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxCapital = new System.Windows.Forms.CheckBox();
            this.checkBoxNumeral = new System.Windows.Forms.CheckBox();
            this.checkBoxSpecial = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.labelBanned = new System.Windows.Forms.Label();
            this.textBoxBanned = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(166, 87);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(122, 23);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(148, 200);
            this.textBox1.TabIndex = 1;
            // 
            // checkBoxCapital
            // 
            this.checkBoxCapital.AutoSize = true;
            this.checkBoxCapital.Location = new System.Drawing.Point(166, 12);
            this.checkBoxCapital.Name = "checkBoxCapital";
            this.checkBoxCapital.Size = new System.Drawing.Size(138, 19);
            this.checkBoxCapital.TabIndex = 2;
            this.checkBoxCapital.Text = "Include capital letters";
            this.checkBoxCapital.UseVisualStyleBackColor = true;
            // 
            // checkBoxNumeral
            // 
            this.checkBoxNumeral.AutoSize = true;
            this.checkBoxNumeral.Location = new System.Drawing.Point(166, 37);
            this.checkBoxNumeral.Name = "checkBoxNumeral";
            this.checkBoxNumeral.Size = new System.Drawing.Size(115, 19);
            this.checkBoxNumeral.TabIndex = 3;
            this.checkBoxNumeral.Text = "Include numbers";
            this.checkBoxNumeral.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpecial
            // 
            this.checkBoxSpecial.AutoSize = true;
            this.checkBoxSpecial.Location = new System.Drawing.Point(166, 62);
            this.checkBoxSpecial.Name = "checkBoxSpecial";
            this.checkBoxSpecial.Size = new System.Drawing.Size(104, 19);
            this.checkBoxSpecial.TabIndex = 4;
            this.checkBoxSpecial.Text = "Include special";
            this.checkBoxSpecial.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(166, 189);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(122, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(166, 160);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(122, 23);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Make password";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // labelBanned
            // 
            this.labelBanned.AutoSize = true;
            this.labelBanned.Location = new System.Drawing.Point(166, 113);
            this.labelBanned.Name = "labelBanned";
            this.labelBanned.Size = new System.Drawing.Size(104, 15);
            this.labelBanned.TabIndex = 7;
            this.labelBanned.Text = "Banned characters";
            // 
            // textBoxBanned
            // 
            this.textBoxBanned.Location = new System.Drawing.Point(166, 131);
            this.textBoxBanned.Name = "textBoxBanned";
            this.textBoxBanned.Size = new System.Drawing.Size(122, 23);
            this.textBoxBanned.TabIndex = 8;
            // 
            // PasswordGenerator
            // 
            this.AcceptButton = this.buttonGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 222);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxBanned);
            this.Controls.Add(this.labelBanned);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxSpecial);
            this.Controls.Add(this.checkBoxNumeral);
            this.Controls.Add(this.checkBoxCapital);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericUpDown1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PasswordGenerator";
            this.Text = "PasswordGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBoxCapital;
        private System.Windows.Forms.CheckBox checkBoxNumeral;
        private System.Windows.Forms.CheckBox checkBoxSpecial;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label labelBanned;
        private System.Windows.Forms.TextBox textBoxBanned;
    }
}