using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Table
    {
        private string name;
        private List<Model.Field> fields = new List<Field>();

        /// <summary>
        /// 表的名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 表中的所有列
        /// </summary>
        public List<Model.Field> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        #region 扩展只读属性
        /// <summary>
        /// 取得修改和删除的条件字段（首选标识列，然后是主键）
        /// </summary>
        public List<Model.Field> ConditionRows
        {
            get
            {
                //查看是否有标识，有标识则返回所有标识
                List<Model.Field> conditionRows = new List<Field>();
                foreach (Model.Field model in fields)
                {
                    if (model.IsIdentifier)
                    {
                        conditionRows.Add(model);
                    }
                }
                if (conditionRows.Count > 0)
                    return conditionRows;

                //查看是否有主键，有主键则返回主键
                foreach (Model.Field model in fields)
                {
                    if (model.IsKeyField)
                    {
                        conditionRows.Add(model);
                    }
                }
                if (conditionRows.Count > 0)
                    return conditionRows;

                //既无标识也无主键，则返回第一个列
                if (fields.Count > 0)
                {
                    fields[0].IsKeyField = true;
                    conditionRows.Add(fields[0]);
                }

                return conditionRows;
            }
        }

        /// <summary>
        /// 是否有标识或主键
        /// </summary>
        public bool HasConditonRow
        {
            get { return (ConditionRows != null && ConditionRows.Count > 0); }
        }

        /// <summary>
        /// 取得修改时要修改的字段
        /// </summary>
        public List<Model.Field> UpdateRows
        {
            get
            {
                List<Model.Field> updateRows = new List<Field>();

                //没有标识和主键
                if (ConditionRows.Count == fields.Count)
                    return fields;

                //以标识为条件字段
                if (ConditionRows.Count == 1 && ConditionRows[0].IsIdentifier)
                {
                    foreach (Model.Field model in fields)
                    {
                        if (!model.IsIdentifier)
                            updateRows.Add(model);
                    }
                    return updateRows;
                }

                //以主键为条件字段
                foreach (Model.Field model in fields)
                {
                    if (!model.IsIdentifier && !ConditionRows.Contains(model))
                        updateRows.Add(model);
                }
                return updateRows;
            }
        }

        /// <summary>
        /// 只读：Camel命名形式。首字母小字,例：nameField
        /// </summary>
        public string CamelName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Name[0].ToString().ToLower());
                sb.Append(Name.Remove(0, 1));
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
                sb.Append(Name[0].ToString().ToUpper());
                sb.Append(Name.Remove(0, 1));
                return sb.ToString();
            }
        } 
        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}
