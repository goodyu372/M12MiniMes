using System;
using System.Collections.Generic;
using System.Text;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 全局统一的缓存类
    /// </summary>
    public class Cache
    {
        private SortedDictionary<string, object> dict = new SortedDictionary<string, object>();
        private static volatile Cache instance = null;
        private static object lockHelper = new object();

        private Cache()
        {

        }

        /// <summary>
        /// 添加指定的键值元素
        /// </summary>
        /// <param name="key">元素的键</param>
        /// <param name="value">元素的值对象</param>
        public void Add(string key, object value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        /// <summary>
        /// 移除指定的键
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
        }

        /// <summary>
        /// 获取索引的对象
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (dict.ContainsKey(key))
                    return dict[key];
                else
                    return null;
            }
            set { dict[key] = value; }
        }

        /// <summary>
        /// 判断是否存在指定的键
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns></returns>
        public bool ContainKey(string key)
        {
            return dict.ContainsKey(key);
        }

        /// <summary>
        /// 判断是否存在指定的键
        /// </summary>
        /// <param name="value">值内容</param>
        /// <returns></returns>
        public bool ContainValue(string value)
        {
            return dict.ContainsValue(value);
        }

        /// <summary>
        /// 单件实例
        /// </summary>
        public static Cache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Cache();
                        }
                    }
                }
                return instance;
            }
        }
    }
}