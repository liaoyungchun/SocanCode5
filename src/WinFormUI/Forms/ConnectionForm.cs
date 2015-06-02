using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SocanCode
{
    public partial class ConnectionForm : Form
    {
        /// <summary>
        /// Ҫ���ص����ݿ�
        /// </summary>
        public Model.Database database;

        public ConnectionForm()
        {
            InitializeComponent();
            LoadConnectionFromFile();
        }

        /// <summary>
        /// ��ȡ��ǰҪ���ӵ����ݿ����ͺ��������
        /// </summary>
        private Fabrics.Schema GetSchema()
        {
            Fabrics.Schema schema = null;
            switch (tcDatabase.SelectedIndex)
            {
                case 0:
                    schema = new Fabrics.Schema(accessConn1.ConnectionString, Model.Database.DatabaseType.Access);
                    break;
                case 2:
                    schema = new Fabrics.Schema(mySqlConn1.ConnectionString, Model.Database.DatabaseType.MySql);
                    break;
                case 1:
                default:
                    schema = new Fabrics.Schema();
                    schema.ConnectionString = sqlConn1.ConnectionString;

                    if (sqlConn1.IsSql2005)
                        schema.Type = Model.Database.DatabaseType.Sql2005;
                    else
                        schema.Type = Model.Database.DatabaseType.Sql2000;
                    break;
            }
            return schema;
        }

        /// <summary>
        /// �������ļ��м������ݿ�����
        /// </summary>
        private void LoadConnectionFromFile()
        {
            try
            {
                Properties.Settings settings = new SocanCode.Properties.Settings();
                if (!string.IsNullOrEmpty(settings.AccessConn))
                {
                    accessConn1.ConnectionString = settings.AccessConn;
                }
                if (!string.IsNullOrEmpty(settings.SqlServerConn))
                {
                    sqlConn1.ConnectionString = settings.SqlServerConn;
                    sqlConn1.IsSql2005 = settings.SqlServerType;
                }
                if (!string.IsNullOrEmpty(settings.MySqlConn))
                {
                    mySqlConn1.ConnectionString = settings.MySqlConn;
                }
                tcDatabase.SelectedIndex = settings.DbType;
            }
            catch
            {
                // ����ʧ���������
            }
        }

        /// <summary>
        /// �������ݿ����ӵ��û�����
        /// </summary>
        private void SaveConnectionToFile()
        {
            Properties.Settings setting = new SocanCode.Properties.Settings();
            setting.DbType = tcDatabase.SelectedIndex;
            setting.AccessConn = accessConn1.ConnectionString;
            setting.SqlServerType = sqlConn1.IsSql2005;
            setting.SqlServerConn = sqlConn1.ConnectionString;
            setting.MySqlConn = mySqlConn1.ConnectionString;
            setting.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            btnConnect.Enabled = false;
            Fabrics.Schema schema = GetSchema();
            backgroundWorker1.RunWorkerAsync(schema);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Fabrics.Schema schema = e.Argument as Fabrics.Schema;
            Model.Database database = schema.GetSchema();

            if (backgroundWorker1.CancellationPending)
                e.Cancel = true;
            else
                e.Result = database;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
            btnConnect.Enabled = true;

            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("����ʧ�ܣ�����" + e.Error.Message, "ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (e.Result == null)
            {
                MessageBox.Show("����ʧ�ܣ�", "ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            database = e.Result as Model.Database;
            SaveConnectionToFile();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}