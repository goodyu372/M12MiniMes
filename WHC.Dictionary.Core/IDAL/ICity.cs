using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;
using WHC.Dictionary.Entity;

namespace WHC.Dictionary.IDAL
{
	/// <summary>
	/// ICity 的摘要说明。
	/// </summary>
	public interface ICity : IBaseDAL<CityInfo>
	{
        List<CityInfo> GetCitysByProvinceName(string provinceName);
    }
}