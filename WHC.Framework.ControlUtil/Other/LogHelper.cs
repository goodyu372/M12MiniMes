using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;

namespace WHC.Framework.ControlUtil
{
    /// <summary>
    /// Log4Net日志记录辅助类
    /// </summary>
    public class LogHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="ex">信息</param>
        public static void Debug(object ex)
        {
            Log.Debug(ex);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="ex">信息</param>
        public static void Warn(object ex)
        {
            Log.Warn(ex);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="ex">信息</param>
        public static void Error(object ex)
        {
            Log.Error(ex);
        }

        /// <summary>
        /// 记录重要提示信息
        /// </summary>
        /// <param name="ex">信息</param>
        public static void Info(object ex)
        {
            Log.Info(ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Debug(object message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Warn(object message, Exception ex)
        {
            Log.Warn(message, ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Error(object message, Exception ex)
        {
            Log.Error(message, ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Info(object message, Exception ex)
        {
            Log.Info(message, ex);
        }
    }

}
