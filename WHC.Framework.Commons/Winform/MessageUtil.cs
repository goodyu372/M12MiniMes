using System;
using System.Windows.Forms;
using WHC.Framework.Language;

namespace WHC.Framework.Commons
{
	/// <summary>
	/// ���ͳһ�ĶԻ�����ʾ������
	/// </summary>
	public static class MessageUtil
    {
        private static string Caption_Tips = "��ʾ��Ϣ";
        private static string Caption_Warning = "������Ϣ";
        private static string Caption_Error = "������Ϣ";

        static MessageUtil()
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
            return ShowTips(message, null);
        }

        /// <summary>
        /// ��ʾһ�����ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        /// <returns></returns>
		public static DialogResult ShowTips(string message, params object[] args)
		{
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }

			return MessageBox.Show(message, Caption_Tips,MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowWarning(string message)
        {
            return ShowWarning(message, null);
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        public static DialogResult ShowWarning(string message, params object[] args)
        {
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }

			return MessageBox.Show(message, Caption_Warning,  MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowError(string message)
        {
            return ShowError(message, null);
        }
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        public static DialogResult ShowError(string message, params object[] args)
        {
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }
			return MessageBox.Show(message, Caption_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ�����־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return ShowYesNoAndError(message, null);
        }
        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ�����־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        public static DialogResult ShowYesNoAndError(string message, params object[] args)
        {
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }
			return MessageBox.Show(message, Caption_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
		}

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ��ʾ��־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return ShowYesNoAndTips(message, null);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ��ʾ��־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        public static DialogResult ShowYesNoAndTips(string message, params object[] args)
        {
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }
			return MessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
		}

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ�����־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return ShowYesNoAndWarning(message, null);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ�����־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        public static DialogResult ShowYesNoAndWarning(string message, params object[] args)
        {
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }
            return MessageBox.Show(message, Caption_Warning,  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ��ʾ��־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return ShowYesNoCancelAndTips(message, null);
        }

        /// <summary>
        /// ��ʾѯ���û���Ϣ������ʾ��ʾ��־
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="args">�ַ�������Ĳ�������</param>
        public static DialogResult ShowYesNoCancelAndTips(string message, params object[] args)
        {
            message = JsonLanguage.Default.GetString(message);
            if (args != null)
            {
                message = string.Format(message, args);
            }
            return MessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ��ʾһ��YesNoѡ��Ի���
        /// </summary>
        /// <param name="message">�Ի����ѡ��������ʾ��Ϣ</param>
        /// <returns>���ѡ��Yes�򷵻�true�����򷵻�false</returns>
        public static bool ConfirmYesNo(string message)
        {
            message = JsonLanguage.Default.GetString(message);
            return MessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// ��ʾһ��YesNoCancelѡ��Ի���
        /// </summary>
        /// <param name="message">�Ի����ѡ��������ʾ��Ϣ</param>
        /// <returns>����ѡ�����ĵ�DialogResultֵ</returns>
        public static DialogResult ConfirmYesNoCancel(string message)
        {
            return MessageBox.Show(message, Caption_Tips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// ѯ��һ�������ַ���
        /// </summary>
        /// <param name="prompt">��ʾ��Ϣ</param>
        /// <param name="initValue">��ʼֵ</param>
        /// <param name="isPassword">�Ƿ������ַ���</param>
        /// <returns>ѯ�ʵ����ַ���</returns>
        public static string QueryInputStr(string prompt, string initValue = "", bool isPassword = false)
        {
            prompt = JsonLanguage.Default.GetString(prompt);

            QueryInputDialog dlg = new QueryInputDialog();
            dlg.Text = prompt;
            dlg.lblPrompt.Text = prompt.EndsWith(":") || prompt.EndsWith("��") ? prompt : prompt + ":";
            dlg.txtInput.Text = initValue;
            dlg.IsEncryptInput = isPassword;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.txtInput.Text;
            }
            return initValue;
        }
	}
}
