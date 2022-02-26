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
    /// ����ҵ����
    /// </summary>
	public class District : BaseBLL<DistrictInfo>
    {
        public District() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// ���ݳ���ID��ȡ��Ӧ�ĵ����б�
        /// </summary>
        /// <param name="cityId">����ID</param>
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
        /// ���ݳ�������ȡ��Ӧ����������
        /// </summary>
        /// <param name="cityName">������</param>
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
        /// ����������ID��ȡ����
        /// </summary>
        /// <param name="id">������ID</param>
        /// <returns></returns>
        public string GetNameByID(int id)
        {
            return base.GetFieldValue(id, "DistrictName");
        }

        /// <summary>
        /// �������ƻ�ȡ��Ӧ�ļ�¼ID
        /// </summary>
        /// <param name="name">����������</param>
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
