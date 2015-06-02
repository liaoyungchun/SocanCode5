using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SocanCode
{
    public partial class CodeOutputForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private Model.Database db;

        public CodeOutputForm(ContextMenuStrip cms, Model.Database db)
        {
            InitializeComponent();
            this.TabPageContextMenuStrip = cms;

            styleUserControl1.DB = db;
            styleUserControl1.LoadConfig();

            selectTableUserControl1.DB = db;

            this.db = db;
            this.TabText = string.Format("输出代码 {0}", db.DbName);
            LoadConfig(db);
        }

        public enum VSVersionFrames
        {
            VS2005,
            VS2008,
            VS2010
        }

        /// <summary>
        /// VisualStudio的版本
        /// </summary>
        private VSVersionFrames VSVersion
        {
            get
            {
                if (radVS2005.Checked)
                    return VSVersionFrames.VS2005;
                if (radVS2008.Checked)
                    return VSVersionFrames.VS2008;

                return VSVersionFrames.VS2010;
            }
        }

        /// <summary>
        /// 生成的路径位置
        /// </summary>
        private string CreateFilePath
        {
            get { return txtPath.Text.Trim(); }
        }

        private void LoadConfig(Model.Database db)
        {
            txtPath.Text = Properties.Settings.Default.CreateCodePath;

            switch (Properties.Settings.Default.VSVersion)
            {
                case 2005:
                    radVS2005.Checked = true;
                    break;
                case 2008:
                    radVS2008.Checked = true;
                    break;
                case 2010:
                    radVS2010.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void SaveConfig()
        {
            Properties.Settings.Default.CreateCodePath = txtPath.Text.Trim();

            if (radVS2005.Checked)
                Properties.Settings.Default.VSVersion = 2005;
            if (radVS2008.Checked)
                Properties.Settings.Default.VSVersion = 2008;
            if (radVS2010.Checked)
                Properties.Settings.Default.VSVersion = 2010;

            Properties.Settings.Default.Save();

            styleUserControl1.SaveConfig();
        }

        private void btnOutputCode_Click(object sender, EventArgs e)
        {
            if (selectTableUserControl1.SelectedTables.Count <= 0)
            {
                MainForm.ShowMessage("未选择任何表!");
                return;
            }

            foreach (Model.Table table in selectTableUserControl1.SelectedTables)
            {
                if (!table.HasConditonRow)
                {
                    MainForm.ShowMessage(string.Format("表{0}不存在任何字段，无法生成！", table.Name));
                    return;
                }
            }

            if (MessageBox.Show("确定要输出代码吗?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if (Directory.Exists(CreateFilePath))
            {
                if (MessageBox.Show("该目录已存在，是否删除?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Directory.Delete(CreateFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            SaveConfig();

            OutputCode(selectTableUserControl1.SelectedTables, styleUserControl1.CodeStyle, styleUserControl1.CreateStyle);

            CreateSln(styleUserControl1.CreateStyle, styleUserControl1.CodeStyle);

            progressBar1.Value = 0;

            if (MessageBox.Show("成功生成,是否打开目录?", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Process.Start(CreateFilePath);
        }

        private void btnOutputStoreProcedure_Click(object sender, EventArgs e)
        {
            if (selectTableUserControl1.SelectedTables.Count <= 0)
            {
                MainForm.ShowMessage("未选择任何表!");
                return;
            }
            if (MessageBox.Show("确定要输出存储过程吗?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            OutputStoreProcedure(selectTableUserControl1.SelectedTables, styleUserControl1.CodeStyle, styleUserControl1.CreateStyle);

            if (MessageBox.Show("成功生成,是否打开目录?", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Process.Start(CreateFilePath);
        }

        private void OutputStoreProcedure(List<Model.Table> tables, Model.CodeStyle codeStyle, Model.CreateStyle createStyle)
        {
            string path = CreateFilePath;
            Directory.CreateDirectory(path);
            CodeFactory.StoreProcedureAccess.CreateStoreProcedureFile(db, tables, path, codeStyle);
        }

        /// <summary>
        /// 创建Sln文件
        /// </summary>
        private void CreateSln(Model.CreateStyle createStyle, Model.CodeStyle codeStyle)
        {
            StringBuilder sln = new StringBuilder();

            switch (VSVersion)
            {
                case VSVersionFrames.VS2005:
                    sln.AppendLine("Microsoft Visual Studio Solution File, Format Version 9.00");
                    sln.AppendLine("# Visual Studio 2005");
                    break;
                case VSVersionFrames.VS2008:
                    sln.AppendLine("Microsoft Visual Studio Solution File, Format Version 10.00");
                    sln.AppendLine("# Visual Studio 2008");
                    break;
                default:
                    sln.AppendLine("Microsoft Visual Studio Solution File, Format Version 11.00");
                    sln.AppendLine("# Visual Studio 2010");
                    break;
            }

            if (createStyle.HasCreateBL)
            {
                switch (codeStyle.BlFrame)
                {
                    case Model.CodeStyle.BLFrame.BLS:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\BLS.csproj"));
                        break;
                    case Model.CodeStyle.BLFrame.BLL:
                    default:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\BLL.csproj"));
                        break;
                }
            }

            if (createStyle.HasCreateDAL)
            {
                switch (codeStyle.DALFrame)
                {
                    case Model.CodeStyle.DALFrames.AccessDAL:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\AccessDAL.csproj"));
                        break;
                    case Model.CodeStyle.DALFrames.SqlServerDAL:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\SqlServerDAL.csproj"));
                        break;
                    case Model.CodeStyle.DALFrames.MySqlDAL:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\MySqlDAL.csproj"));
                        break;
                    case Model.CodeStyle.DALFrames.OracleDAL:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\OracleDAL.csproj"));
                        break;
                    case Model.CodeStyle.DALFrames.SQLiteDAL:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\SQLiteDAL.csproj"));
                        break;
                    default:
                        sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\DAL.csproj"));
                        break;
                }
            }

            if (createStyle.HasCreateDALFactory)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\DALFactory.csproj"));
            }

            if (createStyle.HasCreateDBULibrary)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\DBUtility.csproj"));
            }

            if (createStyle.HasCreateIDAL)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\IDAL.csproj"));
            }

            if (createStyle.HasCreateModel)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\Model.csproj"));
            }

            if (createStyle.HasCreateICacheDependency)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\ICacheDependency.csproj"));
            }

            if (createStyle.HasCreateTableCacheDependency)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\TableCacheDependency.csproj"));
            }

            if (createStyle.HasCreateCacheDependencyFactory)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\CacheDependencyFactory.csproj"));
            }

            if (createStyle.HasCreateUserControl)
            {
                sln.AppendLine(CodeUtility.FileStream.ReadFile(Model.CreateStyle.CURRENT_PATH + "\\Sln\\Web.csproj"));
            }

            Directory.CreateDirectory(CreateFilePath);
            StreamWriter sw = new StreamWriter(CreateFilePath + "\\Socansoft.sln");
            sw.Write(sln.ToString());
            sw.Close();
        }

        /// <summary>
        /// 输出代码
        /// </summary>
        private void OutputCode(List<Model.Table> tables, Model.CodeStyle codeStyle, Model.CreateStyle createStyle)
        {
            string slnDictionary;
            switch (VSVersion)
            {
                case VSVersionFrames.VS2005:
                    slnDictionary = "Template2005";
                    break;
                case VSVersionFrames.VS2008:
                    slnDictionary = "Template2008";
                    break;
                case VSVersionFrames.VS2010:
                default:
                    slnDictionary = "Template2010";
                    break;
            }

            if (createStyle.HasCreateModel)
            {
                string pathModel = CreateFilePath + "\\Model";
                CopyFiles(string.Format("{0}\\{1}\\Model", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathModel);
                CodeFactory.CodeAccess.CreateModelFile(db, tables, codeStyle, pathModel);
            }
            progressBar1.Value = 10;

            if (createStyle.HasCreateIDAL)
            {
                string pathIDAL = CreateFilePath + "\\IDAL";
                CopyFiles(string.Format("{0}\\{1}\\IDAL", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathIDAL);
                CodeFactory.CodeAccess.CreateIDALFile(db, tables, codeStyle, pathIDAL);
            }
            progressBar1.Value = 20;

            if (createStyle.HasCreateDAL)
            {
                string pathDAL = CreateFilePath + "\\" + codeStyle.DALFrame.ToString();
                CopyFiles(string.Format("{0}\\{1}\\{2}", Model.CreateStyle.CURRENT_PATH, slnDictionary, codeStyle.DALFrame.ToString()), pathDAL);
                CodeFactory.CodeAccess.CreateDALFile(db, tables, codeStyle, pathDAL);
            }
            progressBar1.Value = 30;

            if (createStyle.HasCreateBL)
            {
                string pathBL;
                pathBL = CreateFilePath + "\\" + codeStyle.BlFrame.ToString();
                CopyFiles(string.Format("{0}\\{1}\\{2}", Model.CreateStyle.CURRENT_PATH, slnDictionary, codeStyle.BlFrame.ToString()), pathBL);
                CodeFactory.CodeAccess.CreateBLFile(db, tables, codeStyle, pathBL);
            }
            progressBar1.Value = 40;

            if (createStyle.HasCreateUserControl)
            {
                string pathUserControl = CreateFilePath + "\\Web\\Controls";
                CopyFiles(string.Format("{0}\\{1}\\Web", Model.CreateStyle.CURRENT_PATH, slnDictionary), CreateFilePath + "\\Web");
                CodeFactory.CodeAccess.CreateUserControl(db, tables, codeStyle, pathUserControl);
            }
            progressBar1.Value = 50;

            if (createStyle.HasCreateDALFactory)
            {
                string pathDALFactory = CreateFilePath + "\\DALFactory";
                CopyFiles(string.Format("{0}\\{1}\\DALFactory", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathDALFactory);
                CodeFactory.CodeAccess.CreateDALFactoryFile(tables, codeStyle, pathDALFactory);
            }
            progressBar1.Value = 50;

            if (createStyle.HasCreateDBULibrary)
            {
                string pathDBUtility = CreateFilePath + "\\DBUtility";
                CopyFiles(string.Format("{0}\\{1}\\DBUtility", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathDBUtility);
            }
            progressBar1.Value = 60;

            if (createStyle.HasCreateICacheDependency)
            {
                string pathICacheDependency = CreateFilePath + "\\ICacheDependency";
                CopyFiles(string.Format("{0}\\{1}\\ICacheDependency", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathICacheDependency);
                CodeFactory.CodeAccess.CreateICacheDependencyFile(codeStyle, pathICacheDependency);
            }
            progressBar1.Value = 70;

            if (createStyle.HasCreateTableCacheDependency)
            {
                string pathTableCacheDependency = CreateFilePath + "\\TableCacheDependency";
                CopyFiles(string.Format("{0}\\{1}\\TableCacheDependency", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathTableCacheDependency);
                CodeFactory.CodeAccess.CreateTableCacheDependencyFile(db, tables, codeStyle, pathTableCacheDependency);
            }
            progressBar1.Value = 80;

            if (createStyle.HasCreateCacheDependencyFactory)
            {
                string pathCacheDependencyFactory = CreateFilePath + "\\CacheDependencyFactory";
                CopyFiles(string.Format("{0}\\{1}\\CacheDependencyFactory", Model.CreateStyle.CURRENT_PATH, slnDictionary), pathCacheDependencyFactory);
                CodeFactory.CodeAccess.CreateCacheDependencyFactoryFile(tables, codeStyle, pathCacheDependencyFactory);
            }
            progressBar1.Value = 90;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private void CopyFiles(string sourceDirectory, string targetDirectory)
        {
            Directory.CreateDirectory(targetDirectory);

            if (!Directory.Exists(sourceDirectory)) return;

            string[] directories = Directory.GetDirectories(sourceDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyFiles(d, targetDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }

            string[] files = Directory.GetFiles(sourceDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, targetDirectory + s.Substring(s.LastIndexOf("\\")), true);
                }
            }
        }

        private void FormCodeOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }
    }
}