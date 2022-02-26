using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// ȫ��ʡ��
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProvinceInfo : BaseEntity
    {
        /// <summary>
        /// Ĭ�Ϲ��캯������Ҫ��ʼ�����Ե��ڴ˴���
        /// </summary>
        public ProvinceInfo()
        {
            this.ID = 0;

        }

        #region Property Members

        /// <summary>
        /// ʡ��ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// ʡ������
        /// </summary>
        [DataMember]
        public virtual string ProvinceName { get; set; }

        #endregion

    }
}