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

        #region ��Ա����

        private string connectionString;
        private DatabaseType type;
        private List<Model.Table> tables = new List<Table>();
        private List<Model.Table> views = new List<Model.Table>();
        private List<string> storeProcedures = new List<string>();

        /// <summary>
        /// ���ݿ����Ӳ���
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        /// <summary>
        /// ���ݿ������ļ�·��
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
        /// ���ݿ�����
        /// </summary>
        public DatabaseType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// ���б�
        /// </summary>
        public List<Model.Table> Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        /// <summary>
        /// ������ͼ��
        /// </summary>
        public List<Model.Table> Views
        {
            get { return views; }
            set { views = value; }
        }

        /// <summary>
        /// ���д洢������
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
