
namespace Model
{
    public class CreateStyle
    {
        public static string CURRENT_PATH = System.Windows.Forms.Application.StartupPath;

        private bool hasCreateModel;
        private bool hasCreateIDAL;
        private bool hasCreateDAL;
        private bool hasCreatedDBLibrary;
        private bool hasCreateDALFactory;
        private bool hasCreateBLL;
        private bool hasCreateUserControl;
        private bool hasCreateICacheDependency;
        private bool hasCreateTableCacheDependency;
        private bool hasCreateCacheDependencyFactory;

        /// <summary>
        /// 是否生成Model层代码
        /// </summary>
        public bool HasCreateModel
        {
            get { return hasCreateModel; }
            set { hasCreateModel = value; }
        }

        /// <summary>
        /// 是否生成IDAL层代码
        /// </summary>
        public bool HasCreateIDAL
        {
            get { return hasCreateIDAL; }
            set { hasCreateIDAL = value; }
        }

        /// <summary>
        /// 是否生成DAL层代码
        /// </summary>
        public bool HasCreateDAL
        {
            get { return hasCreateDAL; }
            set { hasCreateDAL = value; }
        }

        /// <summary>
        /// 是否生成DBULibrary层代码
        /// </summary>
        public bool HasCreateDBULibrary
        {
            get { return hasCreatedDBLibrary; }
            set { hasCreatedDBLibrary = value; }
        }

        /// <summary>
        /// 是否生成DALFactory层代码
        /// </summary>
        public bool HasCreateDALFactory
        {
            get { return hasCreateDALFactory; }
            set { hasCreateDALFactory = value; }
        }

        /// <summary>
        /// 是否生成BLL层代码
        /// </summary>
        public bool HasCreateBL
        {
            get { return hasCreateBLL; }
            set { hasCreateBLL = value; }
        }

        /// <summary>
        /// 是否生成用户控件代码
        /// </summary>
        public bool HasCreateUserControl
        {
            get { return hasCreateUserControl; }
            set { hasCreateUserControl = value; }
        }

        /// <summary>
        /// 是否生成ICacheDependency层代码
        /// </summary>
        public bool HasCreateICacheDependency
        {
            get { return hasCreateICacheDependency; }
            set { hasCreateICacheDependency = value; }
        }

        /// <summary>
        /// 是否生成TableCacheDependency层代码
        /// </summary>
        public bool HasCreateTableCacheDependency
        {
            get { return hasCreateTableCacheDependency; }
            set { hasCreateTableCacheDependency = value; }
        }

        /// <summary>
        /// 是否生成CacheDependencyFactory层代码
        /// </summary>
        public bool HasCreateCacheDependencyFactory
        {
            get { return hasCreateCacheDependencyFactory; }
            set { hasCreateCacheDependencyFactory = value; }
        }
    }
}
