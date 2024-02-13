using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using AcDBserv = Autodesk.AutoCAD.DatabaseServices;

namespace XlsAcadTabGen_Lib
{
    public partial class XlsAcadTabGenForm : Form
    {
        public XlsAcadTabGenForm()
        {
            InitializeComponent();
        }


        private void genTabButton_Click(object sender, EventArgs e)
        {
            Document acDoc = AcAp.DocumentManager.MdiActiveDocument;
            Database currAcDb = acDoc.Database;
            Editor editor = AcAp.DocumentManager.MdiActiveDocument.Editor;

            PromptPointResult promptPointResult = editor.GetPoint("\nУкажите точку вставки таблицы: ");

            if (promptPointResult.Status != PromptStatus.OK)
                return;


            //открытие транзакции обращения к данным .dwg
            using (AcDBserv.Transaction trCurrDoc = currAcDb.TransactionManager.StartTransaction())
            {
                AcDBserv.BlockTable acBlkTbl = trCurrDoc.GetObject(currAcDb.BlockTableId, //подключение к таблице данных .dwg
                                                                        AcDBserv.OpenMode.ForWrite) as AcDBserv.BlockTable;

                AcDBserv.BlockTableRecord btrCurrSpace = trCurrDoc.GetObject //подключение к таблице записей блоков .dwg, расположенных в про-ве модели
                       (acBlkTbl[AcDBserv.BlockTableRecord.ModelSpace], AcDBserv.OpenMode.ForWrite)
                       as AcDBserv.BlockTableRecord;

                AcDBserv.Table table = new AcDBserv.Table(); //создание объекта ТАБЛИЦА

                table.TableStyle = currAcDb.Tablestyle; //определение изначального стиля таблицы (наличие строк "Название", "Заголовок" и т.д.)
                table.Position = promptPointResult.Value; //запись координат точки вставки таблицы

                table.SetSize(advancedDataGridView1.Rows.Count - 1, advancedDataGridView1.Columns.Count); //определение размера создаваемой таблицы 
                table.Cells[0, -1].Style = ""; //изменение стиля первой строки таб. с "Название" на "Данные"
                table.Cells[1, -1].Style = ""; //изменение стиля второй строки таб. с "Заголовок" на "Данные"

                //циклы перебора всех значений пром. таблицы
                for (int i = 0; i < advancedDataGridView1.Rows.Count - 1; i++)
                {
                    for(int j = 0; j < advancedDataGridView1.Columns.Count; j++)
                    {
                        table.Cells[i, j].TextHeight = 3.0; //высота строки 3.0
                        table.Cells[i, j].TextString =  advancedDataGridView1.Rows[i].Cells[j].Value.ToString(); //запись значения в ячейку таблицы
                        table.Cells[i, j].Alignment = CellAlignment.MiddleCenter; //расположение текст по центру
                    }
                }
                
                
                table.GenerateLayout(); //присвоение таблицы текущего стиля таблиц
                btrCurrSpace.AppendEntity(table); //добавление созданной таблицы на таблицу записи блоков
                trCurrDoc.AddNewlyCreatedDBObject(table, true); //добавление созданной таблицы на пространство модели

                editor.Regen(); //регенерация пространства модели
                trCurrDoc.Commit(); //сохранение внесенных изменений

            }//закрытие транзакции обращения к данным .dwg



        }

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exlFilePath = string.Empty; //расположение файла Excel


            //Очистка формы
            advancedDataGridView1.DataSource = null;
            advancedDataGridView1.Rows.Clear();
            advancedDataGridView1.Columns.Clear();
            lstBxWShs.Items.Clear();


            //определение пути к файлу через файловый диалог
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                //Блок выбора файла Excel
                //------------------------------------------------------
                openFileDialog.InitialDirectory = "c:\\";  //начальный репозиторий при открытии

                openFileDialog.Filter = "Excel format (*.xlsx)|*.xlsx|All files (*.*)|*.*"; //настройки фильтра отображения файлов в проводнике
                                                                                                  //описание1|формат1|описание2|формат2

                //условие нажатия "Ок" в файловом диалоге
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    exlFilePath = openFileDialog.FileName; //определение пути БД
                    //MessageBox.Show("Расположение базы данных: " + dbFilePath, "База данных определена", MessageBoxButtons.OK); //отображение окна об успешном определеии пути БД
                }

