using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WHC.Dictionary.Entity;
using WHC.Dictionary.IDAL;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.DALSQL
{
	/// <summary>
	/// DictData 的摘要说明。
	/// </summary>
    public class DictData : BaseDALSQL<DictDataInfo>, IDictData
	{
		#region 对象实例及构造函数

		public static DictData Instance
		{
			get
			{
				return new DictData();
			}
		}
		public DictData() : base("tb_DictData","ID")
		{
            SortField = "Seq";
            IsDescending = false;
		}

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dataReader">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override DictDataInfo DataReaderToEntity(IDataReader dataReader)
		{
			DictDataInfo dictDataInfo = new DictDataInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            dictDataInfo.ID = reader.GetString("ID");
            dictDataInfo.DictType_ID = reader.GetString("DictType_ID");
			dictDataInfo.Name = reader.GetString("Name");
			dictDataInfo.Value = reader.GetString("Value");
			dictDataInfo.Remark = reader.GetString("Remark");
			dictDataInfo.Seq = reader.GetString("Seq");
            dictDataInfo.Editor = reader.GetString("Editor");
			dictDataInfo.LastUpdated = reader.GetDateTime("LastUpdated");
			
			return dictDataInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(DictDataInfo obj)
		{
		    DictDataInfo info = obj as DictDataInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("ID", info.ID);
 			hash.Add("DictType_ID", info.DictType_ID);
 			hash.Add("Name", info.Name);
 			hash.Add("Value", info.Value);
 			hash.Add("Remark", info.Remark);
 			hash.Add("Seq", info.Seq);
 			hash.Add("Editor", info.Editor);
 			hash.Add("LastUpdated", info.LastUpdated);
 				
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
            #endregion

            return dict;
        }

        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId"></param>
        /// <returns></returns>
        public List<DictDataInfo> FindByTypeID(string dictTypeId)
        {
            string condition = string.Format("DictType_ID='{0}' ", dictTypeId);
            return Find(condition);
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <returns></returns>
        public List<DictDataInfo> FindByDictType(string dictTypeName)
        {
            string sql = string.Format("select d.* from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.Name ='{0}'", 
                dictTypeName);
            sql += string.Format(" Order by d.{0} {1}", GetSafeFileName(SortField), IsDescending ? "DESC" : "ASC");

            return base.GetList(sql);
        }

        /// <summary>
        /// 根据字典类型代码获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictCode">字典类型代码</param>
        /// <returns></returns>
        public List<DictDataInfo> FindByDictCode(string dictCode)
        {
            string sql = string.Format(@"select d.* from tb_DictData d inner join tb_DictType t 
            on d.DictType_ID = t.ID where t.Code ='{0}'", dictCode);
            sql += string.Format(" Order by d.{0} {1}", GetSafeFileName(SortField), IsDescending ? "DESC" : "ASC");

            return base.GetList(sql);
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

        /// <summary>
        /// 获取所有的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllDict()
        {
            string sql = string.Format("select d.Name,d.Value from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID order by d.{0} {1}", 
                SortField, IsDescending ? "DESC" :"ASC" );

            return GetDictBySql(sql);
        }
        
        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictByTypeID(string dictTypeId)
        {
            string sql = string.Format("select d.Name,d.Value from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.ID ='{0}' order by d.{1} {2}", 
                dictTypeId, SortField, IsDescending ? "DESC" : "ASC");

            return GetDictBySql(sql);
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictByDictType(string dictTypeName)
        {
            string sql = string.Format("select d.Name,d.Value from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.Name ='{0}' order by d.{1} {2}", 
                dictTypeName, SortField, IsDescending ? "DESC" : "ASC");

            return GetDictBySql(sql);
        }

        /// <summary>
        /// 根据字典类型名称和字典Value值（即字典编码），解析成字典对应的名称
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="dictValue">字典Value值，即字典编码</param>
        /// <returns>字典对应的名称</returns>
        public string GetDictName(string dictTypeName, string dictValue)
        {
            string sql = string.Format("select d.Name from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.Name ='{0}' and d.Value='{1}'",
                dictTypeName, dictValue);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            string name = "";
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    name = dr["Name"].ToString();
                }
            }

            return name;
        }
    }
}