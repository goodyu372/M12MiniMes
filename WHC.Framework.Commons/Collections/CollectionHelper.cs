using System;
using System.Collections.Generic;
using System.Text;

namespace WHC.Framework.Commons.Collections
{
    /// <summary>
    /// 把实体类树形结构的集合的名称进行缩进转化的辅助类
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public class CollectionHelper<T> where T : class
    {
        /// <summary>
        /// 把实体类树形结构的集合，根据层次关系对名称进行空格缩进，方便显示（如下拉列表）
        /// </summary>
        /// <param name="pID">父节点</param>
        /// <param name="level">层次等级，从0开始</param>
        /// <param name="list">实体类列表</param>
        /// <param name="pidName">父节点名称</param>
        /// <param name="idName">ID名称</param>
        /// <param name="name">树节点名称</param>
        /// <returns></returns>
        public static List<T> Fill(int pID, int level, List<T> list, string pidName, string idName, string name)
        {
            List<T> returnList = new List<T>();
            foreach (T obj in list)
            {
                int typePID = (int)ReflectionUtil.GetProperty(obj, pidName);
                int typeID = (int)ReflectionUtil.GetProperty(obj, idName);
                string typeName = ReflectionUtil.GetProperty(obj, name) as string;

                if (pID == typePID)
                {
                    string newName = new string('　', level * 2) + typeName;
                    ReflectionUtil.SetProperty(obj, name, newName);
                    returnList.Add(obj);

                    returnList.AddRange(Fill(typeID, level + 1, list, pidName, idName, name));
                }
            }
            return returnList;
        }

        /// <summary>
        /// 把实体类树形结构的集合，根据层次关系对名称进行空格缩进，方便显示（如下拉列表）
        /// </summary>
        /// <param name="pID">父节点</param>
        /// <param name="level">层次等级，从0开始</param>
        /// <param name="list">实体类列表</param>
        /// <param name="pidName">父节点名称</param>
        /// <param name="idName">ID名称</param>
        /// <param name="name">树节点名称</param>
        /// <returns></returns>
        public static List<T> Fill(string pID, int level, List<T> list, string pidName, string idName, string name)
        {
            List<T> returnList = new List<T>();
            foreach (T obj in list)
            {
                string typePID = (string)ReflectionUtil.GetProperty(obj, pidName);
                string typeID = (string)ReflectionUtil.GetProperty(obj, idName);
                string typeName = ReflectionUtil.GetProperty(obj, name) as string;

                if (pID == typePID)
                {
                    string newName = new string('　', level * 2) + typeName;
                    ReflectionUtil.SetProperty(obj, name, newName);
                    returnList.Add(obj);

                    returnList.AddRange(Fill(typeID, level + 1, list, pidName, idName, name));
                }
            }
            return returnList;
        }

        /// <summary>
        /// 和Fill方法类似，获取用于绑定字典的有层次的数据集合
        /// </summary>
        /// <param name="list">列表内容</param>
        /// <param name="level">层次等级，从0开始</param>
        /// <param name="childrenName">子集合的对象名</param>
        /// <param name="id">值名称，如ID</param>
        /// <param name="name">显示名称，如Name</param>
        /// <returns></returns>
        public static List<CListItem> GetIndentedItems(List<T> list, int level, string childrenName, string id = "ID", string name = "Name")
        {
            List<CListItem> result = new List<CListItem>();
            foreach (T info in list)
            {
                var objId = ReflectionUtil.GetProperty(info, id);
                string idValue = objId != null ? objId.ToString() : "";
                string nameValue = ReflectionUtil.GetProperty(info, name) as string;
                nameValue = new string('　', level * 2) + nameValue;

                result.Add(new CListItem(nameValue, idValue));

                var children = ReflectionUtil.GetProperty(info, childrenName) as List<T>;
                if (children != null)
                {
                    var itemList = GetIndentedItems(children, level + 1, childrenName, id, name);
                    result.AddRange(itemList);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据普通列表的PID关系，转换为排序后的字典列表
        /// </summary>
        /// <param name="list">列表内容</param>
        /// <param name="pid">父节点</param>
        /// <param name="pidName">父节点名称，如PID</param>
        /// <param name="id">值名称，如ID</param>
        /// <param name="name">显示名称，如Name</param>
        /// <returns></returns>
        public static List<CListItem> GetSortedItems(List<T> list, string pid, string pidName = "PID", string id = "ID", string name = "Name")
        {
            List<CListItem> result = new List<CListItem>();
            foreach (T info in list)
            {
                var objPID = ReflectionUtil.GetProperty(info, pidName);
                string pidValue = objPID != null ? objPID.ToString() : "";

                var objId = ReflectionUtil.GetProperty(info, id);
                string idValue = objId != null ? objId.ToString() : "";
                string nameValue = ReflectionUtil.GetProperty(info, name) as string;

                result.Add(new CListItem(nameValue, idValue));

                if (pid == pidValue)
                {
                    var itemList = GetSortedItems(list, pidValue, pidName, id, name);
                    result.AddRange(itemList);
                }
            }
            return result;
        }
    }
}
