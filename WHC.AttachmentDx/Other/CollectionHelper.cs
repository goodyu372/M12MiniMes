using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using WHC.Framework.Commons;using WHC.Framework.ControlUtil;
using System.IO;
using System.Resources;
using System.Drawing;
using System.ComponentModel;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// 集合操作辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CollectionHelper<T> where T : class
    {
        /// <summary>
        /// 根据集合的树形结构（以PID为索引），对集合中对象的Name进行缩进处理后返回新的集合。
        /// </summary>
        /// <param name="pID">PID的值</param>
        /// <param name="level">记录的层级，默认开始为0</param>
        /// <param name="list">对象集合</param>
        /// <param name="pidName">PID的属性名称</param>
        /// <param name="idName">ID的属性名称</param>
        /// <param name="name">Name的属性名称</param>
        /// <param name="levelChar">缩进或代替字符，默认可以用' '</param>
        /// <returns></returns>
        public static List<T> Fill(string pID, int level, List<T> list, string pidName="PID", string idName = "ID", string name = "Name", char levelChar = ' ')
        {
            List<T> returnList = new List<T>();
            foreach (T obj in list)
            {
                string typePID = (string)ReflectionUtil.GetProperty(obj, pidName);
                string typeID = (string)ReflectionUtil.GetProperty(obj, idName);
                string typeName = ReflectionUtil.GetProperty(obj, name) as string;

                if (pID == typePID)
                {
                    string newName = new string(levelChar, level * 2) + typeName;
                    ReflectionUtil.SetProperty(obj, name, newName);
                    returnList.Add(obj);

                    returnList.AddRange(Fill(typeID, level + 1, list, pidName, idName, name));
                }
            }
            return returnList;
        }
    }

}
