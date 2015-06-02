
using System;
namespace Model
{
    public class CodeLayer
    {
        public enum CodeLayers
        {
            UnKnown,
            InternalModel,
            Model,
            IDAL,
            InternalDAL,
            DAL,
            DALFactory,
            ICacheDependency,
            TableDependency,
            TableCacheDependency,
            DependencyAccess,
            DependencyFacade,
            BL,
            UserControl,
            UserControlDesignerCs,
            UserControlCs
        }

        public CodeLayer(CodeLayers layer)
        {
            _layer = layer;
        }

        private CodeLayers _layer;

        public CodeLayers Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        /// <summary>
        /// 只读：取得生成文件要放入的子文件夹
        /// </summary>
        public string Folder
        {
            get
            {
                switch (_layer)
                {
                    case CodeLayers.UserControl:
                    case CodeLayers.UserControlDesignerCs:
                    case CodeLayers.UserControlCs:
                        return "Web";
                    case CodeLayers.TableDependency:
                    case CodeLayers.TableCacheDependency:
                        return "TableCacheDependency";
                    case CodeLayers.DependencyAccess:
                    case CodeLayers.DependencyFacade:
                        return "CacheDependencyFactory";
                    case CodeLayers.InternalModel:
                        return "Model\\internal";
                    case CodeLayers.Model:
                        return "Model\\editable";
                    case CodeLayers.InternalDAL:
                        return "DAL\\internal";
                    case CodeLayers.DAL:
                        return "DAL\\editable";
                    case CodeLayers.UnKnown:
                    case CodeLayers.IDAL:
                    case CodeLayers.DALFactory:
                    case CodeLayers.BL:
                    case CodeLayers.ICacheDependency:
                    default:
                        return _layer.ToString();
                }
            }
        }

        /// <summary>
        /// 只读：取得生成的的文件名，为"{0}"表示使用表名代替
        /// </summary>
        public string FileName
        {
            get
            {
                switch (_layer)
                {
                    case CodeLayers.DALFactory:
                        return "DataAccess.cs";
                    case CodeLayers.TableDependency:
                    case CodeLayers.DependencyAccess:
                    case CodeLayers.DependencyFacade:
                        return _layer.ToString();
                    case CodeLayers.UserControl:
                        return "{0}Grid.ascx";
                    case CodeLayers.UserControlDesignerCs:
                        return "{0}Grid.ascx.designer.cs";
                    case CodeLayers.UserControlCs:
                        return "{0}Grid.ascx.cs";
                    case CodeLayers.UnKnown:
                    case CodeLayers.Model:
                    case CodeLayers.IDAL:
                    case CodeLayers.DAL:
                    case CodeLayers.BL:
                    case CodeLayers.ICacheDependency:
                    case CodeLayers.TableCacheDependency:
                    default:
                        return "{0}.cs";
                }
            }
        }

        /// <summary>
        /// 只读：取得生成文件的后缀
        /// </summary>
        public string FileExt
        {
            get
            {
                switch (_layer)
                {
                    case CodeLayers.UserControl:
                        return "ascx";
                    default:
                        return "cs";
                }
            }
        }

    }
}
