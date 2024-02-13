namespace XlsAcadTabGen_Lib
{
    partial class XlsAcadTabGenForm
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
            this.genTabButton = new System.Windows.Forms.Button();
            this.advancedDataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblExlFilePath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstBxWShs = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxTableRange = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // genTabButton
            // 
            this.genTabButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.genTabButton.Location = new System.Drawing.Point(418, 35);
            this.genTabButton.Name = "genTabButton";
            this.genTabButton.Size = new System.Drawing.Size(120, 29);
            this.genTabButton.TabIndex = 0;
            this.genTabButton.Text = "Генерировать";
            this.genTabButton.UseVisualStyleBackColor = true;
            this.genTabButton.Click += new System.EventHandler(this.genTabButton_Click);
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.FilterAndSortEnabled = true;
            this.advancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advancedDataGridView1.Location = new System.Drawing.Point(12, 35);
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.advancedDataGridView1.Size = new System.Drawing.Size(381, 279);
            this.advancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advancedDataGridView1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлExcelToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(563, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлExcelToolStripMenuItem
            // 
            this.файлExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьФайлToolStripMenuItem,
            this.загрузитьТаблицуToolStripMenuItem});
            this.файлExcelToolStripMenuItem.Name = "файлExcelToolStripMenuItem";
            this.файлExcelToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.файлExcelToolStripMenuItem.Text = "Файл Excel";
            // 
            // выбратьФайлToolStripMenuItem
            // 
            this.выбратьФайлToolStripMenuItem.Name = "выбратьФайлToolStripMenuItem";
            this.выбратьФайлToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.выбратьФайлToolStripMenuItem.Text = "Выбрать файл";
            this.выбратьФайлToolStripMenuItem.Click += new System.EventHandler(this.выбратьФайлToolStripMenuItem_Click);
            // 
            // загрузитьТаблицуToolStripMenuItem
            // 
            this.загрузитьТаблицуToolStripMenuItem.Name = "загрузитьТаблицуToolStripMenuItem";
            this.загрузитьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.загрузитьТаблицуToolStripMenuItem.Text = "Загрузить таблицу";
            this.загрузитьТаблицуToolStripMenuItem.Click += new System.EventHandler(this.загрузитьТаблицуToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // lblExlFilePath
            // 
            this.lblExlFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExlFilePath.AutoSize = true;
            this.lblExlFilePath.Location = new System.Drawing.Point(16, 360);
            this.lblExlFilePath.Name = "lblExlFilePath";
            this.lblExlFilePath.Size = new System.Drawing.Size(89, 13);
            this.lblExlFilePath.TabIndex = 3;
            this.lblExlFilePath.Text = "*расположение*";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Расположение выбранного файла Excel:";
            // 
            // lstBxWShs
            // 
            this.lstBxWShs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBxWShs.FormattingEnabled = true;
            this.lstBxWShs.Location = new System.Drawing.Point(418, 109);
            this.lstBxWShs.Name = "lstBxWShs";
            this.lstBxWShs.Size = new System.Drawing.Size(120, 160);
            this.lstBxWShs.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Листы книги Excel";
            // 
            // txtBxTableRange
            // 
            this.txtBxTableRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxTableRange.Location = new System.Drawing.Point(418, 294);
            this.txtBxTableRange.Name = "txtBxTableRange";
            this.txtBxTableRange.Size = new System.Drawing.Size(120, 20);
            this.txtBxTableRange.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Диапазон таблицы";
            // 
            // XlsAcadTabGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 387);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxTableRange);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstBxWShs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblExlFilePath);
            this.Controls.Add(this.advancedDataGridView1);
            this.Controls.Add(this.genTabButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(579, 426);
            this.Name = "XlsAcadTabGenForm";
            this.Text = "Плагин импорта таблицы";
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button genTabButton;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьТаблицуToolStripMenuItem;
        private System.Windows.Forms.Label lblExlFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstBxWShs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxTableRange;
        private System.Windows.Forms.Label label3;
    }
}