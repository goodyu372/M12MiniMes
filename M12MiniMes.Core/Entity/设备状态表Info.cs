using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.Entity
{
    /// <summary>
    /// 设备状态表Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class 设备状态表Info : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public 设备状态表Info()
		{
            this.设备状态信息id= 0;
              this.设备id= 0;
     
		}

        #region Property Members
        
		[DataMember]
        public virtual int 设备状态信息id { get; set; }

		[DataMember]
        public virtual DateTime 发生时间 { get; set; }

		[DataMember]
        public virtual int 设备id { get; set; }

		[DataMember]
        public virtual string 设备名称 { get; set; }

		[DataMember]
        public virtual string 设备状态 { get; set; }

		[DataMember]
        public virtual string 报警信息 { get; set; }


        #endregion

    }
}