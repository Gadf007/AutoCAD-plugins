using Autodesk.AutoCAD.Runtime;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace LoopGen_Lib
{
    public class DrawingGeneratorMain : IExtensionApplication
    {
        
        //Document acDoc = AcAp.DocumentManager.MdiActiveDocument;
        //Database dbCurrent = AcAp.DocumentManager.MdiActiveDocument.Database;

        public void Initialize()
        {
            // MessageBox.Show("Plugin is loaded!");
            AcAp.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Загружен плагин генерации!");


        }  // действие при загрузке плагина
       
        public void Terminate()
        {

        }   // действие при выгрузке плагина
       
        //public void EditMtextContents(string handle, string newValue)
        //{

        //    Handle hn = new Handle(Convert.ToInt64(handle, 16));       // создание переменной с присвоенным Handle значением. Значение конвертируется из string to long int

        //    using (Transaction tr = dbCurrent.TransactionManager.StartTransaction())  // открытие транзакции
        //    {
        //        ObjectId id = dbCurrent.GetObjectId(false, hn, 0);                    // вычленение ID объекта по его Handle
        //        var obj = tr.GetObject(id, OpenMode.ForWrite, false);                 // разрешение на запись значений для выбранного объекта
        //        var mtext = obj as MText;                                             // присвоение типа MText переменной с данными о выбранном объекте
        //        mtext.Contents = newValue;                                            // запись нового содержимого в текстовый блок MText

        //        tr.Commit();                                                          // закрытие транзакции
        //    }
        //}   // функция замены содержимого конкретного MText по Handle на заданное значение
        
        //public ObjectId[] GetAllObjectsInLayer()
        //{

        //    using (var tr = acDoc.Database.TransactionManager.StartTransaction())
        //    {
        //        BlockTable acBlkTbl;
        //        acBlkTbl = tr.GetObject(dbCurrent.BlockTableId,
        //                                     OpenMode.ForRead) as BlockTable;

        //        BlockTableRecord acBlkTblRec;
        //        acBlkTblRec = tr.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
        //                                        OpenMode.ForRead) as BlockTableRecord;

        //        var ed = acDoc.Editor;
        //        var selLayer = new TypedValue[1];                                 // создание массива для составления критериев фильтра
        //        selLayer.SetValue(new TypedValue(8, "LinkForPlugIn"), 0);         // установка критериев фильтра выбора требуемых объектов TypedValue( 0 - тип фильтра, "MTEXT" - тип объекта (см. тип фильтра))
        //        var objsByLayer = ed.SelectAll(new SelectionFilter(selLayer));    // фильтрация, результат в виде массива

        //        ObjectId[] arrayOfFiltredObjects = objsByLayer.Value.GetObjectIds();     // объявление переменной массива - результата фльтрации

        //        tr.Commit();

        //        return arrayOfFiltredObjects;


        //    }
        //}  // функция выбора всех объектов требуемого слоя и создание массива этих объектов
           
        //public Handle GetHandleOfObjectInLayer(ObjectId[] arrayOfFiltredObjects, int i)    
        //{
        //    ObjectId obj = arrayOfFiltredObjects[i]; // объект массива с индексом i
        //    Handle handle = obj.Handle;              // переменная с присвоенным Handle

        //    return handle;

        //}  // функция выгрузки Handle по индексу элемента массива отфильтрованных объектов

        //public string GetStringOfObjectInLayer(ObjectId[] arrayOfFiltredObjects, int i)
        //{
        //    ObjectId obj = arrayOfFiltredObjects[i];            // объект массива с индексом i

        //    using (var tr = acDoc.Database.TransactionManager.StartTransaction())
        //    {
        //        var objRead = obj.GetObject(OpenMode.ForRead);      // преобразование через промежуточную переменную для чтения содержимового аттрибутов объекта ACAD

        //        if (objRead.GetType() == typeof(MText))             // условие проверки на тип объекта (если MText)
        //        {
        //            var objReadMText = objRead as MText;
        //            tr.Commit();
        //            return objReadMText.Text;
        //        }
        //        else
        //        {
        //            if (objRead.GetType() == typeof(DBText))        // условие проверки на тип объекта (если однострочный Text)
        //            {
        //                var objReadDBTExt = objRead as DBText;
        //                tr.Commit();
        //                return objReadDBTExt.TextString;
        //            }
        //            else
        //            {
        //                tr.Commit();
        //                return null;                                // если объект не является текстовым, то возращается NULL
        //            }
        //        }
                
        //    }
                

        //} // функция выгрузки содержимового по индексу элемента массива отфильтрованных объектов 

        //public void SelectAndCopyAllObjects()
        //{
        //    var acDoc = AcAp.DocumentManager.MdiActiveDocument;
        //    acDoc.SendStringToExecute("_ai_selall ", false, false, false);
        //    acDoc.SendStringToExecute("_copy 0,0 420,0  ", false, false, false);
        //} // ПЕРЕДЕЛАТЬ_функция выделения и копирования всех объектов в пространстве модели со смещением +420 по оси x (выполняется через командную строку ACAD)






        //[CommandMethod("Test")]
        //public void Test()
        //{

            
        //    //LoopGenerator_WinForm1 winForm = new LoopGenerator_WinForm1();
            






        //    /*                                                                              // транзакция поиска всех объектов из AutoCAD DataBase с выводом DXF name, ObjectID, Handle
        //                                                                                    // DXF name, ObjectID, Handle
        //    using (Transaction trAdding = dbCurrent.TransactionManager.StartTransaction())  
        //    {
        //        BlockTable acBlkTbl;
        //        acBlkTbl = trAdding.GetObject(dbCurrent.BlockTableId,
        //                                     OpenMode.ForRead) as BlockTable;

        //        BlockTableRecord acBlkTblRec;
        //        acBlkTblRec = trAdding.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
        //                                        OpenMode.ForRead) as BlockTableRecord;

        //        foreach (ObjectId asObjId in acBlkTblRec)
        //        {
        //            acDoc.Editor.WriteMessage("\nDXF name: " + asObjId.ObjectClass.DxfName);
        //            acDoc.Editor.WriteMessage("\nObjectID: " + asObjId.ToString());
        //            acDoc.Editor.WriteMessage("\nHandle: " + asObjId.Handle.ToString());
                                        
        //            /*
        //            if (asObjId.ObjectClass.DxfName == "MTEXT")
        //            {
        //                //var text = (DBText)trAdding.GetObject(asObjId, OpenMode.ForRead);
        //                //result[text.Handle] = text.TextString;
        //                //Autodesk.AutoCAD.DatabaseServices.MText
        //                asObjId.Typed

        //            }
        //            /*
        //            else if (asObjId.ObjectClass.DxfName == "MTEXT")
        //                    {
        //                var mtext = (MText)trAdding.GetObject(asObjId, OpenMode.ForRead);
        //                result[mtext.Handle] = mtext.Contents;
        //            }
                    

        //            acDoc.Editor.WriteMessage("\nHandle: " + asObjId.Handle.ToString());
        //            acDoc.Editor.WriteMessage("\n");
        //        }


        //        trAdding.Commit();
        //    }*/

        //    //EditMtextContents("1DA9", "New value!");  // 7593 == 1DA9  ----  2084776572048 -- функция замены содержимого MText на заданное
        //    //var arrayOfFiltredObjects = GetAllObjectsInLayer();
        //    //string temp = GetStringOfObjectInLayer(arrayOfFiltredObjects, 0);
        //    //MessageBox.Show(temp);
        //    //SelectAndCopyAllObjects();

        //}

        [CommandMethod("loop", CommandFlags.Session)]
        public void Loop()
        {

            System.Windows.Forms.Application.Run(new DrawingGenerator()); //запуск оконной формы


        }


        //[CommandMethod("all")]
        //public void all()
        //{

        //    using (Transaction trAdding = dbCurrent.TransactionManager.StartTransaction())
        //    {
        //        BlockTable acBlkTbl;
        //        acBlkTbl = trAdding.GetObject(dbCurrent.BlockTableId,
        //                                     OpenMode.ForRead) as BlockTable;

        //        BlockTableRecord acBlkTblRec;
        //        acBlkTblRec = trAdding.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
        //                                        OpenMode.ForRead) as BlockTableRecord;

        //        foreach (ObjectId asObjId in acBlkTblRec)
        //        {
        //            acDoc.Editor.WriteMessage("\nDXF name: " + asObjId.ObjectClass.DxfName);
        //            acDoc.Editor.WriteMessage("\nObjectID: " + asObjId.ToString());
        //            acDoc.Editor.WriteMessage("\nHandle: " + asObjId.Handle.ToString());
        //            acDoc.Editor.WriteMessage("\nHandle value: " + asObjId.Handle.Value.ToString());
        //            acDoc.Editor.WriteMessage("\n-----------");
        //        }

        //        trAdding.Commit();
        //    } // транзакция поиска всех объектов из AutoCAD DataBase с выводом DXF name, ObjectID, Handle
        //}
    }
}





