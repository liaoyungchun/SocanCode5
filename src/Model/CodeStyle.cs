using System.Data;

namespace Model
{
    public class CodeStyle
    {
        #region 声明枚举值
        public enum SlnFrames
        {
            /// <summary>
            /// 简单三层结构
            /// </summary>
            Simple,
            /// <summary>
            /// 工厂模式三层结构
            /// </summary>
            Factory
        }
        public enum CacheFrames
        {
            /// <summary>
            /// 不使用缓存
            /// </summary>
            None,
            /// <summary>
            /// 缓存对象
            /// </summary>
            ObjectCache,
            /// <summary>
            /// 聚合缓存依赖
            /// </summary>
            AggregateDependency,
            /// <summary>
            /// 缓存对象+聚合缓存依赖
            /// </summary>
            ObjectCacheAndAggregateDependency
        }
        public enum DALFrames
        {
            /// <summary>
            /// 通用DAL
            /// </summary>
            DAL,
            /// <summary>
            /// 针对Access的DAL
            /// </summary>
            AccessDAL,
            /// <summary>
            /// 针对SqlServer的DAL
            /// </summary>
            SqlServerDAL,
            /// <summary>
            /// 针对MySql的DAL
            /// </summary>
            MySqlDAL,
            /// <summary>
            /// 针对Oracle的DAL
            /// </summary>
            OracleDAL,
            /// <summary>
            /// 针对SQLite的DAL
            /// </summary>
            SQLiteDAL
        }
        public enum BLFrame
        {
            /// <summary>
            /// Library
            /// </summary>
            BLL,
            /// <summary>
            /// WebService
            /// </summary>
            BLS
        }
        /// <summary>
        /// Model层样式
        /// </summary>
        public enum ModelStyles
        {
            /// <summary>
            /// c#2.0语法,即每个属性都生成一个局部变量
            /// </summary>
            CS2,
            /// <summary>
            /// c#3.0语法,每个属性生成{get;set;}
            /// </summary>
            CS3,
            /// <summary>
            /// 针对MVC2的验证,对每个属性生成一个验证属性
            /// </summary>
            MVC2
        }
        /// <summary>
        /// 分页方式
        /// </summary>
        public enum PageStyles
        {
            /// <summary>
            /// SQL语句分页
            /// </summary>
            Sql,
            /// <summary>
            /// DataReader分页
            /// </summary>
            DataReader
        }
        #endregion

        #region 成员变量
        private string beforeNamespace;
        private string afterNamespace;
        private string dbHelperName;
        private SlnFrames slnFrame;
        private CacheFrames cacheFrame;
        private CommandType cmdType;
        private DALFrames dalFrame;
        private ModelStyles modelStyle;
        private bool filterFieldOnDALAdd = true;
        private PageStyles pageStyle = PageStyles.DataReader;
        private BLFrame blFrame;

        /// <summary>
        /// 三层结构类型
        /// </summary>
        public SlnFrames SlnFrame
        {
            get { return slnFrame; }
            set { slnFrame = value; }
        }

        /// <summary>
        /// 命名空间前缀
        /// </summary>
        public string BeforeNamespace
        {
            get { return beforeNamespace; }
            set { beforeNamespace = value; }
        }

        /// <summary>
        /// 命名空间后缀
        /// </summary>
        public string AfterNamespace
        {
            get { return afterNamespace; }
            set { afterNamespace = value; }
        }

        /// <summary>
        /// Model层样式
        /// </summary>
        public ModelStyles ModelStyle
        {
            get { return modelStyle; }
            set { modelStyle = value; }
        }

        /// <summary>
        /// DBHelperName的名称，如：NorthWindHelper
        /// </summary>
        public string DBHelperName
        {
            get { return dbHelperName; }
            set { dbHelperName = value; }
        }

        /// <summary>
        /// 缓存结构
        /// </summary>
        public CacheFrames CacheFrame
        {
            get { return cacheFrame; }
            set { cacheFrame = value; }
        }

        /// <summary>
        /// DAL层的CommandType（CommandType.Text、CommandType.StoredProcedure）
        /// </summary>
        public CommandType CmdType
        {
            get { return cmdType; }
            set { cmdType = value; }
        }

