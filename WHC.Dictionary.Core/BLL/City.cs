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
    /// ����ҵ�������
    /// </summary>
	public class City : BaseBLL<CityInfo>
    {
        public City() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// ����ʡ��ID��ȡ��Ӧ�ĳ����б�
        /// </summary>
        /// <param name="provinceID">ʡ��ID</param>
        /// <returns></returns>
        public List<CityInfo> GetCitysByProvinceID(string provinceID)
        {
            string condition = string.Format("ProvinceID ={0} ", provinceID);
            return baseDal.Find(condition);
        }

        /// <summary>
        /// ����ʡ�����ƻ�ȡ��Ӧ�ĳ����б�
        /// </summary>
        /// <param name="provinceName">ʡ������</param>
        /// <returns></returns>
        public List<CityInfo> GetCitysByProvinceName(string provinceName)
        {
            ICity dal = baseDal as ICity;
            return dal.GetCitysByProvinceName(provinceName);
        }

        /// <summary>
        /// ���ݳ���ID��ȡ����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns></returns>
        public string GetNameByID(int id)
        {
            return base.GetFieldValue(id, "CityName");
        }


        /// <summary>
        /// �������ƻ�ȡ��Ӧ�ļ�¼ID
        /// </summary>
        /// <param name="name">��������</param>
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