/*
         
        List<Handle> listHandle = new List<Handle>();


            for (int i = 0; i < arrayOfFiltredObjects.Length; i++)
            {
                ObjectId obj = arrayOfFiltredObjects[i];
                Handle hn = obj.Handle;

                listHandle.Add(hn);

                var objRead = obj.GetObject(OpenMode.ForRead);

                if (objRead.GetType() == typeof(MText))
                {
                    var objReadMText = objRead as MText;
                    listContent.Add(objReadMText.ToString());
                }
                else
                {
                    if (objRead.GetType() == typeof(DBText))
                    {
                        var objReadDBTExt = objRead as DBText;
                        listContent.Add(objReadDBTExt.TextString.ToString());
                    }
                }
            }

            string content1 = listContent[0];
            Handle handle1 = listHandle[0];

         */

//handle1 

//var bb = arrayOfObjects.LongLength;
//var cc = Convert.ToString(bb);
//MessageBox.Show(cc);



//acDoc.Editor.WriteMessage("\nType: " + A.GetType());

//var objA = a.GetObject(OpenMode.ForRead);
//var mtextA = objA as MText;
//var aa = mtextA.Contents.ToString();

//MessageBox.Show(aa);



//tr.Commit();

//var objA = tr.GetObject(a, OpenMode.ForRead, false);



//ObjectId objMTextLink = tr.GetObject(, OpenMode.ForWrite, false);
/*
foreach (ObjectId MTextObjId in MTextObjs.Value.GetObjectIds())
{
    var current_MTextObj = tr.GetObject(MTextObjId, OpenMode.ForWrite) as MText;
    if (current_MTextObj.Text.Equals(TextYouNeed))
    {
        //return current_MTextObj;
        break;

    }

    // else return(null);

    // or
    // do somehting else 
}*/

//Transaction.Commit(); // if you change something.