using System;
using System.Windows.Forms;
using WHC.Framework.Language;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// MessageBox 的摘要说明。
    /// </summary>
    internal static class MessageDxUtil
    {
        private static string Caption_Tips = "提示信息";
        private static string Caption_Warning = "警告信息";
        private static string Caption_Error = "错误信息";

        static MessageDxUtil()
        {
            Caption_Tips = JsonLanguage.Default.GetString(Caption_Tips);
            Caption_Warning = JsonLanguage.Default.GetString(Caption_Warning);
            Caption_Error = JsonLanguage.Default.GetString(Caption_Error);
        }

        /// <summary>
        /// 显示一般的提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        public static DialogResult ShowTips(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Tips, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowWarning(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowError(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示询问用户信息，并显示错误标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示询问用户信息，并显示警告标志
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }

    }
}
