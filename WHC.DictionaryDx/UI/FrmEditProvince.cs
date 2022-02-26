using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WHC.Dictionary.Entity;
using WHC.Dictionary.BLL;

using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.UI
{
    public partial class FrmEditProvince : BaseForm
    {        
        public string ID = string.Empty;
        public string LoginID = "";//登陆用户ID 
        private ProvinceInfo tempInfo = new ProvinceInfo();

        public FrmEditProvince()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            tempInfo.ProvinceName = this.txtProvince.Text;

            try
            {
                bool succeed = false;
                if (string.IsNullOrEmpty(ID))
                {
                    var condition = string.Format("ProvinceName='{0}'", this.txtProvince.Text);
                    bool isExist = BLLFactory<Province>.Instance.IsExistRecord(condition);
                    if (isExist)
                    {
                        MessageDxUtil.ShowTips("省份名称已存在，请选择其他名称");
                        this.txtProvince.Focus();
                        return;
                    }
                    else
                    {                        
                        succeed = BLLFactory<Province>.Instance.Insert(tempInfo);
                    }
                }
                else
                {
                    var condition = string.Format("ProvinceName ='{0}' and ID <> {1} ", this.txtProvince.Text, ID);
                    bool exist = BLLFactory<Province>.Instance.IsExistRecord(condition);
                    if (exist)
                    {
                        MessageDxUtil.ShowTips("省份名称已存在，请选择其他名称");
                        this.txtProvince.Focus();
                        return;
                    }
                    succeed = BLLFactory<Province>.Instance.Update(tempInfo, tempInfo.ID);
                }

                ProcessDataSaved(this.btnOK, new EventArgs());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void FrmEditProvince_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                ProvinceInfo info = BLLFactory<Province>.Instance.FindByID(ID);
                if (info != null)
                {
                    tempInfo = info;
                    this.txtProvince.Text = info.ProvinceName;
                }
            }
        }
    }
}
