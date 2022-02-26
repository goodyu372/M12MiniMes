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
    /// 生产批次生成表
    /// </summary>
	public class 生产批次生成表 : BaseBLL<生产批次生成表Info>
    {
        public 生产批次生成表() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}
