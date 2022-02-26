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
	/// District 的摘要说明。
	/// </summary>
    public class District : BaseDALOracle<DistrictInfo>, IDistrict
	{
		#region 对象实例及构造函数

		public static District Instance
		{
			get
			{
				return new District();
			}
		}
		public District() : base("TB_District","ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
            this.IsDescending = false;
		}

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dataReader">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override DistrictInfo DataReaderToEntity(IDataReader dataReader)
		{
			DistrictInfo districtInfo = new DistrictInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            districtInfo.ID = reader.GetInt32("ID");
			districtInfo.DistrictName = reader.GetString("DistrictName");
            districtInfo.CityID = reader.GetInt32("CityID");
			
			return districtInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(DistrictInfo obj)
		{
		    DistrictInfo info = obj as DistrictInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("DistrictName", info.DistrictName);
 			hash.Add("CityID", info.CityID);
 				
			return hash;
		}

        public List<DistrictInfo> GetDistrictByCityName(string cityName)
        {
            string sql = string.Format("Select c.* from TB_District as c inner join TB_City as p on c.CityID=p.ID where CityName='{0}' ", cityName);
            return base.GetList(sql, null);
        }
    }
}