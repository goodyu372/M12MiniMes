using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Dictionary.Entity;
using WHC.Dictionary.IDAL;
using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;
using WHC.Framework.Commons;

namespace WHC.Dictionary.BLL
{
    /// <summary>
    /// 公司字典数据。
    /// 由于普通字典无法存储不同公司的差异数据，因此增加该表进行处理，先检索本表没有记录的话，从系统中取对应的类型列表。
    /// </summary>
	public class CorpDictData : BaseBLL<CorpDictDataInfo>
    {
        public CorpDictData() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public List<CorpDictDataInfo> FindByTypeID(string dictTypeId, string corpId)
        {
            ICorpDictData dal = baseDal as ICorpDictData;
            List<CorpDictDataInfo> list = dal.FindByTypeID(dictTypeId, corpId);
            
            //如果公司字典没有数据，则从系统字典获取
            if(list.Count == 0)
            {
                List<DictDataInfo> dict = BLLFactory<DictData>.Instance.FindByTypeID(dictTypeId);
                foreach (DictDataInfo info in dict)
                {
                    list.Add(new CorpDictDataInfo(info, corpId));
                }

                //写入公司字典表，避免下次再去获取
                foreach (CorpDictDataInfo info in list)
                {
                    baseDal.Insert(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public List<CorpDictDataInfo> FindByDictType(string dictTypeName, string corpId)
        {
            ICorpDictData dal = baseDal as ICorpDictData;
            List<CorpDictDataInfo> list = dal.FindByDictType(dictTypeName, corpId);

            //如果公司字典没有数据，则从系统字典获取
            if (list.Count == 0)
            {
                List<DictDataInfo> dict = BLLFactory<DictData>.Instance.FindByDictType(dictTypeName);
                foreach (DictDataInfo info in dict)
                {
                    list.Add(new CorpDictDataInfo(info, corpId));                    
                }

                //写入公司字典表，避免下次再去获取
                foreach (CorpDictDataInfo info in list)
                {
                    baseDal.Insert(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictByDictType(string dictTypeName, string corpId)
        {
            ICorpDictData dal = baseDal as ICorpDictData;
            Dictionary<string, string> dict = dal.GetDictByDictType(dictTypeName, corpId);
            if(dict.Count == 0)
            {
                //写入公司字典表，避免下次再去获取
                List<DictDataInfo> list = BLLFactory<DictData>.Instance.FindByDictType(dictTypeName);
                foreach (DictDataInfo info in list)
                {
                   CorpDictDataInfo corpInfo = new CorpDictDataInfo(info, corpId);
                   baseDal.Insert(corpInfo);
                }

                //重新获取一次
                dict = dal.GetDictByDictType(dictTypeName, corpId);
            }

            
            return dict;
        }

        /// <summary>
        /// 根据字典类型获取对应的CListItem集合
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public List<CListItem> GetDictListItemByDictType(string dictTypeName, string corpId)
        {
            List<CListItem> itemList = new List<CListItem>();
            ICorpDictData dal = baseDal as ICorpDictData;
            Dictionary<string, string> dict = dal.GetDictByDictType(dictTypeName, corpId);
            if(dict.Count == 0)
            {
                //写入公司字典表，避免下次再去获取
                List<DictDataInfo> list = BLLFactory<DictData>.Instance.FindByDictType(dictTypeName);
                foreach (DictDataInfo info in list)
                {
                    CorpDictDataInfo corpInfo = new CorpDictDataInfo(info, corpId);
                    baseDal.Insert(corpInfo);
                }

                //重新获取一次
                dict = dal.GetDictByDictType(dictTypeName, corpId);
            }
           
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }
            return itemList;
        }
    }
}
