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
    /// 设备状态时长表
    /// </summary>
	public class 设备状态时长表 : BaseDALSQL<设备状态时长表Info>, I设备状态时长表
	{
		#region 对象实例及构造函数

		public static 设备状态时长表 Instance
		{
			get
			{
				return new 设备状态时长表();
			}
		}
		public 设备状态时长表() : base("设备状态时长表","序号")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override 设备状态时长表Info DataReaderToEntity(IDataReader dataReader)
		{
			设备状态时长表Info info = new 设备状态时长表Info();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.序号 = reader.GetInt32("序号");
			info.班次 = reader.GetString("班次");
			info.设备id = reader.GetInt32("设备id");
			info.设备名称 = reader.GetString("设备名称");
			info.记录时间 = reader.GetDateTime("记录时间");
			info.运行 = reader.GetString("运行");
			info.等待 = reader.GetString("等待");
			info.暂停 = reader.GetString("暂停");
			info.手动 = reader.GetString("手动");
			info.报警 = reader.GetString("报警");
			info.点检 = reader.GetString("点检");
			info.维修 = reader.GetString("维修");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(设备状态时长表Info obj)
		{
		    设备状态时长表Info info = obj as 设备状态时长表Info;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("班次", info.班次);
 			hash.Add("设备id", info.设备id);
 			hash.Add("设备名称", info.设备名称);
 			hash.Add("记录时间", info.记录时间);
 			hash.Add("运行", info.运行);
 			hash.Add("等待", info.等待);
 			hash.Add("暂停", info.暂停);
 			hash.Add("手动", info.手动);
 			hash.Add("报警", info.报警);
 			hash.Add("点检", info.点检);
 			hash.Add("维修", info.维修);
 				
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
             dict.Add("班次", "");
             dict.Add("设备id", "");
             dict.Add("设备名称", "");
             dict.Add("记录时间", "");
             dict.Add("运行", "");
             dict.Add("等待", "");
             dict.Add("暂停", "");
             dict.Add("手动", "");
             dict.Add("报警", "");
             dict.Add("点检", "");
             dict.Add("维修", "");
             #endregion

            return dict;
        }
		
        /// <summary>
        /// 指定具体的列表显示字段
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayColumns()
        {
            return "序号,班次,设备id,设备名称,记录时间,运行,等待,暂停,手动,报警,点检,维修";
        }
    }
}