namespace Positions_Lib
{
    partial class Pos3Form
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
            this.FileComboBox = new System.Windows.Forms.ComboBox();
            this.FileLoadcmdButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileComboBox
            // 
            this.FileComboBox.FormattingEnabled = true;
            this.FileComboBox.Location = new System.Drawing.Point(12, 32);
            this.FileComboBox.Name = "FileComboBox";
            this.FileComboBox.Size = new System.Drawing.Size(206, 21);
            this.FileComboBox.TabIndex = 0;
            // 
            // FileLoadcmdButton
            // 
            this.FileLoadcmdButton.Location = new System.Drawing.Point(239, 32);
            this.FileLoadcmdButton.Name = "FileLoadcmdButton";
            this.FileLoadcmdButton.Size = new System.Drawing.Size(90, 21);
            this.FileLoadcmdButton.TabIndex = 5;
            this.FileLoadcmdButton.Text = "Обновить";
            this.FileLoadcmdButton.UseVisualStyleBackColor = true;
            this.FileLoadcmdButton.Click += new System.EventHandler(this.FileLoadcmdButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Спецификация";
            // 
            // Pos3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 79);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileLoadcmdButton);
            this.Controls.Add(this.FileComboBox);
            this.MaximumSize = new System.Drawing.Size(357, 118);
            this.MinimumSize = new System.Drawing.Size(357, 118);
            this.Name = "Pos3Form";
            this.Text = "Обновление позиций";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox FileComboBox;
        private System.Windows.Forms.Button FileLoadcmdButton;
        private System.Windows.Forms.Label label1;
    }
}