using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WHC.Dictionary.Entity;
using WHC.Dictionary.BLL;

using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Framework.Language;

namespace WHC.Dictionary.UI
{
    public partial class FrmEditDictData : BaseForm
    {
        public string ID = string.Empty;//字典数据记录ID
        public string TypeID = string.Empty;//字典类型ID
        public string LoginID = "";//登陆用户ID 

        public FrmEditDictData()
        {
            InitializeComponent();
        }
        
        private void FrmEditDictData_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TypeID))
            {
                DictTypeInfo typeInfo = BLLFactory<DictType>.Instance.FindByID(TypeID);
                if(typeInfo != null)
                {
                    this.txtDictType.Text = typeInfo.Name;
                    this.txtDictType.Tag = typeInfo.ID;
                    this.txtDictCode.Text = typeInfo.Code;
                }
            }

            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = string.Format("{0} {1}", JsonLanguage.Default.GetString("编辑"), JsonLanguage.Default.GetString(this.Text));
                DictDataInfo info = BLLFactory<DictData>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Remark;
                    this.txtSeq.Text = info.Seq;
                    this.txtValue.Text = info.Value;
                }
                //this.btnOK.Enabled = Portal.gc.HasFunction("Product/Modify");
            }
            else
            {
                this.Text = string.Format("{0} {1}", JsonLanguage.Default.GetString("新建"), JsonLanguage.Default.GetString(this.Text));
                //this.btnOK.Enabled = Portal.gc.HasFunction("Product/Add");
                btnEqual_Click(null, null);
            }

            this.txtName.Focus();
        }

        private void SetInfo(DictDataInfo info)
        {
            info.Editor = LoginID;
            info.LastUpdated = DateTime.Now;
            info.Name = this.txtName.Text.Trim();
            info.Remark = this.txtNote.Text.Trim();
            info.Seq = this.txtSeq.Text;
            info.Value = this.txtValue.Text.Trim();
            info.DictType_ID = this.txtDictType.Tag.ToString();
        }        

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请输入项目名称");
                this.txtName.Focus();
                return;
            }
            if (this.txtValue.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请输入项目值");
                this.txtValue.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(ID))
            {
                DictDataInfo info = BLLFactory<DictData>.Instance.FindByID(ID);
                if (info != null)
                {
                    SetInfo(info);

                    try
                    {
                        bool succeed = BLLFactory<DictData>.Instance.Update(info, info.ID.ToString());
                        if (succeed)
                        {
                            ProcessDataSaved(this.btnOK, new EventArgs());
                            MessageDxUtil.ShowTips("保存成功");
                            if (!this.chkNotClose.Checked)
                            {
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTextHelper.Error(ex);
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
            else
            {
                DictDataInfo info = new DictDataInfo();
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<DictData>.Instance.Insert(info);
                    if (succeed)
                    {
                        ProcessDataSaved(this.btnOK, new EventArgs());
                        MessageDxUtil.ShowTips("保存成功");
                        if (!this.chkNotClose.Checked)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            int intSeq = 0;
                            string seqValue = this.txtSeq.Text;
                            if (int.TryParse(seqValue, out intSeq))
                            {
                                this.txtSeq.Text = (intSeq + 1).ToString().PadLeft(seqValue.Trim().Length, '0');
                            }
                            this.txtName.Focus();
                            this.txtName.SelectAll();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool equal = false;//是否名称和值保持一致
        private void btnEqual_Click(object sender, EventArgs e)
        {
            equal = !equal;
            if (equal)
            {
                this.btnEqual.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            }
            else
            {
                this.btnEqual.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (equal)
            {
                this.txtValue.Text = txtName.Text;
            }
        }

    }
}
