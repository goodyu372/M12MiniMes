using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Attachment.Entity;
using WHC.Attachment.BLL;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Framework.Language;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// 附件控件
    /// </summary>
    public partial class AttachmentControl : DevExpress.XtraEditors.XtraUserControl
    {                
        /// <summary>
        /// 是否显示上传按钮
        /// </summary>
        public bool ShowUpload { get; set; }
        
        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool ShowDelete { get; set; }
                       
        /// <summary>
        /// 是否显示下载按钮
        /// </summary>
        public bool ShowDownload { get; set; }

        /// <summary>
        /// 附件组所属的记录ID，如属于某个主表记录的ID
        /// </summary>
        public string OwerId = "";

        /// <summary>
        /// 操作用户ID，当前登录用户
        /// </summary>
        public string userId = "";

        /// <summary>
        /// 设置附件的存储目录分类
        /// </summary>
        public string AttachmentDirectory = "业务附件";

        /// <summary>
        /// 设置附件组的GUID
        /// </summary>
        private string m_AttachmentGUID = Guid.NewGuid().ToString();

        /// <summary>
        /// 附件组的GUID
        /// </summary>
        [Browsable(true), Description("设置附件组的GUID"), DefaultValue("")]
        public string AttachmentGUID
        {
            get { return m_AttachmentGUID; }
            set
            {
                m_AttachmentGUID = value;
                BindData();
            }
        }

        private string m_TipsContent = "共有【{0}】个附件";

        /// <summary>
        /// 提示内容文本格式
        /// </summary>
        [Browsable(true), Description("提示内容文本格式")]
        public string TipsContent
        {
            get { return m_TipsContent; }
            set 
            {
                m_TipsContent = value;
                BindData();
            }
        }

        /// <summary>
        /// 初始化相关参数
        /// </summary>
        /// <param name="attachmentDir">设置附件的存储目录分类</param>
        /// <param name="owerId">附件组所属的记录ID，如属于某个主表记录的ID</param>
        /// <param name="userId">操作用户ID，当前登录用户</param>
        public void Init(string attachmentDir, string owerId, string userId)
        {
            this.AttachmentDirectory = attachmentDir;
            this.OwerId = owerId;
            this.userId = userId;
        }

        /// <summary>
        /// 初始化按钮的显示状态
        /// </summary>
        /// <param name="showUpload">是否显示上传按钮</param>
        /// <param name="showDelete">是否显示删除按钮</param>
        /// <param name="showDownload">是否显示下载按钮</param>
        public void InitButtonStatus(bool showUpload = true, bool showDelete = true, bool showDownload = true)
        {
            this.ShowUpload = showUpload;
            this.ShowDelete = showDelete;
            this.ShowDownload = showDownload;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AttachmentControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题：获取一个值，用以指示 System.ComponentModel.Component 当前是否处于设计模式。
        /// 描述：DesignMode 在 Visual Studio 2005 产品中存在 Bug ，使用下面的方式可以解决这个问题。
        ///        详细信息地址：http://support.microsoft.com/?scid=kb;zh-cn;839202&x=10&y=15
        /// </summary>
        protected new bool DesignMode
        {
            get
            {
                bool returnFlag = false;
#if DEBUG
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    returnFlag = true;
                }
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
                {
                    returnFlag = true;
                }
#endif
                return returnFlag;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AttachmentDirectory))
            {
                MessageDxUtil.ShowTips("请设置附件的存储目录分类");
                return;
            }
            if (string.IsNullOrEmpty(m_AttachmentGUID))
            {
                MessageDxUtil.ShowTips("请设置附件组的GUID");
                return;
            }

            FrmAttachmentGroupView dlg = new FrmAttachmentGroupView();
            dlg.AttachmentDirectory = AttachmentDirectory;
            dlg.AttachmentGUID = AttachmentGUID;
            dlg.UserId = userId;
            dlg.OwerId = OwerId;
            dlg.InitButtonStatus(this.ShowUpload, this.ShowDelete, this.ShowDownload);
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.ShowDialog();
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void AttachmentControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BindData();

                //默认可以操作各个功能
                InitButtonStatus();
            }
        }

        private void BindData()
        {
            var list = new List<FileUploadInfo>();
            if (!this.DesignMode && !string.IsNullOrEmpty(this.AttachmentGUID))
            {
                list = BLLFactory<FileUpload>.Instance.GetByAttachGUID(this.AttachmentGUID);
            }

            //多语言处理提示信息
            var newTipsContent = JsonLanguage.Default.GetString(TipsContent);
            this.lblTips.Text = string.Format(newTipsContent, list.Count);
        }
    }
}
