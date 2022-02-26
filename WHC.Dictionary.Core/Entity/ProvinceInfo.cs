using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 全国省份
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProvinceInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public ProvinceInfo()
        {
            this.ID = 0;

        }

        #region Property Members

        /// <summary>
        /// 省份ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        [DataMember]
        public virtual string ProvinceName { get; set; }

        #endregion

    }
}