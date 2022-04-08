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
    /// 设备状态表
    /// </summary>
	public class 设备状态表 : BaseDALSQL<设备状态表Info>, I设备状态表
	{
		#region 对象实例及构造函数

		public static 设备状态表 Instance
		{
			get
			{
				return new 设备状态表();
			}
		}
		public 设备状态表() : base("设备状态表","设备状态信息ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override 设备状态表Info DataReaderToEntity(IDataReader dataReader)
		{
			设备状态表Info info = new 设备状态表Info();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.设备状态信息id = reader.GetInt32("设备状态信息ID");
			info.发生时间 = reader.GetDateTime("发生时间");
			info.设备id = reader.GetInt32("设备id");
			info.设备名称 = reader.GetString("设备名称");
			info.设备状态 = reader.GetString("设备状态");
			info.报警信息 = reader.GetString("报警信息");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(设备状态表Info obj)
		{
		    设备状态表Info info = obj as 设备状态表Info;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("发生时间", info.发生时间);
 			hash.Add("设备id", info.设备id);
 			hash.Add("设备名称", info.设备名称);
 			hash.Add("设备状态", info.设备状态);
 			hash.Add("报警信息", info.报警信息);
 				
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
             dict.Add("设备名称", "");
             dict.Add("设备状态", "");
             dict.Add("报警信息", "");
             #endregion

            return dict;
        }
		
        /// <summary>
        /// 指定具体的列表显示字段
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayColumns()
        {
            return "设备状态信息ID,发生时间,设备id,设备名称,设备状态,报警信息";
        }
    }
}