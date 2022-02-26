using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 应用程序渠道信息
    /// </summary>
    [DataContract]
    public class AppChannelInfo : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public AppChannelInfo()
		{
            this.ID= System.Guid.NewGuid().ToString();
       
		}

        #region Property Members
        
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
		[DataMember]
        public virtual string AppId { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
		[DataMember]
        public virtual string AppSecret { get; set; }

        /// <summary>
        /// 渠道类别:渠道，0为网站，1为微信，2为安卓APP，3为苹果APP
        /// </summary>
		[DataMember]
        public virtual string Channel { get; set; }

        /// <summary>
        /// 渠道名称
        /// </summary>
		[DataMember]
        public virtual string ChannelName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
		[DataMember]
        public virtual string Note { get; set; }


        #endregion

    }
}