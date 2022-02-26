using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.Language;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 编辑控件的扩展函数
    /// </summary>
    public static class Control_Extension
    {
        #region 控件校验

        /// <summary>
        /// 校验控件是否为空，如果全部非空，则返回True
        /// </summary>
        /// <param name="dxErrorProvider"></param>
        /// <param name="baseEdits">输入控件集合</param>
        /// <returns></returns>
        public static bool ValidateEditNull(this DXErrorProvider dxErrorProvider, BaseEdit[] baseEdits, 
            string errorText = "不能为空", ErrorType errorType = ErrorType.Critical)
        {
            bool passed = true;
            for (int i = 0; i < baseEdits.Length; i++)
            {
                BaseEdit edit = baseEdits[i];
                string editValue = string.Concat(edit.EditValue).Trim();
                if (string.IsNullOrEmpty(editValue))
                {
                    //多语言支持
                    errorText = JsonLanguage.Default.GetString(errorText);

                    dxErrorProvider.SetError(edit, errorText, errorType);
                    if (i == 0)
                    {
                        edit.Focus();
                        edit.SelectAll();
                    }
                    passed = false;
                }
            }

            return passed;
        }

        /// <summary>
        /// 检验输入控件是否没有输入，为空则显示错误图标
        /// </summary>
        /// <param name="baseEdit">输入控件</param>
        /// <param name="dxErrorProvider"></param>
        /// <returns></returns>
        public static string ValidateEditNullError(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider)
        {
            string editValue = string.Concat(baseEdit.EditValue).Trim();
            if (string.IsNullOrEmpty(editValue))
            {
                //多语言支持
                string errorText = "请输入值";
                errorText = JsonLanguage.Default.GetString(errorText);

                dxErrorProvider.SetError(baseEdit, errorText);
                baseEdit.Focus();
                baseEdit.SelectAll();
            }
            return editValue;
        }

        /// <summary>
        /// 校验输入控件是否为空，为空则提示对话框
        /// </summary>
        /// <param name="baseEdit">输入控件</param>
        /// <returns></returns>
        public static bool ValidateEditNull(this BaseEdit baseEdit)
        {
            bool result;
            if (string.IsNullOrEmpty(baseEdit.Text))
            {
                MessageDxUtil.ShowTips("{0}不能为空", baseEdit.Tag);
                baseEdit.Focus();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        } 
        #endregion

        #region Panel只读和清空操作

        /// <summary>
        /// 设置控件的可见、读写权限显示
        /// </summary>
        /// <param name="panel">控件对象</param>
        /// <param name="permitDict">字段和权限字典，字典值为权限控制：0可读写，1只读，2隐藏值，3不显示</param>
        /// <param name="layoutControl">如果存在布局，则使用布局控件，否则为空</param>
        public static void SetControlPermit(this Control panel, Dictionary<string, int> permitDict, LayoutControl layoutControl = null)
        {
            foreach (Control ctrl in panel.Controls)
            {
                BaseEdit baseCtrl = ctrl as BaseEdit;
                if (baseCtrl != null)
                {
                    var tag = string.Concat(baseCtrl.Tag);
                    if (!string.IsNullOrEmpty(tag) && permitDict.ContainsKey(tag))
                    {
                        var permit = permitDict[tag];
                        var visible = (permit == 0 || permit == 1);//2、3不可见

                        if (layoutControl != null)
                        {
                            var layoutItem = layoutControl.GetItemByControl(baseCtrl);
                            if (layoutItem != null)
                            {
                                layoutItem.ToVisibility(visible);
                            }
                        }
                        baseCtrl.Visible = visible;
                        baseCtrl.ReadOnly = permit == 1;
                    }
                }
                ctrl.SetControlPermit(permitDict, layoutControl);
            }
        }

        /// <summary>
        /// 设置指定Panel内的控件为只读
        /// </summary>
        /// <param name="panel">控件面板</param>
        /// <param name="readOnly">是否只读，true为只读</param>
        public static void SetPanelReadOnly(this Control panel, bool readOnly = true)
        {
            foreach (Control c in panel.Controls)
            {
                if (c is BaseEdit)
                {
                    (c as BaseEdit).Properties.ReadOnly = readOnly;
                }
                c.SetPanelReadOnly(readOnly);
            }
        }

        /// <summary>
        /// 清除指定Panel内的控件的值
        /// </summary>
        /// <param name="panel">控件面板</param>
        public static void ClearPanelEditValue(this Control panel)
        {
            foreach (Control c in panel.Controls)
            {
                if (c is BaseEdit)
                {
                    (c as BaseEdit).EditValue = null;
                }
                ClearPanelEditValue(c);
            }
        } 
        #endregion

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

        /// <summary>
        /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
        /// </summary>
        /// <param name="control">DateEdit控件对象</param>
        /// <param name="dateTime">日期对象</param>
        public static void SetDateTime(this DateEdit control, DateTime? dateTime)
        {
            if(dateTime.HasValue)
            {
                if (dateTime > Convert.ToDateTime("1900-1-1"))
                {
                    control.DateTime = dateTime.Value;
                }
                else
                {
                    control.Text = "";
                }
            }
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
        /// <param name="control"></param>
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
        /// <param name="control"></param>
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
        /// <param name="control"></param>
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

        #endregion
    }
}
