using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.Commons;

namespace WHC.Framework.ControlUtil
{
	/// <summary>
	/// ���ʵ����Ļ���
	/// </summary>
    [DataContract]
    [Serializable]
    public class BaseEntity
    {
        /// <summary>
        /// ��ǰ��¼�û�ID�����ֶβ����浽���ݱ��У�ֻ���ڼ�¼�û��Ĳ�����־��
        /// </summary>
        [DataMember]
        public string CurrentLoginUserId { get; set; }

        #region ��ʵ����洢һЩ���������
        /// <summary>
        /// ������ʵ���ഫ��һЩ��������ݣ��������ת��ȣ����ֶβ����浽���ݱ���
        /// </summary>
        [DataMember]
        public string Data1 { get; set; }

        /// <summary>
        /// ������ʵ���ഫ��һЩ��������ݣ��������ת��ȣ����ֶβ����浽���ݱ���
        /// </summary>
        [DataMember]
        public string Data2 { get; set; }

        /// <summary>
        /// ������ʵ���ഫ��һЩ��������ݣ��������ת��ȣ����ֶβ����浽���ݱ���
        /// </summary>
        [DataMember]
        public string Data3 { get; set; } 
        #endregion
    }
}
