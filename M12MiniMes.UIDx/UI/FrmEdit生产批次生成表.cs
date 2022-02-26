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
using System.Text.RegularExpressions;

namespace M12MiniMes.UI
{
    /// <summary>
    /// 生产批次生成表
    /// </summary>	
    public partial class FrmEdit生产批次生成表 : BaseEditForm
    {
    	/// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
    	private 生产批次生成表Info tempInfo = new 生产批次生成表Info();
    	
        public FrmEdit生产批次生成表()
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
                生产批次生成表Info info = BLLFactory<生产批次生成表>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
                	
                	txt时间.SetDateTime(info.时间);	
                      txt班次.Text = info.班次;
                       txt组装线体号.Text = info.组装线体号;
                       txt机种.Text = info.机种;
                   	txt镜框日期.SetDateTime(info.镜框日期);	
                      txt镜筒模穴号.Text = info.镜筒模穴号;
                       txt镜框批次.Text = info.镜框批次;
                       txt穴号105.Text = info.穴号105;
                       txt穴号104.Text = info.穴号104;
                       txt穴号102.Text = info.穴号102;
                   	txt日期105.SetDateTime(info.日期105);	
                  	txt日期104.SetDateTime(info.日期104);	
                  	txt日期102.SetDateTime(info.日期102);	
                      txt角度.Text = info.角度;
                       txt系列号.Text = info.系列号;
                   	txt镜框投料数.Value = info.镜框投料数;
                       txt隔圈模穴号113b.Text = info.隔圈模穴号113b;
                   	txt成型日113b.SetDateTime(info.成型日113b);	
                      txt隔圈模穴号112.Text = info.隔圈模穴号112;
                   	txt成型日112.SetDateTime(info.成型日112);	
                  	txt隔圈投料数.Value = info.隔圈投料数;
                       txtG3来料供应商.Text = info.G3来料供应商;
                   	txtG3镜片来料日期.SetDateTime(info.G3镜片来料日期);	
                      txtG1来料供应商.Text = info.G1来料供应商;
                   	txtG1来料日期.SetDateTime(info.G1来料日期);	
                  	txt镜片105投料数.Value = info.镜片105投料数;
                   	txt镜片104投料数.Value = info.镜片104投料数;
                   	txt镜片g3投料数.Value = info.镜片g3投料数;
                   	txt镜片102投料数.Value = info.镜片102投料数;
                   	txt镜片95b投料数.Value = info.镜片95b投料数;
                       txt配对监控批次.Text = info.配对监控批次;
                   	txt计划投入数.Value = info.计划投入数;
                   	txt上线数.Value = info.上线数;
                   	txt下线数.Value = info.下线数;
                       txt状态.Text = info.状态;
                       txt生产批次号.Text = info.生产批次号;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("生产批次生成表/Edit");             
            }
            else
            {
                                    
                //this.btnOK.Enabled = HasFunction("生产批次生成表/Add");  
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
            //this.txt时间.Tag = "时间";
            //this.txt班次.Tag = "班次";
            //this.txt组装线体号.Tag = "组装线体号";
            //this.txt机种.Tag = "机种";
            //this.txt镜框日期.Tag = "镜框日期";
            //this.txt镜筒模穴号.Tag = "镜筒模穴号";
            //this.txt镜框批次.Tag = "镜框批次";
            //this.txt穴号105.Tag = "穴号105";
            //this.txt穴号104.Tag = "穴号104";
            //this.txt穴号102.Tag = "穴号102";
            //this.txt日期105.Tag = "日期105";
            //this.txt日期104.Tag = "日期104";
            //this.txt日期102.Tag = "日期102";
            //this.txt角度.Tag = "角度";
            //this.txt系列号.Tag = "系列号";
            //this.txt镜框投料数.Tag = "镜框投料数";
            //this.txt隔圈模穴号113b.Tag = "隔圈模穴号113b";
            //this.txt成型日113b.Tag = "成型日113b";
            //this.txt隔圈模穴号112.Tag = "隔圈模穴号112";
            //this.txt成型日112.Tag = "成型日112";
            //this.txt隔圈投料数.Tag = "隔圈投料数";
            //this.txtG3来料供应商.Tag = "G3来料供应商";
            //this.txtG3镜片来料日期.Tag = "G3镜片来料日期";
            //this.txtG1来料供应商.Tag = "G1来料供应商";
            //this.txtG1来料日期.Tag = "G1来料日期";
            //this.txt镜片105投料数.Tag = "镜片105投料数";
            //this.txt镜片104投料数.Tag = "镜片104投料数";
            //this.txt镜片g3投料数.Tag = "镜片g3投料数";
            //this.txt镜片102投料数.Tag = "镜片102投料数";
            //this.txt镜片95b投料数.Tag = "镜片95b投料数";
            //this.txt配对监控批次.Tag = "配对监控批次";
            //this.txt计划投入数.Tag = "计划投入数";
            //this.txt上线数.Tag = "上线数";
            //this.txt下线数.Tag = "下线数";
            //this.txt状态.Tag = "状态";
            //this.txt生产批次号.Tag = "生产批次号";
            #endregion
			
            //获取列表权限的列表
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(生产批次生成表Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// 查看编辑附件信息
        /// </summary>
        //private void SetAttachInfo(生产批次生成表Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "生产批次生成表";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.生产批次id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new 生产批次生成表Info();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(生产批次生成表Info info)
        {
            info.时间 = txt时间.DateTime;
               info.班次 = txt班次.Text;
                info.组装线体号 = txt组装线体号.Text;
                info.机种 = txt机种.Text;
                info.镜框日期 = txt镜框日期.DateTime;
               info.镜筒模穴号 = txt镜筒模穴号.Text;
                info.镜框批次 = txt镜框批次.Text;
                info.穴号105 = txt穴号105.Text;
                info.穴号104 = txt穴号104.Text;
                info.穴号102 = txt穴号102.Text;
                info.日期105 = txt日期105.DateTime;
               info.日期104 = txt日期104.DateTime;
               info.日期102 = txt日期102.DateTime;
               info.角度 = txt角度.Text;
                info.系列号 = txt系列号.Text;
                info.镜框投料数 = Convert.ToInt32(txt镜框投料数.Value);
                info.隔圈模穴号113b = txt隔圈模穴号113b.Text;
                info.成型日113b = txt成型日113b.DateTime;
               info.隔圈模穴号112 = txt隔圈模穴号112.Text;
                info.成型日112 = txt成型日112.DateTime;
               info.隔圈投料数 = Convert.ToInt32(txt隔圈投料数.Value);
                info.G3来料供应商 = txtG3来料供应商.Text;
                info.G3镜片来料日期 = txtG3镜片来料日期.DateTime;
               info.G1来料供应商 = txtG1来料供应商.Text;
                info.G1来料日期 = txtG1来料日期.DateTime;
               info.镜片105投料数 = Convert.ToInt32(txt镜片105投料数.Value);
                info.镜片104投料数 = Convert.ToInt32(txt镜片104投料数.Value);
                info.镜片g3投料数 = Convert.ToInt32(txt镜片g3投料数.Value);
                info.镜片102投料数 = Convert.ToInt32(txt镜片102投料数.Value);
                info.镜片95b投料数 = Convert.ToInt32(txt镜片95b投料数.Value);
                info.配对监控批次 = txt配对监控批次.Text;
                info.计划投入数 = Convert.ToInt32(txt计划投入数.Value);
                info.上线数 = Convert.ToInt32(txt上线数.Value);
                info.下线数 = Convert.ToInt32(txt下线数.Value);
                info.状态 = txt状态.Text;
                info.生产批次号 = txt生产批次号.Text;
            }
         
        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            生产批次生成表Info info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);

            try
            {
                #region 新增数据

                #region 改变某些字段
                //检查角度格式是否输入有误   要求格式0-90-180
                Regex reg = new Regex(@"^-?\d+--?\d+--?\d+");
                if (!reg.IsMatch(info.角度))
                {
                    throw new Exception($@"{info.角度} 角度输入格式不正确！要求格式如0-90-180");
                }
                if (string.IsNullOrEmpty(info.穴号105) || string.IsNullOrEmpty(info.穴号104) || string.IsNullOrEmpty(info.穴号102)
                    || string.IsNullOrEmpty(info.系列号) || string.IsNullOrEmpty(info.配对监控批次))
                {
                    throw new Exception($@"检查*项有的未输入内容！*项为必填项！");
                }
                if (info.计划投入数 == 0)
                {
                    throw new Exception($@"计划投入数不允许为0！必须是12的正整数倍！如600/1200！"); 
                }

                //可为空的一些属性值赋予null
                info.组装线体号 = string.IsNullOrEmpty(info.组装线体号) ? "zzxt" : info.组装线体号;
                info.机种 = string.IsNullOrEmpty(info.机种) ? "jz" : info.机种;
                info.镜框日期 = (info.镜框日期 == DateTime.MinValue) ? DateTime.Now : info.镜框日期;
                info.镜筒模穴号 = string.IsNullOrEmpty(info.镜筒模穴号) ? "0" : info.镜筒模穴号;
                info.镜框批次 = string.IsNullOrEmpty(info.镜框批次) ? "jkpc" : info.镜框批次;
                info.日期105 = (info.日期105 == DateTime.MinValue) ? DateTime.Now : info.日期105;
                info.日期104 = (info.日期104 == DateTime.MinValue) ? DateTime.Now : info.日期104;
                info.日期102 = (info.日期102 == DateTime.MinValue) ? DateTime.Now : info.日期102;
                info.隔圈模穴号113b = string.IsNullOrEmpty(info.隔圈模穴号113b) ? "0" : info.隔圈模穴号113b;
                info.成型日113b = (info.成型日113b == DateTime.MinValue) ? DateTime.Now : info.成型日113b;
                info.隔圈模穴号112 = string.IsNullOrEmpty(info.隔圈模穴号112) ? "0" : info.隔圈模穴号112;
                info.成型日112 = (info.成型日112 == DateTime.MinValue) ? DateTime.Now : info.成型日112;
                info.G3来料供应商 = string.IsNullOrEmpty(info.G3来料供应商) ? "g3sp" : info.G3来料供应商;
                info.G3镜片来料日期 = (info.G3镜片来料日期 == DateTime.MinValue) ? DateTime.Now : info.G3镜片来料日期;
                info.G1来料供应商 = string.IsNullOrEmpty(info.G1来料供应商) ? "g1sp" : info.G1来料供应商;
                info.G1来料日期 = (info.G1来料日期 == DateTime.MinValue) ? DateTime.Now : info.G1来料日期;


                info.上线数 = info.下线数 = 0;
                info.时间 = DateTime.Now;
                //判断当前时间是白班还是夜班
                string _strWorkingDayAM = "08:00";
                string _strWorkingDayPM = "20:00";
                TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
                TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
                TimeSpan dspNow = DateTime.Now.TimeOfDay;
                if (dspNow >= dspWorkingDayAM && dspNow <= dspWorkingDayPM)
                {
                    info.班次 = "白班";
                }
                else
                {
                    info.班次 = "夜班";
                }
                info.状态 = "待生产";
                info.生产批次号 = $@"{DateTime.Now.ToString("yyMMddhhmmss")}{info.系列号}";
                #endregion

                bool succeed = BLLFactory<生产批次生成表>.Instance.Insert(info);
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

            生产批次生成表Info info = BLLFactory<生产批次生成表>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据

                    //检查角度格式是否输入有误   要求格式0-90-180
                    Regex reg = new Regex(@"^-?\d+--?\d+--?\d+");
                    if (!reg.IsMatch(info.角度))
                    {
                        throw new Exception($@"{info.角度} 角度输入格式不正确！要求格式如0-90-180");
                    }
                    if (string.IsNullOrEmpty(info.穴号105) || string.IsNullOrEmpty(info.穴号104) || string.IsNullOrEmpty(info.穴号102)
                        || string.IsNullOrEmpty(info.系列号) || string.IsNullOrEmpty(info.配对监控批次))
                    {
                        throw new Exception($@"检查*项有的未输入内容！*项为必填项！");
                    }
                    if (info.计划投入数 == 0)
                    {
                        throw new Exception($@"计划投入数不允许为0！必须是12的正整数倍！如600/1200！");
                    }

                    bool succeed = BLLFactory<生产批次生成表>.Instance.Update(info, info.生产批次id);
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
