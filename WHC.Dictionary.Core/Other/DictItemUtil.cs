using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;

using WHC.Dictionary.Entity;
using WHC.Dictionary.BLL;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

namespace WHC.Dictionary
{
    public class DictItemUtil
    {
        /// <summary>
        /// 根据字典类型获取对应的CListItem集合
        /// </summary>
        /// <param name="dictTypeName"></param>
        /// <returns></returns>
        public static CListItem[] GetDictByDictType(string dictTypeName)
        {
            List<CListItem> itemList = new List<CListItem>();
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }
            return itemList.ToArray();
        }

    }
}
