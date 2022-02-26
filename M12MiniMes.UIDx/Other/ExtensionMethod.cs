using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;

using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Dictionary.BLL;

namespace M12MiniMes.UI
{
    /// <summary>
    /// 扩展函数封装
    /// </summary>
    public static class ExtensionMethod
    {
        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="emptyFlag">是否添加空行</param>
        public static void BindDictItems(this ComboBoxEdit control, string dictTypeName, bool emptyFlag = true)
        {
            BindDictItems(control, dictTypeName, null, emptyFlag);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="defaultValue">控件默认值</param>
        /// <param name="emptyFlag">是否添加空行</param>
        public static void BindDictItems(this ComboBoxEdit control, string dictTypeName, string defaultValue, bool emptyFlag = true)
        {
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            List<CListItem> itemList = new List<CListItem>();
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }

            control.BindDictItems(itemList, defaultValue, emptyFlag);
        }


        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="emptyFlag">是否添加空行</param>
        public static List<CListItem> BindDictItems(this RepositoryItemComboBox control, string dictTypeName, bool emptyFlag = true)
        {
            List<CListItem> list = BLLFactory<DictData>.Instance.GetDictListItemByDictType(dictTypeName);
            return control.BindDictItems(list, emptyFlag);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="emptyFlag">是否添加空行</param>
        public static List<CListItem> BindDictItems(this RepositoryItemCheckedComboBoxEdit control, string dictTypeName, bool emptyFlag = true)
        {
            List<CListItem> list = BLLFactory<DictData>.Instance.GetDictListItemByDictType(dictTypeName);
            return control.BindDictItems(list, emptyFlag);
        }


        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this CheckedComboBoxEdit control, string dictTypeName)
        {
            BindDictItems(control, dictTypeName, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this CheckedComboBoxEdit control, string dictTypeName, string defaultValue)
        {
            List<CListItem> itemList = new List<CListItem>();
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }

            control.BindDictItems(itemList, defaultValue);
        }


        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="control">单选框组</param>
        /// <param name="dictTypeName">字典大类</param>
        public static void BindDictItems(this RadioGroup control, string dictTypeName)
        {
            BindDictItems(control, dictTypeName, null);
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="dictTypeName">字典大类</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this RadioGroup radGroup, string dictTypeName, string defaultValue)
        {
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            List<RadioGroupItem> groupList = new List<RadioGroupItem>();
            foreach (string key in dict.Keys)
            {
                groupList.Add(new RadioGroupItem(dict[key], key));
            }

            radGroup.Properties.BeginUpdate();//可以加快
            radGroup.Properties.Items.Clear();
            radGroup.Properties.Items.AddRange(groupList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                radGroup.SetRaidioGroupItem(defaultValue);
            }

            radGroup.Properties.EndUpdate();//可以加快
        }


        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="emptyFlag">是否添加空行</param>
        public static List<CListItem> BindDictItems(this RepositoryItemSearchLookUpEdit lookup, string dictTypeName, bool emptyFlag = true)
        {
            List<CListItem> list = BLLFactory<DictData>.Instance.GetDictListItemByDictType(dictTypeName);
            return lookup.BindDictItems(list, emptyFlag);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="emptyFlag">是否添加空行</param>
        public static List<CListItem> BindDictItems(this RepositoryItemLookUpEdit lookup, string dictTypeName, bool emptyFlag = true)
        {
            List<CListItem> list = BLLFactory<DictData>.Instance.GetDictListItemByDictType(dictTypeName);
            return lookup.BindDictItems(list, emptyFlag);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this CustomGridLookUpEdit combo, string dictTypeName)
        {
            BindDictItems(combo, dictTypeName, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this CustomGridLookUpEdit combo, string dictTypeName, string defaultValue)
        {
            string displayName = dictTypeName;
            const string valueName = "值内容";
            const string pinyin = "拼音码";
            DataTable dt = DataTableHelper.CreateTable(string.Format("{0},{1},{2}", displayName, valueName, pinyin));

            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            foreach (string key in dict.Keys)
            {
                DataRow row = dt.NewRow();
                row[displayName] = key;
                row[valueName] = dict[key];
                row[pinyin] = Pinyin.GetFirstPY(key);
                dt.Rows.Add(row);
            }

            combo.Properties.ValueMember = valueName;
            combo.Properties.DisplayMember = displayName;
            combo.Properties.DataSource = dt;
            combo.Properties.PopulateViewColumns();
            combo.Properties.View.Columns[valueName].Visible = false;
            combo.Properties.View.Columns[displayName].Width = 400;
            combo.Properties.View.Columns[pinyin].Width = 200;
            combo.Properties.PopupFormMinSize = new System.Drawing.Size(600, 0);

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.EditValue = defaultValue;
            }
        }
    }
}
