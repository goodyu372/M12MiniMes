using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Dictionary.Entity;
using WHC.Dictionary.IDAL;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary.BLL
{
    /// <summary>
    /// 字典数据对象
    /// </summary>
	public class DictData : BaseBLL<DictDataInfo>
    {
        public DictData()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public List<DictDataInfo> FindByTypeID(string dictTypeId)
        {
            IDictData dal = baseDal as IDictData;
            return dal.FindByTypeID(dictTypeId);
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <returns></returns>
        public List<DictDataInfo> FindByDictType(string dictTypeName)
        {
            IDictData dal = baseDal as IDictData;
            return dal.FindByDictType(dictTypeName);
        }

        /// <summary>
        /// 根据字典类型代码获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictCode">字典类型代码</param>
        /// <returns></returns>
        public List<DictDataInfo> FindByDictCode(string dictCode)
        {
            IDictData dal = baseDal as IDictData;
            return dal.FindByDictCode(dictCode);
        }
                
        /// <summary>
        /// 获取所有的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllDict()
        {
            IDictData dal = baseDal as IDictData;
            return dal.GetAllDict();

        }

        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictByTypeID(string dictTypeId)
        {
            IDictData dal = baseDal as IDictData;
            return dal.GetDictByTypeID(dictTypeId);
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictByDictType(string dictTypeName)
        {
            IDictData dal = baseDal as IDictData;
            return dal.GetDictByDictType(dictTypeName);
        }

        /// <summary>
        /// 根据字典类型获取对应的CListItem集合
        /// </summary>
        /// <param name="dictTypeName"></param>
        /// <returns></returns>
        public List<CListItem> GetDictListItemByDictType(string dictTypeName)
        {
            IDictData dal = baseDal as IDictData;
            List<CListItem> itemList = new List<CListItem>();
            Dictionary<string, string> dict = dal.GetDictByDictType(dictTypeName);
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }
            return itemList;
        }
                
        /// <summary>
        /// 根据字典类型名称和字典Value值（即字典编码），解析成字典对应的名称
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="dictValue">字典Value值，即字典编码</param>
        /// <returns>字典对应的名称</returns>
        public string GetDictName(string dictTypeName, string dictValue)
        {
            IDictData dal = baseDal as IDictData;
            return dal.GetDictName(dictTypeName, dictValue);
        }
    }
}
