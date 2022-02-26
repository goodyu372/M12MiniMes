using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Drawing;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 反射操作辅助类，如获取或设置字段、属性的值等反射信息。
    /// </summary>
    public static class ReflectionUtil
    {
        #region 属性字段设置
        /// <summary>
        /// 绑定标识
        /// </summary>
        public static BindingFlags bf = BindingFlags.DeclaredOnly | BindingFlags.Public | 
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            Type type = obj.GetType();
            MethodInfo method = type.GetMethod(methodName);
            return method.Invoke(obj, args);
        }

        /// <summary>
        /// 设置对象实例的字段值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        public static void SetField(object obj, string name, object value)
        {
            FieldInfo fi = obj.GetType().GetField(name, bf);
            if (fi != null)
            {
                fi.SetValue(obj, value);
            }
        }

        /// <summary>
        /// 获取对象实例的字段值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public static object GetField(object obj, string name)
        {
            object result = null;
            FieldInfo fi = obj.GetType().GetField(name, bf);
            if (fi != null)
            {
                result = fi.GetValue(obj);
            }
            return result;
        }

        /// <summary>
        /// 获取对象实例的字段集合
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static FieldInfo[] GetFields(object obj)
        {
            FieldInfo[] fieldInfos = obj.GetType().GetFields(bf);
            return fieldInfos;
        }

        /// <summary>
        /// 设置对象实例的属性值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        public static void SetProperty(object obj, string name, object value)
        {
            //PropertyInfo fieldInfo = obj.GetType().GetProperty(name, bf);
            //value = Convert.ChangeType(value, fieldInfo.PropertyType);
            //fieldInfo.SetValue(obj, value, null);

            //下面方法可以获取基类属性
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                if (prop.Name == name)
                {
                    value = Convert.ChangeType(value, prop.PropertyType);
                    prop.SetValue(obj, value);
                }
            }
        }

        /// <summary>
        /// 获取对象实例的属性值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public static object GetProperty(object obj, string name)
        {
            //这个无法获取基类
            //PropertyInfo fieldInfo = obj.GetType().GetProperty(name, bf);
            //return fieldInfo.GetValue(obj, null);

            //下面方法可以获取基类属性
            object result = null;
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                if(prop.Name == name)
                {
                    result = prop.GetValue(obj);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取对象实例的属性列表
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(object obj)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
            return propertyInfos;
        }

        /// <summary>
        /// 获取指定对象的属性名称列表
        /// </summary>
        /// <param name="obj">object对象</param>
        /// <returns></returns>
        public static List<string> GetPropertyNames(object obj)
        {
            List<string> list = new List<string>();

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                list.Add(prop.Name);
            }

            //Type type = obj.GetType();
            //string[] propertyNames = type.GetProperties().Select(p => p.Name).ToArray();
            //if (propertyNames != null)
            //{
            //    list.AddRange(propertyNames.ToArray());
            //}

            return list;
        }

        /// <summary>
        /// 把object对象的属性反射获取到字典列表中
        /// </summary>
        /// <param name="obj">object对象</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertyDict(object obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                object propValue = prop.GetValue(obj);
                if (!dict.ContainsKey(prop.Name))
                {
                    dict.Add(prop.Name, propValue);
                }
            }
            return dict;
        }

        /// <summary>
        /// 把object对象的属性反射获取到字典列表中
        /// </summary>
        /// <param name="obj">object对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyDict2(object obj)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                object propValue = prop.GetValue(obj);
                string value = (propValue != null) ? propValue.ToString() : "";
                if (!dict.ContainsKey(prop.Name))
                {
                    dict.Add(prop.Name, value);
                }
            }
            return dict;
        }

        /// <summary>
        /// 获取属性名称，优先获取DisplayName
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>属性名称</returns>
        public static Dictionary<string, string> GetPropertyNames<T>() where T : class
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            PropertyInfo[] _properties = typeof(T).GetProperties();

            foreach (PropertyInfo prop in _properties)
            {
                object[] _attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                dict.Add(prop.Name, _attribute.Length == 0 ? prop.Name : ((DisplayNameAttribute)_attribute[0]).DisplayName);
            }

            return dict;
        }

        /// <summary>
        /// 将实体类属性转换为字典形式
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体类</param>
        /// <returns>字典</returns>
        public static Dictionary<string, object> DictionaryFromType<T>(this T model) where T : class
        {
            PropertyInfo[] _properties = typeof(T).GetProperties();
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (PropertyInfo prop in _properties)
            {
                object[] _attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                string _proName = _attribute.Length == 0 ? prop.Name : ((DisplayNameAttribute)_attribute[0]).DisplayName;
                object _proValue = prop.GetValue(model, new object[] { });
                dict.Add(_proName, _proValue);
            }

            return dict;
        }

        /// <summary>
        /// 把对象的属性和值，输出一个键值的字符串，如A=1&B=test
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <param name="includeEmptyProperties">是否包含空白属性的键值</param>
        /// <returns></returns>
        public static string ToNameValuePairs(object obj, bool includeEmptyProperties = true)
        {
            string result = "";

            foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
            {
                var objVal = p.GetValue(obj);
                var value = objVal != null ? objVal.ToString() : null;

                if (string.IsNullOrEmpty(value))
                {
                    if (includeEmptyProperties)
                    {
                        if (!string.IsNullOrEmpty(result))
                        {
                            result += "&";
                        }

                        result += string.Format("{0}={1}", p.Name, value);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += "&";
                    }

                    result += string.Format("{0}={1}", p.Name, value);
                }
            }

            return result;
        }

        #endregion

        #region 获取Description

        /// <summary>
        /// 获取枚举字段的Description属性值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>return description or value.ToString()</returns>
        public static string GetDescription(Enum value)
        {
            return GetDescription(value, null);
        }

        /// <summary>
        /// Get The Enum Field Description using Description Attribute and 
        /// objects to format the Description.
        /// </summary>
        /// <param name="value">Enum For Which description is required.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <returns>return null if DescriptionAttribute is not found or return type description</returns>
        public static string GetDescription(Enum value, params object[] args)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string text1;

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            text1 = (attributes.Length > 0) ? attributes[0].Description : value.ToString();

            if ((args != null) && (args.Length > 0))
            {
                return string.Format(null, text1, args);
            }
            return text1;
        }

        /// <summary>
        ///	获取字段的Description属性值
        /// </summary>
        /// <param name="member">Specified Member for which Info is Required</param>
        /// <returns>return null if DescriptionAttribute is not found or return type description</returns>
        public static string GetDescription(MemberInfo member)
        {
            return GetDescription(member, null);
        }

        /// <summary>
        /// Get The Type Description using Description Attribute and 
        /// objects to format the Description.
        /// </summary>
        /// <param name="member"> Specified Member for which Info is Required</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <returns>return <see cref="String.Empty"/> if DescriptionAttribute is 
        /// not found or return type description</returns>
        public static string GetDescription(MemberInfo member, params object[] args)
        {
            string text1;

            if (member == null)
            {
                throw new ArgumentNullException("member");
            }

            if (member.IsDefined(typeof(DescriptionAttribute), false))
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])member.GetCustomAttributes(typeof(DescriptionAttribute), false);
                text1 = attributes[0].Description;
            }
            else
            {
                return String.Empty;
            }

            if ((args != null) && (args.Length > 0))
            {
                return String.Format(null, text1, args);
            }
            return text1;
        }

        #endregion

        #region 获取Attribute信息

        /// <summary>
        /// 获取指定对象实例的attributes内容
        /// </summary>
        /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
        /// <param name="assembly">the assembly in which the specified attribute is defined</param>
        /// <returns>Attribute as Object or null if not found.</returns>
        public static object GetAttribute(Type attributeType, Assembly assembly)
        {
            if (attributeType == null)
            {
                throw new ArgumentNullException("attributeType");
            }

            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }


            if (assembly.IsDefined(attributeType, false))
            {
                object[] attributes = assembly.GetCustomAttributes(attributeType, false);

                return attributes[0];
            }

            return null;
        }


        /// <summary>
        /// 获取指定对象实例的attributes内容
        /// </summary>
        /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
        /// <param name="type">the type on which the specified attribute is defined</param>
        /// <returns>Attribute as Object or null if not found.</returns>
        public static object GetAttribute(Type attributeType, MemberInfo type)
        {
            return GetAttribute(attributeType, type, false);
        }


        /// <summary>
        /// Gets the specified object attributes for type as specified by type with option to serach parent
        /// </summary>
        /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
        /// <param name="type">the type on which the specified attribute is defined</param>
        /// <param name="searchParent">if set to <see langword="true"/> [search parent].</param>
        /// <returns>
        /// Attribute as Object or null if not found.
        /// </returns>
        public static object GetAttribute(Type attributeType, MemberInfo type, bool searchParent)
        {
            if (attributeType == null)
            {
                return null;
            }

            if (type == null)
            {
                return null;
            }

            if (!(attributeType.IsSubclassOf(typeof(Attribute))))
            {
                return null;
            }


            if (type.IsDefined(attributeType, searchParent))
            {
                object[] attributes = type.GetCustomAttributes(attributeType, searchParent);

                if (attributes.Length > 0)
                {
                    return attributes[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the collection of all specified object attributes for type as specified by type
        /// </summary>
        /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
        /// <param name="type">the type on which the specified attribute is defined</param>
        /// <returns>Attribute as Object or null if not found.</returns>
        public static object[] GetAttributes(Type attributeType, MemberInfo type)
        {
            return GetAttributes(attributeType, type, false);
        }


        /// <summary>
        /// Gets the collection of all specified object attributes for type as specified by type with option to serach parent
        /// </summary>
        /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
        /// <param name="type">the type on which the specified attribute is defined</param>
        /// <param name="searchParent">The attribute Type for which the custom attribute is to be returned.</param>
        /// <returns>
        /// Attribute as Object or null if not found.
        /// </returns>
        public static object[] GetAttributes(Type attributeType, MemberInfo type, bool searchParent)
        {
            if (type == null)
            {
                return null;
            }

            if (attributeType == null)
            {
                return null;
            }

            if (!(attributeType.IsSubclassOf(typeof(Attribute))))
            {
                return null;
            }


            if (type.IsDefined(attributeType, false))
            {
                return type.GetCustomAttributes(attributeType, searchParent);
            }

            return null;
        }

        #endregion

        #region 资源获取

        /// <summary>
        /// 根据资源名称获取图片资源流
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        public static Stream GetImageResource(string ResourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(ResourceName);
        }

        /// <summary>
        /// 获取程序集资源的位图资源
        /// </summary>
        /// <param name="assemblyType">程序集中的某一对象类型</param>
        /// <param name="resourceHolder">资源的根名称。例如，名为“MyResource.en-US.resources”的资源文件的根名称为“MyResource”。</param>
        /// <param name="imageName">资源项名称</param>
        public static Bitmap LoadBitmap(Type assemblyType, string resourceHolder, string imageName)
        {
            Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
            ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
            return (Bitmap)rm.GetObject(imageName);
        }

        /// <summary>
        ///  获取程序集资源的文本资源
        /// </summary>
        /// <param name="assemblyType">程序集中的某一对象类型</param>
        /// <param name="resName">资源项名称</param>
        /// <param name="resourceHolder">资源的根名称。例如，名为“MyResource.en-US.resources”的资源文件的根名称为“MyResource”。</param>
        public static string GetStringRes(Type assemblyType, string resName, string resourceHolder)
        {
            Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
            ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
            return rm.GetString(resName);
        }

        /// <summary>
        /// 获取程序集嵌入资源的文本形式
        /// </summary>
        /// <param name="assemblyType">程序集中的某一对象类型</param>
        /// <param name="charset">字符集编码</param>
        /// <param name="ResName">嵌入资源相对路径</param>
        /// <returns>如没找到该资源则返回空字符</returns>
        public static string GetManifestString(Type assemblyType, string charset, string ResName)
        {
            Assembly asm = Assembly.GetAssembly(assemblyType);
            Stream st = asm.GetManifestResourceStream(string.Concat(assemblyType.Namespace,
                ".", ResName.Replace("/", ".")));
            if (st == null) { return ""; }
            int iLen = (int)st.Length;
            byte[] bytes = new byte[iLen];
            st.Read(bytes, 0, iLen);
            return (bytes != null) ? Encoding.GetEncoding(charset).GetString(bytes) : "";
        }

        #endregion

        #region 创建对应实例
        /// <summary>
        /// 创建对应实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>对应实例</returns>
        public static object CreateInstance(string type)
        {
            Type tmp = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                tmp = assemblies[i].GetType(type);
                if (tmp != null)
                {
                    return assemblies[i].CreateInstance(type);

                }
            }
            return null;
            //return Assembly.GetExecutingAssembly().CreateInstance(type);
        }

        /// <summary>
        /// 创建对应实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>对应实例</returns>
        public static object CreateInstance(Type type)
        {
            return CreateInstance(type.FullName);
        } 
        #endregion


        #region 对象属性复制

        /// <summary>
        /// 利用反射实现两个类的对象之间相同属性的值的复制
        /// </summary>
        /// <typeparam name="D">最终生成的新对象类型</typeparam>
        /// <typeparam name="S">传入的源数据对象类型</typeparam>
        /// <param name="s">待复制属性的源对象</param>
        /// <returns></returns>
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>();

            var Types = s.GetType();//获得类型  
            var Typed = typeof(D);
            foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
            {
                foreach (PropertyInfo dp in Typed.GetProperties())
                {
                    if (dp.Name == sp.Name)//判断属性名是否相同  
                    {
                        dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                    }
                }
            }

            return d;
        }  
        #endregion
    }
}