using System;
using System.IO;
using System.Text.RegularExpressions;
using WHC.Framework.Language;

namespace WHC.Framework.Commons
{   
    /// <summary>
    /// 参数验证帮助类，使用扩展函数实现
    /// </summary>
    /// <example>
    /// eg:
    /// ArgumentCheck.Begin().NotNull(sourceArray, "需要操作的数组").NotNull(addArray, "被添加的数组");
    /// </example>
    public static class ArgumentCheck
    {
        #region Methods
        
        /// <summary>
        /// 验证初始化
        /// <para>
        /// eg:
        /// ArgumentCheck.Begin().NotNull(sourceArray, "需要操作的数组").NotNull(addArray, "被添加的数组");
        /// </para>
        /// <para>
        /// ArgumentCheck.Begin().NotNullOrEmpty(tableName, "表名").NotNullOrEmpty(primaryKey, "主键");</para>
        /// <para>
        /// ArgumentCheck.Begin().CheckLessThan(percent, "百分比", 100, true);</para>
        /// <para>
        /// ArgumentCheck.Begin().CheckGreaterThan&lt;int&gt;(pageIndex, "页索引", 0, false).CheckGreaterThan&lt;int&gt;(pageSize, "页大小", 0, false);</para>
        /// <para>
        /// ArgumentCheck.Begin().NotNullOrEmpty(filepath, "文件路径").IsFilePath(filepath).NotNullOrEmpty(regexString, "正则表达式");</para>
        /// <para>
        /// ArgumentCheck.Begin().NotNullOrEmpty(libFilePath, "非托管DLL路径").IsFilePath(libFilePath).CheckFileExists(libFilePath);</para>
        /// <para>
        /// ArgumentCheck.Begin().InRange(brightnessValue, 0, 100, "图片亮度值");</para>
        /// <para>
        /// ArgumentCheck.Begin().Check&lt;ArgumentNullException&gt;(() => config.HasFile, "config文件不存在。");</para>
        /// <para>
        /// ArgumentCheck.Begin().NotNull(serialPort, "串口").Check&lt;ArgumentException&gt;(() => serialPort.IsOpen, "串口尚未打开！").NotNull(data, "串口发送数据");
        /// </para>
        /// </summary>
        /// <returns>Validation对象</returns>
        public static Validation Begin()
        {
            return null;
        }
        
        /// <summary>
        /// 需要验证的正则表达式
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="checkFactory">委托</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation Check(this Validation validation, Func<bool> checkFactory, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentException>(validation, checkFactory, string.Format(ResourceKey.ParameterCheck_Match2, argumentName));
        }
        
        /// <summary>
        /// 自定义参数检查
        /// </summary>
        /// <typeparam name="TException">泛型</typeparam>
        /// <param name="validation">Validation</param>
        /// <param name="checkedFactory">委托</param>
        /// <param name="message">自定义错误消息</param>
        /// <returns>Validation对象</returns>
        public static Validation Check<TException>(this Validation validation, Func<bool> checkedFactory, string message)
        where TException : Exception
        {
            if(checkedFactory())
            {
                return validation ?? new Validation()
                {
                    IsValid = true
                };
            }
            else
            {
                TException _exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw _exception;
            }
        }
        
        /// <summary>
        /// 检查指定路径的文件夹必须存在，否则抛出<see cref="DirectoryNotFoundException"/>异常。
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="path">目录字符串</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="DirectoryNotFoundException">DirectoryNotFoundException</exception>
        /// <returns>Validation对象</returns>
        public static Validation CheckDirectoryExists(this Validation validation, string path)
        {
            return Check<DirectoryNotFoundException>(validation, () => Directory.Exists(path), string.Format(ResourceKey.ParameterCheck_DirectoryNotExists, path));
        }
        
        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="actualFileExt">实际文件类型；eg: .xls</param>
        /// <param name="expectFileExt">期待文件类型</param>
        /// <returns>Validation对象</returns>
        public static Validation CheckedFileExt(this Validation validation, string actualFileExt, string expectFileExt)
        {
            return Check<FileNotFoundException>(validation, () => string.Equals(actualFileExt, expectFileExt, StringComparison.OrdinalIgnoreCase), 
                string.Format(ResourceKey.ParameterCheck_FileExtCompare, expectFileExt));
        }

        /// <summary>
        ///检查文件类型
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="actualFileExt">实际文件类型；eg: .xls</param>
        /// <param name="expectFileExt">期待文件类型</param>
        /// <returns>Validation对象</returns>
        public static Validation CheckedFileExt(this Validation validation, string actualFileExt, string[] expectFileExt)
        {
            string _allowFileExts = string.Join(",", expectFileExt);
            return Check<FileNotFoundException>(validation, () => expectFileExt.ContainIgnoreCase(actualFileExt), string.Format(ResourceKey.ParameterCheck_FileExtCompare, _allowFileExts));
        }
        
        /// <summary>
        /// 检查指定路径的文件必须存在，否则抛出<see cref="FileNotFoundException"/>异常。
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="filePath">文件路径</param>
        /// <exception cref="ArgumentNullException">当文件路径为null时</exception>
        /// <exception cref="FileNotFoundException">当文件路径不存在时</exception>
        /// <returns>Validation对象</returns>
        public static Validation CheckFileExists(this Validation validation, string filePath)
        {
            return Check<FileNotFoundException>(validation, () => File.Exists(filePath), string.Format(ResourceKey.ParameterCheck_FileNotExists, filePath));
        }
        
