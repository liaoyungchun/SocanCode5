
namespace Fabrics
{
    public class Schema
    {
        private ISchema iSchema;

        private string connectionString;
        private Model.Database.DatabaseType type;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public Model.Database.DatabaseType Type
        {
            get { return type; }
            set
            {
                type = value;

                switch (value)
                {
                    case Model.Database.DatabaseType.Access:
                        iSchema = new AccessSchema();
                        break;
                    case Model.Database.DatabaseType.MySql:
                        iSchema = new MySqlSchema();
                        break;
                    case Model.Database.DatabaseType.Sql2000:
                    case Model.Database.DatabaseType.Sql2005:
                    default:
                        iSchema = new SqlSchema();
                        break;
                }
            }
        }

        public Schema()
        {
        }

        public Schema(string connectionString, Model.Database.DatabaseType type)
        {
            this.connectionString = connectionString;
            Type = type;
        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        public Model.Database GetSchema()
        {
            Model.Database db = iSchema.GetSchema(connectionString, type);
            foreach (Model.Table tb in db.Tables)
            {
                foreach (Model.Field fd in tb.Fields)
                {
                    if (fd.FieldLength < 0)
                    {
                        fd.FieldLength = 4000;
                    }
                }
            }
            return db;
        }
    }
}
