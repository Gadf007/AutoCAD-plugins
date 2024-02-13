using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using AcDBserv = Autodesk.AutoCAD.DatabaseServices;



namespace LoopGen_Lib
{
    public partial class DrawingGenerator : Form
    {
        string dbFilePath = string.Empty;  //переменная пути БД
        string tmplFilePath = string.Empty;  //переменная шаблонов
        string acLoopDocPath = string.Empty; //переменная репозитория множества чертежей контуров
        bool flagManyFiles = true;
        ObjectIdCollection clonedObjCol = new ObjectIdCollection(); //коллекция objId клонированных объектов

        public DrawingGenerator()
        {
            InitializeComponent();

        }

        //Блок подключения к Access файлу и парсинг всех запросов и имен таблиц
        private void загрузитьБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Очистка всех раннее загруженных имен таблиц, запросов, данных из БД
            dbTablesComboBox.SelectedIndex = -1;
            dbQueriesComboBox.SelectedIndex = -1;
            dbTablesComboBox.Items.Clear();
            dbQueriesComboBox.Items.Clear();
           

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            //определение пути к файлу через файловый диалог
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
              // if ()

                openFileDialog.InitialDirectory = "c:\\";  //начальный репозиторий при открытии

                openFileDialog.Filter = "Access DB format (*.accdb)|*.accdb|All files (*.*)|*.*"; //настройки фильтра отображения файлов в проводнике
                                                                                                  //описание1|формат1|описание2|формат2

                //условие нажатия "Ок" в файловом диалоге
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dbFilePath = openFileDialog.FileName; //определение пути БД
                    //MessageBox.Show("Расположение базы данных: " + dbFilePath, "База данных определена", MessageBoxButtons.OK); //отображение окна об успешном определеии пути БД
                }

                //dbFilePath = "D:\\CSharp\\LoopGen_Lib\\DB_vibr.accdb"; //удалить 
                //dbFilePath = "C:\\Work\\VisualStudioPrj\\LoopGen_Lib\\DB_vibr.accdb";

