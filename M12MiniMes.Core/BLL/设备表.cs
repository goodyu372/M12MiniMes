using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using M12MiniMes.Entity;
using M12MiniMes.IDAL;
using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.BLL
{
    /// <summary>
    /// 设备表
    /// </summary>
	public class 设备表 : BaseBLL<设备表Info>
    {
        public 设备表() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}
