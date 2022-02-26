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
    /// 应用程序渠道信息
    /// </summary>
	public class AppChannel : BaseBLL<AppChannelInfo>
    {
        public AppChannel() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据应用ID获取对象信息
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <returns></returns>
        public AppChannelInfo FindByAppId(string appId)
        {
            string condition = string.Format("AppId ='{0}'", appId);
            return baseDal.FindSingle(condition);
        }
    }
}
