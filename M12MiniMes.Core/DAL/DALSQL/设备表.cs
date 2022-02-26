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
    /// 设备表
    /// </summary>
	public class 设备表 : BaseDALSQL<设备表Info>, I设备表
	{
		#region 对象实例及构造函数

		public static 设备表 Instance
		{
			get
			{
				return new 设备表();
			}
		}
		public 设备表() : base("设备表","设备ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override 设备表Info DataReaderToEntity(IDataReader dataReader)
		{
			设备表Info info = new 设备表Info();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.设备id = reader.GetInt32("设备ID");
			info.设备名称 = reader.GetString("设备名称");
			info.Ip = reader.GetString("IP");
			info.位置序号 = reader.GetInt32("位置序号");
			info.启用状态 = reader.GetBoolean("启用状态");
			info.生产状态 = reader.GetString("生产状态");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(设备表Info obj)
		{
		    设备表Info info = obj as 设备表Info;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("设备名称", info.设备名称);
 			hash.Add("IP", info.Ip);
 			hash.Add("位置序号", info.位置序号);
 			hash.Add("启用状态", info.启用状态);
 			hash.Add("生产状态", info.生产状态);
 				
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
             dict.Add("设备名称", "设备名称");
             dict.Add("Ip", "IP");
             dict.Add("位置序号", "位置序号");
             dict.Add("启用状态", "启用状态");
             dict.Add("生产状态", "生产状态");
             #endregion

            return dict;
        }
		
        /// <summary>
        /// 指定具体的列表显示字段
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayColumns()
        {
            return "设备ID,设备名称,IP,位置序号,启用状态,生产状态";
        }
    }
}