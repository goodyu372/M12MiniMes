using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WHC.Dictionary.Entity;
using WHC.Dictionary.IDAL;

namespace WHC.Dictionary.DALOracle
{
    /// <summary>
    /// 公司字典数据
    /// </summary>
    public class CorpDictData : BaseDALOracle<CorpDictDataInfo>, ICorpDictData
	{
		#region 对象实例及构造函数

		public static CorpDictData Instance
		{
			get
			{
				return new CorpDictData();
			}
		}
		public CorpDictData() : base("TB_CorpDictData","ID")
        {
            this.SeqName = "";//由于字符型组件，不需要序列
		}

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dataReader">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override CorpDictDataInfo DataReaderToEntity(IDataReader dataReader)
		{
			CorpDictDataInfo info = new CorpDictDataInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetString("ID");
			info.DictType_ID = reader.GetString("DictType_ID");
			info.Name = reader.GetString("Name");
			info.Value = reader.GetString("Value");
			info.Remark = reader.GetString("Remark");
			info.Seq = reader.GetString("Seq");
			info.Editor = reader.GetString("Editor");
			info.LastUpdated = reader.GetDateTime("LastUpdated");
			info.Corp_ID = reader.GetString("Corp_ID");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(CorpDictDataInfo obj)
		{
		    CorpDictDataInfo info = obj as CorpDictDataInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("DictType_ID", info.DictType_ID);
 			hash.Add("Name", info.Name);
 			hash.Add("Value", info.Value);
 			hash.Add("Remark", info.Remark);
 			hash.Add("Seq", info.Seq);
 			hash.Add("Editor", info.Editor);
 			hash.Add("LastUpdated", info.LastUpdated);
 			hash.Add("Corp_ID", info.Corp_ID);
 				
			return hash;
		}

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            //dict.Add("ID", "编号");
            dict.Add("ID", "编号");
             dict.Add("DictType_ID", "字典大类");
             dict.Add("Name", "字典名称");
             dict.Add("Value", "字典值");
             dict.Add("Remark", "备注");
             dict.Add("Seq", "排序");
             dict.Add("Editor", "编辑者");
             dict.Add("LastUpdated", "编辑时间");
             dict.Add("Corp_ID", "所属公司");
             #endregion

            return dict;
        }
                        
        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public List<CorpDictDataInfo> FindByTypeID(string dictTypeId, string corpId)
        {
            string condition = string.Format("DictType_ID='{0}' AND Corp_ID='{1}' ", dictTypeId, corpId);
            return Find(condition);
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public List<CorpDictDataInfo> FindByDictType(string dictTypeName, string corpId)
        {
            string sql = string.Format(@"select d.* from TB_CorpDictData d inner join tb_DictType t on d.DictType_ID = t.ID 
            where t.Name ='{0}' AND d.Corp_ID='{1}' ", dictTypeName, corpId);

            return base.GetList(sql);
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictByDictType(string dictTypeName, string corpId)
        {
            string sql = string.Format(@"select d.Name,d.Value from TB_CorpDictData d inner join tb_DictType t on d.DictType_ID = t.ID 
            where t.Name ='{0}' AND d.Corp_ID='{1}' order by d.{2} {3}",
                dictTypeName, corpId, SortField, IsDescending ? "DESC" : "ASC");

            return GetDictBySql(sql);
        }

        private Dictionary<string, string> GetDictBySql(string sql)
        {
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            Dictionary<string, string> list = new Dictionary<string, string>();
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    string name = dr["Name"].ToString();
                    string value = dr["Value"].ToString();
                    if (!list.ContainsKey(name))
                    {
                        list.Add(name, value);
                    }
                }
            }
            return list;
        }

    }
}