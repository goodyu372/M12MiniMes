using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;
using WHC.Dictionary.Entity;
using WHC.Framework.Commons;

namespace WHC.Dictionary.IDAL
{
    /// <summary>
    /// 公司字典数据
    /// </summary>
	public interface ICorpDictData : IBaseDAL<CorpDictDataInfo>
	{                  
        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        List<CorpDictDataInfo> FindByTypeID(string dictTypeId, string corpId);

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        List<CorpDictDataInfo> FindByDictType(string dictTypeName, string corpId);

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        Dictionary<string, string> GetDictByDictType(string dictTypeName, string corpId);
    }
}