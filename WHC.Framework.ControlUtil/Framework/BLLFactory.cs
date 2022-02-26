using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using WHC.Framework.Commons;

namespace WHC.Framework.ControlUtil
{
    /// <summary>
    /// 对业务类进行构造的工厂类
    /// </summary>
    /// <typeparam name="T">业务对象类型</typeparam>
    public class BLLFactory<T> where T : class
    {
        //采用ConcurrentDictionary线程安全的集合类来缓存，替代Hashtable
        private static ConcurrentDictionary<string, object> conCurrentCache = new ConcurrentDictionary<string, object>(); 

        private static Hashtable objCache = new Hashtable();
        private static object syncRoot = new Object();

        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static T Instance
        {
            get
            {
                string CacheKey = typeof(T).FullName;

                var result = (T)conCurrentCache.GetOrAdd(CacheKey, s =>
                {
                    var bll = Reflect<T>.Create(typeof(T).FullName, typeof(T).Assembly.GetName().Name); //反射创建，并缓存
                    return bll;
                });

                return result;

                //T bll = (T)objCache[CacheKey];　 //从缓存读取  
                //if (bll == null)
                //{
                //    lock (syncRoot)
                //    {
                //        bll = (T)objCache[CacheKey];　 //从缓存读取  
                //        if (bll == null)
                //        {
                //            bll = Reflect<T>.Create(typeof(T).FullName, typeof(T).Assembly.GetName().Name); //反射创建，并缓存
                //            objCache.Add(typeof(T).FullName, bll);
                //        }
                //    }
                //}
                //return bll;
            }
        }
    } 
}
