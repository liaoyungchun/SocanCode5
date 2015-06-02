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
        /// ����
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// �ֶ�����
        /// </summary>
        public int FieldNumber
        {
            get { return fieldNumber; }
            set { fieldNumber = value; }
        }

        /// <summary>
        /// �ֶ���
        /// </summary>
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        /// <summary>
        /// �Ƿ��Ǳ�ʶ
        /// </summary>
        public bool IsIdentifier
        {
            get { return isIdentifier; }
            set { isIdentifier = value; }
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsKeyField
        {
            get { return isKeyfield; }
            set { isKeyfield = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
        }

        /// <summary>
        /// c#�е�����
        /// </summary>
        public string CSharpType
        {
            get { return DataType.DataTypes.Find(x => x.DbType == dbType).CSharpType; }
        }

        /// <summary>
        /// ����ת�����ݵ�DBUtility�еķ���
        /// </summary>
        public string DBUtilityConvertMethod
        {
            get { return DataType.DataTypes.Find(x => x.DbType == dbType).DBUtilityConvertMethod; }
        }

        /// <summary>
        /// ����ת�����ݵķ���
        /// </summary>
        public string CSharpConvertMethod
        {
            get { return DataType.DataTypes.Find(x => x.DbType == dbType).CSharpConvertMethod; }
        }

        /// <summary>
        /// SqlServer�е���������
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
        /// Access�е���������
        /// ��ע�⣬��Ϊ��ȡ���Ľṹ�������ֱ�ʾ�ģ�
        /// ����get��ʱ��Ҫ����DbTypeȡ�ã�set��ʱ��Ҫ����DbType��
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
        /// MySql�е���������
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
        /// ռ���ֽ���
        /// </summary>
        public int FieldSize
        {
            get { return fieldSize; }
            set { fieldSize = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public long FieldLength
        {
            get { return fieldLength; }
            set { fieldLength = value; }
        }

        /// <summary>
        /// �Ƿ������
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        /// <summary>
        /// �ֶ�˵��
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
                fieldDescn = Regex.Replace(value, @"\s*[\n]+\s*", ""); //���˵����У�������ǰ��Ŀո�
            }
        }

        #region ��չֻ������
        /// <summary>
        /// ֻ����Camel������ʽ������ĸС��,����nameField
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
        /// ֻ����Pascal������ʽ������ĸ��д�����磺NameField
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

        #region IComparable ��Ա

        public int CompareTo(object obj)
        {
            Field field = obj as Field;
            return this.FieldNumber.CompareTo(field.FieldNumber);
        }

        #endregion
    }
}
