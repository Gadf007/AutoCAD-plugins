using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using AcDBserv = Autodesk.AutoCAD.DatabaseServices;



namespace Positions_Lib
{
    public partial class Pos3Form : Form
    {
        public Pos3Form()
        {
            InitializeComponent();
        }


        //Глобальные переменные
        string ExcelPath = string.Empty;
        string AcDocPath = string.Empty;


        //метод обработки нажатия кнопки "Загрузить"
        private void FileLoadcmdButton_Click(object sender, EventArgs e)
        {
            //ошибка отсутствия выбранной спецификации
            if (FileComboBox.SelectedItem == null)
            {
                MessageBox.Show("Файл спецификации не указан", "Не выбрана спецификация!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExcelPath = Path.Combine(AcDocPath, FileComboBox.SelectedItem.ToString()); //расположение Excel файла

            DataTable dataTable = new DataTable(); //создание промежуточной таблицы хранения данных
            dataTable.Columns.Add("Номер позиции"); //создание столбца "Номер позиции"
            dataTable.Columns.Add("Уникальный Id ключ"); //создание столбца "Уникальный Id ключ"


            //открытие транзакции обращения к Excel файлу
            using (var document = SpreadsheetDocument.Open(ExcelPath, true))
            {
                dataTable.Rows.Clear(); //удаление ранее добавленных строк 
                                        //в промежуточную таблицу
                
                //связь с листом "Спецификация"
                Sheet sheet;
                sheet = document.
                    WorkbookPart.
                    Workbook.
                    GetFirstChild<Sheets>().
                    Elements<Sheet>().
                    SingleOrDefault(s => s.Name == "Спецификация");

                //установка связи со строками таблицы
                var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id.Value);
                Worksheet worksheet = worksheetPart.Worksheet;
                IEnumerable<DocumentFormat.OpenXml.Spreadsheet.Row> rows = worksheet.
                                                                           GetFirstChild<SheetData>().
                                                                           Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>();

                //цикл перебора строк на листе книги Excel
                foreach (DocumentFormat.OpenXml.Spreadsheet.Row row in rows)
                {
                    //if (row.RowIndex.Value == 1)
                    //{
                    //    foreach (Cell cell in row.Descendants<Cell>())
                    //    {
                    //        dataTable.Columns.Add(GetCellValue(document, cell));
                    //    }
                    //}

                    string posNumber = GetCellValue(document, (Cell)row.ElementAt(0)); //номер позиции 
                    
                    //условие достижения конца спецификации на листе Excel
                    if (posNumber == "END")
                    {
                        break;
                    }
                    
                    string posId = GetCellValue(document, (Cell)row.ElementAt(5));  //уникальный Id

                    DataRow dataRow = dataTable.NewRow(); //создание строки для заполения промежуточной таблицы

                    //проверки на наличие значения
                    if (!string.IsNullOrEmpty(posNumber))
                    {
                        dataRow["Номер позиции"] = posNumber.Trim();
                    }

                    if (!string.IsNullOrEmpty(posId))
                    {
                        dataRow["Уникальный Id ключ"] = posId.Trim();
                    }

                    //добавление строки в пром. таблицу
                    dataTable.Rows.Add(dataRow);


                }//конец цикла перебора строк на "листе" Excel файла 


            }//конец транзакции обращения к Excel файлу

            //создание переменной для подключения к AutoCAD
            var currAcDb = AcAp.DocumentManager.MdiActiveDocument.Database;

            //открытие транзакции обращения к данным .dwg
            using (AcDBserv.Transaction trCurrDoc = currAcDb.TransactionManager.StartTransaction())
            {
                AcDBserv.BlockTable acBlkTbl = trCurrDoc.GetObject(currAcDb.BlockTableId, //подключение к таблице данных .dwg
                                                 AcDBserv.OpenMode.ForWrite) as AcDBserv.BlockTable;

                AcDBserv.BlockTableRecord btrCurrSpace = trCurrDoc.GetObject //подключение к таблице записей блоков .dwg, расположенных в про-ве модели
                       (acBlkTbl[AcDBserv.BlockTableRecord.ModelSpace], AcDBserv.OpenMode.ForWrite) 
                       as AcDBserv.BlockTableRecord;

                //цикл перебора обектов в пространстве модели
                foreach (AcDBserv.ObjectId objId in btrCurrSpace)
                {
                    //проверка на тип объекта ACAD (д.б. INSERT - блок)
                    if (objId.ObjectClass.DxfName == "INSERT")
                    {
                        //создание объекта "блок"
                        AcDBserv.BlockReference blkRef = trCurrDoc.
                            GetObject(objId, AcDBserv.OpenMode.ForWrite) 
                            as AcDBserv.BlockReference;

                        //создание объекта "запись блока"
                        AcDBserv.BlockTableRecord blkTableRec = trCurrDoc.
                            GetObject(blkRef.BlockTableRecord, AcDBserv.OpenMode.ForWrite) 
                            as AcDBserv.BlockTableRecord;

                        //условие определния требуемого блока по названию
                        if (blkTableRec.Name == "Position_right" || blkTableRec.Name == "Position_left")
                        {
                            blkTableRec.Dispose(); //удаление объекта "записи блока"
                            
                            //определение переменной, обращающейся к коллекции аттрибутов текущего блока
                            AcDBserv.AttributeCollection attColl = blkRef.AttributeCollection;

                            //выбор начального объекта коллекции аттрибутов блока
                            AcDBserv.ObjectId attId = attColl[0];

                            //определение переменной ссылки на объект аттрибута блока
                            AcDBserv.AttributeReference attRef = trCurrDoc.
                                GetObject(attId, AcDBserv.OpenMode.ForWrite)
                                as AcDBserv.AttributeReference;

                            //сравнение тега блока и значений строк промежуточной таблицы по "Уникальный Id ключ"
                            if (attRef.Tag == "ELEMHANDLE") //аттрибут ELEMHANDLE - "Уникальный Id ключ"
                            {
                                //цикл сравнения "Id ключей" из таблицы данных и значений аттрибутов блока
                                for (int i = 0; i < dataTable.Rows.Count; i++)
                                {
                                    if (attRef.TextString.Trim() == dataTable.Rows[i]["Уникальный Id ключ"].ToString().Trim())
                                    {
                                        attId = attColl[1];

                                        attRef = trCurrDoc.
                                            GetObject(attId, AcDBserv.OpenMode.ForWrite)
                                            as AcDBserv.AttributeReference;

                                        //запись нового значения "Номер позиции" в аттрибут блока
                                        attRef.TextString = dataTable.Rows[i]["Номер позиции"].ToString(); ;
                                        
                                        break;
                                    }
                                }
                            }
                            
                        }


                    }


                }//конец цикла перебора блоков пространства модели


                AcAp.DocumentManager.MdiActiveDocument.Editor.Regen();
                trCurrDoc.Commit();

            }//закрытие транзакции обращения к данным .dwg

            //удаление неиспользуемых переменных
            dataTable.Dispose();
            currAcDb.Dispose();

            MessageBox.Show("Позиции успешно обновлены!", "Завершение работы макроса", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //попытки на других либах
            /*
            //Excel.Application objApp = new Excel.Application();

            //Excel.Workbook objWorkbook = objApp.Workbooks.Open(ExcelPath, 0, true);
            //Excel.Worksheet objWorksheet = (Excel.Worksheet)objWorkbook.Sheets["Спецификация"];



            //var workbook = new XLWorkbook(ExcelPath);
            //var worksheet = workbook.Worksheet("Спецификация");
            //var row = worksheet.Row(0);
            //var cell = row.Cell(1);
            //string value = cell.Value.ToString();


            //worksheet.Cell("A1").Value = "Hello World!";
            //worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";
            //workbook.SaveAs("HelloWorld.xlsx");
            */

        }//конец функции обработки нажатия кнопки "Загрузить"


        //метод загрузки Win формы
        private void Form1_Load(object sender, EventArgs e)
        {
            AcDocPath = Path.GetDirectoryName(AcAp.DocumentManager.MdiActiveDocument.Database.Filename);

            DirectoryInfo dirInfo = new DirectoryInfo(AcDocPath); //объявление класса для описания свойств файлов системы

            //Условие наличия файлов с форматом .xls | .xlsx
            if (dirInfo.GetFiles("*.xlsx").Length > 0)
            {
                //Циклы записи имен найденных *.xlsx файлов в FileComboBox
                foreach (FileInfo file in dirInfo.GetFiles(@"*.xlsx"))
                {
                    FileComboBox.Items.Add(file.Name);  //запись в FileComboBox имен найденных файлов
                }

            }
            else
            {
                MessageBox.Show("По выбранному пути отсутствуют файлы формата .xlsx | .xls.\nПожалуйста, выберете другой путь", "Не найдены файлы Excel!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//конец метода загрузки Win формы


        //функция вывода данных из ячейки Excel
        private string GetCellValue(SpreadsheetDocument doc, Cell cell)
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


    }
}
