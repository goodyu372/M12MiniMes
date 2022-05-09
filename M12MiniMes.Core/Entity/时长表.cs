using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using M12MiniMes.Entity;
using M12MiniMes.IDAL;

namespace M12MiniMes.DALSQL
{
    /// <summary>
    /// 时长表
    /// </summary>
	public class 时长表 : BaseDALSQL<时长表Info>, I时长表
	{
		#region 对象实例及构造函数

		public static 时长表 Instance
		{
			get
			{
				return new 时长表();
			}
		}
		public 时长表() : base("时长表","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override 时长表Info DataReaderToEntity(IDataReader dataReader)
		{
			时长表Info info = new 时长表Info();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.发生时间 = reader.GetString("发生时间");
			info.设备id = reader.GetString("设备ID");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(时长表Info obj)
		{
		    时长表Info info = obj as 时长表Info;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("发生时间", info.发生时间);
 			hash.Add("设备ID", info.设备id);
 				
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
             dict.Add("发生时间", "");
             dict.Add("设备id", "");
             #endregion

            return dict;
        }
		
        /// <summary>
        /// 指定具体的列表显示字段
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayColumns()
        {
            return "ID,发生时间,设备ID";
        }
    }
}