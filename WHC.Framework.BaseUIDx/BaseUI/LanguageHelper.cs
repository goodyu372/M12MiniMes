using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using WHC.Framework.Language;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 对界面控件进行多语言的处理辅助类
    /// </summary>
    public class LanguageHelper
    {             
        /// <summary>
        /// 初始化语言
        /// </summary>
        public static void InitLanguage(Control control)
        {
            //如果没有资源，那么不必遍历控件，提高速度
            if (!JsonLanguage.Default.HasResource)
                return;

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
            if (baseControl != null)
            {
                //对基础控件，设置它的提示信息和标题
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
                //对布局控件，设置布局项的文本
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
                //对GridControl,设置列的表头以及右键菜单
                var grid = (GridControl)control;
                if (grid != null)
                {
                    foreach (GridView view in grid.Views)
                    {
                        foreach (GridColumn col in view.Columns)
                        {
                            col.Caption = JsonLanguage.Default.GetString(col.Caption);
                        }
                    }
                    if(grid.ContextMenuStrip != null)
                    {
                        SetMenuText(grid.ContextMenuStrip);
                    }
                }
            }
            else if(control is NavBarControl)
            {
                //对NavBarControl，设置项目的标题和分组的标题
                var bar = (NavBarControl)control;
                if(bar != null)
                {
                    foreach(NavBarItem item in bar.Items)
                    {
                        item.Caption = JsonLanguage.Default.GetString(item.Caption);
                    }
                    foreach (NavBarGroup group in bar.Groups)
                    {
                        group.Caption = JsonLanguage.Default.GetString(group.Caption);
                    }
                }
            }
            else if (control is RibbonControl)
            {
                //对RibbonControl，设置RibbonPage、RibbonPageGroup、BarItem几级的显示内容
                var ribbon = (RibbonControl)control;
                if (ribbon != null)
                {
                    foreach(BarButtonItem rbitem in ribbon.Items)
                    {
                        rbitem.Caption = JsonLanguage.Default.GetString(rbitem.Caption);
                        rbitem.Description = JsonLanguage.Default.GetString(rbitem.Description);
                        rbitem.Hint = JsonLanguage.Default.GetString(rbitem.Hint);
                    }

                    foreach (RibbonPage page in ribbon.Pages)
                    {
                        page.Text = JsonLanguage.Default.GetString(page.Text);
                        foreach(RibbonPageGroup group in page.Groups)
                        {
                            group.Text = JsonLanguage.Default.GetString(group.Text);
                            foreach (BarButtonItemLink item in group.ItemLinks)
                            {
                                item.Caption = JsonLanguage.Default.GetString(item.Caption);
                            }
                        }
                    }
                }
            }
            else if(control is TreeList)
            {
                //对TreeList控件，设置列表列表头和右键菜单
                var treeview = (TreeList)control;
                if (treeview != null)
                {
                    foreach (TreeListColumn column in treeview.Columns)
                    {
                        column.Caption = JsonLanguage.Default.GetString(column.Caption);
                    }

                    if (treeview.ContextMenuStrip != null)
                    {
                        SetMenuText(treeview.ContextMenuStrip);
                    }
                }
            }
            else if (control is ListView)
            {
                //对列表控件，设置表头和右键菜单
                var listView = (ListView)control;
                if (listView != null)
                {
                    foreach (ColumnHeader column in listView.Columns)
                    {
                        column.Text = JsonLanguage.Default.GetString(column.Text);
                    }

                    if (listView.ContextMenuStrip != null)
                    {
                        SetMenuText(listView.ContextMenuStrip);
                    }
                }
            }
            else if(control is ToolStrip)
            {
                //对工具条，设置项目的标题和下拉项目的标题
                var tool = (ToolStrip)control;
                if(tool != null)
                {
                    foreach(ToolStripItem item in tool.Items)
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
            else if(control is BarDockControl)
            {
                //对DevExpress的bar控件，设置项目的标题
                var dock = (BarDockControl)control;
                if(dock != null && dock.Manager != null)
                {
                    foreach(BarItem item in dock.Manager.Items)
                    {
                        item.Caption = JsonLanguage.Default.GetString(item.Caption);
                    }
                }
            }
            else if (control is TreeView)
            {
                //对传统树列表，设置节点的文本和菜单
                var treeview = control as TreeView;
                if (treeview != null)
                {
                    foreach (TreeNode node in treeview.Nodes)
                    {
                        SetNodeText(node);
                    }

                    if (treeview.ContextMenuStrip != null)
                    {
                        SetMenuText(treeview.ContextMenuStrip);
                    }
                }
            }
            else if (control is TreeListLookUpEdit)
            {
                //对TreeList控件，设置列表列表头和右键菜单
                var treeview = (TreeListLookUpEdit)control;
                if (treeview != null)
                {
                    if (treeview.Properties.TreeList != null &&
                    treeview.Properties.TreeList.Columns != null)
                    {
                        foreach (TreeListColumn column in treeview.Properties.TreeList.Columns)
                        {
                            column.Caption = JsonLanguage.Default.GetString(column.Caption);
                        }
                    }

                    //提示信息
                    treeview.Properties.NullValuePrompt = JsonLanguage.Default.GetString(treeview.Properties.NullValuePrompt);

                    if (treeview.ContextMenuStrip != null)
                    {
                        SetMenuText(treeview.ContextMenuStrip);
                    }
                }
            }
            else if (control is TextEdit)
            {
                //显示占位符
                var baseEdit = (TextEdit)control;
                if (baseEdit != null)
                {
                    baseEdit.Properties.NullText = JsonLanguage.Default.GetString(baseEdit.Properties.NullText);
                    baseEdit.Properties.NullValuePrompt = JsonLanguage.Default.GetString(baseEdit.Properties.NullValuePrompt);
                }
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
                    item.Text = JsonLanguage.Default.GetString(item.Text);
                }
            }
        }

        /// <summary>
        /// 设置树形列表的Node的文本（可以不用，由数据展示）
        /// </summary>
        /// <param name="node"></param>
        private static void SetNodeText(TreeNode node)
        {
            node.Text = JsonLanguage.Default.GetString(node.Text);
            foreach (TreeNode subNode in node.Nodes)
            {
                SetNodeText(subNode);
            }
        }

    }
}