        /// <summary>
        /// 检查参数必须大于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="validation">Validation</param>
        /// <param name="value">判断数据</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="target">要比较的值</param>
        /// <param name="canEqual">是否可等于</param>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
        /// <returns>Validation对象</returns>
        public static Validation CheckGreaterThan<T>(this Validation validation, T value, string paramName, T target, bool canEqual)
        where T : IComparable<T>
        {
            // bool flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
            string _format = canEqual ? ResourceKey.ParameterCheck_NotGreaterThanOrEqual : ResourceKey.ParameterCheck_NotGreaterThan;
            return Check<ArgumentOutOfRangeException>(validation, () => canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0, string.Format(_format, paramName, target));
        }
        
        /// <summary>
        /// 检查参数必须小于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="validation">Validation</param>
        /// <param name="value">判断数据</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="target">要比较的值</param>
        /// <param name="canEqual">是否可等于</param>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
        /// <returns>Validation对象</returns>
        public static Validation CheckLessThan<T>(this Validation validation, T value, string paramName, T target, bool canEqual)
        where T : IComparable<T>
        {
            string _format = canEqual ? ResourceKey.ParameterCheck_NotLessThanOrEqual : ResourceKey.ParameterCheck_NotLessThan;
            return Check<ArgumentOutOfRangeException>(validation, () => canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0, string.Format(_format, paramName, target));
        }
        
        /// <summary>
        /// 验证是否在范围内
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation InRange(this Validation validation, int data, int min, int max, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentOutOfRangeException>(validation, () => data >= min && data <= max, string.Format(ResourceKey.ParameterCheck_Between, argumentName, min, max));
        }
        
        /// <summary>
        /// 是否是中文
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">中文</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsChinese(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsChinese(data), argumentName);
        }
        
        /// <summary>
        /// 是否是电子邮箱
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="email">需要验证的邮箱</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsEmail(this Validation validation, string email, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsEmail(email), argumentName);
        }
        
        /// <summary>
        /// 是否是文件路径
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">路径</param>
        /// <returns>Validation对象</returns>
        public static Validation IsFilePath(this Validation validation, string data)
        {
            return Check<ArgumentException>(validation, () => ValidateUtil.IsFilePath(data), string.Format(ResourceKey.ParameterCheck_IsFilePath, data));
        }
        
        /// <summary>
        /// 是否是十六进制字符串
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">验证数据</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsHexString(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsHexString(data), argumentName);
        }
        
        /// <summary>
        /// 是否是身份证号码
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">验证数据</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsIdCard(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsIdCard(data), argumentName);
        }
        
        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测的字符串</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsInt(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsInt(data), argumentName);
        }
        
        /// <summary>
        /// 是否是IP
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测到IP</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsIP(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsIP(data), argumentName);
        }
        
        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测的字符串</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsNumber(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsNumeric(data), argumentName);
        }

        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">需要检测的字符串</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsMobile(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsMobile(data), argumentName);
        }
        
        /// <summary>
        /// 是否是合法端口
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">参数值</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsPort(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentException>(validation, () => ValidateUtil.IsPort(data), string.Format(ResourceKey.ParameterCheck_Port, argumentName));
        }
        
        /// <summary>
        /// 是否是邮政编码
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">邮政编码</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsPostCode(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsPostCode(data), argumentName);
        }
        
        /// <summary>
        /// 判断字符串是否是要求的长度
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="input">验证的字符串</param>
        /// <param name="requireLength">要求的长度</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsRequireLen(this Validation validation, string input, int requireLength, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentException>(
                       validation,
                       () => input.Length == requireLength,
                       string.Format(ResourceKey.ParameterCheck_StringLength, argumentName, requireLength));
        }
        
        /// <summary>
        /// 判断类型是否能序列化
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <returns>Validation对象</returns>
        public static Validation IsSerializable(this Validation validation, object data)
        {
            return Check<ArgumentException>(validation, () => data.GetType().IsSerializable, string.Format(ResourceKey.ParameterCheck_Serializable, data.GetType().FullName));
        }
        
        /// <summary>
        /// 是否是URL
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">url</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation IsURL(this Validation validation, string data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check(validation, () => ValidateUtil.IsURL(data), argumentName);
        }
        
        /// <summary>
        /// 验证参数不能等于某个值
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <param name="equalObj">比较项</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation NotEqual(this Validation validation, object data, object equalObj, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentException>(validation, () => data != equalObj, string.Format(ResourceKey.ParameterCheck_NotEqual, argumentName, data));
        }
        
        /// <summary>
        /// 验证非空
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="data">输入项</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation NotNull(this Validation validation, object data, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentNullException>(validation, () => (data != null), string.Format(ResourceKey.ParameterCheck_NotNull, argumentName));
        }
        
        /// <summary>
        /// 不能为空或者NULL验证
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="input">输入项</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation NotNullOrEmpty(this Validation validation, string input, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentNullException>(validation, () => !string.IsNullOrEmpty(input), string.Format(ResourceKey.ParameterCheck_NotNullOrEmpty_String, argumentName));
        }
        
        /// <summary>
        /// 需要验证的正则表达式
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="input">需要匹配的输入项</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns>Validation对象</returns>
        public static Validation RegexMatch(this Validation validation, string input, string pattern, string argumentName)
        {
            //多语言支持，转换为对应语言的参数名称
            argumentName = JsonLanguage.Default.GetString(argumentName);
            return Check<ArgumentException>(validation, () => Regex.IsMatch(input, pattern), string.Format(ResourceKey.ParameterCheck_Match, input, argumentName));
        }
        
        #endregion Methods
    }


    /// <summary>
    /// 参数验证实体类
    /// </summary>
    public sealed class Validation
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsValid { get; set; }
    }
}