using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using WHC.Framework.Language;
using DevExpress.XtraEditors;

namespace WHC.Pager.WinControl
{
    /// <summary>
    /// 对界面控件进行多语言的处理辅助类
    /// </summary>
    internal class LanguageHelper
    {             
        /// <summary>
        /// 初始化语言
        /// </summary>
        public static void InitLanguage(Control control)
        {
            //使用递归的方式对控件及其子控件进行处理
            SetControlLanguage(control);
            foreach (Control ctrl in control.Controls)
            {
                InitLanguage(ctrl);
            }

            //工具栏或者菜单动态构建窗体或者控件的时候，重新对子控件进行处理
            control.ControlAdded += (sender, e) =>
            {
                InitLanguage(e.Control);
            };
        }

        /// <summary>
        /// 对不同的控件类型，转换为对应的对象进行处理
        /// </summary>
        /// <param name="control"></param>
        private static void SetControlLanguage(Control control)
        {
            control.Text = JsonLanguage.Default.GetString(control.Text);

            //设置状态提示信息
            var baseControl = control as BaseControl;
            if(baseControl != null)
            {
                if (!string.IsNullOrEmpty(baseControl.ToolTip))
                {
                    baseControl.ToolTip = JsonLanguage.Default.GetString(baseControl.ToolTip);
                }
                if (!string.IsNullOrEmpty(baseControl.ToolTipTitle))
                {
                    baseControl.ToolTipTitle = JsonLanguage.Default.GetString(baseControl.ToolTipTitle);
                }
            }

            //对不同的控件进行不同的处理
            if (control is LayoutControl)
            {
                var layout = (LayoutControl)control;
                if (layout != null)
                {
                    foreach (BaseLayoutItem item in layout.Items)
                    {
                        item.Text = JsonLanguage.Default.GetString(item.Text);
                    }
                }
            }
            else if (control is GridControl)
            {
                var grid = (GridControl)control;
                if (grid != null)
                {
                    foreach (GridView view in grid.Views)
                    {
                        SetGridViewColumns(view);
                    }
                    if (grid.ContextMenuStrip != null)
                    {
                        SetMenuText(grid.ContextMenuStrip);
                    }
                }
            }
            else if (control is ToolStrip)
            {
                var tool = (ToolStrip)control;
                if (tool != null)
                {
                    foreach (ToolStripItem item in tool.Items)
                    {
                        item.Text = JsonLanguage.Default.GetString(item.Text);

                        //针对下拉列表的处理
                        if (item is ToolStripSplitButton)
                        {
                            var splitButton = (ToolStripSplitButton)item;
                            if (splitButton != null)
                            {
                                foreach (ToolStripItem dropItem in splitButton.DropDownItems)
                                {
                                    dropItem.Text = JsonLanguage.Default.GetString(dropItem.Text);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 在需要时候（一般为DataSourceChanged函数中）动态设置列的多语言信息
        /// </summary>
        /// <param name="gridView"></param>
        public static void SetGridViewColumns(GridView gridView)
        {
            foreach (GridColumn col in gridView.Columns)
            {
                col.Caption = JsonLanguage.Default.GetString(col.Caption);
            }
        }

        /// <summary>
        /// 设置菜单项的显示文本
        /// </summary>
        private static void SetMenuText(ContextMenuStrip menuStrip)
        {
            if (menuStrip != null)
            {
                foreach (ToolStripItem item in menuStrip.Items)
                {
                    if (item.Text.IndexOf("新建") > 0)
                    {
                        ;
                    }

                    item.Text = JsonLanguage.Default.GetString(item.Text);
                }
            }
        }

    }
}
