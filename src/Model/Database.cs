using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace Model
{
    public class Database
    {
        public enum DatabaseType
        {
            Access,
            Sql2000,
            Sql2005,
            MySql
        }

        #region 成员变量

        private string connectionString;
        private DatabaseType type;
        private List<Model.Table> tables = new List<Table>();
        private List<Model.Table> views = new List<Model.Table>();
        private List<string> storeProcedures = new List<string>();

        /// <summary>
        /// 数据库连接参数
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        /// <summary>
        /// 数据库名或文件路径
        /// </summary>
        public string DbName
        {
            get
            {
                if (type == DatabaseType.Access)
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(conn.DataSource);
                            return file.Name.Remove(file.Name.LastIndexOf("."));
                        }
                        catch (Exception)
                        {
                            return conn.DataSource;
                        }
                    }
                }

                if (type == DatabaseType.Sql2000 || type == DatabaseType.Sql2005)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(conn.Database);
                            int start = file.Name.LastIndexOf(".");
                            if (start > 0)
                                return file.Name.Remove(start);

                            return conn.Database;
                        }
                        catch (Exception)
                        {
                            return conn.Database;
                        }
                    }
                }

                if ((type == DatabaseType.MySql))
                {
                    using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                    {
                        try
                        {
                            return conn.Database;
                        }
                        catch (Exception)
                        {
                            return "MySqlDB";
                        }
                    }
                }

                return "UnKnownDB";
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 所有表
        /// </summary>
        public List<Model.Table> Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        /// <summary>
        /// 所有视图名
        /// </summary>
        public List<Model.Table> Views
        {
            get { return views; }
            set { views = value; }
        }

        /// <summary>
        /// 所有存储过程名
        /// </summary>
        public List<string> StoreProcedures
        {
            get { return storeProcedures; }
            set { storeProcedures = value; }
        }

        #endregion

        public override string ToString()
        {
            return DbName;
        }
    }
}
