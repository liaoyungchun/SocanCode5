using System;
using System.Data;
using System.Windows.Forms;

namespace SocanCode
{
    public partial class StyleUserControl : UserControl
    {
        public StyleUserControl()
        {
            InitializeComponent();
        }

        public Model.Database DB { get; set; }

        public void LoadConfig()
        {
            int selectedLevel3Frame = Properties.Settings.Default.Level3Frame;
            if (cobSlnFrame.Items.Count > selectedLevel3Frame)
                cobSlnFrame.SelectedIndex = selectedLevel3Frame;

            txtDBHelperName.Text = Properties.Settings.Default.DBHelperName;
            txtBeforeNamespace.Text = Properties.Settings.Default.BeforeNamespace;
            txtAfterNamespace.Text = Properties.Settings.Default.AfterNamespace;
            chkDBUtility.Checked = Properties.Settings.Default.IsCreateDBUtility;
            chkModel.Checked = Properties.Settings.Default.IsCreateModel;
            chkIDAL.Checked = Properties.Settings.Default.IsCreateIDAL;
            chkDAL.Checked = Properties.Settings.Default.IsCreateDAL;
            chkFilterFieldOnDALAdd.Checked = Properties.Settings.Default.FilterFieldOnDALAdd;
            cobPageStyle.SelectedIndex = Properties.Settings.Default.PageStyle;
            cobModelStyle.SelectedIndex = Properties.Settings.Default.ModelStyle;

            int selectedCmdType = Properties.Settings.Default.CmdType;
            if (cobCmdType.Items.Count > selectedCmdType)
                cobCmdType.SelectedIndex = selectedCmdType;

            int selectedDALFrame = Properties.Settings.Default.DALFrame;
            if (selectedDALFrame < 0)
            {
                switch (DB.Type)
                {
                    case Model.Database.DatabaseType.Access:
                        cobDALFrame.SelectedIndex = 1;
                        break;
                    case Model.Database.DatabaseType.Sql2000:
                    case Model.Database.DatabaseType.Sql2005:
                        cobDALFrame.SelectedIndex = 2;
                        break;
                    case Model.Database.DatabaseType.MySql:
                        cobDALFrame.SelectedIndex = 3;
                        break;
                    default:
                        cobDALFrame.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                if (cobDALFrame.Items.Count > selectedDALFrame)
                    cobDALFrame.SelectedIndex = selectedDALFrame;
            }

            chkDALFactory.Checked = Properties.Settings.Default.IsCreateDALFactory;
            chkBL.Checked = Properties.Settings.Default.IsCreateBL;

            int selectedCacheFrame = Properties.Settings.Default.CacheFrame;
            if (cobCacheFrame.Items.Count > selectedCacheFrame)
                cobCacheFrame.SelectedIndex = selectedCacheFrame;

            int selectedBLFrame = Properties.Settings.Default.BLFrame;
            if (cobBLFrame.Items.Count > selectedBLFrame)
                cobBLFrame.SelectedIndex = selectedBLFrame;

            chkUserControl.Checked = Properties.Settings.Default.IsCreateUserControl;
            chkICacheDependency.Checked = Properties.Settings.Default.IsCreateICacheDependency;
            chkTableCacheDependency.Checked = Properties.Settings.Default.IsCreateTableCacheDependency;
            chkCacheDependencyFactory.Checked = Properties.Settings.Default.IsCreateCacheDependencyFactory;
        }

        public void SaveConfig()
        {
            Properties.Settings.Default.Level3Frame = cobSlnFrame.SelectedIndex;
            Properties.Settings.Default.DBHelperName = txtDBHelperName.Text.Trim();
            Properties.Settings.Default.BeforeNamespace = txtBeforeNamespace.Text;
            Properties.Settings.Default.AfterNamespace = txtAfterNamespace.Text;
            Properties.Settings.Default.IsCreateDBUtility = chkDBUtility.Checked;
            Properties.Settings.Default.IsCreateModel = chkModel.Checked;
            Properties.Settings.Default.IsCreateIDAL = chkIDAL.Checked;
            Properties.Settings.Default.IsCreateDAL = chkDAL.Checked;
            Properties.Settings.Default.FilterFieldOnDALAdd = chkFilterFieldOnDALAdd.Checked;
            Properties.Settings.Default.PageStyle = cobPageStyle.SelectedIndex;
            Properties.Settings.Default.CmdType = cobCmdType.SelectedIndex;
            Properties.Settings.Default.ModelStyle = cobModelStyle.SelectedIndex;

            if (cobDALFrame.SelectedIndex == 0)
                Properties.Settings.Default.DALFrame = 0;
            else
                Properties.Settings.Default.DALFrame = -1;

            Properties.Settings.Default.IsCreateDALFactory = chkDALFactory.Checked;
            Properties.Settings.Default.IsCreateBL = chkBL.Checked;

            Properties.Settings.Default.CacheFrame = cobCacheFrame.SelectedIndex;
            Properties.Settings.Default.BLFrame = cobBLFrame.SelectedIndex;

            Properties.Settings.Default.IsCreateUserControl = chkUserControl.Checked;

            Properties.Settings.Default.IsCreateICacheDependency = chkICacheDependency.Checked;
            Properties.Settings.Default.IsCreateTableCacheDependency = chkTableCacheDependency.Checked;
            Properties.Settings.Default.IsCreateCacheDependencyFactory = chkCacheDependencyFactory.Checked;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 生成样式
        /// </summary>
        public Model.CreateStyle CreateStyle
        {
            get
            {
                Model.CreateStyle createStyle = new Model.CreateStyle();
                createStyle.HasCreateBL = chkBL.Checked;
                createStyle.HasCreateDAL = chkDAL.Checked;
                createStyle.HasCreateDALFactory = chkDALFactory.Checked;
                createStyle.HasCreateDBULibrary = chkDBUtility.Checked;
                createStyle.HasCreateIDAL = chkIDAL.Checked;
                createStyle.HasCreateModel = chkModel.Checked;
                createStyle.HasCreateUserControl = chkUserControl.Checked;
                createStyle.HasCreateICacheDependency = chkICacheDependency.Checked;
                createStyle.HasCreateTableCacheDependency = chkTableCacheDependency.Checked;
                createStyle.HasCreateCacheDependencyFactory = chkCacheDependencyFactory.Checked;
                return createStyle;
            }
        }

        /// <summary>
        /// 代码样式
        /// </summary>
        public Model.CodeStyle CodeStyle
        {
            get
            {
                Model.CodeStyle codeStyle = new Model.CodeStyle();

                if (this.DB != null)
                {
                    codeStyle.DBHelperName = txtDBHelperName.Text.Trim() == string.Empty ? this.DB.DbName + "Helper" : txtDBHelperName.Text.Trim();
                    codeStyle.BeforeNamespace = txtBeforeNamespace.Text.Trim();
                    codeStyle.AfterNamespace = txtAfterNamespace.Text.Trim();

                    switch (cobSlnFrame.SelectedIndex)
                    {
                        case 0:
                            codeStyle.SlnFrame = Model.CodeStyle.SlnFrames.Simple;
                            break;
                        case 1:
                            codeStyle.SlnFrame = Model.CodeStyle.SlnFrames.Factory;
                            break;
                        default:
                            break;
                    }
                    switch (cobCacheFrame.SelectedIndex)
                    {
                        case 0:
                            codeStyle.CacheFrame = Model.CodeStyle.CacheFrames.None;
                            break;
                        case 1:
                            codeStyle.CacheFrame = Model.CodeStyle.CacheFrames.ObjectCache;
                            break;
                        case 2:
                            codeStyle.CacheFrame = Model.CodeStyle.CacheFrames.AggregateDependency;
                            break;
                        case 3:
                            codeStyle.CacheFrame = Model.CodeStyle.CacheFrames.ObjectCacheAndAggregateDependency;
                            break;
                        default:
                            break;
                    }
                    switch (cobCmdType.SelectedIndex)
                    {
                        case 0:
                            codeStyle.CmdType = CommandType.Text;
                            break;
                        case 1:
                            codeStyle.CmdType = CommandType.StoredProcedure;
                            break;
                        default:
                            break;
                    }
                    switch (cobModelStyle.SelectedIndex)
                    {
                        case 0:
                            codeStyle.ModelStyle = Model.CodeStyle.ModelStyles.CS2;
                            break;
                        case 1:
                            codeStyle.ModelStyle = Model.CodeStyle.ModelStyles.CS3;
                            break;
                        case 2:
                            codeStyle.ModelStyle = Model.CodeStyle.ModelStyles.MVC2;
                            break;
                        default:
                            break;
                    }
                    switch (cobDALFrame.SelectedIndex)
                    {
                        case 1:
                            codeStyle.DALFrame = Model.CodeStyle.DALFrames.AccessDAL;
                            break;
                        case 2:
                            codeStyle.DALFrame = Model.CodeStyle.DALFrames.SqlServerDAL;
                            break;
                        case 3:
                            codeStyle.DALFrame = Model.CodeStyle.DALFrames.MySqlDAL;
                            break;
                        case 4:
                            codeStyle.DALFrame = Model.CodeStyle.DALFrames.OracleDAL;
                            break;
                        case 5:
                            codeStyle.DALFrame = Model.CodeStyle.DALFrames.SQLiteDAL;
                            break;
                        default:
                            codeStyle.DALFrame = Model.CodeStyle.DALFrames.DAL;
                            break;
                    }

                    codeStyle.FilterFieldOnDALAdd = chkFilterFieldOnDALAdd.Checked;
                    codeStyle.PageStyle = cobPageStyle.SelectedIndex == 0 ? Model.CodeStyle.PageStyles.DataReader : Model.CodeStyle.PageStyles.Sql;

                    switch (cobBLFrame.SelectedIndex)
                    {
                        case 1:
                            codeStyle.BlFrame = Model.CodeStyle.BLFrame.BLS;
                            break;
                        default:
                            codeStyle.BlFrame = Model.CodeStyle.BLFrame.BLL;
                            break;
                    }
                }

                return codeStyle;
            }
        }

        #region 选项之间的互相约束
        private void cobSlnFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cobSlnFrame.SelectedIndex)
            {
                case 0:
                    chkDBUtility.Checked = true;
                    chkModel.Checked = true;
                    chkDAL.Checked = true;
                    chkBL.Checked = true;
                    chkIDAL.Checked = chkIDAL.Enabled = false;
                    chkDALFactory.Checked = chkDALFactory.Enabled = false;
                    break;
                case 1:
                    chkDBUtility.Checked = true;
                    chkModel.Checked = true;
                    chkDAL.Checked = true;
                    chkBL.Checked = true;
                    chkIDAL.Checked = chkIDAL.Enabled = true;
                    chkDALFactory.Checked = chkDALFactory.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void cobCacheFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cobCacheFrame.SelectedIndex)
            {
                case 2:
                case 3:
                    chkICacheDependency.Enabled = chkICacheDependency.Checked = true;
                    chkTableCacheDependency.Enabled = chkTableCacheDependency.Checked = true;
                    chkCacheDependencyFactory.Enabled = chkCacheDependencyFactory.Checked = true;
                    break;
                default:
                    chkICacheDependency.Enabled = chkICacheDependency.Checked = false;
                    chkTableCacheDependency.Enabled = chkTableCacheDependency.Checked = false;
                    chkCacheDependencyFactory.Enabled = chkCacheDependencyFactory.Checked = false;
                    break;
            }
        }

