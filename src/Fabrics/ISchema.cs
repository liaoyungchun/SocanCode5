
namespace Fabrics
{
    interface ISchema
    {
        Model.Database GetSchema(string connectionString, Model.Database.DatabaseType type);
    }
}
