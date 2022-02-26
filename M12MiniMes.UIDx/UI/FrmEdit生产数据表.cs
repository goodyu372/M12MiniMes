using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Dictionary;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;

using M12MiniMes.BLL;
using M12MiniMes.Entity;

namespace M12MiniMes.UI
{
    /// <summary>
    /// 生产数据表
    /// </summary>	
    public partial class FrmEdit生产数据表 : BaseEditForm
    {
    	/// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
    	private 生产数据表Info tempInfo = new 生产数据表Info();
    	
        public FrmEdit生产数据表()
        {
            InitializeComponent();
        }
                
        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion
            #endregion

            return result;
        }

        /// <summary>
        /// 初始化数据字典
        /// </summary>
        private void InitDictItem()
        {
			//初始化代码
        }                        

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//数据字典加载（公用）

            if (!string.IsNullOrEmpty(ID))
            {
                #region 显示信息
                生产数据表Info info = BLLFactory<生产数据表>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
                	
                	txt生产时间.SetDateTime(info.生产时间);	
                      txt物料生产批次号.Text = info.物料生产批次号;
                       txt治具生产批次号.Text = info.治具生产批次号;
                       txt物料guid.Text = info.物料guid;
                       txt治具guid.Text = info.治具guid;
                       txt治具rfid.Text = info.治具rfid;
                   	txt治具孔号.Value = info.治具孔号;
                   	txt设备id.Value = info.设备id;
                       txt设备名称.Text = info.设备名称;
                       txt工位号.Text = info.工位号;
                       txt工序数据.Text = info.工序数据;
                   	txt结果ok.Text = info.结果ok.ToString();
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("生产数据表/Edit");             
            }
            else
            {
            
                //this.btnOK.Enabled = HasFunction("生产数据表/Add");  
            }
            
            //tempInfo在对象存在则为指定对象，新建则是全新的对象，但有一些初始化的GUID用于附件上传
            //SetAttachInfo(tempInfo);
			
            //SetPermit(); //默认不使用字段权限
        }

        /// <summary>
        /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
        /// </summary>
        private void SetPermit()
        {
            #region 设置控件和字段的对应关系
            //this.txt生产时间.Tag = "生产时间";
            //this.txt物料生产批次号.Tag = "物料生产批次号";
            //this.txt治具生产批次号.Tag = "治具生产批次号";
            //this.txt物料guid.Tag = "物料guid";
            //this.txt治具guid.Tag = "治具guid";
            //this.txt治具rfid.Tag = "治具rfid";
            //this.txt治具孔号.Tag = "治具孔号";
            //this.txt设备id.Tag = "设备id";
            //this.txt设备名称.Tag = "设备名称";
            //this.txt工位号.Tag = "工位号";
            //this.txt工序数据.Tag = "工序数据";
            //this.txt结果ok.Tag = "结果ok";
            #endregion
			
            //获取列表权限的列表
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(生产数据表Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// 查看编辑附件信息
        /// </summary>
        //private void SetAttachInfo(生产数据表Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "生产数据表";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.生产数据id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new 生产数据表Info();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(生产数据表Info info)
        {
            info.生产时间 = txt生产时间.DateTime;
               info.物料生产批次号 = txt物料生产批次号.Text;
                info.治具生产批次号 = txt治具生产批次号.Text;
                info.物料guid = txt物料guid.Text;
                info.治具guid = txt治具guid.Text;
                info.治具rfid = txt治具rfid.Text;
                info.治具孔号 = Convert.ToInt32(txt治具孔号.Value);
                info.设备id = Convert.ToInt32(txt设备id.Value);
                info.设备名称 = txt设备名称.Text;
                info.工位号 = txt工位号.Text;
                info.工序数据 = txt工序数据.Text;
                info.结果ok = txt结果ok.Text.ToBoolean();
            }
         
        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            生产数据表Info info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<生产数据表>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }                 

        /// <summary>
        /// 编辑状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {

            生产数据表Info info = BLLFactory<生产数据表>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<生产数据表>.Instance.Update(info, info.生产数据id);
                    if (succeed)
                    {
                        //可添加其他关联操作
                       
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
           return false;
        }
    }
}
