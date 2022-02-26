using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Concurrent;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 根据业务对象的类型进行反射操作辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Reflect<T> where T : class
    {
        //采用ConcurrentDictionary线程安全的集合类来缓存，替代Hashtable
        private static ConcurrentDictionary<string, object> conCurrentCache = new ConcurrentDictionary<string, object>(); 

        private static Hashtable ObjCache = new Hashtable();
        private static object syncRoot = new Object();

        /// <summary>
        /// 根据参数创建对象实例
        /// </summary>
        /// <param name="sName">对象全局名称</param>
        /// <param name="sFilePath">文件路径，如果文件为空，则从当前执行的GetExecutingAssembly()中获取</param>
        /// <param name="bCache">缓存集合</param>
        /// <returns></returns>
        public static T Create(string sName, string sFilePath = null, bool bCache = true)
        {
            string CacheKey = sName;
            T objType = null;
            if (bCache)
            {
                return (T)conCurrentCache.GetOrAdd(CacheKey, s =>
                {
                    objType = CreateInstance(CacheKey, sFilePath); //反射创建，并缓存
                    return objType;
                });

                //objType = (T)ObjCache[CacheKey];    //从缓存读取 
                //if (!ObjCache.ContainsKey(CacheKey))
                //{
                //    lock (syncRoot)
                //    {
                //        if (!ObjCache.ContainsKey(CacheKey))
                //        {
                //            objType = CreateInstance(CacheKey, sFilePath);
                //            ObjCache.Add(CacheKey, objType);//缓存数据访问对象
                //        }
                //    }
                //}
            }
            else
            {
                objType = CreateInstance(CacheKey, sFilePath);
            }

            return objType;
        }

        /// <summary>
        /// 根据参数创建对象实例
        /// </summary>
        /// <param name="sName">对象全局名称</param>
        /// <param name="assemblyObj">程序集路径</param>
        /// <param name="bCache">缓存集合</param>
        /// <returns></returns>
        public static T Create(string sName, Assembly assemblyObj, bool bCache = true)
        {
            string CacheKey = sName;
            T objType = null;
            if (bCache)
            {
                return (T)conCurrentCache.GetOrAdd(CacheKey, s =>
                {
                    objType = CreateInstance(CacheKey, assemblyObj); //反射创建，并缓存
                    return objType;
                });
            }
            else
            {
                objType = CreateInstance(CacheKey, assemblyObj);
            }

            return objType;
        }

        /// <summary>
        /// 根据全名和路径构造对象
        /// </summary>
        /// <param name="sName">对象全名</param>
        /// <param name="sFilePath">程序集路径</param>
        /// <returns></returns>
        private static T CreateInstance(string sName, string sFilePath = null)
        {
            Assembly assemblyObj = Assembly.GetExecutingAssembly();
            if (!string.IsNullOrEmpty(sFilePath))
            {
                assemblyObj = Assembly.Load(sFilePath);
            }

            if (assemblyObj == null)
            {
                throw new ArgumentNullException("sFilePath", string.Format("无法加载sFilePath={0} 的程序集", sFilePath));
            }

            T obj = (T)assemblyObj.CreateInstance(sName); //反射创建 
            return obj;
        }

        /// <summary>
        /// 根据全名和路径构造对象
        /// </summary>
        /// <param name="sName">对象全名</param>
        /// <param name="assemblyObj">程序集对象</param>
        /// <returns></returns>
        private static T CreateInstance(string sName, Assembly assemblyObj)
        {
            if (assemblyObj == null)
            {
                throw new ArgumentNullException("assemblyObj", "程序集参数不能为空");
            }

            T obj = (T)assemblyObj.CreateInstance(sName); //反射创建 
            return obj;
        }
    }
}
