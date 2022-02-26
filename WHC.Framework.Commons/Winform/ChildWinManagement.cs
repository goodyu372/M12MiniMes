using System;
using System.Windows.Forms;

namespace WHC.Framework.Commons
{
	/// <summary>
	/// ���ڹ�������
	/// </summary>
	public sealed class ChildWinManagement
	{
		private ChildWinManagement()
		{
		}

		/// <summary> 
		/// ��ȡMDI�������Ƿ��д��ڱ���Ϊָ���ַ������Ӵ��ڣ�����Ѿ����ڰѴ��Ӵ�������ǰ̨�� 
		/// </summary> 
		/// <param name="MDIwin">MDI������</param> 
		/// <param name="caption">���ڱ���</param> 
		/// <returns></returns> 
		public static bool ExistWin(Form MDIwin, string caption)
		{
			bool result = false;
			foreach (Form f in MDIwin.MdiChildren)
			{
				if (f.Text == caption)
				{
					result = true;
					f.Show();
					f.Activate();
					break;
				}
			}
			return result;
		}
                
        /// <summary>
        /// Ψһ����ĳ�����͵Ĵ��壬�����������ʾ�����򴴽���
        /// </summary>
        /// <param name="mainDialog">���������</param>
        /// <param name="formType">����ʾ�Ĵ�������</param>
        /// <returns></returns>
        public static Form LoadMdiForm(Form mainDialog, Type formType)
        {
            return LoadMdiForm(mainDialog, formType, null);
        }

        /// <summary>
        /// Ψһ����ĳ�����͵Ĵ��壬�����������ʾ�����򴴽���
        /// </summary>
        /// <param name="mainDialog">���������</param>
        /// <param name="formType">����ʾ�Ĵ�������</param>
        /// <param name="json">���ݵĲ������ݣ��Զ���Json��ʽ</param>
        /// <returns></returns>
		public static Form LoadMdiForm(Form mainDialog, Type formType, string json)
		{
			bool bFound = false;
			Form tableForm = null;
			foreach (Form form in mainDialog.MdiChildren)
			{
				if (form.GetType() == formType)
				{
					bFound = true;
					tableForm = form;
					break;
				}
			}
			if (!bFound)
			{
				tableForm = (Form) Activator.CreateInstance(formType);
                tableForm.MdiParent = mainDialog;
                tableForm.Show();
			}

            //���弤���ʱ�򣬴��ݶ�Ӧ�Ĳ�����Ϣ
            ILoadFormActived formActived = tableForm as ILoadFormActived;
            if (formActived != null)
            {
                formActived.OnLoadFormActived(json);
            }

			//tableForm.Dock = DockStyle.Fill;
			//tableForm.WindowState = FormWindowState.Maximized;
			tableForm.BringToFront();
			tableForm.Activate();

			return tableForm;
		}

        /// <summary>
        /// �ѿؼ����ӵ������ϵ���
        /// </summary>
        /// <param name="control">����ʾ�Ŀؼ�</param>
        /// <param name="caption">������ʾ�ı���</param>
        public static void PopControlForm(Type control, string caption)
        {
            object ctr = ReflectionUtil.CreateInstance(control);
            if ((typeof(Control)).IsAssignableFrom(ctr.GetType()))
            {
                Form tmp = new Form();
                tmp.WindowState = FormWindowState.Maximized;
                tmp.ShowIcon = false;
                tmp.Text = caption;
                tmp.ShowInTaskbar = false;
                tmp.StartPosition = FormStartPosition.CenterScreen;

                Control ctrtmp = ctr as Control;
                ctrtmp.Dock = DockStyle.Fill;
                tmp.Controls.Add(ctrtmp);
                tmp.ShowDialog();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="type">����ʾ�Ĵ�������</param>
        public static void PopDialogForm(Type type)
        {
            object form = ReflectionUtil.CreateInstance(type);
            if ((typeof(Form)).IsAssignableFrom(form.GetType()))
            {
                Form tmp = form as Form;
                tmp.ShowInTaskbar = false;
                tmp.StartPosition = FormStartPosition.CenterScreen;
                tmp.ShowDialog();
            }
        }
	}

    /// <summary>
    /// ʹ��ChildWinManagement�����ദ����ĵ����صĴ��壬�ڹ����򼤻�󣬴���һ��֪ͨ������¼������㴫����ز�����Ŀ�괰�塣
    /// Ϊ�˸���ͨ�õĴ������ݵĲ���ʹ��JSON�����ʽ���ַ�����
    /// </summary>
    public interface ILoadFormActived
    {
        /// <summary>
        /// ���弤����¼�����
        /// </summary>
        /// <param name="json">���ݵĲ������ݣ��Զ���JSON��ʽ</param>
        void OnLoadFormActived(string json);
    }
}