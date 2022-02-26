using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// ����
    /// </summary>
    [Serializable]
    [DataContract]
    public class CityInfo : BaseEntity
    {
        /// <summary>
        /// Ĭ�Ϲ��캯������Ҫ��ʼ�����Ե��ڴ˴���
        /// </summary>
        public CityInfo()
        {
            this.ID = 0;
            this.ProvinceID = 0;
        }

        #region Property Members

        /// <summary>
        /// ����ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public virtual string CityName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// ����ʡ��ID
        /// </summary>
        [DataMember]
        public virtual int ProvinceID { get; set; }

        #endregion

    }
}