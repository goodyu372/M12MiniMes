using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using WHC.Framework.BaseUI;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// 附件管理界面
    /// </summary>
    public partial class FrmAttachment : BaseForm
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAttachment()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.bizAttachment.UserId = UserId;
                this.bizAttachment.AttachmentDirectory = "业务附件";
                this.bizAttachment.BindData();

                this.MyAttachment.UserId = UserId;
                this.MyAttachment.AttachmentDirectory = "个人附件";
                this.MyAttachment.BindData();
            }
        }

        private void MyAttachment_Load(object sender, EventArgs e)
        {

        }
    }
}