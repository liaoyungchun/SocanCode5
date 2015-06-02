using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SocanCode
{
    public partial class RegexForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public RegexForm()
        {
            InitializeComponent();
        }

        private MainForm _mainForm;
        public RegexForm(MainForm mainForm, ContextMenuStrip cms)
        {
            InitializeComponent();
            _mainForm = mainForm;
            this.TabPageContextMenuStrip = cms;
        }

        private void ContainButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string r = btn.Text.Replace("...", txtRegex.SelectedText);
            txtRegex.SelectedText = r;
            txtRegex.Focus();
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            txtRegex.SelectedText = btn.Text.Replace(" ", "");
            txtRegex.Focus();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtRegex.Text);
                labTestResult.Text = "复制成功";
                labTestResult.BackColor = Color.Green;
            }
            catch (Exception)
            {
                labTestResult.Text = "复制失败";
                labTestResult.BackColor = Color.Red;
            }
            labTestResult.Visible = true;
        }

        private void txtRegex_TextChanged(object sender, EventArgs e)
        {
            labTestResult.Visible = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtTest.Text, txtRegex.Text))
                {
                    labTestResult.Text = "测试成功";
                    labTestResult.BackColor = Color.Green;
                }
                else
                {
                    labTestResult.Text = "测试失败";
                    labTestResult.BackColor = Color.Red;
                }
                labTestResult.Visible = true;
            }
            catch (Exception)
            {
                labTestResult.Visible = false;
                MessageBox.Show("正则表达式错误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labRegexTester_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.OpenUrl("http://deerchao.net/tutorials/regex/downloads/RegexTester.zip");
        }
    }
}
