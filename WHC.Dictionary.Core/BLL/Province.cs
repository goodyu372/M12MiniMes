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
    /// 中国省份业务对象类
    /// </summary>
	public class Province : BaseBLL<ProvinceInfo>
    {
        public Province() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据省份ID获取名称
        /// </summary>
        /// <param name="id">省份ID</param>
        /// <returns></returns>
        public string GetNameByID(int id)
        {
            return base.GetFieldValue(id, "ProvinceName");
        }

        /// <summary>
        /// 根据名称获取对应的记录ID
        /// </summary>
        /// <param name="name">省份名称</param>
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
