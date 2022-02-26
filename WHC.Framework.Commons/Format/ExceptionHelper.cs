﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.Commons.Format
{
    /// <summary>
    /// Exception帮助类，对异常信息进行格式化
    /// </summary>
    public static class ExceptionHelper
    {

        #region 格式化方法

        /// <summary>
        /// 格式化异常消息
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="isHideStackTrace">是否显示堆栈信息</param>
        /// <param name="appString">堆栈信息描述前缀；默认空格</param>
        /// <returns>格式化后异常信息</returns>
        public static string FormatMessage(this Exception ex, bool isHideStackTrace, string appString)
        {
            StringBuilder _builder = new StringBuilder();

            while (ex != null)
            {
                _builder.AppendLine(string.Format("{0}异常消息：{1}", appString, ex.Message));
                _builder.AppendLine(string.Format("{0}异常类型：{1}", appString, ex.GetType().FullName));
                _builder.AppendLine(string.Format("{0}异常方法：{1}", appString, ex.TargetSite == null ? null : ex.TargetSite.Name));
                _builder.AppendLine(string.Format("{0}异常来源：{1}", appString, ex.Source));

                if (!isHideStackTrace && ex.StackTrace != null)
                {
                    _builder.AppendLine(string.Format("{0}异常堆栈：{1}", appString, ex.StackTrace));
                }

                if (ex.InnerException != null)
                {
                    _builder.AppendLine(string.Format("{0}内部异常：", appString));
                }

                ex = ex.InnerException;
            }

            return _builder.ToString();
        }

        /// <summary>
        /// 格式化异常消息
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="isHideStackTrace">是否显示堆栈信息</param>
        /// <returns>格式化后异常信息</returns>
        public static string FormatMessage(this Exception ex, bool isHideStackTrace)
        {
            return FormatMessage(ex, isHideStackTrace, "  ");
        }

        /// <summary>
        /// 获取innerException
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Exception</returns>
        /// 日期：2015-10-20 16:22
        /// 备注：
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex;
            }

            return ex.InnerException.GetOriginalException();
        }

        /// <summary>
        /// 判断异常是哪个异常类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">Exception</param>
        /// <returns>判断异常类型</returns>
        public static bool Is<T>(this Exception source)
        where T : Exception
        {
            if (source is T)
            {
                return true;
            }
            else if (source.InnerException != null)
            {
                return source.InnerException.Is<T>();
            }
            else
            {
                return false;
            }
        }

        #endregion Methods
    }
}
