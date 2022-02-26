using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Framework.Language;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 针对公用类库里面常规Winform控件界面的多语言处理辅助类
    /// </summary>
    internal class LanguageHelper
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

            if (control is ListView)
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
            else if (control is ToolStrip)
            {
                //对工具条，设置项目的标题和下拉项目的标题
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
