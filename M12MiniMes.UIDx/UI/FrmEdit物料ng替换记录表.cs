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
    /// ����ng�滻��¼��
    /// </summary>	
    public partial class FrmEdit����ng�滻��¼�� : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private ����ng�滻��¼��Info tempInfo = new ����ng�滻��¼��Info();
    	
        public FrmEdit����ng�滻��¼��()
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
                ����ng�滻��¼��Info info = BLLFactory<����ng�滻��¼��>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
                	txtNg�滻ʱ��.SetDateTime(info.Ng�滻ʱ��);	
                      txt�����������κ�.Text = info.�����������κ�;
                   	txt�豸id.Value = info.�豸id;
                       txt�豸����.Text = info.�豸����;
                       txt��λ��.Text = info.��λ��;
                       txt����guid.Text = info.����guid;
                       txt�滻ǰ�ξ�guid.Text = info.�滻ǰ�ξ�guid;
                       txt�滻ǰ�ξ�rfid.Text = info.�滻ǰ�ξ�rfid;
                   	txt�滻ǰ�ξ߿׺�.Value = info.�滻ǰ�ξ߿׺�;
                       txtǰ�ξ��������κ�.Text = info.ǰ�ξ��������κ�;
                       txt�滻���ξ�guid.Text = info.�滻���ξ�guid;
                       txt�滻���ξ�rfid.Text = info.�滻���ξ�rfid;
                   	txt�滻���ξ߿׺�.Value = info.�滻���ξ߿׺�;
                       txt���ξ��������κ�.Text = info.���ξ��������κ�;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("����ng�滻��¼��/Edit");             
            }
            else
            {
              
                //this.btnOK.Enabled = HasFunction("����ng�滻��¼��/Add");  
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
            //this.txtNg�滻ʱ��.Tag = "Ng�滻ʱ��";
            //this.txt�����������κ�.Tag = "�����������κ�";
            //this.txt�豸id.Tag = "�豸id";
            //this.txt�豸����.Tag = "�豸����";
            //this.txt��λ��.Tag = "��λ��";
            //this.txt����guid.Tag = "����guid";
            //this.txt�滻ǰ�ξ�guid.Tag = "�滻ǰ�ξ�guid";
            //this.txt�滻ǰ�ξ�rfid.Tag = "�滻ǰ�ξ�rfid";
            //this.txt�滻ǰ�ξ߿׺�.Tag = "�滻ǰ�ξ߿׺�";
            //this.txtǰ�ξ��������κ�.Tag = "ǰ�ξ��������κ�";
            //this.txt�滻���ξ�guid.Tag = "�滻���ξ�guid";
            //this.txt�滻���ξ�rfid.Tag = "�滻���ξ�rfid";
            //this.txt�滻���ξ߿׺�.Tag = "�滻���ξ߿׺�";
            //this.txt���ξ��������κ�.Tag = "���ξ��������κ�";
            #endregion
			
            //��ȡ�б�Ȩ�޵��б�
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(����ng�滻��¼��Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// �鿴�༭������Ϣ
        /// </summary>
        //private void SetAttachInfo(����ng�滻��¼��Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "����ng�滻��¼��";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.Ng�滻��¼id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new ����ng�滻��¼��Info();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(����ng�滻��¼��Info info)
        {
            info.Ng�滻ʱ�� = txtNg�滻ʱ��.DateTime;
               info.�����������κ� = txt�����������κ�.Text;
                info.�豸id = Convert.ToInt32(txt�豸id.Value);
                info.�豸���� = txt�豸����.Text;
                info.��λ�� = txt��λ��.Text;
                info.����guid = txt����guid.Text;
                info.�滻ǰ�ξ�guid = txt�滻ǰ�ξ�guid.Text;
                info.�滻ǰ�ξ�rfid = txt�滻ǰ�ξ�rfid.Text;
                info.�滻ǰ�ξ߿׺� = Convert.ToInt32(txt�滻ǰ�ξ߿׺�.Value);
                info.ǰ�ξ��������κ� = txtǰ�ξ��������κ�.Text;
                info.�滻���ξ�guid = txt�滻���ξ�guid.Text;
                info.�滻���ξ�rfid = txt�滻���ξ�rfid.Text;
                info.�滻���ξ߿׺� = Convert.ToInt32(txt�滻���ξ߿׺�.Value);
                info.���ξ��������κ� = txt���ξ��������κ�.Text;
            }
         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            ����ng�滻��¼��Info info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������

                bool succeed = BLLFactory<����ng�滻��¼��>.Instance.Insert(info);
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

            ����ng�滻��¼��Info info = BLLFactory<����ng�滻��¼��>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<����ng�滻��¼��>.Instance.Update(info, info.Ng�滻��¼id);
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
