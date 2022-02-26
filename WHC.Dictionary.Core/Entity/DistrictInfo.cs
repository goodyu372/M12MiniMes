using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 行政区
    /// </summary>
    [Serializable]
    [DataContract]
    public class DistrictInfo : BaseEntity
    {    
        /// <summary>
        /// 构造函数
        /// </summary>
        public DistrictInfo()
        {

        }

        #region Property Members

        /// <summary>
        /// 行政区ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// 行政区名称
        /// </summary>
        [DataMember]
        public virtual string DistrictName { get; set; }

        /// <summary>
        /// 所属城市ID
        /// </summary>
        [DataMember]
        public virtual int CityID { get; set; }


        #endregion

    }
}