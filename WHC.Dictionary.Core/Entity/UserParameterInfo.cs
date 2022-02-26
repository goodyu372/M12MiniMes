using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 用户参数配置
    /// </summary>
    [DataContract]
    public class UserParameterInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public UserParameterInfo()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.CreateTime = System.DateTime.Now;

        }

        #region Property Members
        
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
		[DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 参数文本内容
        /// </summary>
		[DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		[DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreateTime { get; set; }

        #endregion

    }
}