        private void cobDALFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            cobCacheFrame.Items.Clear();
            cobCacheFrame.Items.Add("不使用");
            cobCacheFrame.Items.Add("缓存对象[推荐]");

            switch (cobDALFrame.SelectedIndex)
            {
                case 2:
                    cobCacheFrame.Items.Add("聚合缓存依赖");
                    cobCacheFrame.Items.Add("缓存对象+聚合缓存依赖");
                    cobCacheFrame.SelectedIndex = Properties.Settings.Default.CacheFrame;
                    break;
                default:
                    cobCacheFrame.SelectedIndex = Properties.Settings.Default.CacheFrame == 0 ? 0 : 1;
                    break;
            }

            switch (cobDALFrame.SelectedIndex)
            {
                case 0:
                    cobSlnFrame.SelectedIndex = 0;
                    cobSlnFrame.Enabled = false;
                    break;
                default:
                    cobSlnFrame.Enabled = true;
                    break;
            }
        }

        private void cobBLFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cobBLFrame.SelectedIndex)
            {
                case 1:
                    MainForm.ShowMessage("不建议直接把业务逻辑层做成Web服务，\n更好的做法是新建一个Web服务项目，调用业务逻辑层的方法。");
                    break;
                default:
                    break;
            }
        }

        private void chkDAL_CheckedChanged(object sender, EventArgs e)
        {
            chkFilterFieldOnDALAdd.Enabled = cobPageStyle.Enabled = chkDAL.Enabled && chkDAL.Checked;
        }
        #endregion
    }
}
