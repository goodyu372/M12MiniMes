using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.Entity
{
    /// <summary>
    /// 生产批次生成表Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class 生产批次生成表Info : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public 生产批次生成表Info()
		{
            this.生产批次id= 0;
                            this.镜框投料数= 0;
                 this.隔圈投料数= 0;
                 this.镜片105投料数= 0;
             this.镜片104投料数= 0;
             this.镜片g3投料数= 0;
             this.镜片102投料数= 0;
             this.镜片95b投料数= 0;
              this.计划投入数= 0;
             this.上线数= 0;
             this.下线数= 0;
    
		}

        #region Property Members
        
        /// <summary>
        /// 生产批次ID
        /// </summary>
		[DataMember]
        public virtual int 生产批次id { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
		[DataMember]
        public virtual DateTime 时间 { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
		[DataMember]
        public virtual string 班次 { get; set; }

        /// <summary>
        /// 组装线体号
        /// </summary>
		[DataMember]
        public virtual string 组装线体号 { get; set; }

        /// <summary>
        /// 机种
        /// </summary>
		[DataMember]
        public virtual string 机种 { get; set; }

        /// <summary>
        /// 镜框日期
        /// </summary>
		[DataMember]
        public virtual DateTime 镜框日期 { get; set; }

        /// <summary>
        /// 镜筒模穴号
        /// </summary>
		[DataMember]
        public virtual string 镜筒模穴号 { get; set; }

        /// <summary>
        /// 镜框批次
        /// </summary>
		[DataMember]
        public virtual string 镜框批次 { get; set; }

        /// <summary>
        /// 穴号105
        /// </summary>
		[DataMember]
        public virtual string 穴号105 { get; set; }

        /// <summary>
        /// 穴号104
        /// </summary>
		[DataMember]
        public virtual string 穴号104 { get; set; }

        /// <summary>
        /// 穴号102
        /// </summary>
		[DataMember]
        public virtual string 穴号102 { get; set; }

        /// <summary>
        /// 日期105
        /// </summary>
		[DataMember]
        public virtual DateTime 日期105 { get; set; }

        /// <summary>
        /// 日期104
        /// </summary>
		[DataMember]
        public virtual DateTime 日期104 { get; set; }

        /// <summary>
        /// 日期102
        /// </summary>
		[DataMember]
        public virtual DateTime 日期102 { get; set; }

        /// <summary>
        /// 角度
        /// </summary>
		[DataMember]
        public virtual string 角度 { get; set; }

        /// <summary>
        /// 系列号
        /// </summary>
		[DataMember]
        public virtual string 系列号 { get; set; }

        /// <summary>
        /// 镜框投料数
        /// </summary>
		[DataMember]
        public virtual int 镜框投料数 { get; set; }

        /// <summary>
        /// 隔圈模穴号113B
        /// </summary>
		[DataMember]
        public virtual string 隔圈模穴号113b { get; set; }

        /// <summary>
        /// 成型日113B
        /// </summary>
		[DataMember]
        public virtual DateTime 成型日113b { get; set; }

        /// <summary>
        /// 隔圈模穴号112
        /// </summary>
		[DataMember]
        public virtual string 隔圈模穴号112 { get; set; }

        /// <summary>
        /// 成型日112
        /// </summary>
		[DataMember]
        public virtual DateTime 成型日112 { get; set; }

        /// <summary>
        /// 隔圈投料数
        /// </summary>
		[DataMember]
        public virtual int 隔圈投料数 { get; set; }

        /// <summary>
        /// G3来料供应商
        /// </summary>
		[DataMember]
        public virtual string G3来料供应商 { get; set; }

        /// <summary>
        /// G3镜片来料日期
        /// </summary>
		[DataMember]
        public virtual DateTime G3镜片来料日期 { get; set; }

        /// <summary>
        /// G1来料供应商
        /// </summary>
		[DataMember]
        public virtual string G1来料供应商 { get; set; }

        /// <summary>
        /// G1来料日期
        /// </summary>
		[DataMember]
        public virtual DateTime G1来料日期 { get; set; }

        /// <summary>
        /// 镜片105投料数
        /// </summary>
		[DataMember]
        public virtual int 镜片105投料数 { get; set; }

        /// <summary>
        /// 镜片104投料数
        /// </summary>
		[DataMember]
        public virtual int 镜片104投料数 { get; set; }

        /// <summary>
        /// 镜片G3投料数
        /// </summary>
		[DataMember]
        public virtual int 镜片g3投料数 { get; set; }

        /// <summary>
        /// 镜片102投料数
        /// </summary>
		[DataMember]
        public virtual int 镜片102投料数 { get; set; }

        /// <summary>
        /// 镜片95B投料数
        /// </summary>
		[DataMember]
        public virtual int 镜片95b投料数 { get; set; }

        /// <summary>
        /// 配对监控批次
        /// </summary>
		[DataMember]
        public virtual string 配对监控批次 { get; set; }

        /// <summary>
        /// 计划投入数
        /// </summary>
		[DataMember]
        public virtual int 计划投入数 { get; set; }

        /// <summary>
        /// 上线数
        /// </summary>
		[DataMember]
        public virtual int 上线数 { get; set; }

        /// <summary>
        /// 下线数
        /// </summary>
		[DataMember]
        public virtual int 下线数 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
		[DataMember]
        public virtual string 状态 { get; set; }

        /// <summary>
        /// 生成出的生产批次号
        /// </summary>
		[DataMember]
        public virtual string 生产批次号 { get; set; }


        #endregion

    }
}