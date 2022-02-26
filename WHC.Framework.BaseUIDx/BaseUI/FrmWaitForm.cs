using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 等待提示窗体
    /// </summary>
    public partial class FrmWaitForm : WaitForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmWaitForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="caption"></param>
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }

        /// <summary>
        /// 设置正文内容
        /// </summary>
        /// <param name="description"></param>
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }
    }
}