                lblDbFilePath.Text = dbFilePath; //отображение пути выбранной БД

            }

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.16.0;Data source={dbFilePath}"; // строка соединения //$"provider =Microsoft.Jet.OLEDB.4.0;
            OleDbConnection dbConnection = new OleDbConnection(connectionString);  //создание соединения

            dbConnection.Open(); //подключение к БД


            //Блок определения таблиц файла Access
            //------------------------------------------------------
            //Определение списка наименований всех пользовательских таблиц Access файла
            string[] restrictions = new string[4];   //массив для введения ограничения (только пользовательские таблицы)
                                                     //выбора данных методом GetSchema
            restrictions[3] = "Table";               //только пользовательские таблицы
            var userTables = dbConnection.GetSchema("Tables", restrictions); //получение таблицы с данными о всех "Tables" в Access файле

            List<string> tableNames = new List<string>();   //список имен таблиц
            for (int i = 0; i < userTables.Rows.Count; i++) //добавление элементов списка 
                tableNames.Add(userTables.Rows[i][2].ToString()); //строка таблицы данных состоит из массива, где
                                                                  //[2] - наименование таблицы
                                                                  //[3] - тип таблицы, в данном случае "TABLE"

            //Добавление наименований таблиц в выпадающий список
            foreach (string name in tableNames)
            {
                dbTablesComboBox.Items.Add(name);
            }
            //------------------------------------------------------



            //Блок определения запросов файла Access
            //------------------------------------------------------
            System.Data.DataTable queries = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Views, null); //получение таблицы с данными о всех "Views" в Access файле
            List<string> queryNames = new List<string>();   //список имен запросов
            for (int i = 0; i < queries.Rows.Count; i++) //добавление элементов списка 
                queryNames.Add(queries.Rows[i]["TABLE_NAME"].ToString());

            //Добавление наименований запросов в выпадающий список
            foreach (string name in queryNames)
            {
                dbQueriesComboBox.Items.Add(name);
            }
            //------------------------------------------------------


            dbConnection.Close(); //отключение от БД

        }


        //Блок загрузки данных из Access файла в макрос
        private void загрузитьБДToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dbTablesComboBox.SelectedItem == null && dbQueriesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите таблицу или запрос БД для работы", "Не выбраны данные БД!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (dbTablesComboBox.SelectedItem != null && dbQueriesComboBox.SelectedItem != null)
                {
                    MessageBox.Show("Невозможно выбрать данные. \nПожалуйста, выберете либо таблицу либо запрос БД", "Ошибка выбора!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


            string connectionString = $"Provider=Microsoft.ACE.OLEDB.16.0;Data source={dbFilePath}"; // строка соединения //$"provider =Microsoft.Jet.OLEDB.4.0;
            OleDbConnection dbConnection = new OleDbConnection(connectionString);  //создание соединения

            dbConnection.Open(); //подключение к БД

            //Вывод данных выбранной таблицы пользователю
            if (dbTablesComboBox.SelectedItem != null && dbQueriesComboBox.SelectedItem == null) //условие, что таблица выбрана пользователем
            {
                string query = "SELECT * FROM " + "[" + dbTablesComboBox.Text + "]"; //строка запроса
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection);

                DataSet dataSet = new DataSet(); //создание пустого набора данных
                dataAdapter.Fill(dataSet); //заполнение созданного набора данных   (dataSet, dbTablesComboBox.Text)
                dataGridView1.DataSource = dataSet.Tables[0]; //вывод данных ввиде таблицы

                dbConnection.Close(); //отключение от БД
            }
            else
            {
                if (dbTablesComboBox.SelectedItem == null && dbQueriesComboBox.SelectedItem != null) //условие, что запрос выбран пользователем
                {
                    string query = string.Empty;

                    System.Data.DataTable queries = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Views, null); //получение таблицы с данными о всех "Views" в Access файле
                    List<(string, string)> queryDefinitions = new List<(string, string)>();   //список SQL определений запросов
                    for (int i = 0; i < queries.Rows.Count; i++)    //добавление элементов списка 
                    {
                        queryDefinitions.Add((queries.Rows[i]["TABLE_NAME"].ToString(), queries.Rows[i]["VIEW_DEFINITION"].ToString()));
                    }

                    for (int i = 0; i < queryDefinitions.Count(); i++) //поиск требуемого запроса для вывода его SQL определения
                    {
                        if (dbQueriesComboBox.SelectedItem.ToString() == queryDefinitions[i].Item1)
                        {
                            query = queryDefinitions[i].Item2; //строка SQL определения
                            break;
                        }
                    }

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection);

                    DataSet dataSet = new DataSet(); //создание пустого набора данных
                    dataAdapter.Fill(dataSet); //заполнение созданного набора данных   (dataSet, dbTablesComboBox.Text)
                    dataGridView1.DataSource = dataSet.Tables[0]; //вывод данных ввиде таблицы

                    dbConnection.Close(); //отключение от БД
                }
            }

            dbConnection.Close();

            totalRows.Text = dataGridView1.Rows.Count.ToString(); //вывод кол-ва отображаемых строк


        }


        //Блок обновления лейбла кол-ва строк выводимой таблицы 
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            totalRows.Text = dataGridView1.Rows.Count.ToString(); //вывод кол-ва отображаемых строк
        }
        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            totalRows.Text = dataGridView1.Rows.Count.ToString(); //вывод кол-ва отображаемых строк
        }
        //-------------------------------------------------------------


        //Блок описания макроса "About"
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Это тестовая программа работы с БД Access \nчерез оконное приложение на .NET", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //Блок определения расположения шаблонов ACAD и запись всех шаблонов в ListBox
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //очистка ComboBox
            dwgTmplListBox.Items.Clear();
            dwgTmplFullNamesListBox.Items.Clear();

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {

                int index = lblDbFilePath.Text.LastIndexOf(@"\");   //определяем индекс последнего вхождения @"\"
                folderBrowserDialog.SelectedPath = lblDbFilePath.Text.Substring(0, index);  //начальный репозиторий при открытии 
                                                                                            //путь расположения выбранного Access файла без имени файла

                //условие нажатия "Ок" в файловом диалоге
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    tmplFilePath = folderBrowserDialog.SelectedPath.ToString(); //определение пути репозитория шаблонов
                    //MessageBox.Show("Расположение шаблонов AutoCAD: " + filePath, "Расположение шаблонов определено", MessageBoxButtons.OK); 
                    //отображение окна об успешном определеии пути шаблонов
                    lblTmplFilePath.Text = tmplFilePath; //отображение пути шаблонов

                    DirectoryInfo dirInfo = new DirectoryInfo(tmplFilePath); //объявление класса для описания свойств файлов системы


                    //Условие наличия файлов с форматом .dwg
                    if (dirInfo.GetFiles("*.dwg").Length > 0)
                    {
                        //Цикл записи имен найденных *.dwg файлов в ListBox
                        foreach (FileInfo file in dirInfo.GetFiles("*.dwg"))
                        {
                            dwgTmplListBox.Items.Add(file.Name);  //запись в ListBox имен найденных файлов
                            dwgTmplFullNamesListBox.Items.Add(file.FullName); //запись в ListBox полных имен найденных файлов
                        }
                    }
                    else
                    {
                        MessageBox.Show("По выбранному пути отсутствуют файлы формата .dwg. \nПожалуйста, выберете другой путь", "Не найдены файлы DWG!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        folderBrowserDialog.SelectedPath = lblDbFilePath.Text.Substring(0, index);
                    }
                    
                }
                else
                {
                    return;
                }

                
            }

        }


        //Функция перебора всех ObjIDs в выбранной коллекции
        //public void allObjsCheck(this Transaction trCurrDoc, BlockTableRecord btrCurrSpace, int loopNumber,
        //                                                                                    int rowIndex, int acDocPageNum)
        //{
        //    string objValue; //промежуточная перем-я для записи значений текстовых блоков

        //    //цикл перебора всех объектов в про-ве модели
        //    foreach (ObjectId objId in btrCurrSpace)
        //    {
        //        DBObject obj = trCurrDoc.GetObject(objId, OpenMode.ForWrite); //создание промеж. переменной для записи объектов 

        //        //условие соответствия типов текстовых блоков
        //        switch (objId.ObjectClass.DxfName)
        //        {
        //            case "MTEXT":                      //текст. блок тип "многострочный текст"
        //                var objMText = obj as MText;
        //                objValue = objMText.Text;

        //                //условие наличия "якорных" символов в текст. блоке шаблона .dwg с учетом номера контура
        //                if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
        //                                                                           Regex.Match(objValue, @"\d+").Value))
        //                {
        //                    string colName;

        //                    //цикл перебора столбцов таблицы БД
        //                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
        //                    {
        //                        colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
        //                        int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

        //                        //условие совпадения "якоря" шаблона и имени столбца БД
        //                        if (colName == objValue.Substring(index + 1))
        //                        {
        //                            //условие наличия данных в таблице БД макроса
        //                            if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
        //                            {
        //                                objMText.Contents = "HOLD!";

        //                                if (flagManyFiles && manyFiles.Checked)
        //                                {
        //                                    //создание txt файла отчета
        //                                    FileStream fs = File.Create(
        //                                        Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

        //                                    fs.Dispose();

        //                                    flagManyFiles = false;
        //                                }
        //                                else
        //                                {
        //                                    if (!flagManyFiles)
        //                                    {
        //                                        //добавление в txt файл информации
        //                                        //по расположению отсутствующих данных
        //                                        using (StreamWriter reportFile = new StreamWriter(
        //                                            Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
        //                                        {
        //                                            reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
        //                                                acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
        //                                        }

        //                                    }

        //                                }


        //                            }
        //                            else
        //                            {
        //                                objMText.Contents = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
        //                            }
        //                            break;
        //                        }
        //                    }

        //                }
        //                break;

        //            case "TEXT":                        //текст. блок тип "однострочный текст"
        //                var objDBText = obj as DBText;
        //                objValue = objDBText.TextString;

        //                // условие наличия "якорных" символов в текст.блоке шаблона .dwg с учетом номера контура
        //                if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
        //                                                                           Regex.Match(objValue, @"\d+").Value))
        //                {
        //                    string colName;

        //                    //цикл перебора столбцов таблицы БД
        //                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
        //                    {
        //                        colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
        //                        int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

        //                        //условие совпадения "якоря" шаблона и имени столбца БД
        //                        if (colName == objValue.Substring(index + 1))
        //                        {
        //                            //условие наличия данных в таблице БД макроса
        //                            if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
        //                            {
        //                                objDBText.TextString = "HOLD!";

        //                                if (flagManyFiles && manyFiles.Checked)
        //                                {
        //                                    //создание txt файла отчета
        //                                    FileStream fs = File.Create(
        //                                        Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

        //                                    fs.Dispose();
        //                                    flagManyFiles = false;
        //                                }
        //                                else
        //                                {
        //                                    if (!flagManyFiles)
        //                                    {
        //                                        //добавление в txt файл информации
        //                                        //по расположению отсутствующих данных
        //                                        using (StreamWriter reportFile = new StreamWriter(
        //                                            Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
        //                                        {
        //                                            reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
        //                                                acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
        //                                        }

        //                                    }

        //                                }
        //                            }
        //                            else
        //                            {
        //                                objDBText.TextString = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
        //                            }
        //                            break;
        //                        }
        //                    }

        //                }

        //                break;

        //            default:
        //                continue;

        //        }


        //    }//конец цикла перебора всех объектов в про-ве модели
        //}



        //Функция генерации контуров


        public void LoopGen(Document acDoc, AcDBserv.Database currAcDb, int rowIndex, int loopsQuantity, int loopNumber, int acDocPageNum = 0)
        {
            ////условие определения индекса начальной строки
            //int tempRowIndex;
            //if (rowIndex == 0)
            //{ tempRowIndex = rowIndex; }
            //else { tempRowIndex = rowIndex + 1; }


            using (DocumentLock acDocLock = acDoc.LockDocument()) //блокируем созданный файл .dwg, т.к. CommandFlags имеет свойство Session
                                                                  //(взаимодействие макроса со всеми открытыми документами AutoCAD)
            {
                using (Transaction trCurrDoc = currAcDb.TransactionManager.StartTransaction()) //открытие транзакции обращения к данным .dwg
                {
                    BlockTable acBlkTbl = trCurrDoc.GetObject(currAcDb.BlockTableId, //подключение к таблице данных .dwg
                                                 OpenMode.ForWrite) as BlockTable;

                    BlockTableRecord btrCurrSpace = trCurrDoc.GetObject //подключение к таблице записей блоков .dwg, расположенных в про-ве модели
                           (acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    

                    if (oneFile.Checked)
                    {
                        do
                        {
                            if (rowIndex < loopsQuantity)
                            {
                                foreach (ObjectId objId in btrCurrSpace)
                                {
                                    string objValue;

                                    DBObject obj = trCurrDoc.GetObject(objId, OpenMode.ForWrite); //создание промеж. переменной для записи объектов 

                                    //условие соответствия типов текстовых блоков
                                    switch (objId.ObjectClass.DxfName)
                                    {
                                        case "MTEXT":                      //текст. блок тип "многострочный текст"
                                            var objMText = obj as MText;
                                            objValue = objMText.Text;

                                            //условие наличия "якорных" символов в текст. блоке шаблона .dwg с учетом номера контура
                                            if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
                                                                                                               Regex.Match(objValue, @"\d+").Value))
                                            {
                                                string colName;

                                                //цикл перебора столбцов таблицы БД
                                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                                {
                                                    colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
                                                    int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

                                                    //условие совпадения "якоря" шаблона и имени столбца БД
                                                    if (colName == objValue.Substring(index + 1))
                                                    {
                                                        //условие наличия данных в таблице БД макроса
                                                        if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
                                                        {
                                                            objMText.Contents = "HOLD!";

                                                            if (flagManyFiles && manyFiles.Checked)
                                                            {
                                                                //создание txt файла отчета
                                                                FileStream fs = File.Create(
                                                                    Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

                                                                fs.Dispose();

                                                                flagManyFiles = false;
                                                            }
                                                            else
                                                            {
                                                                if (!flagManyFiles)
                                                                {
                                                                    //добавление в txt файл информации
                                                                    //по расположению отсутствующих данных
                                                                    using (StreamWriter reportFile = new StreamWriter(
                                                                        Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
                                                                    {
                                                                        reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
                                                                            acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                                                                    }

                                                                }

                                                            }


                                                        }
                                                        else
                                                        {
                                                            objMText.Contents = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
                                                        }
                                                        break;
                                                    }
                                                }

                                            }
                                            break;

                                        case "TEXT":                        //текст. блок тип "однострочный текст"
                                            var objDBText = obj as DBText;
                                            objValue = objDBText.TextString;

                                            // условие наличия "якорных" символов в текст.блоке шаблона .dwg с учетом номера контура
                                            if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
                                                                                                               Regex.Match(objValue, @"\d+").Value))
                                            {
                                                string colName;

                                                //цикл перебора столбцов таблицы БД
                                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                                {
                                                    colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
                                                    int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

                                                    //условие совпадения "якоря" шаблона и имени столбца БД
                                                    if (colName == objValue.Substring(index + 1))
                                                    {
                                                        //условие наличия данных в таблице БД макроса
                                                        if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
                                                        {
                                                            objDBText.TextString = "HOLD!";

                                                            if (flagManyFiles && manyFiles.Checked)
                                                            {
                                                                //создание txt файла отчета
                                                                FileStream fs = File.Create(
                                                                    Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

                                                                fs.Dispose();
                                                                flagManyFiles = false;
                                                            }
                                                            else
                                                            {
                                                                if (!flagManyFiles)
                                                                {
                                                                    //добавление в txt файл информации
                                                                    //по расположению отсутствующих данных
                                                                    using (StreamWriter reportFile = new StreamWriter(
                                                                        Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
                                                                    {
                                                                        reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
                                                                            acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                                                                    }

                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            objDBText.TextString = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
                                                        }
                                                        break;
                                                    }
                                                }

                                            }

                                            break;

                                        default:
                                            continue;

                                    }


                                }//конец цикла перебора всех объектов в про-ве модели
                                //allObjsCheck(trCurrDoc, btrCurrSpace, loopNumber, rowIndex, acDocPageNum);
                            }
                            else
                            {
                                //цикл перебора всех объектов в коллекции скопированных объектов
                                foreach (ObjectId objId in clonedObjCol)
                                {
                                    string objValue;

                                    DBObject obj = trCurrDoc.GetObject(objId, OpenMode.ForWrite); //создание промеж. переменной для записи объектов 

                                    //условие соответствия типов текстовых блоков
                                    switch (objId.ObjectClass.DxfName)
                                    {
                                        case "MTEXT":                      //текст. блок тип "многострочный текст"
                                            var objMText = obj as MText;
                                            objValue = objMText.Text;

                                            //условие наличия "якорных" символов в текст. блоке шаблона .dwg с учетом номера контура
                                            if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
                                                                                                       Regex.Match(objValue, @"\d+").Value))
                                            {
                                                string colName;

                                                //цикл перебора столбцов таблицы БД
                                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                                {
                                                    colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
                                                    int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

                                                    //условие совпадения "якоря" шаблона и имени столбца БД
                                                    if (colName == objValue.Substring(index + 1))
                                                    {
                                                        //условие наличия данных в таблице БД макроса
                                                        if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
                                                        {
                                                            objMText.Contents = "HOLD!";

                                                            if (flagManyFiles && manyFiles.Checked)
                                                            {
                                                                //создание txt файла отчета
                                                                FileStream fs = File.Create(
                                                                    Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

                                                                fs.Dispose();

                                                                flagManyFiles = false;
                                                            }
                                                            else
                                                            {
                                                                if (!flagManyFiles)
                                                                {
                                                                    //добавление в txt файл информации
                                                                    //по расположению отсутствующих данных
                                                                    using (StreamWriter reportFile = new StreamWriter(
                                                                        Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
                                                                    {
                                                                        reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
                                                                            acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                                                                    }

                                                                }

                                                            }


                                                        }
                                                        else
                                                        {
                                                            objMText.Contents = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
                                                        }
                                                        break;
                                                    }
                                                }

                                            }
                                            break;

                                        case "TEXT":                        //текст. блок тип "однострочный текст"
                                            var objDBText = obj as DBText;
                                            objValue = objDBText.TextString;

                                            // условие наличия "якорных" символов в текст.блоке шаблона .dwg с учетом номера контура
                                            if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
                                                                                                       Regex.Match(objValue, @"\d+").Value))
                                            {
                                                string colName;

                                                //цикл перебора столбцов таблицы БД
                                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                                {
                                                    colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
                                                    int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

                                                    //условие совпадения "якоря" шаблона и имени столбца БД
                                                    if (colName == objValue.Substring(index + 1))
                                                    {
                                                        //условие наличия данных в таблице БД макроса
                                                        if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
                                                        {
                                                            objDBText.TextString = "HOLD!";

                                                            if (flagManyFiles && manyFiles.Checked)
                                                            {
                                                                //создание txt файла отчета
                                                                FileStream fs = File.Create(
                                                                    Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

                                                                fs.Dispose();
                                                                flagManyFiles = false;
                                                            }
                                                            else
                                                            {
                                                                if (!flagManyFiles)
                                                                {
                                                                    //добавление в txt файл информации
                                                                    //по расположению отсутствующих данных
                                                                    using (StreamWriter reportFile = new StreamWriter(
                                                                        Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
                                                                    {
                                                                        reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
                                                                            acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                                                                    }

                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            objDBText.TextString = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
                                                        }
                                                        break;
                                                    }
                                                }

                                            }

                                            break;

                                        default:
                                            continue;

                                    }


                                }//конец цикла перебора всех объектов в коллекции скопированных объектов
                            }
                        }
                        while (false);

                    }
                    else
                    {
                        //цикл перебора всех объектов в про-ве модели
                        foreach (ObjectId objId in btrCurrSpace)
                        {
                            string objValue;

                            DBObject obj = trCurrDoc.GetObject(objId, OpenMode.ForWrite); //создание промеж. переменной для записи объектов 

                            //условие соответствия типов текстовых блоков
                            switch (objId.ObjectClass.DxfName)
                            {
                                case "MTEXT":                      //текст. блок тип "многострочный текст"
                                    var objMText = obj as MText;
                                    objValue = objMText.Text;

                                    //условие наличия "якорных" символов в текст. блоке шаблона .dwg с учетом номера контура
                                    if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
                                                                                                       Regex.Match(objValue, @"\d+").Value))
                                    {
                                        string colName;

                                        //цикл перебора столбцов таблицы БД
                                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                        {
                                            colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
                                            int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

                                            //условие совпадения "якоря" шаблона и имени столбца БД
                                            if (colName == objValue.Substring(index + 1))
                                            {
                                                //условие наличия данных в таблице БД макроса
                                                if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
                                                {
                                                    objMText.Contents = "HOLD!";

                                                    if (flagManyFiles && manyFiles.Checked)
                                                    {
                                                        //создание txt файла отчета
                                                        FileStream fs = File.Create(
                                                            Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

                                                        fs.Dispose();

                                                        flagManyFiles = false;
                                                    }
                                                    else
                                                    {
                                                        if (!flagManyFiles)
                                                        {
                                                            //добавление в txt файл информации
                                                            //по расположению отсутствующих данных
                                                            using (StreamWriter reportFile = new StreamWriter(
                                                                Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
                                                            {
                                                                reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
                                                                    acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                                                            }

                                                        }

                                                    }


                                                }
                                                else
                                                {
                                                    objMText.Contents = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
                                                }
                                                break;
                                            }
                                        }

                                    }
                                    break;

                                case "TEXT":                        //текст. блок тип "однострочный текст"
                                    var objDBText = obj as DBText;
                                    objValue = objDBText.TextString;

                                    // условие наличия "якорных" символов в текст.блоке шаблона .dwg с учетом номера контура
                                    if (objValue.StartsWith("&" + loopNumber) && loopNumber == Int32.Parse(
                                                                                                       Regex.Match(objValue, @"\d+").Value))
                                    {
                                        string colName;

                                        //цикл перебора столбцов таблицы БД
                                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                        {
                                            colName = dataGridView1.Columns[j].Name; //имя столбца таблицы БД
                                            int index = objValue.IndexOf("!"); //имя поля в шаблоне без "якорных" символов

                                            //условие совпадения "якоря" шаблона и имени столбца БД
                                            if (colName == objValue.Substring(index + 1))
                                            {
                                                //условие наличия данных в таблице БД макроса
                                                if (dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() == "")
                                                {
                                                    objDBText.TextString = "HOLD!";

                                                    if (flagManyFiles && manyFiles.Checked)
                                                    {
                                                        //создание txt файла отчета
                                                        FileStream fs = File.Create(
                                                            Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"));

                                                        fs.Dispose();
                                                        flagManyFiles = false;
                                                    }
                                                    else
                                                    {
                                                        if (!flagManyFiles)
                                                        {
                                                            //добавление в txt файл информации
                                                            //по расположению отсутствующих данных
                                                            using (StreamWriter reportFile = new StreamWriter(
                                                                Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), true))
                                                            {
                                                                reportFile.WriteLine("Поле: " + colName + ". Файл.dwg: " +
                                                                    acLoopDocPath + @"\" + acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                                                            }

                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    objDBText.TextString = dataGridView1.Rows[rowIndex].Cells[j].Value.ToString(); //запись значения из таблицы БД в шаблон .dwg
                                                }
                                                break;
                                            }
                                        }

                                    }

                                    break;

                                default:
                                    continue;

                            }


                        }//конец цикла перебора всех объектов в про-ве модели


                        //allObjsCheck(trCurrDoc, btrCurrSpace, loopNumber, rowIndex, acDocPageNum);
                    }

                    //acDoc.SendStringToExecute("_REGENALL ", true, false, false);
                    acDoc.Editor.Regen();

                    trCurrDoc.Commit();

                }//конец транзакции AutoCAD


            }//конец блокирования файла AutoCAD



        }//конец функции 


        //Функция подсчета кол-ва контуров
        public int CountLoops(AcDBserv.Database currAcDb, Document acDoc = null)
        {
            string objValue;
            int loopsQuantity;
            List <int>listOfLoopNums = new List<int>(); //список номеров контуров

            if (acDoc == null)
            {
                using (Transaction trCurrDoc = currAcDb.TransactionManager.StartTransaction()) //открытие транзакции обращения к данным .dwg
                {
                    BlockTable acBlkTbl = trCurrDoc.GetObject(currAcDb.BlockTableId, //подключение к таблице данных .dwg
                                                 OpenMode.ForWrite) as BlockTable;

                    BlockTableRecord btrCurrSpace = trCurrDoc.GetObject //подключение к таблице записей блоков .dwg, расположенных в про-ве модели
                           (acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    foreach (ObjectId objId in btrCurrSpace)
                    {
                        DBObject obj = trCurrDoc.GetObject(objId, OpenMode.ForWrite); //создание промеж. переменной для записи объектов 

                        //условие соответствия типов текстовых блоков
                        switch (objId.ObjectClass.DxfName)
                        {
                            case "MTEXT":                      //текст. блок тип "многострочный текст"
                                var objMText = obj as MText;
                                objValue = objMText.Text;

                                //условие наличия "якорных" символов в текст. блоке шаблона .dwg с учетом номера контура
                                if (objValue.StartsWith("&"))
                                {
                                    string loopNum = Regex.Match(objValue, @"\d+").Value;
                                    loopsQuantity = Int32.Parse(loopNum);
                                    listOfLoopNums.Add(loopsQuantity);
                                }
                                break;

                            case "TEXT":                        //текст. блок тип "однострочный текст"
                                var objDBText = obj as DBText;
                                objValue = objDBText.TextString;

                                // условие наличия "якорных" символов в текст.блоке шаблона .dwg с учетом номера контура
                                if (objValue.StartsWith("&"))
                                {
                                    string loopNum = Regex.Match(objValue, @"\d+").Value;
                                    loopsQuantity = Int32.Parse(loopNum);
                                    listOfLoopNums.Add(loopsQuantity);
                                }
                                break;

                            default:
                                continue;

                        }


                        //конец цикла перебора объектов ACAD
                    }

                    trCurrDoc.Commit();
                    
                    //конец транзакции 
                }

                return listOfLoopNums.Max();
            }
            else
            {
                using (DocumentLock acDocLock = acDoc.LockDocument()) //блокируем созданный файл .dwg, т.к. CommandFlags имеет свойство Session
                                                                      //(взаимодействие макроса со всеми открытыми документами AutoCAD)
                {
                    using (Transaction trCurrDoc = currAcDb.TransactionManager.StartTransaction()) //открытие транзакции обращения к данным .dwg
                    {
                        BlockTable acBlkTbl = trCurrDoc.GetObject(currAcDb.BlockTableId, //подключение к таблице данных .dwg
                                                     OpenMode.ForWrite) as BlockTable;

                        BlockTableRecord btrCurrSpace = trCurrDoc.GetObject //подключение к таблице записей блоков .dwg, расположенных в про-ве модели
                               (acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        foreach (ObjectId objId in btrCurrSpace)
                        {
                            DBObject obj = trCurrDoc.GetObject(objId, OpenMode.ForWrite); //создание промеж. переменной для записи объектов 

                            //условие соответствия типов текстовых блоков
                            switch (objId.ObjectClass.DxfName)
                            {
                                case "MTEXT":                      //текст. блок тип "многострочный текст"
                                    var objMText = obj as MText;
                                    objValue = objMText.Text;

                                    //условие наличия "якорных" символов в текст. блоке шаблона .dwg с учетом номера контура
                                    if (objValue.StartsWith("&"))
                                    {
                                        string loopNum = Regex.Match(objValue, @"\d+").Value;
                                        loopsQuantity = Int32.Parse(loopNum);
                                        listOfLoopNums.Add(loopsQuantity);
                                    }
                                    break;

                                case "TEXT":                        //текст. блок тип "однострочный текст"
                                    var objDBText = obj as DBText;
                                    objValue = objDBText.TextString;

                                    // условие наличия "якорных" символов в текст.блоке шаблона .dwg с учетом номера контура
                                    if (objValue.StartsWith("&"))
                                    {
                                        string loopNum = Regex.Match(objValue, @"\d+").Value;
                                        loopsQuantity = Int32.Parse(loopNum);
                                        listOfLoopNums.Add(loopsQuantity);
                                    }
                                    break;

                                default:
                                    continue;

                            }


                            //конец цикла перебора объектов ACAD
                        }

                        trCurrDoc.Commit();

                        //конец транзакции 
                    }

                    //конец блокирования .dwg файла
                }

                return listOfLoopNums.Max();
            }

            
        }


        //Функция копирования и перемещения объектов 
        public void CopyObjects(Document acDoc, AcDBserv.Database currAcDb, string selTmplFilePath, int xDiff)
        {
            var extents = new Extents3d(); //переменная хранения координат всех точек контура объекта

            ObjectIdCollection blockIds = new ObjectIdCollection(); //коллекция объектов для копирования
            AcDBserv.Database srcDb = new AcDBserv.Database(false, true); //промежуточная база данных
            srcDb.ReadDwgFile(selTmplFilePath, System.IO.FileShare.Read, true, ""); //запись данных из шаблона в промежуточную БД
            
            List<ObjectId> lisOfObjIds = new List<ObjectId>(); //список objId

            //открытие транзакции БД источника
            using (Transaction trSrcDb = srcDb.TransactionManager.StartTransaction())
            {
                BlockTable srcAcBlkTbl = trSrcDb.GetObject(srcDb.BlockTableId,
                                                            OpenMode.ForRead) as BlockTable;

                BlockTableRecord srcAcBlkTblRec = trSrcDb.GetObject(srcAcBlkTbl[BlockTableRecord.ModelSpace],
                                                                        OpenMode.ForRead) as BlockTableRecord;


                //цикл запись объектов в коллекцию для копирования
                foreach (ObjectId objId in srcAcBlkTblRec)
                {
                    blockIds.Add(objId);
                }

                trSrcDb.Abort(); //закрытие транзакции без сохранения измений БД источника
                
            }//конец транзакции промежуточной БД

            //блокирование текущего документа
            using (DocumentLock acDocLock = acDoc.LockDocument())
            {
                //открытие транзакции БД текущего документа
                using (Transaction trCurrAcDb = currAcDb.TransactionManager.StartTransaction())
                {
                    //ObjectIdCollection clonedObjCol = new ObjectIdCollection(); //коллекция objId клонированных объектов
                    if(clonedObjCol.Count != 0)
                    {
                        clonedObjCol.Clear();
                    }
                    

                    BlockTable currAcDbBlkTbl = trCurrAcDb.GetObject(currAcDb.BlockTableId, 
                                                                     OpenMode.ForWrite) as BlockTable;

                    BlockTableRecord currAcDbBlkTblRec = trCurrAcDb.GetObject(currAcDbBlkTbl[BlockTableRecord.ModelSpace],
                                                                              OpenMode.ForWrite) as BlockTableRecord;

                    IdMapping mapping = new IdMapping(); //переменная маппинг для копирования объектов 
                                                         //Хранит информацию о копированных объектах, например старые и новые objIDs

                    //копирование объектов
                    currAcDb.WblockCloneObjects(blockIds, currAcDbBlkTblRec.ObjectId, mapping, DuplicateRecordCloning.Ignore, false); 

                    //цикл добавления objIDs скопированных блоков в коллекцию
                    foreach (IdPair idPair in mapping)
                    {
                        if (idPair.IsCloned && idPair.IsPrimary)
                        {
                            clonedObjCol.Add(idPair.Value);
                        }
                    }

                    //цикл перемещения каждого скопированного объекта
                    foreach (ObjectId objId in clonedObjCol)
                    {
                        Entity ent = trCurrAcDb.GetObject(objId, OpenMode.ForWrite) as Entity;

                        if (ent != null && ent.Bounds.HasValue)
                        {
                            extents.AddExtents(ent.GeometricExtents);
                            Point3d minExPt = extents.MinPoint;
                            Vector3d vector = minExPt.GetVectorTo(new Point3d(minExPt.X + xDiff, minExPt.Y, minExPt.Z));
                            ent.TransformBy(Matrix3d.Displacement(vector));
                            
                        }

                    }

                    trCurrAcDb.Commit(); //закрытие транзакции с сохранением изменений, внесенных в БД текущего документа

                }//конец транзакции БД текущего документа

                acDoc.Editor.Regen(); //перерисовка пространства чертежа

            }//конец блокирования текущего документа

            blockIds.Dispose(); //разрушение коллекции блоков копирования
            srcDb.Dispose(); //разрушение промежуточной БД
        }//конец функции


        private void startCreateLoops_Click(object sender, EventArgs e)
        {
            //ошибка отсутствия данных в таблице БД
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Отсутствуют данные БД! \nПожалуйста, загрузите данные", "Отсутсвуют данные БД!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string selTmplFilePath = string.Empty;
            string currTmlpName = string.Empty;
            int loopNumber = 1; //номер контура
            int xDiff = 0; //разница приращения по X коорд. для перемещения скопированных объектов
            bool loopTempFlag = false;


            //способ генераци с одним шаблоном за цикл
            if (одинШаблонЗаЦиклToolStripMenuItem.Checked)
            {
                
                //ошибка отсутствия выбора шаблона для генерации
                if (dwgTmplListBox.SelectedItem == null)
                {
                    MessageBox.Show("Не выбран шаблон! \nПожалуйста, выберете шаблон", "Не выбран шаблон",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //полный путь выбранного шаблона
                    selTmplFilePath = lblTmplFilePath.Text + @"\" + dwgTmplListBox.SelectedItem.ToString();
                }

                //Создаем новый .dwg файл 
                DocumentCollection acDocMgr = AcAp.DocumentManager;//создаем объект, хранящий в себе коллекцию всех документов активного окна ACA
                Document acDoc = acDocMgr.Add(selTmplFilePath); //добавляем новый документ
                acDocMgr.MdiActiveDocument = acDoc; //делаем новый файл .dwg активным

                if (oneFile.Checked)
                {
                    //AcDBserv.Database currAcDb = acDocMgr.MdiActiveDocument.Database; //считывание БД активного файла .dwg

                    int loopsQuantity = CountLoops(acDocMgr.MdiActiveDocument.Database,
                        acDocMgr.MdiActiveDocument); //кол-во контуров на чертеже

                    //цикл начала отрисовки всех контуров по строкам таблицы макроса
                    for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
                    {
                        //переход к новому листу с контурами
                        if (loopNumber > loopsQuantity)
                        {
                            xDiff += 420; //приращение изменения по коорд. X

                            //функция копирования объектов чертежа шаблона в текущий файл .dwg
                            CopyObjects(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                selTmplFilePath, xDiff);

                            loopNumber = 1;

                            //подтягивание данных из БД Access на чертеж
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber);

                            loopNumber++;

                        }
                        else //работа с тем же листом документа
                        {
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber);
                            loopNumber++;
                        }


                    }//конец цикла генерации по строкам таблицы макроса

                    MessageBox.Show("Генерация успешно завершена!", "Завершено", MessageBoxButtons.OK);

                }

                if (manyFiles.Checked)
                {
                    int loopsQuantity = CountLoops(acDocMgr.MdiActiveDocument.Database,
                        acDocMgr.MdiActiveDocument); //кол-во контуров на чертеже

                    int acDocPageNum = Convert.ToInt32(pageNumberTxtBox.Text);

                    //цикл начала отрисовки всех контуров по строкам таблицы макроса
                    for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
                    {
                        //переход к новому листу с контурами
                        if (loopNumber > loopsQuantity)
                        {
                            acDocMgr.MdiActiveDocument.CloseAndSave(acLoopDocPath + @"\" +
                              acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                            acDocPageNum++;
                            loopNumber = 1;

                            Document newAcDoc = acDocMgr.Add(selTmplFilePath);
                            acDocMgr.MdiActiveDocument = newAcDoc; //делаем новый файл .dwg активным

                            //подтягивание данных из БД Access на чертеж
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber, acDocPageNum);

                            loopNumber++;

                        }
                        else //работа с тем же листом документа
                        {
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber, acDocPageNum);
                            loopNumber++;
                        }


                    }//конец цикла генерации по строкам таблицы макроса

                    acDocMgr.MdiActiveDocument.CloseAndSave(acLoopDocPath + @"\" +
                              acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");

                }

            }

            //способ генераци с несколькими шаблонами за цикл
            if (несколькоШаблоновЗаЦиклToolStripMenuItem.Checked)
            {
                //цикл поиска столбца "Шаблон контура"
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (dataGridView1.Columns[i].Name == "Шаблон контура")
                    {
                        loopTempFlag = true;
                        break;
                    }
                   
                }

                //ошибка Отсутствие столбца с именами шаблонов контуров
                if (!loopTempFlag)
                {
                    MessageBox.Show("Не найден столбец 'Шаблон контура' с именами шаблонов контуров",
                                                "Отсутствуют данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Шаблон контура"].Value.ToString() == null)
                    {
                        MessageBox.Show("В таблице присутствуют строки без имени шаблона", 
                            "Не хватает данных!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        return;
                    }
                    
                }

                selTmplFilePath = Path.Combine(lblTmplFilePath.Text, 
                    dataGridView1.Rows[0].Cells["Шаблон контура"].Value.ToString());
                currTmlpName = dataGridView1.Rows[0].Cells["Шаблон контура"].Value.ToString();

                //Создаем новый .dwg файл 
                DocumentCollection acDocMgr = AcAp.DocumentManager;//создаем объект, хранящий в себе коллекцию всех документов активного окна ACA
                Document acDoc = acDocMgr.Add(selTmplFilePath); //добавляем новый документ
                acDocMgr.MdiActiveDocument = acDoc; //делаем новый файл .dwg активным

                if (oneFile.Checked)
                {
                    int loopsQuantity = CountLoops(acDocMgr.MdiActiveDocument.Database, 
                        acDocMgr.MdiActiveDocument); //кол-во контуров на чертеже
                    string tempTmplName = currTmlpName;

                    //цикл начала отрисовки всех контуров по строкам таблицы макроса
                    for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
                    {
                        currTmlpName = dataGridView1.Rows[rowIndex].Cells["Шаблон контура"].Value.ToString();
                        selTmplFilePath = Path.Combine(lblTmplFilePath.Text, currTmlpName);

                        //переход к новому листу при всех заполненных контурах
                        //либо при изменении шаблона
                        if (loopNumber > loopsQuantity ||
                            tempTmplName != currTmlpName)
                        {
                            xDiff += 420; //приращение изменения по коорд. X

                            //функция копирования объектов чертежа шаблона в текущий файл .dwg
                            CopyObjects(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                selTmplFilePath, xDiff);

                            loopNumber = 1;

                            AcDBserv.Database tempDb = new AcDBserv.Database(false, true); //создание БД источника копирования
                            tempDb.ReadDwgFile(selTmplFilePath, System.IO.FileShare.Read, false, null); //подключение к БД шаблона
                            loopsQuantity = CountLoops(tempDb); //кол-во контуров на чертеже
                            tempDb.Dispose();

                            //подтягивание данных из БД Access на чертеж
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber);

                            loopNumber++;

                        }
                        else //работа с тем же листом документа
                        {
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber);
                            loopNumber++;
                        }

                        tempTmplName = dataGridView1.Rows[rowIndex].Cells["Шаблон контура"].Value.ToString();

                    }//конец цикла генерации по строкам таблицы макроса

                    MessageBox.Show("Генерация успешно завершена!", "Завершено", MessageBoxButtons.OK);

                }

                if (manyFiles.Checked)
                {
                    int loopsQuantity = CountLoops(acDocMgr.MdiActiveDocument.Database,
                        acDocMgr.MdiActiveDocument); //кол-во контуров на чертеже
                    string tempTmplName = currTmlpName;

                    int acDocPageNum = Convert.ToInt32(pageNumberTxtBox.Text);

                    //цикл начала отрисовки всех контуров по строкам таблицы макроса
                    for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
                    {
                        currTmlpName = dataGridView1.Rows[rowIndex].Cells["Шаблон контура"].Value.ToString();
                        selTmplFilePath = Path.Combine(lblTmplFilePath.Text, currTmlpName);

                        //переход к новому листу с контурами
                        if (loopNumber > loopsQuantity ||
                            tempTmplName != currTmlpName)
                        {
                            acDocMgr.MdiActiveDocument.CloseAndSave(acLoopDocPath + @"\" +
                              acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");
                            acDocPageNum++;
                            loopNumber = 1;

                            Document newAcDoc = acDocMgr.Add(selTmplFilePath);
                            acDocMgr.MdiActiveDocument = newAcDoc; //делаем новый файл .dwg активным

                            loopsQuantity = CountLoops(acDocMgr.MdiActiveDocument.Database,
                                acDocMgr.MdiActiveDocument); //кол-во контуров на чертеже

                            //подтягивание данных из БД Access на чертеж
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber, acDocPageNum);

                            loopNumber++;

                        }
                        else //работа с тем же листом документа
                        {
                            LoopGen(acDocMgr.MdiActiveDocument, acDocMgr.MdiActiveDocument.Database,
                                rowIndex, loopsQuantity, loopNumber, acDocPageNum);
                            loopNumber++;
                        }

                        tempTmplName = dataGridView1.Rows[rowIndex].Cells["Шаблон контура"].Value.ToString();

                    }//конец цикла генерации по строкам таблицы макроса

                    acDocMgr.MdiActiveDocument.CloseAndSave(acLoopDocPath + @"\" +
                              acDocNameTxtBox.Text + "-Лист-" + acDocPageNum + ".dwg");

                }


            }

            if (selTmplFilePath == "")
            { 
                MessageBox.Show("Пожалуйста, выберете способ генерации", "Не выбран способ генерации", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }


            //окно предупреждение о наличии незаполненных полей в схемах контуров
            if (File.Exists(Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt")) && manyFiles.Checked)
            {
                MessageBox.Show("В сгенерированных чертежах присутствуют незаполненные поля. " +
                    "\nИнформация по отсутствующим данным представлена в отчете, расположенному по пути\n" +
                    Path.Combine(acLoopDocPath, "Отчет пустых полей контуров.txt"), "Отсутствуют данные в чертежах!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            clonedObjCol.Dispose(); 

        }//конец обработки события клика "Начать генерацию"


        //Выбор пути расположения генерируемых чертежей
        private void btnSelLoopPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                int index = lblDbFilePath.Text.LastIndexOf(@"\");   //определяем индекс последнего вхождения @"\"
                folderBrowserDialog.SelectedPath = lblDbFilePath.Text.Substring(0, index);  //начальный репозиторий при открытии 
                                                                                            //путь расположения выбранного Access файла без имени файла

                //условие нажатия "Ок" в файловом диалоге
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    acLoopDocPath = folderBrowserDialog.SelectedPath.ToString(); //определение пути репозитория
                                                                                 //множества чертежей контуров
                    txtBoxLoopPath.Text = acLoopDocPath; //определение пути репозитория
                                                        //множества чертежей контуров
                }

            }//конец потока ввода пути генерируемых чертежей

        }


        //Блок обработки исключения одновременного применения запроса и имени таблицы БД
        private void dbQueriesComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dbTablesComboBox.SelectedIndex = -1; //сброс выбора комбобокса с таблицами
        }

        private void dbTablesComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dbQueriesComboBox.SelectedIndex = -1; // сброс выбора комбобокса с запросами
        }


        //Блок обработки поведения элементов окна
        private void LoopGenerator_WinForm1_Load(object sender, EventArgs e)
        {
            if (!manyFiles.Checked)
            {
                acDocNameTxtBox.ReadOnly = true;
                pageNumberTxtBox.ReadOnly = true;
                btnSelLoopPath.Enabled = false;
            }

        }

        private void manyFiles_CheckedChanged(object sender, EventArgs e)
        {
            acDocNameTxtBox.ReadOnly = !acDocNameTxtBox.ReadOnly;
            pageNumberTxtBox.ReadOnly = !pageNumberTxtBox.ReadOnly;
            btnSelLoopPath.Enabled = !btnSelLoopPath.Enabled;
           
        }

        private void одинШаблонЗаЦиклToolStripMenuItem_Click(object sender, EventArgs e)
        {
            несколькоШаблоновЗаЦиклToolStripMenuItem.Checked = false;
            dwgTmplListBox.Enabled = true;
        }

        private void несколькоШаблоновЗаЦиклToolStripMenuItem_Click(object sender, EventArgs e)
        {
            одинШаблонЗаЦиклToolStripMenuItem.Checked = false;
            dwgTmplListBox.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }//конец класса
}


namespace GroupExtents
{
    public static class TransactionExtensions
    {
        public static Extents3d GetExtents(this Transaction tr, List<ObjectId> objIds)
        {
            var ext = new Extents3d();

            foreach (var id in objIds)
            {

                var ent = tr.GetObject(id, OpenMode.ForRead) as Entity;

                if (ent != null && ent.Bounds.HasValue)
                {
                    ext.AddExtents(ent.GeometricExtents);
                }
            }

            return ext;
        }
    }
}


//копирование БД с применением DataSet


//string connectionString = $"Provider=Microsoft.ACE.OLEDB.16.0;Data source={filePath}"; // строка соединения //$"provider =Microsoft.Jet.OLEDB.4.0;
//OleDbConnection dbConnection = new OleDbConnection(connectionString);  //создание соединения


////выполнение запроса к БД
//dbConnection.Open(); //открытие соединения
//string query = "SELECT * FROM DB_vibr"; //строка запроса
//OleDbCommand dbCommand = new OleDbCommand(query, dbConnection); //команда к ДБ
//OleDbDataReader dbReader = dbCommand.ExecuteReader(); //считывание данных

////проверка данных
//if (dbReader.HasRows == false)
//{
//    MessageBox.Show("Данные не найдены!", "Ошибка!");
//}

//var schemaTable = dbReader.GetSchemaTable();
//var namesOfColumns = schemaTable.Columns;
////string array = namesOfColumns.ToString();  ["ColumnName"]

//foreach (DataColumn column in namesOfColumns) 
//{
//    MessageBox.Show(column.ColumnName);
//}


//MessageBox.Show(array);

//добавление столбцов в таблицу формы
// while (dbReader.Read())
// {




//int a =  dbReader.FieldCount; //кол-во столбцов в БД
//MessageBox.Show(a.ToString());



//var b = dbReader.GetValue(2);
//MessageBox.Show(b.ToString());



//dataGridView1.Columns.Add()

//var text = dbReader[3].ToString();
//MessageBox.Show(text);




//}



//запись строк в таблицу формы
//while (dbReader.Read())
//{

// dataGridView1.Rows.Add();
//}




//закрытие соединения с БД
//dbReader.Close(); 

/*
            AcDBserv.Database destDb = AcAp.DocumentManager.MdiActiveDocument.Database; //выбор БД ACAD для заполнения





            AcDBserv.Database sourceDb = new AcDBserv.Database(false, true); //создание БД источника копирования
            sourceDb.ReadDwgFile(selTmplFilePath, System.IO.FileShare.Read, false, null); //подключение к БД шаблона
            dynamic SourceObjectIds = new AcDBserv.ObjectIdCollection();
            AcDBserv.TransactionManager SourceTM = sourceDb.TransactionManager; //вызов менеджера транзакции

            using (AcDBserv.Transaction tr = destDb.TransactionManager.StartTransaction())
            {
                try
                {
                    using (AcDBserv.Transaction extTr = sourceDb.TransactionManager.StartTransaction())
                    {
                        dynamic extBlockTable = (AcDBserv.BlockTable)extTr.GetObject(sourceDb.BlockTableId, AcDBserv.OpenMode.ForRead);
                        dynamic extModelSpace = (AcDBserv.BlockTableRecord)extTr.GetObject(extBlockTable(AcDBserv.BlockTableRecord.ModelSpace), AcDBserv.OpenMode.ForRead);
                        foreach (AcDBserv.ObjectId id in extModelSpace)
                        {
                            SourceObjectIds.Add(id);
                        }
                    }
                    sourceDb.CloseInput(true);
                    dynamic idMapping = new AcDBserv.IdMapping();
                    destDb.WblockCloneObjects(SourceObjectIds, destDb.BlockTableId, idMapping, AcDBserv.DuplicateRecordCloning.Replace, false);
                    tr.Commit();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Исключение:" + ex.Message);
                }
            }
            sourceDb.Dispose();
            */