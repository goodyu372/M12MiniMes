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
    /// 生产批次生成表
    /// </summary>
	public class 生产批次生成表 : BaseDALSQL<生产批次生成表Info>, I生产批次生成表
	{
		#region 对象实例及构造函数

		public static 生产批次生成表 Instance
		{
			get
			{
				return new 生产批次生成表();
			}
		}
		public 生产批次生成表() : base("生产批次生成表","生产批次ID")
		{
            IsDescending = false; 
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override 生产批次生成表Info DataReaderToEntity(IDataReader dataReader)
		{
			生产批次生成表Info info = new 生产批次生成表Info();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.生产批次id = reader.GetInt32("生产批次ID");
			info.时间 = reader.GetDateTime("时间");
			info.班次 = reader.GetString("班次");
			info.组装线体号 = reader.GetString("组装线体号");
			info.机种 = reader.GetString("机种");
			info.镜框日期 = reader.GetDateTime("镜框日期");
			info.镜筒模穴号 = reader.GetString("镜筒模穴号");
			info.镜框批次 = reader.GetString("镜框批次");
			info.穴号105 = reader.GetString("穴号105");
			info.穴号104 = reader.GetString("穴号104");
			info.穴号102 = reader.GetString("穴号102");
			info.日期105 = reader.GetDateTime("日期105");
			info.日期104 = reader.GetDateTime("日期104");
			info.日期102 = reader.GetDateTime("日期102");
			info.角度 = reader.GetString("角度");
			info.系列号 = reader.GetString("系列号");
			info.镜框投料数 = reader.GetInt32("镜框投料数");
			info.隔圈模穴号113b = reader.GetString("隔圈模穴号113B");
			info.成型日113b = reader.GetDateTime("成型日113B");
			info.隔圈模穴号112 = reader.GetString("隔圈模穴号112");
			info.成型日112 = reader.GetDateTime("成型日112");
			info.隔圈投料数 = reader.GetInt32("隔圈投料数");
			info.G3来料供应商 = reader.GetString("G3来料供应商");
			info.G3镜片来料日期 = reader.GetDateTime("G3镜片来料日期");
			info.G1来料供应商 = reader.GetString("G1来料供应商");
			info.G1来料日期 = reader.GetDateTime("G1来料日期");
			info.镜片105投料数 = reader.GetInt32("镜片105投料数");
			info.镜片104投料数 = reader.GetInt32("镜片104投料数");
			info.镜片g3投料数 = reader.GetInt32("镜片G3投料数");
			info.镜片102投料数 = reader.GetInt32("镜片102投料数");
			info.镜片95b投料数 = reader.GetInt32("镜片95B投料数");
			info.配对监控批次 = reader.GetString("配对监控批次");
			info.计划投入数 = reader.GetInt32("计划投入数");
			info.上线数 = reader.GetInt32("上线数");
			info.下线数 = reader.GetInt32("下线数");
			info.状态 = reader.GetString("状态");
			info.生产批次号 = reader.GetString("生产批次号");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(生产批次生成表Info obj)
		{
		    生产批次生成表Info info = obj as 生产批次生成表Info;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("时间", info.时间);
 			hash.Add("班次", info.班次);
 			hash.Add("组装线体号", info.组装线体号);
 			hash.Add("机种", info.机种);
 			hash.Add("镜框日期", info.镜框日期);
 			hash.Add("镜筒模穴号", info.镜筒模穴号);
 			hash.Add("镜框批次", info.镜框批次);
 			hash.Add("穴号105", info.穴号105);
 			hash.Add("穴号104", info.穴号104);
 			hash.Add("穴号102", info.穴号102);
 			hash.Add("日期105", info.日期105);
 			hash.Add("日期104", info.日期104);
 			hash.Add("日期102", info.日期102);
 			hash.Add("角度", info.角度);
 			hash.Add("系列号", info.系列号);
 			hash.Add("镜框投料数", info.镜框投料数);
 			hash.Add("隔圈模穴号113B", info.隔圈模穴号113b);
 			hash.Add("成型日113B", info.成型日113b);
 			hash.Add("隔圈模穴号112", info.隔圈模穴号112);
 			hash.Add("成型日112", info.成型日112);
 			hash.Add("隔圈投料数", info.隔圈投料数);
 			hash.Add("G3来料供应商", info.G3来料供应商);
 			hash.Add("G3镜片来料日期", info.G3镜片来料日期);
 			hash.Add("G1来料供应商", info.G1来料供应商);
 			hash.Add("G1来料日期", info.G1来料日期);
 			hash.Add("镜片105投料数", info.镜片105投料数);
 			hash.Add("镜片104投料数", info.镜片104投料数);
 			hash.Add("镜片G3投料数", info.镜片g3投料数);
 			hash.Add("镜片102投料数", info.镜片102投料数);
 			hash.Add("镜片95B投料数", info.镜片95b投料数);
 			hash.Add("配对监控批次", info.配对监控批次);
 			hash.Add("计划投入数", info.计划投入数);
 			hash.Add("上线数", info.上线数);
 			hash.Add("下线数", info.下线数);
 			hash.Add("状态", info.状态);
 			hash.Add("生产批次号", info.生产批次号);
 				
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
             dict.Add("时间", "时间");
             dict.Add("班次", "班次");
             dict.Add("组装线体号", "组装线体号");
             dict.Add("机种", "机种");
             dict.Add("镜框日期", "镜框日期");
             dict.Add("镜筒模穴号", "镜筒模穴号");
             dict.Add("镜框批次", "镜框批次");
             dict.Add("穴号105", "穴号105");
             dict.Add("穴号104", "穴号104");
             dict.Add("穴号102", "穴号102");
             dict.Add("日期105", "日期105");
             dict.Add("日期104", "日期104");
             dict.Add("日期102", "日期102");
             dict.Add("角度", "角度");
             dict.Add("系列号", "系列号");
             dict.Add("镜框投料数", "镜框投料数");
             dict.Add("隔圈模穴号113b", "隔圈模穴号113B");
             dict.Add("成型日113b", "成型日113B");
             dict.Add("隔圈模穴号112", "隔圈模穴号112");
             dict.Add("成型日112", "成型日112");
             dict.Add("隔圈投料数", "隔圈投料数");
             dict.Add("G3来料供应商", "G3来料供应商");
             dict.Add("G3镜片来料日期", "G3镜片来料日期");
             dict.Add("G1来料供应商", "G1来料供应商");
             dict.Add("G1来料日期", "G1来料日期");
             dict.Add("镜片105投料数", "镜片105投料数");
             dict.Add("镜片104投料数", "镜片104投料数");
             dict.Add("镜片g3投料数", "镜片G3投料数");
             dict.Add("镜片102投料数", "镜片102投料数");
             dict.Add("镜片95b投料数", "镜片95B投料数");
             dict.Add("配对监控批次", "配对监控批次");
             dict.Add("计划投入数", "计划投入数");
             dict.Add("上线数", "上线数");
             dict.Add("下线数", "下线数");
             dict.Add("状态", "状态");
             dict.Add("生产批次号", "生成出的生产批次号");
             #endregion

            return dict;
        }
		
        /// <summary>
        /// 指定具体的列表显示字段
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayColumns()
        {
            return "生产批次ID,时间,班次,组装线体号,机种,镜框日期,镜筒模穴号,镜框批次,穴号105,穴号104,穴号102,日期105,日期104,日期102,角度,系列号,镜框投料数,隔圈模穴号113B,成型日113B,隔圈模穴号112,成型日112,隔圈投料数,G3来料供应商,G3镜片来料日期,G1来料供应商,G1来料日期,镜片105投料数,镜片104投料数,镜片G3投料数,镜片102投料数,镜片95B投料数,配对监控批次,计划投入数,上线数,下线数,状态,生产批次号";
        }
    }
}