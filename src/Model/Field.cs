using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Model
{
    public class Field : IComparable
    {
        private string tableName;
        private bool isIdentifier;
        private bool isKeyfield;
        private DbType dbType;
        private int fieldSize;
        private long fieldLength;
        private bool allowNull;
        private string fieldDescn;
        private int fieldNumber;
        private string fieldName;
        private string defaultValue;
        private string sqlTypeString;
        private string mySqlTypeString;

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// 字段序列
        /// </summary>
        public int FieldNumber
        {
            get { return fieldNumber; }
            set { fieldNumber = value; }
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        /// <summary>
        /// 是否是标识
        /// </summary>
        public bool IsIdentifier
        {
            get { return isIdentifier; }
            set { isIdentifier = value; }
        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsKeyField
        {
            get { return isKeyfield; }
            set { isKeyfield = value; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
        }

        /// <summary>
        /// c#中的类型
        /// </summary>
        public string CSharpType
        {
            get { return DataType.DataTypes.Find(x => x.DbType == dbType).CSharpType; }
        }

        /// <summary>
        /// 用来转化数据的DBUtility中的方法
        /// </summary>
        public string DBUtilityConvertMethod
        {
            get { return DataType.DataTypes.Find(x => x.DbType == dbType).DBUtilityConvertMethod; }
        }

        /// <summary>
        /// 用来转化数据的方法
        /// </summary>
        public string CSharpConvertMethod
        {
            get { return DataType.DataTypes.Find(x => x.DbType == dbType).CSharpConvertMethod; }
        }

        /// <summary>
        /// SqlServer中的数据类型
        /// </summary>
        public string SqlTypeString
        {
            get
            {
                return sqlTypeString;
            }
            set
            {
                sqlTypeString = value;
                DataType dataType = DataType.DataTypes.Find(x =>
                {
                    string[] sqlTypes = x.SqlType.Split(',');
                    foreach (string item in sqlTypes)
                    {
                        if (item.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                    }
                    return false;
                });

                if (dataType != null)
                {
                    dbType = dataType.DbType;
                }
                else
                {
                    dbType = DataType.DataTypes.Find(x => x.SqlType.Equals("*")).DbType;
                }
            }
        }

        /// <summary>
        /// Access中的数据类型
        /// （注意，因为获取到的结构中是数字表示的，
        /// 所以get的时候要根据DbType取得，set的时候要设置DbType）
        /// </summary>
        public string OleDbTypeString
        {
            set
            {
                DataType dataType = DataType.DataTypes.Find(x =>
                {
                    string[] sqlTypes = x.OleDbType.Split(',');
                    foreach (string item in sqlTypes)
                    {
                        if (item.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                    }
                    return false;
                });

                if (dataType != null)
                {
                    dbType = dataType.DbType;
                }
                else
                {
                    dbType = DataType.DataTypes.Find(x => x.OleDbType.Equals("*")).DbType;
                }
            }
        }

        /// <summary>
        /// MySql中的数据类型
        /// </summary>
        public string MySqlTypeString
        {
            get
            {
                return mySqlTypeString;
            }
            set
            {
                mySqlTypeString = value;
                DataType dataType = DataType.DataTypes.Find(x =>
                {
                    string[] sqlTypes = x.MySqlType.Split(',');
                    foreach (string item in sqlTypes)
                    {
                        if (item.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                    }
                    return false;
                });

                if (dataType != null)
                {
                    dbType = dataType.DbType;
                }
                else
                {
                    dbType = DataType.DataTypes.Find(x => x.MySqlType.Equals("*")).DbType;
                }
            }
        }

        /// <summary>
        /// 占用字节数
        /// </summary>
        public int FieldSize
        {
            get { return fieldSize; }
            set { fieldSize = value; }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public long FieldLength
        {
            get { return fieldLength; }
            set { fieldLength = value; }
        }

        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        /// <summary>
        /// 字段说明
        /// </summary>
        public string FieldDescn
        {
            get
            {
                if (!string.IsNullOrEmpty(fieldDescn))
                {
                    return fieldDescn;
                }
                else
                {
                    return FieldName;
                }
            }
            set
            {
                fieldDescn = Regex.Replace(value, @"\s*[\n]+\s*", ""); //过滤掉换行，及换行前后的空格
            }
        }

        #region 扩展只读属性
        /// <summary>
        /// 只读：Camel命名形式。首字母小字,例：nameField
        /// </summary>
        public string CamelName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(fieldName[0].ToString().ToLower());
                sb.Append(fieldName.Remove(0, 1));
                return sb.ToString();
            }
        }

        /// <summary>
        /// 只读：Pascal命名形式。首字母大写，例如：NameField
        /// </summary>
        public string PascalName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(fieldName[0].ToString().ToUpper());
                sb.Append(fieldName.Remove(0, 1));
                return sb.ToString();
            }
        }
        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            Field field = obj as Field;
            return this.FieldNumber.CompareTo(field.FieldNumber);
        }

        #endregion
    }
}
