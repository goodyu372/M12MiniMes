using System;
using System.Collections.Generic;
using System.Text;

namespace WHC.Framework.BaseUI.Settings
{
    /// <summary>
    /// ҳ�������Ϣ
    /// </summary>
	public class PageProp
    {       
        /// <summary>
        /// �ؼ��ı���ʾ
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// �ؼ�ͼƬ����
        /// </summary>
        public int ImageIndex { get; set; }

        /// <summary>
        /// PropertyPage����
        /// </summary>
        public PropertyPage Page { get; set; }
        
        /// <summary>
        /// MozItem����
        /// </summary>
        public MozItem MozItem { get; set; }	
	}
}
