using System.Data;

namespace Model
{
    public class CodeStyle
    {
        #region ����ö��ֵ
        public enum SlnFrames
        {
            /// <summary>
            /// ������ṹ
            /// </summary>
            Simple,
            /// <summary>
            /// ����ģʽ����ṹ
            /// </summary>
            Factory
        }
        public enum CacheFrames
        {
            /// <summary>
            /// ��ʹ�û���
            /// </summary>
            None,
            /// <summary>
            /// �������
            /// </summary>
            ObjectCache,
            /// <summary>
            /// �ۺϻ�������
            /// </summary>
            AggregateDependency,
            /// <summary>
            /// �������+�ۺϻ�������
            /// </summary>
            ObjectCacheAndAggregateDependency
        }
        public enum DALFrames
        {
            /// <summary>
            /// ͨ��DAL
            /// </summary>
            DAL,
            /// <summary>
            /// ���Access��DAL
            /// </summary>
            AccessDAL,
            /// <summary>
            /// ���SqlServer��DAL
            /// </summary>
            SqlServerDAL,
            /// <summary>
            /// ���MySql��DAL
            /// </summary>
            MySqlDAL,
            /// <summary>
            /// ���Oracle��DAL
            /// </summary>
            OracleDAL,
            /// <summary>
            /// ���SQLite��DAL
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
        /// Model����ʽ
        /// </summary>
        public enum ModelStyles
        {
            /// <summary>
            /// c#2.0�﷨,��ÿ�����Զ�����һ���ֲ�����
            /// </summary>
            CS2,
            /// <summary>
            /// c#3.0�﷨,ÿ����������{get;set;}
            /// </summary>
            CS3,
            /// <summary>
            /// ���MVC2����֤,��ÿ����������һ����֤����
            /// </summary>
            MVC2
        }
        /// <summary>
        /// ��ҳ��ʽ
        /// </summary>
        public enum PageStyles
        {
            /// <summary>
            /// SQL����ҳ
            /// </summary>
            Sql,
            /// <summary>
            /// DataReader��ҳ
            /// </summary>
            DataReader
        }
        #endregion

        #region ��Ա����
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
        /// ����ṹ����
        /// </summary>
        public SlnFrames SlnFrame
        {
            get { return slnFrame; }
            set { slnFrame = value; }
        }

        /// <summary>
        /// �����ռ�ǰ׺
        /// </summary>
        public string BeforeNamespace
        {
            get { return beforeNamespace; }
            set { beforeNamespace = value; }
        }

        /// <summary>
        /// �����ռ��׺
        /// </summary>
        public string AfterNamespace
        {
            get { return afterNamespace; }
            set { afterNamespace = value; }
        }

        /// <summary>
        /// Model����ʽ
        /// </summary>
        public ModelStyles ModelStyle
        {
            get { return modelStyle; }
            set { modelStyle = value; }
        }

        /// <summary>
        /// DBHelperName�����ƣ��磺NorthWindHelper
        /// </summary>
        public string DBHelperName
        {
            get { return dbHelperName; }
            set { dbHelperName = value; }
        }

        /// <summary>
        /// ����ṹ
        /// </summary>
        public CacheFrames CacheFrame
        {
            get { return cacheFrame; }
            set { cacheFrame = value; }
        }

        /// <summary>
        /// DAL���CommandType��CommandType.Text��CommandType.StoredProcedure��
        /// </summary>
        public CommandType CmdType
        {
            get { return cmdType; }
            set { cmdType = value; }
        }

        /// <summary>
        /// DAL��Ĵ�����ʽ����DAL,AccessDAL,SqlServerDAL
        /// </summary>
        public DALFrames DALFrame
        {
            get { return dalFrame; }
            set { dalFrame = value; }
        }

        /// <summary>
        /// dal���add�����Ƿ���˱�ʶ����Ĭ��ֵ���ֶ�
        /// </summary>
        public bool FilterFieldOnDALAdd
        {
            get { return filterFieldOnDALAdd; }
            set { filterFieldOnDALAdd = value; }
        }

        /// <summary>
        /// ��ҳ��ʽ
        /// </summary>
        public PageStyles PageStyle
        {
            get { return pageStyle; }
            set { pageStyle = value; }
        }

        /// <summary>
        /// ҵ���߼�����ʽ����⡢WebService��
        /// </summary>
        public BLFrame BlFrame
        {
            get { return blFrame; }
            set { blFrame = value; }
        }
        #endregion

        #region ����ֻ������
        /// <summary>
        /// Model ��������ռ�
        /// </summary>
        public string ModelNameSpace
        {
            get
            {
                return TrimDot(BeforeNamespace + ".Model." + AfterNamespace);
            }
        }

        /// <summary>
        /// IDAL ��������ռ�
        /// </summary>
        public string IDALNameSpace
        {
            get
            {
                return TrimDot(BeforeNamespace + ".IDAL." + AfterNamespace);
            }
        }

        /// <summary>
        /// DAL ��������ռ�
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
        /// BL ��������ռ䣬�磺BeforeNamespace.BLL.Afternamespace
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
        /// ICacheDependency ��������ռ�
        /// </summary>
        public string ICacheDependencyNameSpace
        {
            get
            {
                return TrimDot(beforeNamespace + ".ICacheDependency");
            }
        }

        /// <summary>
        /// CacheDependencyFactory ��������ռ�
        /// </summary>
        public string CacheDependencyFactoryNameSpace
        {
            get
            {
                return TrimDot(beforeNamespace + ".CacheDependencyFactory." + afterNamespace);
            }
        }

        /// <summary>
        /// TableCacheDependency ��������ռ�
        /// </summary>
        public string TableCacheDependencyNamespace
        {
            get
            {
                return TrimDot(beforeNamespace + ".TableCacheDependency." + afterNamespace);
            }
        }

        /// <summary>
        /// �����ռ��׺�ӵ㣬���硰Basic.��
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
        /// �����ռ��׺���»��ߣ����硰Basic_��
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
        /// ȥ���������ߵĵ�
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
