
namespace Codes
{
    public class TableCacheDependencyCode : CodeHelper
    {
        public static string GetTableCacheDependencyCode(Model.Database db, Model.Table table, Model.CodeStyle style)
        {
            return ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\TableCacheDependency\\TableCacheDependency.template", db, table, style);
        }

        public static string GetTableDependencyCode(Model.Database db, Model.CodeStyle style)
        {
            return ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\TableCacheDependency\\TableDependency.template", db, null, style);
        }
    }
}
