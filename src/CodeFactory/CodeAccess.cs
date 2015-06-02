using System.Collections.Generic;
using CodeUtility;

namespace CodeFactory
{
    public class CodeAccess
    {
        public static void CreateUserControl(Model.Database db, List<Model.Table> selTables, Model.CodeStyle style, string path)
        {
            System.IO.Directory.CreateDirectory(path);
            foreach (Model.Table table in selTables)
            {
                string filePath = path + "\\" + style.AfterNamespace + table.Name + "Grid.ascx";
                FileStream.WriteFile(filePath, Codes.UserControlCode.GetUserControlCode(table, style));

                string filePath1 = path + "\\" + style.AfterNamespace + table.Name + "Grid.ascx.cs";
                FileStream.WriteFile(filePath1, Codes.UserControlCode.GetWebUserControlCsCode(table, style));

                string filePath2 = path + "\\" + style.AfterNamespace + table.Name + "Grid.ascx.designer.cs";
                FileStream.WriteFile(filePath2, Codes.UserControlCode.GetWebUserControlDesignerCsCode(table, style));
            }
        }

        public static void CreateDALFactoryFile(List<Model.Table> tables, Model.CodeStyle style, string path)
        {
            string filePath = path + "\\DataAccess.cs";
            string fileCode = Codes.DALFactoryCode.GetDALFactoryCode(tables, style);
            FileStream.WriteFile(filePath, fileCode);
        }

        public static void CreateDALFile(Model.Database db, List<Model.Table> selTables, Model.CodeStyle style, string path)
        {
            FileStream.WriteFile(path + "\\" + style.AfterNamespaceDot + "DALHelper.cs", Codes.DALCode.GetDALHelperCode(db, style));

            foreach (Model.Table table in selTables)
            {
                string editablePath = path + "\\editable\\";
                if (!System.IO.Directory.Exists(editablePath))
                    System.IO.Directory.CreateDirectory(editablePath);
                string filePath = editablePath + "\\" + style.AfterNamespaceDot + table.Name + ".cs";
                FileStream.WriteFile(filePath, Codes.DALCode.GetDALCode(db, table, style));
            }

            foreach (Model.Table table in selTables)
            {
                string internalPath = path + "\\internal\\";
                if (!System.IO.Directory.Exists(internalPath))
                    System.IO.Directory.CreateDirectory(internalPath);
                string filePath = internalPath + style.AfterNamespaceDot + table.Name + ".cs";
                FileStream.WriteFile(filePath, Codes.DALCode.GetInternalDALCode(db, table, style));
            }

        }

        public static void CreateIDALFile(Model.Database db, List<Model.Table> selTables, Model.CodeStyle style, string path)
        {
            foreach (Model.Table table in selTables)
            {
                string filePath = path + "\\" + style.AfterNamespaceDot + "I" + table.Name + ".cs";
                FileStream.WriteFile(filePath, Codes.IDALCode.GetIDALCode(table, style));
            }
        }

        public static void CreateModelFile(Model.Database db, List<Model.Table> selTables, Model.CodeStyle style, string path)
        {
            foreach (Model.Table table in selTables)
            {
                string editablePath = path + "\\editable\\";
                if (!System.IO.Directory.Exists(editablePath))
                    System.IO.Directory.CreateDirectory(editablePath);
                string filePath = editablePath + "\\" + style.AfterNamespaceDot + table.Name + ".cs";
                FileStream.WriteFile(filePath, Codes.ModelCode.GetModelCode(table, style));
            }

            foreach (Model.Table table in selTables)
            {
                string internalPath = path + "\\internal\\";
                if (!System.IO.Directory.Exists(internalPath))
                    System.IO.Directory.CreateDirectory(internalPath);
                string filePath = internalPath + style.AfterNamespaceDot + table.Name + ".cs";
                FileStream.WriteFile(filePath, Codes.ModelCode.GetInternalModelCode(table, style));
            }
        }

        public static void CreateBLFile(Model.Database db, List<Model.Table> selTables, Model.CodeStyle style, string path)
        {
            foreach (Model.Table table in selTables)
            {
                string filePath;
                List<Model.Field> l = table.Fields;
                switch (style.BlFrame)
                {
                    case Model.CodeStyle.BLFrame.BLS:
                        filePath = path + "\\" + style.AfterNamespaceDot + table.Name + ".asmx";
                        FileStream.WriteFile(filePath, Codes.BLCode.GetBLCode(db, table, style));

                        filePath = path + "\\" + style.AfterNamespaceDot + table.Name + ".asmx.cs";
                        FileStream.WriteFile(filePath, Codes.BLCode.GetBLCSCode(db, table, style));
                        break;
                    case Model.CodeStyle.BLFrame.BLL:
                    default:
                        filePath = path + "\\" + style.AfterNamespaceDot + table.Name + ".cs";
                        FileStream.WriteFile(filePath, Codes.BLCode.GetBLCSCode(db, table, style));
                        break;
                }
                FileStream.WriteFile(filePath, Codes.BLCode.GetBLCSCode(db, table, style));
            }

            FileStream.WriteFile(path + "\\BLHelper.cs", Codes.BLCode.GetBLHelperCode(style));
        }

        public static void CreateICacheDependencyFile(Model.CodeStyle style, string path)
        {
            FileStream.WriteFile(path + "\\ICacheDependency.cs", Codes.ICacheDependencyCode.GetICacheDependencyCode(style));
        }

        public static void CreateTableCacheDependencyFile(Model.Database db, List<Model.Table> tables, Model.CodeStyle style, string path)
        {
            FileStream.WriteFile(path + "\\TableDependency.cs", Codes.TableCacheDependencyCode.GetTableDependencyCode(db, style));
            foreach (Model.Table table in tables)
            {
                FileStream.WriteFile(path + "\\" + style.AfterNamespaceDot + table.Name + ".cs", Codes.TableCacheDependencyCode.GetTableCacheDependencyCode(db, table, style));
            }
        }

        public static void CreateCacheDependencyFactoryFile(List<Model.Table> tables, Model.CodeStyle style, string path)
        {
            FileStream.WriteFile(path + "\\DependencyAccess.cs", Codes.CacheDependencyFactoryCode.GetDependencyAccessCode(tables, style));
            FileStream.WriteFile(path + "\\DependencyFacade.cs", Codes.CacheDependencyFactoryCode.GetDependencyFacadeCode(tables, style));
        }
    }
}
