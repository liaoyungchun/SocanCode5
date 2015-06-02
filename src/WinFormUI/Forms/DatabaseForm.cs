using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SocanCode
{
    public partial class DatabaseForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        ContextMenuStrip _cms = null;

        public DatabaseForm(ContextMenuStrip cms)
        {
            InitializeComponent();
            _cms = cms;
        }

        /// <summary>
        /// �����ݿ���ʾ������
        /// </summary>
        private void ShowDatabase(Model.Database database, TreeNode nodeRoot)
        {
            TreeNode nodeTables = new TreeNode("��", 1, 1);
            nodeRoot.Nodes.Add(nodeTables);
            ShowTables(database.Tables, nodeTables);

            TreeNode nodeViews = new TreeNode("��ͼ", 1, 1);
            nodeRoot.Nodes.Add(nodeViews);
            ShowTables(database.Views, nodeViews);

            TreeNode tnStoreProcedures = new TreeNode("�洢����", 1, 1);
            nodeRoot.Nodes.Add(tnStoreProcedures);
            foreach (string storeProcedure in database.StoreProcedures)
            {
                tnStoreProcedures.Nodes.Add(new TreeNode(storeProcedure, 5, 5));
            }
        }

        /// <summary>
        /// �����б���ʾ������
        /// </summary>
        private void ShowTables(List<Model.Table> tables, TreeNode nodeRoot)
        {
            foreach (Model.Table table in tables)
            {
                TreeNode nodeTable = new TreeNode(table.Name, 2, 2);
                nodeTable.Tag = table;
                nodeRoot.Nodes.Add(nodeTable);

                ShowTable(table, nodeTable);
            }
        }

        /// <summary>
        /// ��һ������ʾ������
        /// </summary>
        private void ShowTable(Model.Table table, TreeNode nodeRoot)
        {
            foreach (Model.Field field in table.Fields)
            {
                TreeNode nodeField;
                nodeField = new TreeNode(field.FieldName + ":" + field.DbType.ToString(), 3, 3);
                nodeRoot.Nodes.Add(nodeField);
            }
        }

        /// <summary>
        /// �Ҽ��˵����ݽڵ���ʾ������
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // ȫ���Ҽ��˵�����
            foreach (ToolStripItem menu in contextMenuStrip1.Items)
            {
                menu.Visible = false;
            }

            if (tvDatabase.SelectedNode != null)
            {
                switch (tvDatabase.SelectedNode.Level)
                {
                    case 0: //���ָ�����ݿ�
                        menuOutput.Visible = true;
                        menuDeleteDatabase.Visible = true;

                        switch ((tvDatabase.SelectedNode.Tag as Model.Database).Type)
                        {
                            case Model.Database.DatabaseType.Sql2000:
                            case Model.Database.DatabaseType.Sql2005:
                                menuEnableDatabaseForSqlCacheDependency.Visible = true;
                                menuDisableDatabaseForSqlCacheDependency.Visible = true;
                                break;
                            case Model.Database.DatabaseType.MySql:
                            case Model.Database.DatabaseType.Access:
                            default:
                                break;
                        }
                        break;
                    case 1: //���ָ�����ͼ���洢���̵��ļ��нڵ�
                        if (tvDatabase.SelectedNode.Index == 0)
                        {
                            switch ((tvDatabase.SelectedNode.Parent.Tag as Model.Database).Type)
                            {
                                case Model.Database.DatabaseType.Sql2000:
                                case Model.Database.DatabaseType.Sql2005:
                                    menuEnableAllTablesForSqlCacheDependency.Visible = true;
                                    menuDisableAllTablesForSqlCacheDependency.Visible = true;
                                    break;
                                case Model.Database.DatabaseType.Access:
                                case Model.Database.DatabaseType.MySql:
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        //���ָ���
                        int parentIndex = tvDatabase.SelectedNode.Parent.Index;
                        if (parentIndex == 0 || parentIndex == 1)
                        {
                            menuCreateCode.Visible = true;

                            switch ((tvDatabase.SelectedNode.Parent.Parent.Tag as Model.Database).Type)
                            {
                                case Model.Database.DatabaseType.Sql2000:
                                case Model.Database.DatabaseType.Sql2005:
                                    if (parentIndex == 0)
                                    {
                                        menuEnableTableForSqlCacheDependency.Visible = true;
                                        menuDisableTableForSqlCacheDependency.Visible = true;
                                    }
                                    menuCreateStoreProcedure.Visible = true;
                                    break;
                                case Model.Database.DatabaseType.MySql:
                                    menuCreateStoreProcedure.Visible = true;
                                    break;
                                case Model.Database.DatabaseType.Access:
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void menuOutput_Click(object sender, EventArgs e)
        {
            if (tvDatabase.SelectedNode != null && tvDatabase.SelectedNode.Level == 0)
            {
                CodeOutputForm frm = new CodeOutputForm(_cms, tvDatabase.SelectedNode.Tag as Model.Database);
                frm.Show(MainForm.dockPanel);
            }
            else
            {
                MainForm.ShowMessage("����ѡ��һ�����ݿ�");
            }
        }

        public void menuCreateCode_Click(object sender, EventArgs e)
        {
            if (tvDatabase.SelectedNode != null && tvDatabase.SelectedNode.Level == 2 && tvDatabase.SelectedNode.Parent.Index != 2)
            {
                string dbName = tvDatabase.SelectedNode.Parent.Parent.Text;
                Model.Database db = tvDatabase.SelectedNode.Parent.Parent.Tag as Model.Database;
                Model.Table table = tvDatabase.SelectedNode.Tag as Model.Table;
                if (!table.HasConditonRow)
                {
                    MainForm.ShowMessage("�ñ������κ��ֶΣ��޷����ɣ�");
                    return;
                }

                CodeCreateForm frm = new CodeCreateForm(_cms, db, table);
                frm.Show(MainForm.dockPanel);
            }
            else
            {
                MainForm.ShowMessage("����ѡ��һ����");
            }
        }

        public void menuCreateStoreProcedure_Click(object sender, EventArgs e)
        {
            if (tvDatabase.SelectedNode != null && tvDatabase.SelectedNode.Level == 2 && tvDatabase.SelectedNode.Parent.Index != 2)
            {
                if ((tvDatabase.SelectedNode.Parent.Parent.Tag as Model.Database).Type == Model.Database.DatabaseType.Access)
                {
                    MessageBox.Show("�����ݿⲻ֧�ִ洢���̣�");
                    return;
                }

                Model.Table table = tvDatabase.SelectedNode.Tag as Model.Table;
                if (!table.HasConditonRow)
                {
                    MainForm.ShowMessage("�ñ������κ��ֶΣ��޷����ɣ�");
                    return;
                }

                Model.CodeStyle style = new Model.CodeStyle();
                style.FilterFieldOnDALAdd = MessageBox.Show("�Ƿ������Ĭ��ֵ���ֶΣ�", "��ʾ",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,
                    Properties.Settings.Default.FilterFieldOnDALAdd ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2) == DialogResult.Yes;

                string storeProcedure;
                switch ((tvDatabase.SelectedNode.Parent.Parent.Tag as Model.Database).Type)
                {
                    case Model.Database.DatabaseType.Sql2000:
                    case Model.Database.DatabaseType.Sql2005:
                        storeProcedure = Codes.SqlStoredProcedureCode.GetSqlStoredProcedureCode(table, style);
                        break;
                    case Model.Database.DatabaseType.MySql:
                        storeProcedure = Codes.MySqlStoreProcedureCode.GetMySqlStoreProcedureCode(table, style);
                        break;
                    case Model.Database.DatabaseType.Access:
                    default:
                        return;
                }
                CodeViewForm frm = new CodeViewForm(_cms, tvDatabase.SelectedNode.Text + "�Ĵ洢����", storeProcedure, "TSQL");
                frm.Show(MainForm.dockPanel);
            }
            else
            {
                MainForm.ShowMessage("����ѡ��һ����");
            }
        }

        private void btnAddDatabase_Click(object sender, EventArgs e)
        {
            ConnectionForm frm = new ConnectionForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                TreeNode nodeDatabase = new TreeNode(frm.database.DbName, 0, 0);
                nodeDatabase.Tag = frm.database;
                this.tvDatabase.Nodes.Add(nodeDatabase);
                ShowDatabase(frm.database, nodeDatabase);
            }
        }

        #region ----------- ����/���û������� ---------------

        private void menuEnableDatabaseForSqlCacheDependency_Click(object sender, EventArgs e)
        {
            CodeUtility.RegSqlCode.EnableDatabaseForSqlCacheDependency(tvDatabase.SelectedNode.Tag as Model.Database);
        }

        private void menuDisableDatabaseForSqlCacheDependency_Click(object sender, EventArgs e)
        {
            CodeUtility.RegSqlCode.DisableDatabaseForSqlCacheDependency(tvDatabase.SelectedNode.Tag as Model.Database);
        }

        private void menuEnableAllTablesForSqlCacheDependency_Click(object sender, EventArgs e)
        {
            CodeUtility.RegSqlCode.EnableAllTablesForSqlCacheDependency(tvDatabase.SelectedNode.Parent.Tag as Model.Database);
        }

        private void menuDisableAllTablesForSqlCacheDependency_Click(object sender, EventArgs e)
        {
            CodeUtility.RegSqlCode.DisableAllTablesForSqlCacheDependency(tvDatabase.SelectedNode.Parent.Tag as Model.Database);
        }

        private void menuEnableTableForSqlCacheDependency_Click(object sender, EventArgs e)
        {
            CodeUtility.RegSqlCode.EnableTableForSqlCacheDependency(tvDatabase.SelectedNode.Parent.Parent.Tag as Model.Database, tvDatabase.SelectedNode.Tag as Model.Table);
        }

        private void menuDisableTableForSqlCacheDependency_Click(object sender, EventArgs e)
        {
            CodeUtility.RegSqlCode.DisableTableForSqlCacheDependency(tvDatabase.SelectedNode.Parent.Parent.Tag as Model.Database, tvDatabase.SelectedNode.Tag as Model.Table);
        }

        #endregion

        /// <summary>
        /// �õ�TreeView�����ָ��Ľڵ�,ͬʱ�Ѹýڵ�����Ϊ��ǰѡ�еĽڵ�
        /// </summary>
        public TreeNode GetMouseNode(TreeView tv, Control currentForm)
        {
            Point pt = currentForm.PointToScreen(tv.Location);
            Point p = new Point(Control.MousePosition.X - pt.X, Control.MousePosition.Y - pt.Y);
            TreeNode tn = tv.GetNodeAt(p);
            return tn;
        }

        private void tvDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tn = GetMouseNode(tvDatabase, this);
            if (tn != null)
            {
                tvDatabase.SelectedNode = tn;
            }
        }

        private void menuDeleteDatabase_Click(object sender, EventArgs e)
        {
            if (tvDatabase.SelectedNode != null && tvDatabase.SelectedNode.Level == 0)
            {
                tvDatabase.Nodes.Remove(tvDatabase.SelectedNode);
                tvDatabase_AfterSelect(null, null); // �����ٴ��ж�ɾ����ť�Ŀ�����
            }
        }

        private void tvDatabase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // ����ѡ�еĽڵ��жϡ�ɾ������ť�Ƿ����
            btnDelete.Enabled = tvDatabase.SelectedNode != null && tvDatabase.SelectedNode.Level == 0;
        }
    }
}