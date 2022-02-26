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
    /// �й�ʡ��ҵ�������
    /// </summary>
	public class Province : BaseBLL<ProvinceInfo>
    {
        public Province() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// ����ʡ��ID��ȡ����
        /// </summary>
        /// <param name="id">ʡ��ID</param>
        /// <returns></returns>
        public string GetNameByID(int id)
        {
            return base.GetFieldValue(id, "ProvinceName");
        }

        /// <summary>
        /// �������ƻ�ȡ��Ӧ�ļ�¼ID
        /// </summary>
        /// <param name="name">ʡ������</param>
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
