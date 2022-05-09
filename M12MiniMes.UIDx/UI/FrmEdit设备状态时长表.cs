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
    /// �豸״̬ʱ����
    /// </summary>	
    public partial class FrmEdit�豸״̬ʱ���� : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private �豸״̬ʱ����Info tempInfo = new �豸״̬ʱ����Info();
    	
        public FrmEdit�豸״̬ʱ����()
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
                �豸״̬ʱ����Info info = BLLFactory<�豸״̬ʱ����>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
                	txt�豸id.Value = info.�豸id;
                       txt�豸����.Text = info.�豸����;
                   	txt��¼ʱ��.SetDateTime(info.��¼ʱ��);	
                      txt����.Text = info.����;
                       txt�ȴ�.Text = info.�ȴ�;
                       txt��ͣ.Text = info.��ͣ;
                       txt�ֶ�.Text = info.�ֶ�;
                       txt����.Text = info.����;
                       txt���.Text = info.���;
                       txtά��.Text = info.ά��;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("�豸״̬ʱ����/Edit");             
            }
            else
            {
          
                //this.btnOK.Enabled = HasFunction("�豸״̬ʱ����/Add");  
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
            //this.txt�豸id.Tag = "�豸id";
            //this.txt�豸����.Tag = "�豸����";
            //this.txt��¼ʱ��.Tag = "��¼ʱ��";
            //this.txt����.Tag = "����";
            //this.txt�ȴ�.Tag = "�ȴ�";
            //this.txt��ͣ.Tag = "��ͣ";
            //this.txt�ֶ�.Tag = "�ֶ�";
            //this.txt����.Tag = "����";
            //this.txt���.Tag = "���";
            //this.txtά��.Tag = "ά��";
            #endregion
			
            //��ȡ�б�Ȩ�޵��б�
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(�豸״̬ʱ����Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// �鿴�༭������Ϣ
        /// </summary>
        //private void SetAttachInfo(�豸״̬ʱ����Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "�豸״̬ʱ����";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.���, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new �豸״̬ʱ����Info();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(�豸״̬ʱ����Info info)
        {
            info.�豸id = Convert.ToInt32(txt�豸id.Value);
                info.�豸���� = txt�豸����.Text;
                info.��¼ʱ�� = txt��¼ʱ��.DateTime;
               info.���� = txt����.Text;
                info.�ȴ� = txt�ȴ�.Text;
                info.��ͣ = txt��ͣ.Text;
                info.�ֶ� = txt�ֶ�.Text;
                info.���� = txt����.Text;
                info.��� = txt���.Text;
                info.ά�� = txtά��.Text;
            }
         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            �豸״̬ʱ����Info info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������

                bool succeed = BLLFactory<�豸״̬ʱ����>.Instance.Insert(info);
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

            �豸״̬ʱ����Info info = BLLFactory<�豸״̬ʱ����>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<�豸״̬ʱ����>.Instance.Update(info, info.���);
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
