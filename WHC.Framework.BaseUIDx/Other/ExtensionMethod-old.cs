using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using WHC.Framework.Commons;
using DevExpress.XtraEditors.Controls;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 扩展函数封装
    /// </summary>
    public static class ExtensionMethod
    {
        #region 日期控件
        /// <summary>
        /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
        /// </summary>
        /// <param name="control">DateEdit控件对象</param>
        /// <param name="dateTime">日期对象</param>
        public static void SetDateTime(this DateEdit control, DateTime dateTime)
        {
            if (dateTime > Convert.ToDateTime("1900-1-1"))
            {
                control.DateTime = dateTime;
            }
            else
            {
                control.Text = "";
            }
        }

        #endregion

        #region ComboBoxEdit控件

        /// <summary>
        /// 获取下拉列表的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <returns></returns>
        public static string GetComboBoxValue(this ComboBoxEdit combo)
        {
            CListItem item = combo.SelectedItem as CListItem;
            if (item != null)
            {
                return item.Value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static int SetDropDownValue(this ComboBoxEdit combo, string value)
        {
            int result = -1;
            for (int i = 0; i < combo.Properties.Items.Count; i++)
            {
                if (combo.Properties.Items[i].ToString() == value)
                {
                    combo.SelectedIndex = i;
                    result = i;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this ComboBoxEdit combo, string value)
        {
            for (int i = 0; i < combo.Properties.Items.Count; i++)
            {
                CListItem item = combo.Properties.Items[i] as CListItem;
                if (item != null && item.Value == value)
                {
                    combo.SelectedIndex = i;
                }
            }
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this ComboBoxEdit combo, List<CListItem> itemList)
        {
            BindDictItems(combo, itemList, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this ComboBoxEdit combo, List<CListItem> itemList, string defaultValue)
        {
            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            combo.Properties.Items.AddRange(itemList);

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }

            combo.Properties.EndUpdate();//可以加快
        }

        /// <summary>
        /// 绑定枚举类型
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="enumType">枚举类型</param>
        public static void BindDictItems<T>(this ComboBoxEdit combo)
        {
            Dictionary<string, object> dict = EnumHelper.GetMemberKeyValue<T>();
            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            foreach (string key in dict.Keys)
            {
                combo.Properties.Items.Add(new CListItem(key, dict[key].ToString()));
            }
            combo.Properties.EndUpdate();//可以加快
        }

        #endregion

        #region CheckedComboBoxEdit
        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this CheckedComboBoxEdit combo, string value)
        {
            combo.SetEditValue(value);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, List<CListItem> itemList)
        {
            BindDictItems(combo, itemList, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        /// <param name="defaultValue">默认值</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, List<CListItem> itemList, string defaultValue)
        {
            List<CheckedListBoxItem> checkList = new List<CheckedListBoxItem>();
            foreach (CListItem item in itemList)
            {
                checkList.Add(new CheckedListBoxItem(item.Value, item.Text));
            }

            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            combo.Properties.Items.AddRange(checkList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }

            combo.Properties.EndUpdate();//可以加快
        }

        #endregion

        #region 单选框组RadioGroup

        /// <summary>
        /// 设置单选框组的选定内容
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="value">选定内容</param>
        public static void SetRaidioGroupItem(this RadioGroup radGroup, string value)
        {
            radGroup.SelectedIndex = radGroup.Properties.Items.GetItemIndexByValue(value);
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="itemList">字典列表</param>
        public static void BindDictItems(this RadioGroup radGroup, List<CListItem> itemList)
        {
            BindDictItems(radGroup, itemList, null);
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="itemList">字典列表</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this RadioGroup radGroup, List<CListItem> itemList, string defaultValue)
        {
            List<RadioGroupItem> groupList = new List<RadioGroupItem>();
            foreach (CListItem item in itemList)
            {
                groupList.Add(new RadioGroupItem(item.Value, item.Text));
            }

            radGroup.Properties.BeginUpdate();//可以加快
            radGroup.Properties.Items.Clear();
            radGroup.Properties.Items.AddRange(groupList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                SetRaidioGroupItem(radGroup, defaultValue);
            }
            radGroup.Properties.EndUpdate();//可以加快
        }


        #endregion

        #region 查询相关扩展
        /// <summary>
        /// 添加开始日期和结束日期的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startCtrl">开始日期控件</param>
        /// <param name="endCtrl">结束日期控件</param>
        /// <returns></returns>
        public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, DateEdit startCtrl, DateEdit endCtrl)
        {
            if (startCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, Convert.ToDateTime(startCtrl.DateTime.ToShortDateString()), SqlOperator.MoreThanOrEqual);
            }
            if (endCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, Convert.ToDateTime(endCtrl.DateTime.AddDays(1).ToShortDateString()), SqlOperator.LessThan);
            }
            return condition;
        }

        /// <summary>
        /// 添加数值区间的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startCtrl">开始范围控件</param>
        /// <param name="endCtrl">结束范围控件</param>
        /// <returns></returns>
        public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, SpinEdit startCtrl, SpinEdit endCtrl)
        {
            if (startCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, startCtrl.Value, SqlOperator.MoreThanOrEqual);
            }
            if (endCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, endCtrl.Value, SqlOperator.LessThanOrEqual);
            }
            return condition;
        }

        /// <summary>
        /// 添加数值区间的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startCtrl">开始范围控件</param>
        /// <param name="endCtrl">结束范围控件</param>
        /// <returns></returns>
        public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
        {
            decimal value = 0;
            if (decimal.TryParse(startCtrl.Text.Trim(), out value))
            {
                condition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
            }
            if (decimal.TryParse(endCtrl.Text.Trim(), out value))
            {
                condition.AddCondition(fieldName, value, SqlOperator.LessThanOrEqual);
            }
            return condition;
        }

        #endregion

        #region 控件布局显示
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraLayout.LayoutControlGroup control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraLayout.LayoutControlGroup control)
        {
            return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraLayout.LayoutControlItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraLayout.LayoutControlItem control)
        {
            return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraLayout.EmptySpaceItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraLayout.EmptySpaceItem control)
        {
            return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraBars.BarButtonItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraBars.BarButtonItem control)
        {
            return control.Visibility == DevExpress.XtraBars.BarItemVisibility.Always;
        }

        #endregion

        #region GridControl设置

        /// <summary>
        /// 设置表格的宽度
        /// </summary>
        /// <param name="gridView">表格控件</param>
        /// <param name="columnName">列名称</param>
        /// <param name="width">宽度</param>
        public static void SetGridColumWidth(this DevExpress.XtraGrid.Views.Grid.GridView gridView, string columnName, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = gridView.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
            else
            {
                column = gridView.Columns.ColumnByFieldName(columnName.ToUpper());
                if (column != null)
                {
                    column.Width = width;
                }
            }
        }

        #endregion
    }
}
