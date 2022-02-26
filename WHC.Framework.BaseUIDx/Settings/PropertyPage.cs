using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace WHC.Framework.BaseUI.Settings
{
	public class PropertyPage : BaseUserControl
	{
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// �Ƿ��Ѿ���ʼ��
        /// </summary>
        public bool IsInit { get; set; }
	
        /// <summary>
        /// ��ʼ������
        /// </summary>
		public PropertyPage()
		{
			InitComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		private void InitComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Overridables

        /// <summary>
        /// �ؼ��ı���ʾ
        /// </summary>
		public new virtual string Text
		{
			get { return this.GetType().Name; }
		}

        /// <summary>
        /// �ؼ�ͼƬ����
        /// </summary>
		public virtual Image Image
		{
			get { return null; }
		}

        /// <summary>
        /// ��ʼ������
        /// </summary>
		public virtual void OnInit()
		{
			this.IsInit = true;
		}

        /// <summary>
        /// ҳ�漤��Ĵ���
        /// </summary>
		public virtual void OnSetActive()
		{
		}

        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <returns></returns>
		public virtual bool OnApply()
		{
            return true;
		}


		#endregion
	}
}
