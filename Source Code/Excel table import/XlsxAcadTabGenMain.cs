using Autodesk.AutoCAD.Runtime;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace XlsAcadTabGen_Lib
{
    public class XlsxAcadTabGenMain : IExtensionApplication
    {
        public void Initialize()
        {
            AcAp.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Загружен плагин генерации таблицы из Excel!");

        }  // действие при загрузке плагина

        public void Terminate()
        {

        }   // действие при выгрузке плагина


        [CommandMethod("tabgen")]
        public void Tabgen()
        {
            System.Windows.Forms.Application.Run(new XlsAcadTabGenForm()); //запуск оконной формы

        }
    }
}
