namespace SocanCode
{
    partial class CodeCreateForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeCreateForm));
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnSaveCurrentTab = new System.Windows.Forms.Button();
            this.tcCodes = new System.Windows.Forms.TabControl();
            this.tpInternalModel = new System.Windows.Forms.TabPage();
            this.txtInternalModel = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpModel = new System.Windows.Forms.TabPage();
            this.txtModel = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpIDAL = new System.Windows.Forms.TabPage();
            this.txtIDAL = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpInternalDAL = new System.Windows.Forms.TabPage();
            this.txtInternalDAL = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpDAL = new System.Windows.Forms.TabPage();
            this.txtDAL = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpDALFactory = new System.Windows.Forms.TabPage();
            this.txtDALFactory = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpICacheDependency = new System.Windows.Forms.TabPage();
            this.txtICacheDependency = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpTableDependency = new System.Windows.Forms.TabPage();
            this.txtTableDependency = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpTableCacheDependency = new System.Windows.Forms.TabPage();
            this.txtTableCacheDependency = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpDependencyAccess = new System.Windows.Forms.TabPage();
            this.txtDependencyAccess = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpDependencyFacade = new System.Windows.Forms.TabPage();
            this.txtDependencyFacade = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpBL = new System.Windows.Forms.TabPage();
            this.txtBL = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpUserControl = new System.Windows.Forms.TabPage();
            this.txtUserControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpUserControlDesignerCs = new System.Windows.Forms.TabPage();
            this.txtUserControlDesignerCs = new ICSharpCode.TextEditor.TextEditorControl();
            this.tpUserControlCs = new System.Windows.Forms.TabPage();
            this.txtUserControlCs = new ICSharpCode.TextEditor.TextEditorControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.styleUserControl1 = new SocanCode.StyleUserControl();
            this.tcCodes.SuspendLayout();
            this.tpInternalModel.SuspendLayout();
            this.tpModel.SuspendLayout();
            this.tpIDAL.SuspendLayout();
            this.tpInternalDAL.SuspendLayout();
            this.tpDAL.SuspendLayout();
            this.tpDALFactory.SuspendLayout();
            this.tpICacheDependency.SuspendLayout();
            this.tpTableDependency.SuspendLayout();
            this.tpTableCacheDependency.SuspendLayout();
            this.tpDependencyAccess.SuspendLayout();
            this.tpDependencyFacade.SuspendLayout();
            this.tpBL.SuspendLayout();
            this.tpUserControl.SuspendLayout();
            this.tpUserControlDesignerCs.SuspendLayout();
            this.tpUserControlCs.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(928, 35);
            this.label7.TabIndex = 39;
            this.label7.Text = "注：生成代码中不会生成数据库操作基类、DAL层必需的DALHelper.cs和BL层必需的BLHelper.cs文件，请使用“输出代码”功能得相关文件。";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnOK, 2);
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(5);
            this.btnOK.Size = new System.Drawing.Size(318, 32);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "生成代码";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveAll.Enabled = false;
            this.btnSaveAll.Location = new System.Drawing.Point(165, 41);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Padding = new System.Windows.Forms.Padding(5);
            this.btnSaveAll.Size = new System.Drawing.Size(156, 33);
            this.btnSaveAll.TabIndex = 38;
            this.btnSaveAll.Text = "保存所有代码";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnSaveCurrentTab
            // 
            this.btnSaveCurrentTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveCurrentTab.Enabled = false;
            this.btnSaveCurrentTab.Location = new System.Drawing.Point(3, 41);
            this.btnSaveCurrentTab.Name = "btnSaveCurrentTab";
            this.btnSaveCurrentTab.Padding = new System.Windows.Forms.Padding(5);
            this.btnSaveCurrentTab.Size = new System.Drawing.Size(156, 33);
            this.btnSaveCurrentTab.TabIndex = 38;
            this.btnSaveCurrentTab.Text = "保存当前页代码";
            this.btnSaveCurrentTab.UseVisualStyleBackColor = true;
            this.btnSaveCurrentTab.Click += new System.EventHandler(this.btnSaveCurrentTab_Click);
            // 
            // tcCodes
            // 
            this.tcCodes.Controls.Add(this.tpInternalModel);
            this.tcCodes.Controls.Add(this.tpModel);
            this.tcCodes.Controls.Add(this.tpIDAL);
            this.tcCodes.Controls.Add(this.tpInternalDAL);
            this.tcCodes.Controls.Add(this.tpDAL);
            this.tcCodes.Controls.Add(this.tpDALFactory);
            this.tcCodes.Controls.Add(this.tpICacheDependency);
            this.tcCodes.Controls.Add(this.tpTableDependency);
            this.tcCodes.Controls.Add(this.tpTableCacheDependency);
            this.tcCodes.Controls.Add(this.tpDependencyAccess);
            this.tcCodes.Controls.Add(this.tpDependencyFacade);
            this.tcCodes.Controls.Add(this.tpBL);
            this.tcCodes.Controls.Add(this.tpUserControl);
            this.tcCodes.Controls.Add(this.tpUserControlDesignerCs);
            this.tcCodes.Controls.Add(this.tpUserControlCs);
            this.tcCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCodes.Location = new System.Drawing.Point(0, 0);
            this.tcCodes.Multiline = true;
            this.tcCodes.Name = "tcCodes";
            this.tcCodes.SelectedIndex = 0;
            this.tcCodes.Size = new System.Drawing.Size(600, 484);
            this.tcCodes.TabIndex = 2;
            // 
            // tpInternalModel
            // 
            this.tpInternalModel.Controls.Add(this.txtInternalModel);
            this.tpInternalModel.Location = new System.Drawing.Point(4, 58);
            this.tpInternalModel.Name = "tpInternalModel";
            this.tpInternalModel.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternalModel.Size = new System.Drawing.Size(592, 422);
            this.tpInternalModel.TabIndex = 14;
            this.tpInternalModel.Text = "InternalModel";
            this.tpInternalModel.UseVisualStyleBackColor = true;
            // 
            // txtInternalModel
            // 
            this.txtInternalModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInternalModel.Encoding = ((System.Text.Encoding)(resources.GetObject("txtInternalModel.Encoding")));
            this.txtInternalModel.Location = new System.Drawing.Point(3, 3);
            this.txtInternalModel.Name = "txtInternalModel";
            this.txtInternalModel.ShowEOLMarkers = true;
            this.txtInternalModel.ShowInvalidLines = false;
            this.txtInternalModel.ShowMatchingBracket = false;
            this.txtInternalModel.ShowSpaces = true;
            this.txtInternalModel.ShowTabs = true;
            this.txtInternalModel.ShowVRuler = true;
            this.txtInternalModel.Size = new System.Drawing.Size(586, 416);
            this.txtInternalModel.TabIndex = 1;
            // 
            // tpModel
            // 
            this.tpModel.Controls.Add(this.txtModel);
            this.tpModel.Location = new System.Drawing.Point(4, 58);
            this.tpModel.Name = "tpModel";
            this.tpModel.Padding = new System.Windows.Forms.Padding(3);
            this.tpModel.Size = new System.Drawing.Size(552, 422);
            this.tpModel.TabIndex = 0;
            this.tpModel.Text = "Model";
            this.tpModel.UseVisualStyleBackColor = true;
            // 
            // txtModel
            // 
            this.txtModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModel.Encoding = ((System.Text.Encoding)(resources.GetObject("txtModel.Encoding")));
            this.txtModel.Location = new System.Drawing.Point(3, 3);
            this.txtModel.Name = "txtModel";
            this.txtModel.ShowEOLMarkers = true;
            this.txtModel.ShowInvalidLines = false;
            this.txtModel.ShowMatchingBracket = false;
            this.txtModel.ShowSpaces = true;
            this.txtModel.ShowTabs = true;
            this.txtModel.ShowVRuler = true;
            this.txtModel.Size = new System.Drawing.Size(546, 452);
            this.txtModel.TabIndex = 0;
            // 
            // tpIDAL
            // 
            this.tpIDAL.Controls.Add(this.txtIDAL);
            this.tpIDAL.Location = new System.Drawing.Point(4, 58);
            this.tpIDAL.Name = "tpIDAL";
            this.tpIDAL.Padding = new System.Windows.Forms.Padding(3);
            this.tpIDAL.Size = new System.Drawing.Size(552, 422);
            this.tpIDAL.TabIndex = 1;
            this.tpIDAL.Text = "IDAL";
            this.tpIDAL.UseVisualStyleBackColor = true;
            // 
            // txtIDAL
            // 
            this.txtIDAL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIDAL.Encoding = ((System.Text.Encoding)(resources.GetObject("txtIDAL.Encoding")));
            this.txtIDAL.Location = new System.Drawing.Point(3, 3);
            this.txtIDAL.Name = "txtIDAL";
            this.txtIDAL.ShowEOLMarkers = true;
            this.txtIDAL.ShowInvalidLines = false;
            this.txtIDAL.ShowSpaces = true;
            this.txtIDAL.ShowTabs = true;
            this.txtIDAL.ShowVRuler = true;
            this.txtIDAL.Size = new System.Drawing.Size(546, 452);
            this.txtIDAL.TabIndex = 1;
            // 
            // tpInternalDAL
            // 
            this.tpInternalDAL.Controls.Add(this.txtInternalDAL);
            this.tpInternalDAL.Location = new System.Drawing.Point(4, 58);
            this.tpInternalDAL.Name = "tpInternalDAL";
            this.tpInternalDAL.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternalDAL.Size = new System.Drawing.Size(552, 422);
            this.tpInternalDAL.TabIndex = 15;
            this.tpInternalDAL.Text = "InternalDAL";
            this.tpInternalDAL.UseVisualStyleBackColor = true;
            // 
            // txtInternalDAL
            // 
            this.txtInternalDAL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInternalDAL.Encoding = ((System.Text.Encoding)(resources.GetObject("txtInternalDAL.Encoding")));
            this.txtInternalDAL.Location = new System.Drawing.Point(3, 3);
            this.txtInternalDAL.Name = "txtInternalDAL";
            this.txtInternalDAL.ShowEOLMarkers = true;
            this.txtInternalDAL.ShowInvalidLines = false;
            this.txtInternalDAL.ShowMatchingBracket = false;
            this.txtInternalDAL.ShowSpaces = true;
            this.txtInternalDAL.ShowTabs = true;
            this.txtInternalDAL.ShowVRuler = true;
            this.txtInternalDAL.Size = new System.Drawing.Size(546, 452);
            this.txtInternalDAL.TabIndex = 1;
            // 
            // tpDAL
            // 
            this.tpDAL.Controls.Add(this.txtDAL);
            this.tpDAL.Location = new System.Drawing.Point(4, 58);
            this.tpDAL.Name = "tpDAL";
            this.tpDAL.Size = new System.Drawing.Size(552, 422);
            this.tpDAL.TabIndex = 2;
            this.tpDAL.Text = "DAL";
            this.tpDAL.UseVisualStyleBackColor = true;
            // 
            // txtDAL
            // 
            this.txtDAL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDAL.Encoding = ((System.Text.Encoding)(resources.GetObject("txtDAL.Encoding")));
            this.txtDAL.Location = new System.Drawing.Point(0, 0);
            this.txtDAL.Name = "txtDAL";
            this.txtDAL.ShowEOLMarkers = true;
            this.txtDAL.ShowInvalidLines = false;
            this.txtDAL.ShowSpaces = true;
            this.txtDAL.ShowTabs = true;
            this.txtDAL.ShowVRuler = true;
            this.txtDAL.Size = new System.Drawing.Size(552, 458);
            this.txtDAL.TabIndex = 1;
            // 
            // tpDALFactory
            // 
            this.tpDALFactory.Controls.Add(this.txtDALFactory);
            this.tpDALFactory.Location = new System.Drawing.Point(4, 58);
            this.tpDALFactory.Name = "tpDALFactory";
            this.tpDALFactory.Size = new System.Drawing.Size(552, 422);
            this.tpDALFactory.TabIndex = 5;
            this.tpDALFactory.Text = "DALFactory";
            this.tpDALFactory.UseVisualStyleBackColor = true;
            // 
            // txtDALFactory
            // 
            this.txtDALFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDALFactory.Encoding = ((System.Text.Encoding)(resources.GetObject("txtDALFactory.Encoding")));
            this.txtDALFactory.Location = new System.Drawing.Point(0, 0);
            this.txtDALFactory.Name = "txtDALFactory";
            this.txtDALFactory.ShowEOLMarkers = true;
            this.txtDALFactory.ShowInvalidLines = false;
            this.txtDALFactory.ShowSpaces = true;
            this.txtDALFactory.ShowTabs = true;
            this.txtDALFactory.ShowVRuler = true;
            this.txtDALFactory.Size = new System.Drawing.Size(552, 458);
            this.txtDALFactory.TabIndex = 1;
            // 
            // tpICacheDependency
            // 
            this.tpICacheDependency.Controls.Add(this.txtICacheDependency);
            this.tpICacheDependency.Location = new System.Drawing.Point(4, 58);
            this.tpICacheDependency.Name = "tpICacheDependency";
            this.tpICacheDependency.Padding = new System.Windows.Forms.Padding(3);
            this.tpICacheDependency.Size = new System.Drawing.Size(552, 422);
            this.tpICacheDependency.TabIndex = 8;
            this.tpICacheDependency.Text = "ICacheDependency";
            this.tpICacheDependency.UseVisualStyleBackColor = true;
            // 
            // txtICacheDependency
            // 
            this.txtICacheDependency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtICacheDependency.Encoding = ((System.Text.Encoding)(resources.GetObject("txtICacheDependency.Encoding")));
            this.txtICacheDependency.Location = new System.Drawing.Point(3, 3);
            this.txtICacheDependency.Name = "txtICacheDependency";
            this.txtICacheDependency.ShowEOLMarkers = true;
            this.txtICacheDependency.ShowInvalidLines = false;
            this.txtICacheDependency.ShowSpaces = true;
            this.txtICacheDependency.ShowTabs = true;
            this.txtICacheDependency.ShowVRuler = true;
            this.txtICacheDependency.Size = new System.Drawing.Size(546, 452);
            this.txtICacheDependency.TabIndex = 2;
            // 
            // tpTableDependency
            // 
            this.tpTableDependency.Controls.Add(this.txtTableDependency);
            this.tpTableDependency.Location = new System.Drawing.Point(4, 58);
            this.tpTableDependency.Name = "tpTableDependency";
            this.tpTableDependency.Size = new System.Drawing.Size(552, 422);
            this.tpTableDependency.TabIndex = 10;
            this.tpTableDependency.Text = "TableDependency";
            this.tpTableDependency.UseVisualStyleBackColor = true;
            // 
            // txtTableDependency
            // 
            this.txtTableDependency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTableDependency.Encoding = ((System.Text.Encoding)(resources.GetObject("txtTableDependency.Encoding")));
            this.txtTableDependency.Location = new System.Drawing.Point(0, 0);
            this.txtTableDependency.Name = "txtTableDependency";
            this.txtTableDependency.ShowEOLMarkers = true;
            this.txtTableDependency.ShowInvalidLines = false;
            this.txtTableDependency.ShowSpaces = true;
            this.txtTableDependency.ShowTabs = true;
            this.txtTableDependency.ShowVRuler = true;
            this.txtTableDependency.Size = new System.Drawing.Size(552, 440);
            this.txtTableDependency.TabIndex = 3;
            // 
            // tpTableCacheDependency
            // 
            this.tpTableCacheDependency.Controls.Add(this.txtTableCacheDependency);
            this.tpTableCacheDependency.Location = new System.Drawing.Point(4, 58);
            this.tpTableCacheDependency.Name = "tpTableCacheDependency";
            this.tpTableCacheDependency.Size = new System.Drawing.Size(552, 422);
            this.tpTableCacheDependency.TabIndex = 9;
            this.tpTableCacheDependency.Text = "TableCacheDependency";
            this.tpTableCacheDependency.UseVisualStyleBackColor = true;
            // 
            // txtTableCacheDependency
            // 
            this.txtTableCacheDependency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTableCacheDependency.Encoding = ((System.Text.Encoding)(resources.GetObject("txtTableCacheDependency.Encoding")));
            this.txtTableCacheDependency.Location = new System.Drawing.Point(0, 0);
            this.txtTableCacheDependency.Name = "txtTableCacheDependency";
            this.txtTableCacheDependency.ShowEOLMarkers = true;
            this.txtTableCacheDependency.ShowInvalidLines = false;
            this.txtTableCacheDependency.ShowSpaces = true;
            this.txtTableCacheDependency.ShowTabs = true;
            this.txtTableCacheDependency.ShowVRuler = true;
            this.txtTableCacheDependency.Size = new System.Drawing.Size(552, 440);
            this.txtTableCacheDependency.TabIndex = 3;
            // 
            // tpDependencyAccess
            // 
            this.tpDependencyAccess.Controls.Add(this.txtDependencyAccess);
            this.tpDependencyAccess.Location = new System.Drawing.Point(4, 58);
            this.tpDependencyAccess.Name = "tpDependencyAccess";
            this.tpDependencyAccess.Size = new System.Drawing.Size(552, 422);
            this.tpDependencyAccess.TabIndex = 11;
            this.tpDependencyAccess.Text = "DependencyAccess";
            this.tpDependencyAccess.UseVisualStyleBackColor = true;
            // 
            // txtDependencyAccess
            // 
            this.txtDependencyAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDependencyAccess.Encoding = ((System.Text.Encoding)(resources.GetObject("txtDependencyAccess.Encoding")));
            this.txtDependencyAccess.Location = new System.Drawing.Point(0, 0);
            this.txtDependencyAccess.Name = "txtDependencyAccess";
            this.txtDependencyAccess.ShowEOLMarkers = true;
            this.txtDependencyAccess.ShowInvalidLines = false;
            this.txtDependencyAccess.ShowSpaces = true;
            this.txtDependencyAccess.ShowTabs = true;
            this.txtDependencyAccess.ShowVRuler = true;
            this.txtDependencyAccess.Size = new System.Drawing.Size(552, 440);
            this.txtDependencyAccess.TabIndex = 3;
            // 
            // tpDependencyFacade
            // 
            this.tpDependencyFacade.Controls.Add(this.txtDependencyFacade);
            this.tpDependencyFacade.Location = new System.Drawing.Point(4, 58);
            this.tpDependencyFacade.Name = "tpDependencyFacade";
            this.tpDependencyFacade.Size = new System.Drawing.Size(552, 422);
            this.tpDependencyFacade.TabIndex = 12;
            this.tpDependencyFacade.Text = "DependencyFacade";
            this.tpDependencyFacade.UseVisualStyleBackColor = true;
            // 
            // txtDependencyFacade
            // 
            this.txtDependencyFacade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDependencyFacade.Encoding = ((System.Text.Encoding)(resources.GetObject("txtDependencyFacade.Encoding")));
            this.txtDependencyFacade.Location = new System.Drawing.Point(0, 0);
            this.txtDependencyFacade.Name = "txtDependencyFacade";
            this.txtDependencyFacade.ShowEOLMarkers = true;
            this.txtDependencyFacade.ShowInvalidLines = false;
            this.txtDependencyFacade.ShowSpaces = true;
            this.txtDependencyFacade.ShowTabs = true;
            this.txtDependencyFacade.ShowVRuler = true;
            this.txtDependencyFacade.Size = new System.Drawing.Size(552, 440);
            this.txtDependencyFacade.TabIndex = 4;
            // 
            // tpBL
            // 
            this.tpBL.Controls.Add(this.txtBL);
            this.tpBL.Location = new System.Drawing.Point(4, 58);
            this.tpBL.Name = "tpBL";
            this.tpBL.Size = new System.Drawing.Size(552, 422);
            this.tpBL.TabIndex = 4;
            this.tpBL.Text = "BL";
            this.tpBL.UseVisualStyleBackColor = true;
            // 
            // txtBL
            // 
            this.txtBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBL.Encoding = ((System.Text.Encoding)(resources.GetObject("txtBL.Encoding")));
            this.txtBL.Location = new System.Drawing.Point(0, 0);
            this.txtBL.Name = "txtBL";
            this.txtBL.ShowEOLMarkers = true;
            this.txtBL.ShowInvalidLines = false;
            this.txtBL.ShowSpaces = true;
            this.txtBL.ShowTabs = true;
            this.txtBL.ShowVRuler = true;
            this.txtBL.Size = new System.Drawing.Size(552, 440);
            this.txtBL.TabIndex = 1;
            // 
            // tpUserControl
            // 
            this.tpUserControl.Controls.Add(this.txtUserControl);
            this.tpUserControl.Location = new System.Drawing.Point(4, 58);
            this.tpUserControl.Name = "tpUserControl";
            this.tpUserControl.Size = new System.Drawing.Size(552, 422);
            this.tpUserControl.TabIndex = 6;
            this.tpUserControl.Text = "UserControl.ascx";
            this.tpUserControl.UseVisualStyleBackColor = true;
            // 
            // txtUserControl
            // 
            this.txtUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserControl.Encoding = ((System.Text.Encoding)(resources.GetObject("txtUserControl.Encoding")));
            this.txtUserControl.Location = new System.Drawing.Point(0, 0);
            this.txtUserControl.Name = "txtUserControl";
            this.txtUserControl.ShowEOLMarkers = true;
            this.txtUserControl.ShowInvalidLines = false;
            this.txtUserControl.ShowSpaces = true;
            this.txtUserControl.ShowTabs = true;
            this.txtUserControl.ShowVRuler = true;
            this.txtUserControl.Size = new System.Drawing.Size(552, 422);
            this.txtUserControl.TabIndex = 2;
            // 
            // tpUserControlDesignerCs
            // 
            this.tpUserControlDesignerCs.Controls.Add(this.txtUserControlDesignerCs);
            this.tpUserControlDesignerCs.Location = new System.Drawing.Point(4, 58);
            this.tpUserControlDesignerCs.Name = "tpUserControlDesignerCs";
            this.tpUserControlDesignerCs.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserControlDesignerCs.Size = new System.Drawing.Size(552, 422);
            this.tpUserControlDesignerCs.TabIndex = 13;
            this.tpUserControlDesignerCs.Text = "UserControl.ascx.designer.cs";
            this.tpUserControlDesignerCs.UseVisualStyleBackColor = true;
            // 
            // txtUserControlDesignerCs
            // 
            this.txtUserControlDesignerCs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserControlDesignerCs.Encoding = ((System.Text.Encoding)(resources.GetObject("txtUserControlDesignerCs.Encoding")));
            this.txtUserControlDesignerCs.Location = new System.Drawing.Point(3, 3);
            this.txtUserControlDesignerCs.Name = "txtUserControlDesignerCs";
            this.txtUserControlDesignerCs.ShowEOLMarkers = true;
            this.txtUserControlDesignerCs.ShowInvalidLines = false;
            this.txtUserControlDesignerCs.ShowSpaces = true;
            this.txtUserControlDesignerCs.ShowTabs = true;
            this.txtUserControlDesignerCs.ShowVRuler = true;
            this.txtUserControlDesignerCs.Size = new System.Drawing.Size(546, 416);
            this.txtUserControlDesignerCs.TabIndex = 3;
            // 
            // tpUserControlCs
            // 
            this.tpUserControlCs.Controls.Add(this.txtUserControlCs);
            this.tpUserControlCs.Location = new System.Drawing.Point(4, 58);
            this.tpUserControlCs.Name = "tpUserControlCs";
            this.tpUserControlCs.Size = new System.Drawing.Size(552, 422);
            this.tpUserControlCs.TabIndex = 7;
            this.tpUserControlCs.Text = "UserControl.ascx.cs";
            this.tpUserControlCs.UseVisualStyleBackColor = true;
            // 
            // txtUserControlCs
            // 
            this.txtUserControlCs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserControlCs.Encoding = ((System.Text.Encoding)(resources.GetObject("txtUserControlCs.Encoding")));
            this.txtUserControlCs.Location = new System.Drawing.Point(0, 0);
            this.txtUserControlCs.Name = "txtUserControlCs";
            this.txtUserControlCs.ShowEOLMarkers = true;
            this.txtUserControlCs.ShowInvalidLines = false;
            this.txtUserControlCs.ShowSpaces = true;
            this.txtUserControlCs.ShowTabs = true;
            this.txtUserControlCs.ShowVRuler = true;
            this.txtUserControlCs.Size = new System.Drawing.Size(552, 422);
            this.txtUserControlCs.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tcCodes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.styleUserControl1);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(928, 484);
            this.splitContainer1.SplitterDistance = 600;
            this.splitContainer1.TabIndex = 42;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnSaveAll, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveCurrentTab, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 77);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // styleUserControl1
            // 
            this.styleUserControl1.DB = null;
            this.styleUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.styleUserControl1.Location = new System.Drawing.Point(0, 77);
            this.styleUserControl1.Name = "styleUserControl1";
            this.styleUserControl1.Size = new System.Drawing.Size(324, 407);
            this.styleUserControl1.TabIndex = 40;
            // 
            // FormCodeCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 519);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormCodeCreate";
            this.TabText = "生成代码";
            this.Text = "frmCode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCodeCreate_FormClosing);
            this.tcCodes.ResumeLayout(false);
            this.tpInternalModel.ResumeLayout(false);
            this.tpModel.ResumeLayout(false);
            this.tpIDAL.ResumeLayout(false);
            this.tpInternalDAL.ResumeLayout(false);
            this.tpDAL.ResumeLayout(false);
            this.tpDALFactory.ResumeLayout(false);
            this.tpICacheDependency.ResumeLayout(false);
            this.tpTableDependency.ResumeLayout(false);
            this.tpTableCacheDependency.ResumeLayout(false);
            this.tpDependencyAccess.ResumeLayout(false);
            this.tpDependencyFacade.ResumeLayout(false);
            this.tpBL.ResumeLayout(false);
            this.tpUserControl.ResumeLayout(false);
            this.tpUserControlDesignerCs.ResumeLayout(false);
            this.tpUserControlCs.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCodes;
        private System.Windows.Forms.TabPage tpModel;
        private ICSharpCode.TextEditor.TextEditorControl txtModel;
        private System.Windows.Forms.TabPage tpIDAL;
        private ICSharpCode.TextEditor.TextEditorControl txtIDAL;
        private System.Windows.Forms.TabPage tpDAL;
        private ICSharpCode.TextEditor.TextEditorControl txtDAL;
        private System.Windows.Forms.TabPage tpDALFactory;
        private ICSharpCode.TextEditor.TextEditorControl txtDALFactory;
        private System.Windows.Forms.TabPage tpBL;
        private ICSharpCode.TextEditor.TextEditorControl txtBL;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabPage tpUserControl;
        private ICSharpCode.TextEditor.TextEditorControl txtUserControl;
        private System.Windows.Forms.TabPage tpUserControlCs;
        private ICSharpCode.TextEditor.TextEditorControl txtUserControlCs;
        private System.Windows.Forms.TabPage tpICacheDependency;
        private ICSharpCode.TextEditor.TextEditorControl txtICacheDependency;
        private System.Windows.Forms.TabPage tpTableDependency;
        private ICSharpCode.TextEditor.TextEditorControl txtTableDependency;
        private System.Windows.Forms.TabPage tpTableCacheDependency;
        private ICSharpCode.TextEditor.TextEditorControl txtTableCacheDependency;
        private System.Windows.Forms.TabPage tpDependencyAccess;
        private ICSharpCode.TextEditor.TextEditorControl txtDependencyAccess;
        private System.Windows.Forms.TabPage tpDependencyFacade;
        private ICSharpCode.TextEditor.TextEditorControl txtDependencyFacade;
        private System.Windows.Forms.Button btnSaveCurrentTab;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tpUserControlDesignerCs;
        private ICSharpCode.TextEditor.TextEditorControl txtUserControlDesignerCs;
        private StyleUserControl styleUserControl1;
        private System.Windows.Forms.TabPage tpInternalModel;
        private System.Windows.Forms.TabPage tpInternalDAL;
        private ICSharpCode.TextEditor.TextEditorControl txtInternalModel;
        private ICSharpCode.TextEditor.TextEditorControl txtInternalDAL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}