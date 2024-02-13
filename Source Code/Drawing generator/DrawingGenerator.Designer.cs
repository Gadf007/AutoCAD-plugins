namespace LoopGen_Lib
{
    partial class DrawingGenerator
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.выборСпособаГенерацииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.одинШаблонЗаЦиклToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.несколькоШаблоновЗаЦиклToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьБДToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbTablesComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dbQueriesComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTmplFilePath = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dwgTmplListBox = new System.Windows.Forms.ListBox();
            this.dwgTmplFullNamesListBox = new System.Windows.Forms.ListBox();
            this.startCreateLoops = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.totalRows = new System.Windows.Forms.Label();
            this.lblDbFilePath = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
            this.oneFile = new System.Windows.Forms.RadioButton();
            this.manyFiles = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelLoopPath = new System.Windows.Forms.Button();
            this.acDocNameTxtBox = new System.Windows.Forms.TextBox();
            this.lblAcDocName = new System.Windows.Forms.Label();
            this.lblPageNumber = new System.Windows.Forms.Label();
            this.pageNumberTxtBox = new System.Windows.Forms.TextBox();
            this.txtBoxLoopPath = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выборСпособаГенерацииToolStripMenuItem,
            this.файлToolStripMenuItem,
            this.редактироватьБДToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(454, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // выборСпособаГенерацииToolStripMenuItem
            // 
            this.выборСпособаГенерацииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.одинШаблонЗаЦиклToolStripMenuItem,
            this.несколькоШаблоновЗаЦиклToolStripMenuItem});
            this.выборСпособаГенерацииToolStripMenuItem.Name = "выборСпособаГенерацииToolStripMenuItem";
            this.выборСпособаГенерацииToolStripMenuItem.Size = new System.Drawing.Size(166, 20);
            this.выборСпособаГенерацииToolStripMenuItem.Text = "Выбор способа генерации";
            // 
            // одинШаблонЗаЦиклToolStripMenuItem
            // 
            this.одинШаблонЗаЦиклToolStripMenuItem.CheckOnClick = true;
            this.одинШаблонЗаЦиклToolStripMenuItem.Name = "одинШаблонЗаЦиклToolStripMenuItem";
            this.одинШаблонЗаЦиклToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.одинШаблонЗаЦиклToolStripMenuItem.Text = "Один шаблон за цикл";
            this.одинШаблонЗаЦиклToolStripMenuItem.Click += new System.EventHandler(this.одинШаблонЗаЦиклToolStripMenuItem_Click);
            // 
            // несколькоШаблоновЗаЦиклToolStripMenuItem
            // 
            this.несколькоШаблоновЗаЦиклToolStripMenuItem.CheckOnClick = true;
            this.несколькоШаблоновЗаЦиклToolStripMenuItem.Name = "несколькоШаблоновЗаЦиклToolStripMenuItem";
            this.несколькоШаблоновЗаЦиклToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.несколькоШаблоновЗаЦиклToolStripMenuItem.Text = "Несколько шаблонов за цикл";
            this.несколькоШаблоновЗаЦиклToolStripMenuItem.Click += new System.EventHandler(this.несколькоШаблоновЗаЦиклToolStripMenuItem_Click);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьБДToolStripMenuItem,
            this.загрузитьБДToolStripMenuItem1});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.файлToolStripMenuItem.Text = "Файл Access";
            // 
            // загрузитьБДToolStripMenuItem
            // 
            this.загрузитьБДToolStripMenuItem.Name = "загрузитьБДToolStripMenuItem";
            this.загрузитьБДToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.загрузитьБДToolStripMenuItem.Text = "Выбрать БД";
            this.загрузитьБДToolStripMenuItem.Click += new System.EventHandler(this.загрузитьБДToolStripMenuItem_Click);
            // 
            // загрузитьБДToolStripMenuItem1
            // 
            this.загрузитьБДToolStripMenuItem1.Name = "загрузитьБДToolStripMenuItem1";
            this.загрузитьБДToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.загрузитьБДToolStripMenuItem1.Text = "Загрузить БД";
            this.загрузитьБДToolStripMenuItem1.Click += new System.EventHandler(this.загрузитьБДToolStripMenuItem1_Click);
            // 
            // редактироватьБДToolStripMenuItem
            // 
            this.редактироватьБДToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem});
            this.редактироватьБДToolStripMenuItem.Name = "редактироватьБДToolStripMenuItem";
            this.редактироватьБДToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.редактироватьБДToolStripMenuItem.Text = "Файл AutoCAD";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.добавитьToolStripMenuItem.Text = "Загрузить шаблоны";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // dbTablesComboBox
            // 
            this.dbTablesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dbTablesComboBox.FormattingEnabled = true;
            this.dbTablesComboBox.Location = new System.Drawing.Point(498, 448);
            this.dbTablesComboBox.Name = "dbTablesComboBox";
            this.dbTablesComboBox.Size = new System.Drawing.Size(172, 21);
            this.dbTablesComboBox.TabIndex = 7;
            this.dbTablesComboBox.SelectionChangeCommitted += new System.EventHandler(this.dbTablesComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(445, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Таблица:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 423);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Запрос:";
            // 
            // dbQueriesComboBox
            // 
            this.dbQueriesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dbQueriesComboBox.FormattingEnabled = true;
            this.dbQueriesComboBox.Location = new System.Drawing.Point(498, 421);
            this.dbQueriesComboBox.Name = "dbQueriesComboBox";
            this.dbQueriesComboBox.Size = new System.Drawing.Size(172, 21);
            this.dbQueriesComboBox.TabIndex = 10;
            this.dbQueriesComboBox.SelectionChangeCommitted += new System.EventHandler(this.dbQueriesComboBox_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(695, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Расположение шаблонов:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTmplFilePath
            // 
            this.lblTmplFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTmplFilePath.AutoSize = true;
            this.lblTmplFilePath.Location = new System.Drawing.Point(695, 53);
            this.lblTmplFilePath.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblTmplFilePath.Name = "lblTmplFilePath";
            this.lblTmplFilePath.Size = new System.Drawing.Size(54, 13);
            this.lblTmplFilePath.TabIndex = 15;
            this.lblTmplFilePath.Text = "Tmpl path";
            this.lblTmplFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Список найденных шаблонов:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dwgTmplListBox
            // 
            this.dwgTmplListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dwgTmplListBox.FormattingEnabled = true;
            this.dwgTmplListBox.Location = new System.Drawing.Point(13, 87);
            this.dwgTmplListBox.Name = "dwgTmplListBox";
            this.dwgTmplListBox.Size = new System.Drawing.Size(155, 121);
            this.dwgTmplListBox.TabIndex = 17;
            // 
            // dwgTmplFullNamesListBox
            // 
            this.dwgTmplFullNamesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dwgTmplFullNamesListBox.FormattingEnabled = true;
            this.dwgTmplFullNamesListBox.Location = new System.Drawing.Point(281, 452);
            this.dwgTmplFullNamesListBox.Name = "dwgTmplFullNamesListBox";
            this.dwgTmplFullNamesListBox.Size = new System.Drawing.Size(137, 17);
            this.dwgTmplFullNamesListBox.TabIndex = 18;
            this.dwgTmplFullNamesListBox.Visible = false;
            // 
            // startCreateLoops
            // 
            this.startCreateLoops.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startCreateLoops.Location = new System.Drawing.Point(721, 426);
            this.startCreateLoops.Name = "startCreateLoops";
            this.startCreateLoops.Size = new System.Drawing.Size(112, 43);
            this.startCreateLoops.TabIndex = 19;
            this.startCreateLoops.Text = "Начать генерацию";
            this.startCreateLoops.UseVisualStyleBackColor = true;
            this.startCreateLoops.Click += new System.EventHandler(this.startCreateLoops_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.totalRows);
            this.groupBox1.Controls.Add(this.lblDbFilePath);
            this.groupBox1.Location = new System.Drawing.Point(8, 407);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 69);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "База данных";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Расположение БД:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Выбрано записей:";
            // 
            // totalRows
            // 
            this.totalRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalRows.AutoSize = true;
            this.totalRows.Location = new System.Drawing.Point(104, 51);
            this.totalRows.Name = "totalRows";
            this.totalRows.Size = new System.Drawing.Size(13, 13);
            this.totalRows.TabIndex = 13;
            this.totalRows.Text = "0";
            // 
            // lblDbFilePath
            // 
            this.lblDbFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDbFilePath.AutoSize = true;
            this.lblDbFilePath.Location = new System.Drawing.Point(105, 23);
            this.lblDbFilePath.Name = "lblDbFilePath";
            this.lblDbFilePath.Size = new System.Drawing.Size(46, 13);
            this.lblDbFilePath.TabIndex = 5;
            this.lblDbFilePath.Text = "DB path";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.dwgTmplListBox);
            this.groupBox4.Location = new System.Drawing.Point(685, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(178, 219);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Шаблоны";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.FilterAndSortEnabled = true;
            this.dataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dataGridView1.Location = new System.Drawing.Point(8, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(671, 371);
            this.dataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // oneFile
            // 
            this.oneFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.oneFile.AutoSize = true;
            this.oneFile.Location = new System.Drawing.Point(12, 20);
            this.oneFile.Name = "oneFile";
            this.oneFile.Size = new System.Drawing.Size(106, 17);
            this.oneFile.TabIndex = 22;
            this.oneFile.TabStop = true;
            this.oneFile.Text = "Один файл .dwg";
            this.oneFile.UseVisualStyleBackColor = true;
            // 
            // manyFiles
            // 
            this.manyFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.manyFiles.AutoSize = true;
            this.manyFiles.Location = new System.Drawing.Point(12, 43);
            this.manyFiles.Name = "manyFiles";
            this.manyFiles.Size = new System.Drawing.Size(150, 17);
            this.manyFiles.TabIndex = 23;
            this.manyFiles.TabStop = true;
            this.manyFiles.Text = "Множество файлов .dwg";
            this.manyFiles.UseVisualStyleBackColor = true;
            this.manyFiles.CheckedChanged += new System.EventHandler(this.manyFiles_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.manyFiles);
            this.groupBox2.Controls.Add(this.btnSelLoopPath);
            this.groupBox2.Controls.Add(this.oneFile);
            this.groupBox2.Location = new System.Drawing.Point(685, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 167);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Режимы генерации";
            // 
            // btnSelLoopPath
            // 
            this.btnSelLoopPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelLoopPath.Location = new System.Drawing.Point(6, 93);
            this.btnSelLoopPath.Name = "btnSelLoopPath";
            this.btnSelLoopPath.Size = new System.Drawing.Size(168, 23);
            this.btnSelLoopPath.TabIndex = 29;
            this.btnSelLoopPath.Text = "Путь генерации чертежей";
            this.btnSelLoopPath.UseVisualStyleBackColor = true;
            this.btnSelLoopPath.Click += new System.EventHandler(this.btnSelLoopPath_Click);
            // 
            // acDocNameTxtBox
            // 
            this.acDocNameTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acDocNameTxtBox.Location = new System.Drawing.Point(691, 373);
            this.acDocNameTxtBox.Name = "acDocNameTxtBox";
            this.acDocNameTxtBox.Size = new System.Drawing.Size(80, 20);
            this.acDocNameTxtBox.TabIndex = 25;
            // 
            // lblAcDocName
            // 
            this.lblAcDocName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAcDocName.AutoSize = true;
            this.lblAcDocName.Location = new System.Drawing.Point(688, 354);
            this.lblAcDocName.Name = "lblAcDocName";
            this.lblAcDocName.Size = new System.Drawing.Size(84, 13);
            this.lblAcDocName.TabIndex = 26;
            this.lblAcDocName.Text = "Шифр чертежа:";
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageNumber.AutoSize = true;
            this.lblPageNumber.Location = new System.Drawing.Point(776, 354);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Size = new System.Drawing.Size(76, 13);
            this.lblPageNumber.TabIndex = 27;
            this.lblPageNumber.Text = "Номер листа:";
            // 
            // pageNumberTxtBox
            // 
            this.pageNumberTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageNumberTxtBox.Location = new System.Drawing.Point(779, 373);
            this.pageNumberTxtBox.Name = "pageNumberTxtBox";
            this.pageNumberTxtBox.Size = new System.Drawing.Size(80, 20);
            this.pageNumberTxtBox.TabIndex = 28;
            // 
            // txtBoxLoopPath
            // 
            this.txtBoxLoopPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxLoopPath.Location = new System.Drawing.Point(691, 304);
            this.txtBoxLoopPath.Name = "txtBoxLoopPath";
            this.txtBoxLoopPath.ReadOnly = true;
            this.txtBoxLoopPath.Size = new System.Drawing.Size(168, 20);
            this.txtBoxLoopPath.TabIndex = 30;
            // 
            // DrawingGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 480);
            this.Controls.Add(this.txtBoxLoopPath);
            this.Controls.Add(this.pageNumberTxtBox);
            this.Controls.Add(this.lblPageNumber);
            this.Controls.Add(this.lblAcDocName);
            this.Controls.Add(this.acDocNameTxtBox);
            this.Controls.Add(this.startCreateLoops);
            this.Controls.Add(this.dwgTmplFullNamesListBox);
            this.Controls.Add(this.lblTmplFilePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dbQueriesComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dbTablesComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(891, 519);
            this.Name = "DrawingGenerator";
            this.Text = "Drawing Generator v1.0";
            this.Load += new System.EventHandler(this.LoopGenerator_WinForm1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьБДToolStripMenuItem1;
        private System.Windows.Forms.ComboBox dbTablesComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dbQueriesComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTmplFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox dwgTmplListBox;
        private System.Windows.Forms.ListBox dwgTmplFullNamesListBox;
        private System.Windows.Forms.Button startCreateLoops;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label totalRows;
        private System.Windows.Forms.Label lblDbFilePath;
        private Zuby.ADGV.AdvancedDataGridView dataGridView1;
        private System.Windows.Forms.RadioButton oneFile;
        private System.Windows.Forms.RadioButton manyFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox acDocNameTxtBox;
        private System.Windows.Forms.Label lblAcDocName;
        private System.Windows.Forms.Label lblPageNumber;
        private System.Windows.Forms.TextBox pageNumberTxtBox;
        private System.Windows.Forms.Button btnSelLoopPath;
        private System.Windows.Forms.TextBox txtBoxLoopPath;
        private System.Windows.Forms.ToolStripMenuItem выборСпособаГенерацииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem одинШаблонЗаЦиклToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem несколькоШаблоновЗаЦиклToolStripMenuItem;
    }
}

