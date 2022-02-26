using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    [DataContract]
    public class DistrictInfo : BaseEntity
    {    
        /// <summary>
        /// ���캯��
        /// </summary>
        public DistrictInfo()
        {

        }

        #region Property Members

        /// <summary>
        /// ������ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        [DataMember]
        public virtual string DistrictName { get; set; }

        /// <summary>
        /// ��������ID
        /// </summary>
        [DataMember]
        public virtual int CityID { get; set; }


        #endregion

    }
}