using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 城市
    /// </summary>
    [Serializable]
    [DataContract]
    public class CityInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public CityInfo()
        {
            this.ID = 0;
            this.ProvinceID = 0;
        }

        #region Property Members

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        [DataMember]
        public virtual string CityName { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [DataMember]
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// 所属省份ID
        /// </summary>
        [DataMember]
        public virtual int ProvinceID { get; set; }

        #endregion

    }
}