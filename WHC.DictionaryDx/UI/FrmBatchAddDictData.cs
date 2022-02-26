using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

using WHC.Dictionary.Entity;
using WHC.Dictionary.BLL;

using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.UI
{
    public partial class FrmBatchAddDictData : BaseForm
    {
        public string ID = string.Empty;
        public string LoginID = "";//登陆用户ID 

        public FrmBatchAddDictData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用事务参数，插入数据，最后统一提交事务处理
        /// </summary>
        /// <param name="dictData">字典数据</param>
        /// <param name="seq">排序</param>
        /// <param name="trans">事务对象</param>
        private void InsertDictData(string dictData, string seq, DbTransaction trans)
        {
            if (!string.IsNullOrWhiteSpace(dictData))
            {
                DictDataInfo info = new DictDataInfo();
                info.Editor = LoginID;
                info.LastUpdated = DateTime.Now;
                info.DictType_ID = this.txtDictType.Tag.ToString();

                var array = System.Text.RegularExpressions.Regex.Split(dictData, @"\s+");
                info.Name = array[0];
                info.Value = array.Length > 1 ? array[1] : array[0];

                info.Remark = this.txtNote.Text.Trim();
                info.Seq = seq;

                bool succeed = BLLFactory<DictData>.Instance.Insert(info, trans);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] arrayItems = this.txtDictData.Lines;
            int intSeq = -1;
            int seqLength = 3;
            string strSeq = this.txtSeq.Text.Trim();
            if (int.TryParse(strSeq, out intSeq))
            {
                seqLength = strSeq.Length;
            }

            if (arrayItems != null && arrayItems.Length > 0)
            {
                DbTransaction trans = BLLFactory<DictData>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        #region MyRegion
                        foreach (string strItem in arrayItems)
                        {
                            if (this.radSplit.Checked)
                            {
                                if (!string.IsNullOrWhiteSpace(strItem))
                                {
                                    string[] dataItems = strItem.Split(new char[] { ',', '，', ';', '；', '/', '、' });
                                    foreach (string dictData in dataItems)
                                    {
                                        #region 保存数据
                                        string seq = "";
                                        if (intSeq > 0)
                                        {
                                            seq = (intSeq++).ToString().PadLeft(seqLength, '0');
                                        }
                                        else
                                        {
                                            seq = string.Format("{0}{1}", strSeq, intSeq++);
                                        }

                                        InsertDictData(dictData, seq, trans);
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region 保存数据
                                if (!string.IsNullOrWhiteSpace(strItem))
                                {
                                    string seq = "";
                                    if (intSeq > 0)
                                    {
                                        seq = (intSeq++).ToString().PadLeft(seqLength, '0');
                                    }
                                    else
                                    {
                                        seq = string.Format("{0}{1}", strSeq, intSeq++);
                                    }

                                    InsertDictData(strItem, seq, trans);
                                }
                                #endregion
                            }
                        }
                        #endregion

                        trans.Commit();
                        ProcessDataSaved(this.btnOK, new EventArgs());
                        MessageDxUtil.ShowTips("保存成功");
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        LogTextHelper.Error(ex);
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        private void FrmBatchAddDictData_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(ID))
            {
                DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(ID);
                if(info != null)
                {
                    this.txtDictType.Text = info.Name;
                    this.txtDictCode.Text = info.Code;
                }
            }
        }
    }
}