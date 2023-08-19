
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
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            textBox1 = new System.Windows.Forms.TextBox();
            checkBoxCapital = new System.Windows.Forms.CheckBox();
            checkBoxNumeral = new System.Windows.Forms.CheckBox();
            checkBoxSpecial = new System.Windows.Forms.CheckBox();
            buttonOk = new System.Windows.Forms.Button();
            buttonGenerate = new System.Windows.Forms.Button();
            labelBanned = new System.Windows.Forms.Label();
            textBoxBanned = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(219, 109);
            numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            numericUpDown1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(139, 27);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(14, 16);
            textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(199, 253);
            textBox1.TabIndex = 1;
            // 
            // checkBoxCapital
            // 
            checkBoxCapital.AutoSize = true;
            checkBoxCapital.Location = new System.Drawing.Point(219, 13);
            checkBoxCapital.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            checkBoxCapital.Name = "checkBoxCapital";
            checkBoxCapital.Size = new System.Drawing.Size(173, 24);
            checkBoxCapital.TabIndex = 2;
            checkBoxCapital.Text = "Include capital letters";
            checkBoxCapital.UseVisualStyleBackColor = true;
            // 
            // checkBoxNumeral
            // 
            checkBoxNumeral.AutoSize = true;
            checkBoxNumeral.Location = new System.Drawing.Point(219, 45);
            checkBoxNumeral.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            checkBoxNumeral.Name = "checkBoxNumeral";
            checkBoxNumeral.Size = new System.Drawing.Size(140, 24);
            checkBoxNumeral.TabIndex = 3;
            checkBoxNumeral.Text = "Include numbers";
            checkBoxNumeral.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpecial
            // 
            checkBoxSpecial.AutoSize = true;
            checkBoxSpecial.Location = new System.Drawing.Point(219, 77);
            checkBoxSpecial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            checkBoxSpecial.Name = "checkBoxSpecial";
            checkBoxSpecial.Size = new System.Drawing.Size(129, 24);
            checkBoxSpecial.TabIndex = 4;
            checkBoxSpecial.Text = "Include special";
            checkBoxSpecial.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.Location = new System.Drawing.Point(219, 238);
            buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(139, 31);
            buttonOk.TabIndex = 5;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new System.Drawing.Point(219, 199);
            buttonGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new System.Drawing.Size(139, 31);
            buttonGenerate.TabIndex = 6;
            buttonGenerate.Text = "Make password";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // labelBanned
            // 
            labelBanned.AutoSize = true;
            labelBanned.Location = new System.Drawing.Point(219, 140);
            labelBanned.Name = "labelBanned";
            labelBanned.Size = new System.Drawing.Size(130, 20);
            labelBanned.TabIndex = 7;
            labelBanned.Text = "Banned characters";
            // 
            // textBoxBanned
            // 
            textBoxBanned.Location = new System.Drawing.Point(219, 164);
            textBoxBanned.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textBoxBanned.Name = "textBoxBanned";
            textBoxBanned.Size = new System.Drawing.Size(139, 27);
            textBoxBanned.TabIndex = 8;
            // 
            // PasswordGenerator
            // 
            AcceptButton = buttonGenerate;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(404, 295);
            ControlBox = false;
            Controls.Add(textBoxBanned);
            Controls.Add(labelBanned);
            Controls.Add(buttonGenerate);
            Controls.Add(buttonOk);
            Controls.Add(checkBoxSpecial);
            Controls.Add(checkBoxNumeral);
            Controls.Add(checkBoxCapital);
            Controls.Add(textBox1);
            Controls.Add(numericUpDown1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "PasswordGenerator";
            Text = "PasswordGenerator";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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