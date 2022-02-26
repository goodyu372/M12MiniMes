using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// CheckListBox列表控件选择项操作辅助类
    /// </summary>
    public static class CheckBoxListUtil
    {
        /// <summary>
        /// 设置列表选择项，如果列表值在字符串中，则选中
        /// </summary>
        /// <param name="cblItems">列表控件</param>
        /// <param name="valueList">值列表，逗号分开各个值</param>
        public static void SetCheck(CheckedListBox cblItems, string valueList)
        {
            string[] strtemp = valueList.Split(',');
            foreach (string str in strtemp)
            {
                for (int i = 0; i < cblItems.Items.Count; i++)
                {
                    if (cblItems.GetItemText(cblItems.Items[i]) == str)
                    {
                        cblItems.SetItemChecked(i, true);
                    }
                }
            }
        }

        /// <summary>
        /// 获取列表控件的选中的值，各值通过逗号分开
        /// </summary>
        /// <param name="cblItems">列表控件</param>
        /// <returns></returns>
        public static string GetCheckedItems(CheckedListBox cblItems)
        {
            string resultList = "";
            for (int i = 0; i < cblItems.CheckedItems.Count; i++)
            {
                if (cblItems.GetItemChecked(i))
                {
                    resultList += string.Format("{0},", cblItems.GetItemText(cblItems.Items[i]));
                }
            }
            return resultList.Trim(',');
        }

        /// <summary>
        /// 如果值列表中有的,根据内容勾选GroupBox里面的成员.
        /// </summary>
        /// <param name="group">包含CheckBox控件组的GroupBox控件</param>
        /// <param name="valueList">逗号分隔的值列表</param>
        public static void SetCheck(GroupBox group, string valueList)
        {
            string[] strtemp = valueList.Split(',');
            foreach (string str in strtemp)
            {
                foreach (Control control in group.Controls)
                {
                    CheckBox chk = control as CheckBox;
                    if (chk != null && chk.Text == str)
                    {
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// 获取GroupBox控件成员勾选的值
        /// </summary>
        /// <param name="group">包含CheckBox控件组的GroupBox控件</param>
        /// <returns>返回逗号分隔的值列表</returns>
        public static string GetCheckedItems(GroupBox group)
        {
            string resultList = "";
            foreach (Control control in group.Controls)
            {
                CheckBox chk = control as CheckBox;
                if (chk != null && chk.Checked)
                {
                    resultList += string.Format("{0},", chk.Text);
                }
            }
            return resultList.Trim(',');
        }


        /// <summary>
        /// 获取选中项集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="checkedListBox">CheckedListBox</param>
        /// <returns>选中项集合</returns>
        public static List<T> GetCheckedItemList<T>(this CheckedListBox checkedListBox)
            where T : class
        {
            List<T> _checkedItemList = new List<T>();
            for (int i = 0; i < checkedListBox.CheckedItems.Count; i++)
            {
                T _item = (T)checkedListBox.Items[i];
                _checkedItemList.Add(_item);
            }
            return _checkedItemList;
        }

        /// <summary>
        /// 设置项勾选状态
        /// </summary>
        /// <param name="checkedListBox">CheckedListBox</param>
        /// <param name="state">是否勾选</param>
        public static void SetAllItemState(this CheckedListBox checkedListBox, bool state)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, state);
            }
        }

        /// <summary>
        /// CheckedListBox 数据绑定
        /// </summary>
        /// <param name="checkedListBox">CheckedListBox</param>
        /// <param name="dataSource">绑定数据源</param>
        /// <param name="valueMember">隐式字段</param>
        /// <param name="displayMember">显示字段</param>
        public static void SetDataSource(this CheckedListBox checkedListBox, object dataSource, string valueMember, string displayMember)
        {
            checkedListBox.DataSource = dataSource;
            checkedListBox.ValueMember = valueMember;
            checkedListBox.DisplayMember = displayMember;
        }
    }
}
