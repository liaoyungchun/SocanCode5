namespace SocanCode
{
    partial class CodeOutputForm
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnOutputCode = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radVS2010 = new System.Windows.Forms.RadioButton();
            this.radVS2008 = new System.Windows.Forms.RadioButton();
            this.radVS2005 = new System.Windows.Forms.RadioButton();
            this.btnOutputSqlStoreProcedure = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.styleUserControl1 = new SocanCode.StyleUserControl();
            this.selectTableUserControl1 = new SocanCode.SelectTableUserControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(20, 32);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(437, 21);
            this.txtPath.TabIndex = 0;
            this.txtPath.Text = "C:\\Socansoft";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSelectPath.Location = new System.Drawing.Point(463, 32);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(34, 23);
            this.btnSelectPath.TabIndex = 1;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // btnOutputCode
            // 
            this.btnOutputCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOutputCode.Location = new System.Drawing.Point(718, 30);
            this.btnOutputCode.Name = "btnOutputCode";
            this.btnOutputCode.Size = new System.Drawing.Size(75, 23);
            this.btnOutputCode.TabIndex = 2;
            this.btnOutputCode.Text = "输出代码";
            this.btnOutputCode.UseVisualStyleBackColor = true;
            this.btnOutputCode.Click += new System.EventHandler(this.btnOutputCode_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 521);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(917, 19);
            this.progressBar1.TabIndex = 21;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radVS2010);
            this.groupBox4.Controls.Add(this.radVS2008);
            this.groupBox4.Controls.Add(this.radVS2005);
            this.groupBox4.Controls.Add(this.txtPath);
            this.groupBox4.Controls.Add(this.btnOutputSqlStoreProcedure);
            this.groupBox4.Controls.Add(this.btnOutputCode);
            this.groupBox4.Controls.Add(this.btnSelectPath);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(917, 77);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "输出路径";
            // 
            // radVS2010
            // 
            this.radVS2010.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radVS2010.AutoSize = true;
            this.radVS2010.Location = new System.Drawing.Point(649, 35);
            this.radVS2010.Name = "radVS2010";
            this.radVS2010.Size = new System.Drawing.Size(59, 16);
            this.radVS2010.TabIndex = 3;
            this.radVS2010.Tag = "2010";
            this.radVS2010.Text = "VS2010";
            this.radVS2010.UseVisualStyleBackColor = true;
            // 
            // radVS2008
            // 
            this.radVS2008.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radVS2008.AutoSize = true;
            this.radVS2008.Location = new System.Drawing.Point(584, 35);
            this.radVS2008.Name = "radVS2008";
            this.radVS2008.Size = new System.Drawing.Size(59, 16);
            this.radVS2008.TabIndex = 3;
            this.radVS2008.Tag = "2008";
            this.radVS2008.Text = "VS2008";
            this.radVS2008.UseVisualStyleBackColor = true;
            // 
            // radVS2005
            // 
            this.radVS2005.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radVS2005.AutoSize = true;
            this.radVS2005.Checked = true;
            this.radVS2005.Location = new System.Drawing.Point(516, 35);
            this.radVS2005.Name = "radVS2005";
            this.radVS2005.Size = new System.Drawing.Size(59, 16);
            this.radVS2005.TabIndex = 3;
            this.radVS2005.TabStop = true;
            this.radVS2005.Tag = "2005";
            this.radVS2005.Text = "VS2005";
            this.radVS2005.UseVisualStyleBackColor = true;
            // 
            // btnOutputSqlStoreProcedure
            // 
            this.btnOutputSqlStoreProcedure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOutputSqlStoreProcedure.Location = new System.Drawing.Point(799, 30);
            this.btnOutputSqlStoreProcedure.Name = "btnOutputSqlStoreProcedure";
            this.btnOutputSqlStoreProcedure.Size = new System.Drawing.Size(106, 23);
            this.btnOutputSqlStoreProcedure.TabIndex = 2;
            this.btnOutputSqlStoreProcedure.Tag = "";
            this.btnOutputSqlStoreProcedure.Text = "输出存储过程";
            this.btnOutputSqlStoreProcedure.UseVisualStyleBackColor = true;
            this.btnOutputSqlStoreProcedure.Click += new System.EventHandler(this.btnOutputStoreProcedure_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 444);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 77);
            this.panel1.TabIndex = 37;
            // 
            // styleUserControl1
            // 
            this.styleUserControl1.DB = null;
            this.styleUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.styleUserControl1.Location = new System.Drawing.Point(0, 0);
            this.styleUserControl1.Name = "styleUserControl1";
            this.styleUserControl1.Size = new System.Drawing.Size(313, 444);
            this.styleUserControl1.TabIndex = 0;
            // 
            // selectTableUserControl1
            // 
            this.selectTableUserControl1.DB = null;
            this.selectTableUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectTableUserControl1.Location = new System.Drawing.Point(0, 0);
            this.selectTableUserControl1.Name = "selectTableUserControl1";
            this.selectTableUserControl1.Size = new System.Drawing.Size(600, 444);
            this.selectTableUserControl1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.selectTableUserControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.styleUserControl1);
            this.splitContainer1.Size = new System.Drawing.Size(917, 444);
            this.splitContainer1.SplitterDistance = 600;
            this.splitContainer1.TabIndex = 39;
            // 
            // FormCodeOutput
            // 
            this.AcceptButton = this.btnOutputCode;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 540);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormCodeOutput";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.TabText = "输出代码";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCodeOutput_FormClosing);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnOutputCode;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOutputSqlStoreProcedure;
        private System.Windows.Forms.RadioButton radVS2008;
        private System.Windows.Forms.RadioButton radVS2005;
        private StyleUserControl styleUserControl1;
        private SelectTableUserControl selectTableUserControl1;
        private System.Windows.Forms.RadioButton radVS2010;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

