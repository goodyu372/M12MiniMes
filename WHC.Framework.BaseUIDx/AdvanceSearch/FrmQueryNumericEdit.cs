﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WHC.Framework.BaseUI
{
    internal partial class FrmQueryNumericEdit : FrmQueryBase
    {
        public FrmQueryNumericEdit()
        {
            InitializeComponent();

            this.txtStart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            this.txtEnd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);

            //设置最大
            this.txtStart.Maximum = Decimal.MaxValue;
            this.txtEnd.Maximum = Decimal.MaxValue;
        }

        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                btnOK_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ProcessDataClear(FieldName);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //判断输入的内容是否为空，来决定是否匹配日期
            if (txtStart.Text.Length > 0)
            {
                this.ReturnDisplay = string.Format("{0}", txtStart.Value);
                this.ReturnValue = string.Format("{0}", txtStart.Value);
            }
            if (txtEnd.Text.Length > 0)
            {
                this.ReturnDisplay += string.Format(" ~ {0}", txtEnd.Value);
                this.ReturnValue += string.Format(" ~ {0}", txtEnd.Value);
            }

            ProcessDataSearch(null, null);
        }

        private void FrmQueryNumericEdit_Load(object sender, EventArgs e)
        {
            txtStart.Text = "";
            txtEnd.Text = "";

            this.lblFieldName.Text = this.FieldDisplayName;
            if (!string.IsNullOrEmpty(FieldDefaultValue))
            {
                string[] itemArray = FieldDefaultValue.Split('~');
                if (itemArray != null)
                {   
                    decimal value = 0M;
                    bool result = false;

                    if (itemArray.Length > 0)
                    {
                        result = decimal.TryParse(itemArray[0].Trim(), out value);
                        if (result)
                        {
                            this.txtStart.Value = value;
                        }
                    }
                    if (itemArray.Length > 1)
                    {
                        result = decimal.TryParse(itemArray[1].Trim(), out value);
                        if (result)
                        {
                            this.txtEnd.Value = value;
                        }
                    }
                }
            }

            this.txtStart.Focus();
        }
    }
}
