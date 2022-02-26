using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 展示姓名控件
    /// </summary>
    public partial class NameControl : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DeleteEventHandler(string ID);
        public event DeleteEventHandler OnDeleteItem;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NameControl()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OnDeleteItem != null)
            {
                if (this.lblInfo.Tag != null)
                {
                    OnDeleteItem(this.lblInfo.Tag.ToString());
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        public void BindData(string ID, string Name)
        {
            this.lblInfo.Text = Name;
            this.lblInfo.Tag = ID;

            this.btnDelete.Tag = ID;
        }
    }
}
