using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using System.ComponentModel;

namespace SocanCode
{
    public partial class MainForm : Form
    {
        private const string HOME_URL = "http://www.Socansoft.com";
        private const string XML_URL = "http://www.socansoft.com/downloads/socancode/SocanCode.xml";
        private const string DOWNLOAD_URL = "http://www.socansoft.com/downloads/SocanCode/SocanCode.rar";

        public static WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private static DatabaseForm frmDatabase;
        public static StatusForm frmStatus;

        public MainForm()
        {
            InitializeComponent();
            labNewVersion.Tag = DOWNLOAD_URL;

            dockPanel = this.dockPanel1;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " V" + new AboutForm().AssemblyVersion;

            frmDatabase = new DatabaseForm(this.contextMenuStrip1);
            frmDatabase.Show(dockPanel);

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();

            OpenUrl(HOME_URL);
        }

        #region 版本检测
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 0;
            bool hasGetVersion = false;
            while (!hasGetVersion && count < 3)
            {
                try
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(XML_URL);
                    e.Result = xml;
                    return;
                }
                catch
                {
                    count++;
                }
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                labNewVersion.Text = "连接服务器失败!";
                labNewVersion.LinkColor = Color.Red;
                return;
            }

            XmlDocument xml = e.Result as XmlDocument;
            XmlNode display = xml.SelectSingleNode("DOCUMENT").SelectSingleNode("item").SelectSingleNode("display");
            Version lastVersion = new Version(display.SelectSingleNode("content2").InnerText);
            Version currVersion = Assembly.GetExecutingAssembly().GetName().Version;
            labNewVersion.Tag = display.SelectSingleNode("button").Attributes["buttonlink"].Value;

            if (lastVersion > currVersion)
            {
                labNewVersion.Text = "发现新版本 V" + lastVersion + ", 请点击此处下载";
                labNewVersion.LinkColor = Color.Red;
            }
            else
            {
                labNewVersion.Text = "当前版本已经是最新版本";
                labNewVersion.LinkColor = Color.Green;
            }
        }
        #endregion

        private void labNewVersion_Click(object sender, EventArgs e)
        {
            OpenUrl((sender as ToolStripStatusLabel).Tag.ToString());
        }

        private void menuWebsite_Click(object sender, EventArgs e)
        {
            OpenUrl(HOME_URL);
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        public static void ShowMessage(string msg)
        {
            frmStatus.txtMsg.Text = msg;
            frmStatus.Show(dockPanel);
        }

        private void menufrmDatabase_Click(object sender, EventArgs e)
        {
            frmDatabase.Show(dockPanel);
        }

        private void menuHtmlToJs_Click(object sender, EventArgs e)
        {
            HtmlToJsForm frm = new HtmlToJsForm(this.contextMenuStrip1);
            frm.Show(dockPanel);
        }

        private void menuRegexGen_Click(object sender, EventArgs e)
        {
            RegexForm frm = new RegexForm(this, this.contextMenuStrip1);
            frm.Show(dockPanel1);
        }

        #region 打开一个新链接
        private delegate void OpenUrlEventHandler(string url);
        public void OpenUrl(string url)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OpenUrlEventHandler(OpenUrlEvent), new object[] { url });
            }
            else
            {
                OpenUrlEvent(url);
            }
        }
        private void OpenUrlEvent(string url)
        {
            WebBrowser.BrowserForm frm = new WebBrowser.BrowserForm(this.contextMenuStrip1);
            frm.Show(dockPanel);
            frm.Go(url);
        }
        #endregion

        private void menuOutput_Click(object sender, EventArgs e)
        {
            frmDatabase.menuOutput_Click(null, null);
        }

        private void menuCreateCode_Click(object sender, EventArgs e)
        {
            frmDatabase.menuCreateCode_Click(null, null);
        }

        private void menuCreateStoreProcedure_Click(object sender, EventArgs e)
        {
            frmDatabase.menuCreateStoreProcedure_Click(null, null);
        }

        private void menuDBUtility_Click(object sender, EventArgs e)
        {
            Process.Start(Model.CreateStyle.CURRENT_PATH + "\\Template2005\\DBUtility");
        }

        #region 标签页上的右键菜单

        private void mnuClose_Click(object sender, EventArgs e)
        {
            this.dockPanel1.ActiveContent.DockHandler.Close();
        }

        private void mnuCloseOther_Click(object sender, EventArgs e)
        {
            List<DockContent> contents = new List<DockContent>();
            foreach (DockContent content in dockPanel1.Documents)
            {
                contents.Add(content);
            }

            foreach (DockContent content in contents)
            {
                if (content != this.dockPanel1.ActiveContent)
                {
                    content.Close();
                }
            }
        }

        private void mnuCloseAll_Click(object sender, EventArgs e)
        {
            mnuCloseOther_Click(sender, e);
            mnuClose_Click(sender, e);
        }

        #endregion
    }
}