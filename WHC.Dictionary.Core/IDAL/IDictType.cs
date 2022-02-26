using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Dictionary.Entity;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.IDAL
{
	/// <summary>
    /// 字典类型对象接口
	/// </summary>
	public interface IDictType : IBaseDAL<DictTypeInfo>
	{               
        /// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAllType();
                
        /// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <param name="PID">字典类型ID</param>
        /// <returns></returns>
        Dictionary<string, string> GetAllType(string PID);

        /// <summary>
        /// 获取字典类型的树形结构列表
        /// </summary>
        /// <returns></returns>
        List<DictTypeNodeInfo> GetTree();

        /// <summary>
        /// 获取字典类型顶级的列表
        /// </summary>
        /// <returns></returns>
        List<DictTypeInfo> GetTopItems();

        /// <summary>
        /// 获取指定ID下的树形结构列表
        /// </summary>
        /// <param name="mainID">字典类型ID</param>
        /// <returns></returns>
        List<DictTypeNodeInfo> GetTreeByID(string mainID);
    }
}