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
    /// 设备状态时长表
    /// </summary>	
    public partial class FrmEdit设备状态时长表 : BaseEditForm
    {
    	/// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
    	private 设备状态时长表Info tempInfo = new 设备状态时长表Info();
    	
        public FrmEdit设备状态时长表()
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
                设备状态时长表Info info = BLLFactory<设备状态时长表>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
                	
                	txt设备id.Value = info.设备id;
                       txt设备名称.Text = info.设备名称;
                   	txt记录时间.SetDateTime(info.记录时间);	
                      txt运行.Text = info.运行;
                       txt等待.Text = info.等待;
                       txt暂停.Text = info.暂停;
                       txt手动.Text = info.手动;
                       txt报警.Text = info.报警;
                       txt点检.Text = info.点检;
                       txt维修.Text = info.维修;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("设备状态时长表/Edit");             
            }
            else
            {
          
                //this.btnOK.Enabled = HasFunction("设备状态时长表/Add");  
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
            //this.txt设备id.Tag = "设备id";
            //this.txt设备名称.Tag = "设备名称";
            //this.txt记录时间.Tag = "记录时间";
            //this.txt运行.Tag = "运行";
            //this.txt等待.Tag = "等待";
            //this.txt暂停.Tag = "暂停";
            //this.txt手动.Tag = "手动";
            //this.txt报警.Tag = "报警";
            //this.txt点检.Tag = "点检";
            //this.txt维修.Tag = "维修";
            #endregion
			
            //获取列表权限的列表
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(设备状态时长表Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// 查看编辑附件信息
        /// </summary>
        //private void SetAttachInfo(设备状态时长表Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "设备状态时长表";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.序号, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new 设备状态时长表Info();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(设备状态时长表Info info)
        {
            info.设备id = Convert.ToInt32(txt设备id.Value);
                info.设备名称 = txt设备名称.Text;
                info.记录时间 = txt记录时间.DateTime;
               info.运行 = txt运行.Text;
                info.等待 = txt等待.Text;
                info.暂停 = txt暂停.Text;
                info.手动 = txt手动.Text;
                info.报警 = txt报警.Text;
                info.点检 = txt点检.Text;
                info.维修 = txt维修.Text;
            }
         
        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            设备状态时长表Info info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<设备状态时长表>.Instance.Insert(info);
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

            设备状态时长表Info info = BLLFactory<设备状态时长表>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<设备状态时长表>.Instance.Update(info, info.序号);
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
