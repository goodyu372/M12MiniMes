using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Serializable]
    [DataContract]
    public class DictTypeInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public DictTypeInfo()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.LastUpdated = System.DateTime.Now;

        }

        #region Property Members

        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }   
        
        /// <summary>
        /// 字典代码
        /// </summary>
        [DataMember]
        public virtual string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public virtual string Seq { get; set; }

        /// <summary>
        /// 编辑者
        /// </summary>
        [DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        [DataMember]
        public virtual DateTime LastUpdated { get; set; }

        /// <summary>
        /// 字典大类
        /// </summary>
        [DataMember]
        public virtual string PID { get; set; }


        #endregion

    }
}