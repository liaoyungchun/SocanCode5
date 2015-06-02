using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SocanCode
{
    public partial class CodeCreateForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private Model.Table table;
        private Model.Database db;

        public CodeCreateForm(ContextMenuStrip cms, Model.Database db, Model.Table table)
        {
            InitializeComponent();
            this.TabPageContextMenuStrip = cms;

            styleUserControl1.DB = db;
            styleUserControl1.LoadConfig();

            this.db = db;
            this.table = table;
            this.TabText = string.Format("生成代码 {0}", table.Name);

            tcCodes.Controls.Clear();

            TextEditor.SetStyle(txtInternalModel, "C#");
            TextEditor.SetStyle(txtModel, "C#");
            TextEditor.SetStyle(txtIDAL, "C#");
            TextEditor.SetStyle(txtDALFactory, "C#");
            TextEditor.SetStyle(txtInternalDAL, "C#");
            TextEditor.SetStyle(txtDAL, "C#");
            TextEditor.SetStyle(txtBL, "C#");
            TextEditor.SetStyle(txtUserControl, "HTML");
            TextEditor.SetStyle(txtUserControlDesignerCs, "C#");
            TextEditor.SetStyle(txtUserControlCs, "C#");

            TextEditor.SetStyle(txtICacheDependency, "C#");
            TextEditor.SetStyle(txtTableDependency, "C#");
            TextEditor.SetStyle(txtTableCacheDependency, "C#");
            TextEditor.SetStyle(txtDependencyAccess, "C#");
            TextEditor.SetStyle(txtDependencyFacade, "C#");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            styleUserControl1.SaveConfig();

            List<Model.Table> tables = new List<Model.Table>();
            tables.Add(table);

            tcCodes.Controls.Clear();

            if (styleUserControl1.CreateStyle.HasCreateModel)
            {
                //InternalModel
                txtInternalModel.Tag = Model.CodeLayer.CodeLayers.InternalModel;
                txtInternalModel.Text = Codes.ModelCode.GetInternalModelCode(table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpInternalModel);

                //Model
                txtModel.Tag = Model.CodeLayer.CodeLayers.Model;
                txtModel.Text = Codes.ModelCode.GetModelCode(table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpModel);
            }

            if (styleUserControl1.CreateStyle.HasCreateIDAL)
            {
                //IDAL
                txtIDAL.Tag = Model.CodeLayer.CodeLayers.IDAL;
                txtIDAL.Text = Codes.IDALCode.GetIDALCode(table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpIDAL);
            }

            if (styleUserControl1.CreateStyle.HasCreateDAL)
            {
                //InternalDAL
                txtInternalDAL.Tag = Model.CodeLayer.CodeLayers.InternalDAL;
                txtInternalDAL.Text = Codes.DALCode.GetInternalDALCode(db, table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpInternalDAL);

                //DAL
                txtDAL.Tag = Model.CodeLayer.CodeLayers.DAL;
                txtDAL.Text = Codes.DALCode.GetDALCode(db, table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpDAL);
            }

            if (styleUserControl1.CreateStyle.HasCreateDALFactory)
            {
                //DALFactory
                txtDALFactory.Tag = Model.CodeLayer.CodeLayers.DALFactory;
                txtDALFactory.Text = Codes.DALFactoryCode.GetDALFactoryCode(tables, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpDALFactory);
            }

            if (styleUserControl1.CreateStyle.HasCreateICacheDependency)
            {
                //ICacheDependency
                txtICacheDependency.Tag = Model.CodeLayer.CodeLayers.ICacheDependency;
                txtICacheDependency.Text = Codes.ICacheDependencyCode.GetICacheDependencyCode(styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpICacheDependency);
            }

            if (styleUserControl1.CreateStyle.HasCreateTableCacheDependency)
            {
                //TableDependency
                txtTableDependency.Tag = Model.CodeLayer.CodeLayers.TableDependency;
                txtTableDependency.Text = Codes.TableCacheDependencyCode.GetTableDependencyCode(db, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpTableDependency);

                //TableCacheDependency
                txtTableCacheDependency.Tag = Model.CodeLayer.CodeLayers.TableCacheDependency;
                txtTableCacheDependency.Text = Codes.TableCacheDependencyCode.GetTableCacheDependencyCode(db, table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpTableCacheDependency);
            }

            if (styleUserControl1.CreateStyle.HasCreateCacheDependencyFactory)
            {
                //DependencyAccess
                txtDependencyAccess.Tag = Model.CodeLayer.CodeLayers.DependencyAccess;
                txtDependencyAccess.Text = Codes.CacheDependencyFactoryCode.GetDependencyAccessCode(tables, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpDependencyAccess);

                //DependencyFacade

                txtDependencyFacade.Tag = Model.CodeLayer.CodeLayers.DependencyFacade;
                txtDependencyFacade.Text = Codes.CacheDependencyFactoryCode.GetDependencyFacadeCode(tables, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpDependencyFacade);
            }

            if (styleUserControl1.CreateStyle.HasCreateBL)
            {
                //BL
                txtBL.Tag = Model.CodeLayer.CodeLayers.BL;
                txtBL.Text = Codes.BLCode.GetBLCSCode(db, table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpBL);
            }

            if (styleUserControl1.CreateStyle.HasCreateUserControl)
            {
                //UserControl
                txtUserControl.Tag = Model.CodeLayer.CodeLayers.UserControl;
                txtUserControl.Text = Codes.UserControlCode.GetUserControlCode(table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpUserControl);

                //UserControlDesignerCs
                txtUserControlDesignerCs.Tag = Model.CodeLayer.CodeLayers.UserControlDesignerCs;
                txtUserControlDesignerCs.Text = Codes.UserControlCode.GetWebUserControlDesignerCsCode(table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpUserControlDesignerCs);

                //UserControlCs
                txtUserControlCs.Tag = Model.CodeLayer.CodeLayers.UserControlCs;
                txtUserControlCs.Text = Codes.UserControlCode.GetWebUserControlCsCode(table, styleUserControl1.CodeStyle);
                tcCodes.Controls.Add(tpUserControlCs);
            }

            btnSaveCurrentTab.Enabled = btnSaveAll.Enabled = true;
        }

        private void btnSaveCurrentTab_Click(object sender, EventArgs e)
        {
            if (tcCodes.SelectedTab == null)
            {
                MessageBox.Show("请先生成代码！");
                return;
            }

            ICSharpCode.TextEditor.TextEditorControl txtEditor = tcCodes.SelectedTab.Controls[0] as ICSharpCode.TextEditor.TextEditorControl;
            if (txtEditor != null)
            {
                Model.CodeLayer.CodeLayers layer = (Model.CodeLayer.CodeLayers)txtEditor.Tag;
                Model.CodeLayer codeLayer = new Model.CodeLayer(layer);

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.FileName = string.Format(codeLayer.FileName, table.Name);
                dlg.Filter = string.Format(".{0}|*.{0}", codeLayer.FileExt);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    CodeUtility.FileStream.WriteFile(dlg.FileName, txtEditor.Text);
                }
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (tcCodes.TabPages == null)
            {
                MessageBox.Show("请先生成代码！");
                return;
            }

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (TabPage page in tcCodes.TabPages)
                {
                    ICSharpCode.TextEditor.TextEditorControl txtEditor = page.Controls[0] as ICSharpCode.TextEditor.TextEditorControl;
                    if (txtEditor != null)
                    {
                        Model.CodeLayer.CodeLayers layer = (Model.CodeLayer.CodeLayers)txtEditor.Tag;
                        Model.CodeLayer codeLayer = new Model.CodeLayer(layer);

                        string filePath = dlg.SelectedPath + "\\" + codeLayer.Folder;

                        string fileName = string.Format(codeLayer.FileName, table.Name);

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        CodeUtility.FileStream.WriteFile(filePath + "\\" + fileName, txtEditor.Text);
                    }
                }
            }
        }

        private void FormCodeCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            styleUserControl1.SaveConfig();
        }
    }
}