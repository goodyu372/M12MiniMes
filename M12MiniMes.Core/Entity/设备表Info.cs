using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.Entity
{
    /// <summary>
    /// 设备表Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class 设备表Info : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public 设备表Info()
		{
            this.设备id= 0;
               this.位置序号= 0;
             this.启用状态= false;
   
		}

        #region Property Members
        
        /// <summary>
        /// 设备ID
        /// </summary>
		[DataMember]
        public virtual int 设备id { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
		[DataMember]
        public virtual string 设备名称 { get; set; }

        /// <summary>
        /// IP
        /// </summary>
		[DataMember]
        public virtual string Ip { get; set; }

        /// <summary>
        /// 位置序号
        /// </summary>
		[DataMember]
        public virtual int 位置序号 { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
		[DataMember]
        public virtual bool 启用状态 { get; set; }

        /// <summary>
        /// 生产状态
        /// </summary>
		[DataMember]
        public virtual string 生产状态 { get; set; }


        #endregion

    }
}