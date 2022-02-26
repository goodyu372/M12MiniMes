using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.Entity
{
    /// <summary>
    /// 生产数据表Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class 生产数据表Info : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public 生产数据表Info()
		{
            this.生产数据id= 0;
                   this.治具孔号= 0;
             this.设备id= 0;
                this.结果ok= false;
  
		}

        #region Property Members
        
        /// <summary>
        /// 生产数据ID
        /// </summary>
		[DataMember]
        public virtual int 生产数据id { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
		[DataMember]
        public virtual DateTime 生产时间 { get; set; }

        /// <summary>
        /// 物料生产批次号
        /// </summary>
		[DataMember]
        public virtual string 物料生产批次号 { get; set; }

        /// <summary>
        /// 治具生产批次号
        /// </summary>
		[DataMember]
        public virtual string 治具生产批次号 { get; set; }

        /// <summary>
        /// 物料GUID
        /// </summary>
		[DataMember]
        public virtual string 物料guid { get; set; }

        /// <summary>
        /// 治具GUID
        /// </summary>
		[DataMember]
        public virtual string 治具guid { get; set; }

        /// <summary>
        /// 治具RFID
        /// </summary>
		[DataMember]
        public virtual string 治具rfid { get; set; }

        /// <summary>
        /// 治具孔号
        /// </summary>
		[DataMember]
        public virtual int 治具孔号 { get; set; }

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
        /// 工位号
        /// </summary>
		[DataMember]
        public virtual string 工位号 { get; set; }

        /// <summary>
        /// 工序数据
        /// </summary>
		[DataMember]
        public virtual string 工序数据 { get; set; }

        /// <summary>
        /// 结果OK
        /// </summary>
		[DataMember]
        public virtual bool 结果ok { get; set; }


        #endregion

    }
}