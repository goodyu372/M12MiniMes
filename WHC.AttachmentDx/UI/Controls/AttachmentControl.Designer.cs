﻿namespace WHC.Attachment.UI
{
    partial class AttachmentControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttachmentControl));
            this.lblTips = new DevExpress.XtraEditors.LabelControl();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTips
            // 
            this.lblTips.Location = new System.Drawing.Point(4, 4);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(91, 14);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "共有【0】个附件";
            // 
            // btnView
            // 
            this.btnView.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnView.ImageOptions.Image")));
            this.btnView.Location = new System.Drawing.Point(138, 0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(28, 23);
            this.btnView.TabIndex = 1;
            this.btnView.ToolTip = "查看附件";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(169, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 23);
            this.label1.TabIndex = 2;
            // 
            // AttachmentControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblTips);
            this.MaximumSize = new System.Drawing.Size(180, 25);
            this.MinimumSize = new System.Drawing.Size(180, 25);
            this.Name = "AttachmentControl";
            this.Size = new System.Drawing.Size(180, 25);
            this.Load += new System.EventHandler(this.AttachmentControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTips;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private System.Windows.Forms.Label label1;
    }
}
