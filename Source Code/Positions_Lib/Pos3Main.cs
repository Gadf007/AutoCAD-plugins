using Autodesk.AutoCAD.Runtime;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace Positions_Lib
{
    public class Pos3Main : IExtensionApplication
    {
        public void Initialize()
        {
            AcAp.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Загружен плагин обновления позиций!");

        }  // действие при загрузке плагина

        public void Terminate()
        {

        }   // действие при выгрузке плагина


        [CommandMethod("pos")]
        public void pos()
        {
            System.Windows.Forms.Application.Run(new Pos3Form()); //запуск оконной формы

        }


        //[CommandMethod("all")]
        //public void all()
        //{
        //    var acDoc = AcAp.DocumentManager.MdiActiveDocument;
        //    Database dbCurrent = AcAp.DocumentManager.MdiActiveDocument.Database;

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
