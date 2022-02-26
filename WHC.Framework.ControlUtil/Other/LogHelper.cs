using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;

namespace WHC.Framework.ControlUtil
{
    /// <summary>
    /// Log4Net��־��¼������
    /// </summary>
    public class LogHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ��¼������Ϣ
        /// </summary>
        /// <param name="ex">��Ϣ</param>
        public static void Debug(object ex)
        {
            Log.Debug(ex);
        }

        /// <summary>
        /// ��¼������Ϣ
        /// </summary>
        /// <param name="ex">��Ϣ</param>
        public static void Warn(object ex)
        {
            Log.Warn(ex);
        }

        /// <summary>
        /// ��¼������Ϣ
        /// </summary>
        /// <param name="ex">��Ϣ</param>
        public static void Error(object ex)
        {
            Log.Error(ex);
        }

        /// <summary>
        /// ��¼��Ҫ��ʾ��Ϣ
        /// </summary>
        /// <param name="ex">��Ϣ</param>
        public static void Info(object ex)
        {
            Log.Info(ex);
        }

        /// <summary>
        /// ��¼��Ϣ���쳣��Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="ex">�쳣����</param>
        public static void Debug(object message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        /// <summary>
        /// ��¼��Ϣ���쳣��Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="ex">�쳣����</param>
        public static void Warn(object message, Exception ex)
        {
            Log.Warn(message, ex);
        }

        /// <summary>
        /// ��¼��Ϣ���쳣��Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="ex">�쳣����</param>
        public static void Error(object message, Exception ex)
        {
            Log.Error(message, ex);
        }

        /// <summary>
        /// ��¼��Ϣ���쳣��Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="ex">�쳣����</param>
        public static void Info(object message, Exception ex)
        {
            Log.Info(message, ex);
        }
    }

}
