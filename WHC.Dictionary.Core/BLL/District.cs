using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Dictionary.Entity;
using WHC.Dictionary.IDAL;
using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.BLL
{
    /// <summary>
    /// 地区业务类
    /// </summary>
	public class District : BaseBLL<DistrictInfo>
    {
        public District() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据城市ID获取对应的地区列表
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistrictByCity(string cityId)
        {
            List<DistrictInfo> list = new List<DistrictInfo>();
            if (!string.IsNullOrEmpty(cityId))
            {
                string condition = string.Format("CityID={0}", cityId);
                list = Find(condition);
            }
            return list;
        }

        /// <summary>
        /// 根据城市名获取对应的行政区划
        /// </summary>
        /// <param name="cityName">城市名</param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistrictByCityName(string cityName)
        {
            List<DistrictInfo> list = new List<DistrictInfo>();
            if (!string.IsNullOrEmpty(cityName))
            {
                IDistrict dal = baseDal as IDistrict;
                list = dal.GetDistrictByCityName(cityName);
            }
            return list;
        }

        /// <summary>
        /// 根据行政区ID获取名称
        /// </summary>
        /// <param name="id">行政区ID</param>
        /// <returns></returns>
        public string GetNameByID(int id)
        {
            return base.GetFieldValue(id, "DistrictName");
        }

        /// <summary>
        /// 根据名称获取对应的记录ID
        /// </summary>
        /// <param name="name">行政区名称</param>
        /// <returns></returns>
        public string GetIdByName(string name)
        {
            string result = "";
            string condition = string.Format("Name ='{0}'", name);
            List<string> list = base.GetFieldListByCondition("ID", condition);
            if (list != null && list.Count > 0)
            {
                result = list[0];
            }
            return result;
        }
    }
}
