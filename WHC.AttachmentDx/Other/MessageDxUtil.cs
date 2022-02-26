using System;
using System.Windows.Forms;
using WHC.Framework.Language;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// MessageBox ��ժҪ˵����
    /// </summary>
    internal static class MessageDxUtil
    {
        private static string Caption_Tips = "��ʾ��Ϣ";
        private static string Caption_Warning = "������Ϣ";
        private static string Caption_Error = "������Ϣ";

        static MessageDxUtil()
        {
            Caption_Tips = JsonLanguage.Default.GetString(Caption_Tips);
            Caption_Warning = JsonLanguage.Default.GetString(Caption_Warning);
            Caption_Error = JsonLanguage.Default.GetString(Caption_Error);
        }

        /// <summary>
        /// ��ʾһ�����ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        public static DialogResult ShowTips(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Tips, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowWarning(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowError(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ�����־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ��ʾ��־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ�����־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ��ʾ��־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }

    }
}
