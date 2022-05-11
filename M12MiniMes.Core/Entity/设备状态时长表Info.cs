using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.Entity
{
    /// <summary>
    /// 设备状态时长表Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class 设备状态时长表Info : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public 设备状态时长表Info()
		{
            this.序号= 0;
              this.设备id= 0;
           
		}

        #region Property Members
        
		[DataMember]
        public virtual int 序号 { get; set; }

		[DataMember]
        public virtual string 班次 { get; set; }

		[DataMember]
        public virtual int 设备id { get; set; }

		[DataMember]
        public virtual string 设备名称 { get; set; }

		[DataMember]
        public virtual DateTime 记录时间 { get; set; }

		[DataMember]
        public virtual string 运行 { get; set; }

		[DataMember]
        public virtual string 等待 { get; set; }

		[DataMember]
        public virtual string 暂停 { get; set; }

		[DataMember]
        public virtual string 手动 { get; set; }

		[DataMember]
        public virtual string 报警 { get; set; }

		[DataMember]
        public virtual string 点检 { get; set; }

		[DataMember]
        public virtual string 维修 { get; set; }


        #endregion

    }
}