        /// <summary>
        /// DAL层的代码样式，如DAL,AccessDAL,SqlServerDAL
        /// </summary>
        public DALFrames DALFrame
        {
            get { return dalFrame; }
            set { dalFrame = value; }
        }

        /// <summary>
        /// dal层的add方法是否过滤标识和有默认值的字段
        /// </summary>
        public bool FilterFieldOnDALAdd
        {
            get { return filterFieldOnDALAdd; }
            set { filterFieldOnDALAdd = value; }
        }

        /// <summary>
        /// 分页方式
        /// </summary>
        public PageStyles PageStyle
        {
            get { return pageStyle; }
            set { pageStyle = value; }
        }

        /// <summary>
        /// 业务逻辑层样式（类库、WebService）
        /// </summary>
        public BLFrame BlFrame
        {
            get { return blFrame; }
            set { blFrame = value; }
        }
        #endregion

        #region 附加只读属性
        /// <summary>
        /// Model 层的命名空间
        /// </summary>
        public string ModelNameSpace
        {
            get
            {
                return TrimDot(BeforeNamespace + ".Model." + AfterNamespace);
            }
        }

        /// <summary>
        /// IDAL 层的命名空间
        /// </summary>
        public string IDALNameSpace
        {
            get
            {
                return TrimDot(BeforeNamespace + ".IDAL." + AfterNamespace);
            }
        }

        /// <summary>
        /// DAL 层的命名空间
        /// </summary>
        public string DALNameSpace
        {
            get
            {
                switch (dalFrame)
                {
                    case DALFrames.AccessDAL:
                        return TrimDot(beforeNamespace + ".AccessDAL." + afterNamespace);
                    case DALFrames.SqlServerDAL:
                        return TrimDot(beforeNamespace + ".SqlServerDAL." + afterNamespace);
                    case DALFrames.MySqlDAL:
                        return TrimDot(beforeNamespace + ".MySqlDAL." + afterNamespace);
                    case DALFrames.OracleDAL:
                        return TrimDot(beforeNamespace + ".OracleDAL." + afterNamespace);
                    case DALFrames.SQLiteDAL:
                        return TrimDot(beforeNamespace + ".SQLiteDAL." + afterNamespace);
                    default:
                    case DALFrames.DAL:
                        return TrimDot(beforeNamespace + ".DAL." + afterNamespace);
                }
            }
        }

        /// <summary>
        /// BL 层的命名空间，如：BeforeNamespace.BLL.Afternamespace
        /// </summary>
        public string BLNameSpace
        {
            get
            {
                switch (blFrame)
                {
                    case BLFrame.BLS:
                        return TrimDot(beforeNamespace + ".BLS." + afterNamespace);
                    case BLFrame.BLL:
                    default:
                        return TrimDot(beforeNamespace + ".BLL." + afterNamespace);
                }
            }
        }

        /// <summary>
        /// ICacheDependency 层的命名空间
        /// </summary>
        public string ICacheDependencyNameSpace
        {
            get
            {
                return TrimDot(beforeNamespace + ".ICacheDependency");
            }
        }

        /// <summary>
        /// CacheDependencyFactory 层的命名空间
        /// </summary>
        public string CacheDependencyFactoryNameSpace
        {
            get
            {
                return TrimDot(beforeNamespace + ".CacheDependencyFactory." + afterNamespace);
            }
        }

        /// <summary>
        /// TableCacheDependency 层的命名空间
        /// </summary>
        public string TableCacheDependencyNamespace
        {
            get
            {
                return TrimDot(beforeNamespace + ".TableCacheDependency." + afterNamespace);
            }
        }

        /// <summary>
        /// 命名空间后缀加点，例如“Basic.”
        /// </summary>
        public string AfterNamespaceDot
        {
            get
            {
                if (afterNamespace != string.Empty)
                    return afterNamespace + ".";
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 命名空间后缀加下划线，例如“Basic_”
        /// </summary>
        public string AfterNamespaceLine
        {
            get
            {
                if (afterNamespace != string.Empty)
                    return afterNamespace + "_";
                else
                    return string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// 去掉左右两边的点
        /// </summary>
        private string TrimDot(string str)
        {
            if (str.StartsWith("."))
                str = str.Remove(0, 1);
            if (str.EndsWith("."))
                str = str.Remove(str.Length - 1, 1);
            return str;
        }
    }
}
