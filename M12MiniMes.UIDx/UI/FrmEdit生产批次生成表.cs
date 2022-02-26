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
    /// �����������ɱ�
    /// </summary>	
    public partial class FrmEdit�����������ɱ� : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private �����������ɱ�Info tempInfo = new �����������ɱ�Info();
    	
        public FrmEdit�����������ɱ�()
        {
            InitializeComponent();
        }
                
        /// <summary>
        /// ʵ�ֿؼ�������ĺ���
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//Ĭ���ǿ���ͨ��

            #region MyRegion
            #endregion

            return result;
        }

        /// <summary>
        /// ��ʼ�������ֵ�
        /// </summary>
        private void InitDictItem()
        {
			//��ʼ������
        }                        

        /// <summary>
        /// ������ʾ�ĺ���
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//�����ֵ���أ����ã�

            if (!string.IsNullOrEmpty(ID))
            {
                #region ��ʾ��Ϣ
                �����������ɱ�Info info = BLLFactory<�����������ɱ�>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
                	txtʱ��.SetDateTime(info.ʱ��);	
                      txt���.Text = info.���;
                       txt��װ�����.Text = info.��װ�����;
                       txt����.Text = info.����;
                   	txt��������.SetDateTime(info.��������);	
                      txt��ͲģѨ��.Text = info.��ͲģѨ��;
                       txt��������.Text = info.��������;
                       txtѨ��105.Text = info.Ѩ��105;
                       txtѨ��104.Text = info.Ѩ��104;
                       txtѨ��102.Text = info.Ѩ��102;
                   	txt����105.SetDateTime(info.����105);	
                  	txt����104.SetDateTime(info.����104);	
                  	txt����102.SetDateTime(info.����102);	
                      txt�Ƕ�.Text = info.�Ƕ�;
                       txtϵ�к�.Text = info.ϵ�к�;
                   	txt����Ͷ����.Value = info.����Ͷ����;
                       txt��ȦģѨ��113b.Text = info.��ȦģѨ��113b;
                   	txt������113b.SetDateTime(info.������113b);	
                      txt��ȦģѨ��112.Text = info.��ȦģѨ��112;
                   	txt������112.SetDateTime(info.������112);	
                  	txt��ȦͶ����.Value = info.��ȦͶ����;
                       txtG3���Ϲ�Ӧ��.Text = info.G3���Ϲ�Ӧ��;
                   	txtG3��Ƭ��������.SetDateTime(info.G3��Ƭ��������);	
                      txtG1���Ϲ�Ӧ��.Text = info.G1���Ϲ�Ӧ��;
                   	txtG1��������.SetDateTime(info.G1��������);	
                  	txt��Ƭ105Ͷ����.Value = info.��Ƭ105Ͷ����;
                   	txt��Ƭ104Ͷ����.Value = info.��Ƭ104Ͷ����;
                   	txt��Ƭg3Ͷ����.Value = info.��Ƭg3Ͷ����;
                   	txt��Ƭ102Ͷ����.Value = info.��Ƭ102Ͷ����;
                   	txt��Ƭ95bͶ����.Value = info.��Ƭ95bͶ����;
                       txt��Լ������.Text = info.��Լ������;
                   	txt�ƻ�Ͷ����.Value = info.�ƻ�Ͷ����;
                   	txt������.Value = info.������;
                   	txt������.Value = info.������;
                       txt״̬.Text = info.״̬;
                       txt�������κ�.Text = info.�������κ�;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("�����������ɱ�/Edit");             
            }
            else
            {
                                    
                //this.btnOK.Enabled = HasFunction("�����������ɱ�/Add");  
            }
            
            //tempInfo�ڶ��������Ϊָ�������½�����ȫ�µĶ��󣬵���һЩ��ʼ����GUID���ڸ����ϴ�
            //SetAttachInfo(tempInfo);
			
            //SetPermit(); //Ĭ�ϲ�ʹ���ֶ�Ȩ��
        }

        /// <summary>
        /// ���ÿؼ��ֶε�Ȩ����ʾ��������(Ĭ�ϲ�ʹ���ֶ�Ȩ��)
        /// </summary>
        private void SetPermit()
        {
            #region ���ÿؼ����ֶεĶ�Ӧ��ϵ
            //this.txtʱ��.Tag = "ʱ��";
            //this.txt���.Tag = "���";
            //this.txt��װ�����.Tag = "��װ�����";
            //this.txt����.Tag = "����";
            //this.txt��������.Tag = "��������";
            //this.txt��ͲģѨ��.Tag = "��ͲģѨ��";
            //this.txt��������.Tag = "��������";
            //this.txtѨ��105.Tag = "Ѩ��105";
            //this.txtѨ��104.Tag = "Ѩ��104";
            //this.txtѨ��102.Tag = "Ѩ��102";
            //this.txt����105.Tag = "����105";
            //this.txt����104.Tag = "����104";
            //this.txt����102.Tag = "����102";
            //this.txt�Ƕ�.Tag = "�Ƕ�";
            //this.txtϵ�к�.Tag = "ϵ�к�";
            //this.txt����Ͷ����.Tag = "����Ͷ����";
            //this.txt��ȦģѨ��113b.Tag = "��ȦģѨ��113b";
            //this.txt������113b.Tag = "������113b";
            //this.txt��ȦģѨ��112.Tag = "��ȦģѨ��112";
            //this.txt������112.Tag = "������112";
            //this.txt��ȦͶ����.Tag = "��ȦͶ����";
            //this.txtG3���Ϲ�Ӧ��.Tag = "G3���Ϲ�Ӧ��";
            //this.txtG3��Ƭ��������.Tag = "G3��Ƭ��������";
            //this.txtG1���Ϲ�Ӧ��.Tag = "G1���Ϲ�Ӧ��";
            //this.txtG1��������.Tag = "G1��������";
            //this.txt��Ƭ105Ͷ����.Tag = "��Ƭ105Ͷ����";
            //this.txt��Ƭ104Ͷ����.Tag = "��Ƭ104Ͷ����";
            //this.txt��Ƭg3Ͷ����.Tag = "��Ƭg3Ͷ����";
            //this.txt��Ƭ102Ͷ����.Tag = "��Ƭ102Ͷ����";
            //this.txt��Ƭ95bͶ����.Tag = "��Ƭ95bͶ����";
            //this.txt��Լ������.Tag = "��Լ������";
            //this.txt�ƻ�Ͷ����.Tag = "�ƻ�Ͷ����";
            //this.txt������.Tag = "������";
            //this.txt������.Tag = "������";
            //this.txt״̬.Tag = "״̬";
            //this.txt�������κ�.Tag = "�������κ�";
            #endregion
			
            //��ȡ�б�Ȩ�޵��б�
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(�����������ɱ�Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// �鿴�༭������Ϣ
        /// </summary>
        //private void SetAttachInfo(�����������ɱ�Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "�����������ɱ�";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.��������id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new �����������ɱ�Info();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(�����������ɱ�Info info)
        {
            info.ʱ�� = txtʱ��.DateTime;
               info.��� = txt���.Text;
                info.��װ����� = txt��װ�����.Text;
                info.���� = txt����.Text;
                info.�������� = txt��������.DateTime;
               info.��ͲģѨ�� = txt��ͲģѨ��.Text;
                info.�������� = txt��������.Text;
                info.Ѩ��105 = txtѨ��105.Text;
                info.Ѩ��104 = txtѨ��104.Text;
                info.Ѩ��102 = txtѨ��102.Text;
                info.����105 = txt����105.DateTime;
               info.����104 = txt����104.DateTime;
               info.����102 = txt����102.DateTime;
               info.�Ƕ� = txt�Ƕ�.Text;
                info.ϵ�к� = txtϵ�к�.Text;
                info.����Ͷ���� = Convert.ToInt32(txt����Ͷ����.Value);
                info.��ȦģѨ��113b = txt��ȦģѨ��113b.Text;
                info.������113b = txt������113b.DateTime;
               info.��ȦģѨ��112 = txt��ȦģѨ��112.Text;
                info.������112 = txt������112.DateTime;
               info.��ȦͶ���� = Convert.ToInt32(txt��ȦͶ����.Value);
                info.G3���Ϲ�Ӧ�� = txtG3���Ϲ�Ӧ��.Text;
                info.G3��Ƭ�������� = txtG3��Ƭ��������.DateTime;
               info.G1���Ϲ�Ӧ�� = txtG1���Ϲ�Ӧ��.Text;
                info.G1�������� = txtG1��������.DateTime;
               info.��Ƭ105Ͷ���� = Convert.ToInt32(txt��Ƭ105Ͷ����.Value);
                info.��Ƭ104Ͷ���� = Convert.ToInt32(txt��Ƭ104Ͷ����.Value);
                info.��Ƭg3Ͷ���� = Convert.ToInt32(txt��Ƭg3Ͷ����.Value);
                info.��Ƭ102Ͷ���� = Convert.ToInt32(txt��Ƭ102Ͷ����.Value);
                info.��Ƭ95bͶ���� = Convert.ToInt32(txt��Ƭ95bͶ����.Value);
                info.��Լ������ = txt��Լ������.Text;
                info.�ƻ�Ͷ���� = Convert.ToInt32(txt�ƻ�Ͷ����.Value);
                info.������ = Convert.ToInt32(txt������.Value);
                info.������ = Convert.ToInt32(txt������.Value);
                info.״̬ = txt״̬.Text;
                info.�������κ� = txt�������κ�.Text;
            }
         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            �����������ɱ�Info info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������

                #region �ı�ĳЩ�ֶ�
                //���Ƕȸ�ʽ�Ƿ���������   Ҫ���ʽ0-90-180
                Regex reg = new Regex(@"^-?\d+--?\d+--?\d+");
                if (!reg.IsMatch(info.�Ƕ�))
                {
                    throw new Exception($@"{info.�Ƕ�} �Ƕ������ʽ����ȷ��Ҫ���ʽ��0-90-180");
                }
                if (string.IsNullOrEmpty(info.Ѩ��105) || string.IsNullOrEmpty(info.Ѩ��104) || string.IsNullOrEmpty(info.Ѩ��102)
                    || string.IsNullOrEmpty(info.ϵ�к�) || string.IsNullOrEmpty(info.��Լ������))
                {
                    throw new Exception($@"���*���е�δ�������ݣ�*��Ϊ�����");
                }
                if (info.�ƻ�Ͷ���� == 0)
                {
                    throw new Exception($@"�ƻ�Ͷ����������Ϊ0��������12��������������600/1200��"); 
                }

                //��Ϊ�յ�һЩ����ֵ����null
                info.��װ����� = string.IsNullOrEmpty(info.��װ�����) ? "zzxt" : info.��װ�����;
                info.���� = string.IsNullOrEmpty(info.����) ? "jz" : info.����;
                info.�������� = (info.�������� == DateTime.MinValue) ? DateTime.Now : info.��������;
                info.��ͲģѨ�� = string.IsNullOrEmpty(info.��ͲģѨ��) ? "0" : info.��ͲģѨ��;
                info.�������� = string.IsNullOrEmpty(info.��������) ? "jkpc" : info.��������;
                info.����105 = (info.����105 == DateTime.MinValue) ? DateTime.Now : info.����105;
                info.����104 = (info.����104 == DateTime.MinValue) ? DateTime.Now : info.����104;
                info.����102 = (info.����102 == DateTime.MinValue) ? DateTime.Now : info.����102;
                info.��ȦģѨ��113b = string.IsNullOrEmpty(info.��ȦģѨ��113b) ? "0" : info.��ȦģѨ��113b;
                info.������113b = (info.������113b == DateTime.MinValue) ? DateTime.Now : info.������113b;
                info.��ȦģѨ��112 = string.IsNullOrEmpty(info.��ȦģѨ��112) ? "0" : info.��ȦģѨ��112;
                info.������112 = (info.������112 == DateTime.MinValue) ? DateTime.Now : info.������112;
                info.G3���Ϲ�Ӧ�� = string.IsNullOrEmpty(info.G3���Ϲ�Ӧ��) ? "g3sp" : info.G3���Ϲ�Ӧ��;
                info.G3��Ƭ�������� = (info.G3��Ƭ�������� == DateTime.MinValue) ? DateTime.Now : info.G3��Ƭ��������;
                info.G1���Ϲ�Ӧ�� = string.IsNullOrEmpty(info.G1���Ϲ�Ӧ��) ? "g1sp" : info.G1���Ϲ�Ӧ��;
                info.G1�������� = (info.G1�������� == DateTime.MinValue) ? DateTime.Now : info.G1��������;


                info.������ = info.������ = 0;
                info.ʱ�� = DateTime.Now;
                //�жϵ�ǰʱ���ǰװ໹��ҹ��
                string _strWorkingDayAM = "08:00";
                string _strWorkingDayPM = "20:00";
                TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
                TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
                TimeSpan dspNow = DateTime.Now.TimeOfDay;
                if (dspNow >= dspWorkingDayAM && dspNow <= dspWorkingDayPM)
                {
                    info.��� = "�װ�";
                }
                else
                {
                    info.��� = "ҹ��";
                }
                info.״̬ = "������";
                info.�������κ� = $@"{DateTime.Now.ToString("yyMMddhhmmss")}{info.ϵ�к�}";
                #endregion

                bool succeed = BLLFactory<�����������ɱ�>.Instance.Insert(info);
                if (succeed)
                {
                    //�����������������

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
        /// �༭״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {

            �����������ɱ�Info info = BLLFactory<�����������ɱ�>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������

                    //���Ƕȸ�ʽ�Ƿ���������   Ҫ���ʽ0-90-180
                    Regex reg = new Regex(@"^-?\d+--?\d+--?\d+");
                    if (!reg.IsMatch(info.�Ƕ�))
                    {
                        throw new Exception($@"{info.�Ƕ�} �Ƕ������ʽ����ȷ��Ҫ���ʽ��0-90-180");
                    }
                    if (string.IsNullOrEmpty(info.Ѩ��105) || string.IsNullOrEmpty(info.Ѩ��104) || string.IsNullOrEmpty(info.Ѩ��102)
                        || string.IsNullOrEmpty(info.ϵ�к�) || string.IsNullOrEmpty(info.��Լ������))
                    {
                        throw new Exception($@"���*���е�δ�������ݣ�*��Ϊ�����");
                    }
                    if (info.�ƻ�Ͷ���� == 0)
                    {
                        throw new Exception($@"�ƻ�Ͷ����������Ϊ0��������12��������������600/1200��");
                    }

                    bool succeed = BLLFactory<�����������ɱ�>.Instance.Update(info, info.��������id);
                    if (succeed)
                    {
                        //�����������������
                       
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
