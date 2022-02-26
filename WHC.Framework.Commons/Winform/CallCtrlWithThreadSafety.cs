using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// ���߳������ݵİ󶨺͸�ֵ������
    /// </summary>
    public class CallCtrlWithThreadSafety
    {
        #region ���̵߳Ŀؼ���ȫ���ʷ�ʽ
        delegate void SetTextCallback2(ToolStripStatusLabel objCtrl, string text, Form winf);
        delegate void SetTextCallback(System.Windows.Forms.Control objCtrl, string text, Form winf);
        delegate void SetEnableCallback(System.Windows.Forms.Control objCtrl, bool enable, Form winf);
        delegate void SetFocusCallback(System.Windows.Forms.Control objCtrl, Form winf);
        delegate void SetCheckedCallback(System.Windows.Forms.CheckBox objCtrl, bool isCheck,Form winf);

        /// <summary>
        /// ���ÿؼ����ı�����
        /// </summary>
        /// <typeparam name="TObject">�ؼ���������</typeparam>
        /// <param name="objCtrl">�ؼ�����</param>
        /// <param name="text">�ı���Ϣ</param>
        /// <param name="winf">���ڴ���</param>
        public static void SetText<TObject>(TObject objCtrl, string text, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, text, winf });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        
        /// <summary>
        /// ���ÿؼ��Ŀ���״̬
        /// </summary>
        /// <typeparam name="TObject">�ؼ���������</typeparam>
        /// <param name="objCtrl">�ؼ�����</param>
        /// <param name="enable">�ؼ��Ƿ����</param>
        /// <param name="winf">���ڴ���</param>
        public static void SetEnable<TObject>(TObject objCtrl, bool enable, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetEnableCallback d = new SetEnableCallback(SetEnable);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, enable,winf });
            }
            else
            {
                objCtrl.Enabled = enable;
            }
        }
        
        /// <summary>
        /// ���ÿؼ��Ľ��㶨λ
        /// </summary>
        /// <typeparam name="TObject">�ؼ���������</typeparam>
        /// <param name="objCtrl">�ؼ�����</param>
        /// <param name="winf">���ڴ���</param>
        public static void SetFocus<TObject>(TObject objCtrl, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetFocusCallback d = new SetFocusCallback(SetFocus);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, winf });
            }
            else
            {
                objCtrl.Focus();
            }
        }
        
        /// <summary>
        /// ���ÿؼ���ѡ��״̬
        /// </summary>
        /// <typeparam name="TObject">�ؼ���������</typeparam>
        /// <param name="objCtrl">�ؼ�����</param>
        /// <param name="isChecked">�Ƿ�ѡ��</param>
        /// <param name="winf">���ڴ���</param>
        public static void SetChecked<TObject>(TObject objCtrl, bool isChecked,Form winf) where TObject : System.Windows.Forms.CheckBox
        {
            if (objCtrl.InvokeRequired)
            {
                SetCheckedCallback d = new SetCheckedCallback(SetChecked);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, isChecked, winf });
            }
            else
            {
                objCtrl.Checked = isChecked;
            }
        }
        
        /// <summary>
        /// ���ÿؼ��Ŀɼ�״̬
        /// </summary>
        /// <typeparam name="TObject">�ؼ���������</typeparam>
        /// <param name="objCtrl">�ؼ�����</param>
        /// <param name="isVisible">�Ƿ�ɼ�</param>
        /// <param name="winf">���ڴ���</param>
        public static void SetVisible<TObject>(TObject objCtrl, bool isVisible, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetCheckedCallback d = new SetCheckedCallback(SetChecked);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, isVisible, winf });
            }
            else
            {
                objCtrl.Visible = isVisible;
            }
        }

        /// <summary>
        /// ���ù���״̬�����ı�����
        /// </summary>
        /// <typeparam name="TObject">�ؼ���������</typeparam>
        /// <param name="objCtrl">�ؼ�����</param>
        /// <param name="text">�ı���Ϣ</param>
        /// <param name="winf">���ڴ���</param>
        public static void SetText2<TObject>(TObject objCtrl, string text, Form winf) where TObject : ToolStripStatusLabel
        {
            if (objCtrl.Owner.InvokeRequired)
            {
                SetTextCallback2 d = new SetTextCallback2(SetText2);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, text, winf });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        #endregion
    }
}
