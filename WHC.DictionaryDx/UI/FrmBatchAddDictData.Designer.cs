namespace WHC.Dictionary.UI
{
    partial class FrmBatchAddDictData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchAddDictData));
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDictType = new DevExpress.XtraEditors.TextEdit();
            this.txtSeq = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radLine = new System.Windows.Forms.RadioButton();
            this.radSplit = new System.Windows.Forms.RadioButton();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDictData = new DevExpress.XtraEditors.MemoEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.txtDictCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(17, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 14);
            this.label9.TabIndex = 44;
            this.label9.Text = "项目排序";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 43;
            this.label2.Text = "字典大类";
            // 
            // txtDictType
            // 
            this.txtDictType.Enabled = false;
            this.txtDictType.Location = new System.Drawing.Point(118, 19);
            this.txtDictType.Name = "txtDictType";
            this.txtDictType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDictType.Properties.Appearance.Options.UseBackColor = true;
            this.txtDictType.Properties.ReadOnly = true;
            this.txtDictType.Size = new System.Drawing.Size(175, 20);
            this.txtDictType.TabIndex = 0;
            // 
            // txtSeq
            // 
            this.txtSeq.EditValue = "001";
            this.txtSeq.Location = new System.Drawing.Point(118, 47);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(121, 20);
            this.txtSeq.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 14);
            this.label7.TabIndex = 46;
            this.label7.Text = "字典数据";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.Location = new System.Drawing.Point(526, 485);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 32);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(625, 485);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "关闭";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(245, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 14);
            this.label3.TabIndex = 44;
            this.label3.Text = "（数值开始或字符作为前缀）";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(17, 428);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 14);
            this.label4.TabIndex = 46;
            this.label4.Text = "备注";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radLine);
            this.groupBox1.Controls.Add(this.radSplit);
            this.groupBox1.Location = new System.Drawing.Point(164, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 78);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // radLine
            // 
            this.radLine.AutoSize = true;
            this.radLine.Location = new System.Drawing.Point(16, 43);
            this.radLine.Name = "radLine";
            this.radLine.Size = new System.Drawing.Size(229, 18);
            this.radLine.TabIndex = 1;
            this.radLine.Text = "一行一个记录模式，忽略所有分隔符号";
            this.radLine.UseVisualStyleBackColor = true;
            // 
            // radSplit
            // 
            this.radSplit.AutoSize = true;
            this.radSplit.Checked = true;
            this.radSplit.Location = new System.Drawing.Point(16, 18);
            this.radSplit.Name = "radSplit";
            this.radSplit.Size = new System.Drawing.Size(488, 18);
            this.radSplit.TabIndex = 0;
            this.radSplit.TabStop = true;
            this.radSplit.Text = "分隔符方式，多个数据中英文逗号，分号，斜杠或顿号[, ， ; ； / 、]分开，或一行一个";
            this.radSplit.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(17, 360);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 14);
            this.label5.TabIndex = 50;
            this.label5.Text = "数据分开方式";
            // 
            // txtDictData
            // 
            this.txtDictData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDictData.Location = new System.Drawing.Point(118, 75);
            this.txtDictData.Name = "txtDictData";
            this.txtDictData.Size = new System.Drawing.Size(582, 247);
            this.txtDictData.TabIndex = 3;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Location = new System.Drawing.Point(118, 412);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(582, 54);
            this.txtNote.TabIndex = 4;
            // 
            // txtDictCode
            // 
            this.txtDictCode.Enabled = false;
            this.txtDictCode.Location = new System.Drawing.Point(435, 19);
            this.txtDictCode.Name = "txtDictCode";
            this.txtDictCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDictCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtDictCode.Properties.ReadOnly = true;
            this.txtDictCode.Size = new System.Drawing.Size(175, 20);
            this.txtDictCode.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(334, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 43;
            this.labelControl1.Text = "字典代码";
            // 
            // FrmBatchAddDictData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 529);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDictCode);
            this.Controls.Add(this.txtDictType);
            this.Controls.Add(this.txtSeq);
            this.Controls.Add(this.txtDictData);
            this.Controls.Add(this.txtNote);
            this.Name = "FrmBatchAddDictData";
            this.Text = "批量添加字典数据";
            this.Load += new System.EventHandler(this.FrmBatchAddDictData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDictType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl label9;
        private DevExpress.XtraEditors.LabelControl label2;
        public DevExpress.XtraEditors.TextEdit txtDictType;
        private DevExpress.XtraEditors.TextEdit txtSeq;
        private DevExpress.XtraEditors.LabelControl label7;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.XtraEditors.LabelControl label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl label5;
        private System.Windows.Forms.RadioButton radSplit;
        private System.Windows.Forms.RadioButton radLine;
        private DevExpress.XtraEditors.MemoEdit txtDictData;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        public DevExpress.XtraEditors.TextEdit txtDictCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}