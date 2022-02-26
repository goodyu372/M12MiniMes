using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WHC.Pager.WinControl
{
    /// <summary>
    /// ��ӡ����
    /// </summary>
    public partial class PrintOptions : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// ��ӡ���ù��캯��
        /// </summary>
        public PrintOptions()
        {
            InitializeComponent();
        }

        public PrintOptions(List<string> availableFields)
        {
            InitializeComponent();

            foreach (string field in availableFields)
            {
                chklst.Items.Add(field, true);
            }
        }

        /// <summary>
        /// ����ѡ����Ŀ
        /// </summary>
        /// <param name="items"></param>
        public void SetCheckedItems(string[] items)
        {
            for (int i = 0; i < this.chklst.Items.Count; i++)
            {
                this.chklst.SetItemChecked(i, false);
                foreach (string item in items)
                {
                    if (item == this.chklst.Items[i].ToString())
                    {
                        this.chklst.SetItemChecked(i, true);
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�û�ѡ������Ŀ����
        /// </summary>
        /// <returns></returns>
        public List<string> GetCheckItems()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < this.chklst.Items.Count; i++)
            {
                if (this.chklst.GetItemChecked(i))
                {
                    list.Add(this.chklst.Items[i].ToString());
                }
            }
            return list;
        }

        private void PrintOtions_Load(object sender, EventArgs e)
        {
            // Initialize some controls
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true; 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// ��ȡѡ����
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedColumns()
        {
            List<string> lst = new List<string>();
            foreach (object item in chklst.CheckedItems)
            {
                lst.Add(item.ToString());
            }
            return lst;
        }

        /// <summary>
        /// ��ӡҳ����
        /// </summary>
        public string PrintTitle
        {
            get { return txtTitle.Text; }
            set { this.txtTitle.Text = value; }
        }

        /// <summary>
        /// ��ӡ������
        /// </summary>
        public bool PrintAllRows
        {
            get { return rdoAllRows.Checked; }
        }

        /// <summary>
        /// ��Ӧҳ����
        /// </summary>
        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chklst.Items.Count; i++)
            {
                this.chklst.SetItemChecked(i, this.chkSelectAll.Checked);
            }
        }

        private void PrintOptions_Shown(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                LanguageHelper.InitLanguage(this);
            }
        }

    }
}