                lblExlFilePath.Text = exlFilePath; //отображение пути выбранного файла Excel
                //------------------------------------------------------

            }//конец транзакции обращение к проводнику Win

            //открытие транзакции обращения к Excel файлу
            using (var document = SpreadsheetDocument.Open(exlFilePath, false))
            {
                IEnumerable<Sheet> sheets = document.
                                            WorkbookPart.
                                            Workbook.Sheets.Elements<Sheet>();

                //Sheets sheets = document.WorkbookPart.Workbook.Sheets; //набор всех листов книги
                
                //цикл добавления наименований листов книги Excel в листбокс 
                foreach(var sheet in sheets)
                {
                    lstBxWShs.Items.Add(sheet.Name);
                }


            }//конец транзакции обращения к Excel файлу


        }//конец обработки нажатия "Выбрать файл"

        private void загрузитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            advancedDataGridView1.DataSource = null;
            advancedDataGridView1.Rows.Clear();
            advancedDataGridView1.Columns.Clear();


            string tblAddrs = string.Empty;
            string wshName = string.Empty;
            int rowCount, colCount;
            int[] startCellArr = new int[2];
            int[] endCellArr = new int[2];


            if (string.IsNullOrEmpty(txtBxTableRange.Text) || lstBxWShs.SelectedItems.Count != 1)
            {
                MessageBox.Show("Укажите адрес диапазона таблицы!", "Не указан адрес");
                return;
            }
            else
            {
                tblAddrs = txtBxTableRange.Text;             //переменная адреса диапазона
                wshName = lstBxWShs.SelectedItem.ToString(); //переменная выбранного имени листа    
            }

            

            tblAddrs.Trim();
            tblAddrs.ToUpper();
            string[] addrArray = tblAddrs.Split(':');
            
            string startCell = addrArray[0];
            string endCell = addrArray[1];

            //startRowIndex = Int32.Parse(Regex.Match(startCell, @"\d+").Value);
            //endRowIndex = Int32.Parse(Regex.Match(endCell, @"\d+").Value);

            startCellArr = CellReferenceToIndex(startCell);
            endCellArr = CellReferenceToIndex(endCell);

            rowCount = endCellArr[0] - startCellArr[0];
            colCount = endCellArr[1] - startCellArr[1];

            //создание промежуточной таблицы хранения данных
            System.Data.DataTable dataTable = new System.Data.DataTable(); 
            
            for (int i = 0; i <= colCount; i++)
            {
                dataTable.Columns.Add(); //создание столбцов
            }
            
            /*for (int i = 0; i < rowCount; i++)
            {
                dataTable.NewRow(); //создание строк
            }*/


            //открытие транзакции обращения к Excel файлу
            using (var document = SpreadsheetDocument.Open(lblExlFilePath.Text, false))
            {
                

                //связь с листом "Спецификация"
                Sheet sheet = document.
                    WorkbookPart.
                    Workbook.
                    GetFirstChild<Sheets>().
                    Elements<Sheet>().
                    SingleOrDefault(s => s.Name == wshName);

                //установка связи со строками таблицы
                var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id.Value);
                Worksheet worksheet = worksheetPart.Worksheet;
                //IEnumerable<DocumentFormat.OpenXml.Spreadsheet.Row> rows = worksheet.
                //                                                           GetFirstChild<SheetData>().
                //                                                           Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>();

                IEnumerable<SheetData> sheetData = worksheetPart.Worksheet.Elements<SheetData>();
                
                foreach(SheetData sd in sheetData)
                {
                    int rowCurr = 0;
                    int cellCurr = 0;
                    int dtColInd = 0;

                    foreach (DocumentFormat.OpenXml.Spreadsheet.Row row in sd.Elements<DocumentFormat.OpenXml.Spreadsheet.Row>())
                    {
                        if (rowCurr >= startCellArr[0] && rowCurr <= endCellArr[0])
                        {
                            DataRow dataRow = dataTable.NewRow();

                            foreach (DocumentFormat.OpenXml.Spreadsheet.Cell cell in row.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                            {

                                if (cellCurr >= startCellArr[1] && cellCurr <= endCellArr[1])
                                {
                                    dataRow[dtColInd] = GetCellValue(document, cell);
                                    dtColInd++;

                                }

                                cellCurr++;
                            }

                            cellCurr = 0;
                            dtColInd = 0;

                            dataTable.Rows.Add(dataRow); //добавление строки в пром. таблицу
                        }


                        rowCurr++;
                    }
                }




            }//конец транзакции обращения к Excel файлу


            advancedDataGridView1.DataSource = dataTable;



        }//конец обработки нажатия "Загрузить таблицу"



        //функция определения адреса ячейки 
        public static int[] CellReferenceToIndex(string reference)
        {
            int[] arr = new int[2];
            
            int row_index = 0;
            int col_index = 0;


            foreach (char c in reference)
            {
                if (c >= '0' && c <= '9')
                {
                    row_index = row_index * 10 + (c - '0');
                }
                if (c >= 'A' && c <= 'Z')
                {
                    col_index = col_index * ('Z' - 'A' + 1) + (c - 'A' + 1);
                }
            }

            arr[0] = row_index - 1;
            arr[1] = col_index - 1;

            return arr;
        }


        //функция вывода данных из ячейки Excel
        private string GetCellValue(SpreadsheetDocument doc, DocumentFormat.OpenXml.Spreadsheet.Cell cell)
        {
            if (cell.CellValue != null)
            {
                string value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    // in older version e.g. 2.0, you can use GetItem instead of ElementAt
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.ElementAt(int.Parse(value)).InnerText;
                }
                else
                {
                    return value;
                }
            }
            return string.Empty;
        }//конец функции вывода данных из ячейки Excel

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данный плагин позволяет импортировать заданный\n диапазон ячеек с листа Excel в пространство модели AutoCAD", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }//конец главной функции
}
