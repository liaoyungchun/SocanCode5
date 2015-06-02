using System.Text;

namespace Codes
{
    public partial class ModelCode : CodeHelper
    {
        /// <summary>
        /// 生成Model实体层代码(Internal)
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static string GetInternalModelCode(Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());
            Head(code, style);
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "/// <summary>");
            AppendFormatLine(code, 1, "/// 实体类 {0}，此类请勿动，以方便表字段更改时重新生成覆盖", table.Name);
            AppendFormatLine(code, 1, "/// </summary>");
            AppendFormatLine(code, 1, "[Serializable]");
            AppendFormatLine(code, 1, "public partial class {0} : ICloneable", table.Name);
            AppendFormatLine(code, 1, "{");

            #region ----------  空构造函数  ----------
            AppendFormatLine(code, 2, "public {0}()", table.Name);
            AppendFormatLine(code, 2, "{ }");
            #endregion

            code.AppendLine();

            #region ----------  构造函数  ----------
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 构造函数 {0}", table.Name);
            AppendFormatLine(code, 2, "/// </summary>");
            foreach (Model.Field field in table.Fields)
            {
                AppendFormatLine(code, 2, "/// <param name=\"{0}\">{1}</param>", field.CamelName, field.FieldDescn);
            }
            AppendFormat(code, 2, "public {0}(", table.Name);
            foreach (Model.Field field in table.Fields)
            {
                code.Append(string.Format("{0} {1}, ", field.CSharpType, field.CamelName)); //参数列表
            }
            code.Remove(code.Length - 2, 2);
            code.Append(")");
            code.AppendLine();
            AppendFormatLine(code, 2, "{");
            foreach (Model.Field field in table.Fields)
            {
                AppendFormatLine(code, 3, "this.{0} = {1};", field.FieldName, field.CamelName);
            }
            AppendFormatLine(code, 2, "}");
            #endregion

            code.AppendLine();
            AppendFormatLine(code, 2, "#region 实体属性");
            code.AppendLine();

            switch (style.ModelStyle)
            {
                case Model.CodeStyle.ModelStyles.CS2:
                    GetCS2Model(table, code);
                    break;
                case Model.CodeStyle.ModelStyles.CS3:
                case Model.CodeStyle.ModelStyles.MVC2:
                    GetCS3OrMvc2Model(table, code, style);
                    break;
                default:
                    break;
            }

            AppendFormatLine(code, 2, "#endregion");
            code.AppendLine();

            //实现ICloneable接口
            AppendFormatLine(code, 2, "#region ICloneable 成员");
            code.AppendLine();
            AppendFormatLine(code, 2, "public object Clone()");
            AppendFormatLine(code, 2, "{");
            AppendFormatLine(code, 3, "return this.MemberwiseClone();");
            AppendFormatLine(code, 2, "}");
            code.AppendLine();
            AppendFormatLine(code, 2, "#endregion");
            code.AppendLine();

            //重写Equals方法
            AppendFormatLine(code, 2, "public override bool Equals(object obj)");
            AppendFormatLine(code, 2, "{");
            AppendFormatLine(code, 3, "{0}.{1} model = obj as {0}.{1};", style.ModelNameSpace, table.Name);
            AppendFormat(code, 3, "if (model != null");
            foreach (Model.Field field in table.ConditionRows)
            {
                AppendFormat(code, 0, " && model.{0} == this.{0}", field.FieldName);
            }
            AppendFormat(code, 0, ")\r\n");
            AppendFormatLine(code, 4, "return true;");
            code.AppendLine();
            AppendFormatLine(code, 3, "return false;");
            AppendFormatLine(code, 2, "}");
            code.AppendLine();

            //重写GetHashCode方法
            AppendFormatLine(code, 2, "public override int GetHashCode()");
            AppendFormatLine(code, 2, "{");
            AppendFormat(code, 3, "return ");
            foreach (Model.Field field in table.ConditionRows)
            {
                AppendFormat(code, 0, "{0}.GetHashCode() ^ ", field.FieldName);
            }
            code.Remove(code.Length - 3, 3);
            AppendFormat(code, 0, ";\r\n");
            AppendFormatLine(code, 2, "}");

            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");
            return code.ToString();
        }

        private static void GetCS3OrMvc2Model(Model.Table table, StringBuilder code, Model.CodeStyle style)
        {
            foreach (Model.Field model in table.Fields)
            {
                AppendFormatLine(code, 2, "/// <summary>");
                AppendFormatLine(code, 2, "/// {0}", model.FieldDescn);
                AppendFormatLine(code, 2, "/// </summary>");

                if (style.ModelStyle == Model.CodeStyle.ModelStyles.MVC2)
                {
                    int minLength = 0;
                    if (!model.IsIdentifier && !model.IsKeyField && CodeHelper.IsStringDbType(model) && !model.AllowNull)
                    {
                        AppendFormatLine(code, 2, "[Required(ErrorMessage=\"{0}不能为空\")]", model.FieldDescn);
                        minLength = 1;
                    }

                    AppendFormatLine(code, 2, "[DisplayName(\"{0}\")]", model.FieldDescn);
                    if (!model.IsIdentifier && !model.IsKeyField && CodeHelper.IsStringDbType(model))
                        AppendFormatLine(code, 2, "[RegularExpression(@\"[\\w\\W]{{1,{0}}}\", ErrorMessage = \"{1}为{2}－{0}位\")]//此默认生成的正则为允许任意字符，请根据业务逻辑修改", model.FieldLength, model.FieldDescn, minLength);
                }
                AppendFormatLine(code, 2, "public {0} {1} {{ get; set; }}", model.CSharpType, model.FieldName);
                code.AppendLine();
            }
        }

        private static void GetCS2Model(Model.Table table, StringBuilder code)
        {
            foreach (Model.Field field in table.Fields)
            {
                AppendFormatLine(code, 2, "private {0} {1};", field.CSharpType, GetModelField(field));
            }
            code.AppendLine();

            foreach (Model.Field field in table.Fields)
            {
                AppendFormatLine(code, 2, "/// <summary>");
                AppendFormatLine(code, 2, "/// {0}", field.FieldDescn);
                AppendFormatLine(code, 2, "/// </summary>");
                AppendFormatLine(code, 2, "public {0} {1}", field.CSharpType, field.FieldName);
                AppendFormatLine(code, 2, "{");
                AppendFormatLine(code, 3, "set {{ {0} = value; }}", GetModelField(field));
                AppendFormatLine(code, 3, "get {{ return {0}; }}", GetModelField(field));
                AppendFormatLine(code, 2, "}");
                code.AppendLine();
            }
        }

        /// <summary>
        /// 生成Model实体层代码
        /// </summary>
        public static string GetModelCode(Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());
            Head(code, style);
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "/// <summary>");
            AppendFormatLine(code, 1, "/// 实体类 {0}", table.Name);
            AppendFormatLine(code, 1, "/// </summary>");
            AppendFormatLine(code, 1, "public partial class {0}", table.Name);
            AppendFormatLine(code, 1, "{");
            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");
            return code.ToString();
        }

        private static void Head(StringBuilder code, Model.CodeStyle style)
        {
            AppendFormatLine(code, 0, "using System;");
            if (style.ModelStyle == Model.CodeStyle.ModelStyles.MVC2)
            {
                AppendFormatLine(code, 0, "using System.ComponentModel;");
                AppendFormatLine(code, 0, "using System.ComponentModel.DataAnnotations;");
            }
            code.AppendLine();
            AppendFormatLine(code, 0, "namespace {0}", style.ModelNameSpace);
        }
    }
}
