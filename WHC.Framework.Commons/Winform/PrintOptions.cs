using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// ��ӡѡ��
    /// </summary>
    public partial class PrintOptions : Form
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PrintOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="availableFields">�ɼ��ֶ��б�</param>
        public PrintOptions(List<string> availableFields)
        {
            InitializeComponent();

            foreach (string field in availableFields)
                     chklst.Items.Add(field, true);
        }

        private void PrintOtions_Load(object sender, EventArgs e)
        {
            // ��ʼ��
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true; 
            this.txtTitle.Text = this.PrintTitle;

            //������֧��
            if (!this.DesignMode)
            {
                LanguageHelper.InitLanguage(this);
            }
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
        /// ��ȡѡ�����
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedColumns()
        {
            List<string> lst = new List<string>();
            foreach (object item in chklst.CheckedItems)
                    lst.Add(item.ToString());
            return lst;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        public string PrintTitle
        {
            get { return txtTitle.Text; }
            set { this.txtTitle.Text = value;}
        }

        /// <summary>
        /// �Ƿ��ӡ������
        /// </summary>
        public bool PrintAllRows
        {
            get { return rdoAllRows.Checked; }
        }

        /// <summary>
        /// �Ƿ��ʿ�
        /// </summary>
        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
        }

    }
}