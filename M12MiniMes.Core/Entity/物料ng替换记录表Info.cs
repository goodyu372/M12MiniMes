using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.Entity
{
    /// <summary>
    /// 物料ng替换记录表Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class 物料ng替换记录表Info : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public 物料ng替换记录表Info()
		{
            this.Ng替换记录id= 0;
               this.设备id= 0;
                  this.替换前治具孔号= 0;
                this.替换后治具孔号= 0;
   
		}

        #region Property Members
        
        /// <summary>
        /// NG替换记录ID
        /// </summary>
		[DataMember]
        public virtual int Ng替换记录id { get; set; }

        /// <summary>
        /// NG替换时间
        /// </summary>
		[DataMember]
        public virtual DateTime Ng替换时间 { get; set; }

        /// <summary>
        /// 物料生产批次号
        /// </summary>
		[DataMember]
        public virtual string 物料生产批次号 { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
		[DataMember]
        public virtual int 设备id { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
		[DataMember]
        public virtual string 设备名称 { get; set; }

        /// <summary>
        /// 工位号
        /// </summary>
		[DataMember]
        public virtual string 工位号 { get; set; }

        /// <summary>
        /// 物料GUID
        /// </summary>
		[DataMember]
        public virtual string 物料guid { get; set; }

        /// <summary>
        /// 替换前治具GUID
        /// </summary>
		[DataMember]
        public virtual string 替换前治具guid { get; set; }

        /// <summary>
        /// 替换前治具RFID
        /// </summary>
		[DataMember]
        public virtual string 替换前治具rfid { get; set; }

        /// <summary>
        /// 替换前治具孔号
        /// </summary>
		[DataMember]
        public virtual int 替换前治具孔号 { get; set; }

        /// <summary>
        /// 前治具生产批次号
        /// </summary>
		[DataMember]
        public virtual string 前治具生产批次号 { get; set; }

        /// <summary>
        /// 替换后治具GUID
        /// </summary>
		[DataMember]
        public virtual string 替换后治具guid { get; set; }

        /// <summary>
        /// 替换后治具RFID
        /// </summary>
		[DataMember]
        public virtual string 替换后治具rfid { get; set; }

        /// <summary>
        /// 替换后治具孔号
        /// </summary>
		[DataMember]
        public virtual int 替换后治具孔号 { get; set; }

        /// <summary>
        /// 后治具生产批次号
        /// </summary>
		[DataMember]
        public virtual string 后治具生产批次号 { get; set; }


        #endregion

    }
}