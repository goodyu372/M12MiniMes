using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.Entity
{
    /// <summary>
    /// 公司字典数据
    /// </summary>
    [DataContract]
    public class CorpDictDataInfo : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public CorpDictDataInfo()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.LastUpdated = System.DateTime.Now;
        }

         /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary> 
        public CorpDictDataInfo(DictDataInfo info, string corpId) : this()
        {
            //this.ID = info.ID;
            //this.LastUpdated = info.LastUpdated;
            this.Name = info.Name;
            this.Value = info.Value;
            this.Remark = info.Remark;
            this.Seq = info.Seq;
            this.Editor = info.Editor;
            this.DictType_ID = info.DictType_ID;

            this.Corp_ID = corpId;
        }

        #region Property Members
        
        /// <summary>
        /// 编号
        /// </summary>
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 字典大类
        /// </summary>
		[DataMember]
        public virtual string DictType_ID { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
		[DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
		[DataMember]
        public virtual string Value { get; set; }

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
        /// 所属公司
        /// </summary>
		[DataMember]
        public virtual string Corp_ID { get; set; }


        #endregion

    }
}