using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace Model
{
    public class DataType
    {
        private static List<DataType> _dataTypes = null;
        public static List<DataType> DataTypes
        {
            get
            {
                if (_dataTypes == null)
                {
                    _dataTypes = new List<DataType>();
                    XmlDocument xml = new XmlDocument();
                    xml.Load(Model.CreateStyle.CURRENT_PATH + "\\DataTypes.xml");
                    XmlNode root = xml.SelectSingleNode("DataTypes");
                    foreach (XmlNode node in root.SelectNodes("DataType"))
                    {
                        DataType type = new DataType();
                        type.DbType = (DbType)Enum.Parse(typeof(DbType), node.Attributes["DbType"].Value, true);
                        type.SqlType = node.Attributes["SqlType"].Value;
                        type.OleDbType = node.Attributes["OleDbType"].Value;
                        type.MySqlType = node.Attributes["MySqlType"].Value;
                        type.CSharpType = node.Attributes["CSharpType"].Value;
                        type.DBUtilityConvertMethod = node.Attributes["DBUtilityConvertMethod"].Value;
                        type.CSharpConvertMethod = node.Attributes["CSharpConvertMethod"].Value;
                        _dataTypes.Add(type);
                    }
                }
                return _dataTypes;
            }
        }

        public DbType DbType { get; set; }
        public string SqlType { get; set; }
        public string OleDbType { get; set; }
        public string MySqlType { get; set; }
        public string CSharpType { get; set; }
        public string DBUtilityConvertMethod { get; set; }
        public string CSharpConvertMethod { get; set; }
    }
}
