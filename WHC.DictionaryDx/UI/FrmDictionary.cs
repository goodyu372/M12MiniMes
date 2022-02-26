﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using WHC.Pager.Entity;
using WHC.Dictionary.Entity;
using WHC.Dictionary.BLL;

using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Framework.Language;

namespace WHC.Dictionary.UI
{
    public partial class FrmDictionary : BaseForm
    {
        public string LoginID = "";
        private LicenseCheckResult LicenseResult = new LicenseCheckResult();//授权码检查的结果

        public FrmDictionary()
        {
            InitializeComponent();
            WHC.Pager.WinControl.MyConstants.License = "070eV0hDLlBhZ2VyfOS8jeWNjuiBqnx8RmFsc2Uv"; 

            TreeViewDrager drager = new TreeViewDrager(this.treeView1);
            drager.TreeImageList = this.imageList1;
            drager.ProcessDragNode += new ProcessDragNodeEventHandler(drager_ProcessDragNode);
        }

        bool drager_ProcessDragNode(TreeNode dragNode, TreeNode dropNode)
        {
            if (dragNode != null && dragNode.Text == "数据字典管理")
                return false;

            if (dropNode != null && dropNode.Tag != null)
            {
                string dropTypeId = dropNode.Tag.ToString();
                string dragTypeId = dragNode.Tag.ToString();
                //MessageDxUtil.ShowTips(string.Format("dropTypeId:{0} dragTypeId:{1}", dropTypeId, drageTypeId));

                try
                {
                    DictTypeInfo dragTypeInfo = BLLFactory<DictType>.Instance.FindByID(dragTypeId);
                    if (dragTypeInfo != null)
                    {
                        dragTypeInfo.PID = dropTypeId;
                        BLLFactory<DictType>.Instance.Update(dragTypeInfo, dragTypeInfo.ID);
                    }
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                    return false;
                }                
            }
            return true;
        }

        private void FrmDictionary_Load(object sender, EventArgs e)
        {
            InitTreeView();
            this.lblDictType.Text = "";
            BindData();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip2;

            this.winGridViewPager1.BestFitColumnWith = false;
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                this.winGridViewPager1.gridView1.Columns["Name"].Width = 200;
                this.winGridViewPager1.gridView1.Columns["Value"].Width = 200;
            }
        }

        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<DictData>.Instance.Delete(ID);
            }
            BindData();
        }

        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditDictData dlg = new FrmEditDictData();
                dlg.ID = ID;
                dlg.txtDictType.Tag = this.lblDictType.Tag;
                dlg.TypeID = this.lblDictType.Tag.ToString();
                dlg.LoginID = LoginID;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            if (this.lblDictType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请选择指定的字典大类，然后添加！");
                return;
            }

            FrmEditDictData dlg = new FrmEditDictData();
            dlg.txtDictType.Text = this.lblDictType.Text;
            dlg.txtDictType.Tag = this.lblDictType.Tag;
            dlg.TypeID = this.lblDictType.Tag.ToString();
            dlg.LoginID = LoginID;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private void btnBatchAdd_Click(object sender, EventArgs e)
        {
            if (this.lblDictType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请选择指定的字典大类，然后添加！");
                return;
            }

            FrmBatchAddDictData dlg = new FrmBatchAddDictData();
            dlg.ID = this.lblDictType.Tag.ToString();
            dlg.txtDictType.Text = this.lblDictType.Text;
            dlg.txtDictType.Tag = this.lblDictType.Tag;
            dlg.LoginID = LoginID;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string condition = GetCondtionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<DictData>.Instance.FindToDataTable(condition);
        }

        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private string GetCondtionSql()
        {
            SearchCondition conditon = new SearchCondition();
            if (lblDictType.Tag != null)
            {
                conditon.AddCondition("DictType_ID", this.lblDictType.Tag.ToString(), SqlOperator.Equal);
            }
            string sql = conditon.BuildConditionSql().Replace("Where", "");
            return sql;
        }

        private void BindData()
        {
            #region 添加别名解析
            this.winGridViewPager1.DisplayColumns = "Name,Value,Seq,Remark,EditTime";
            this.winGridViewPager1.AddColumnAlias("ID", "编号");
            this.winGridViewPager1.AddColumnAlias("DictType_ID", "字典大类");
            this.winGridViewPager1.AddColumnAlias("Name", "项目名称");
            this.winGridViewPager1.AddColumnAlias("Value", "项目值");
            this.winGridViewPager1.AddColumnAlias("Seq", "字典排序");
            this.winGridViewPager1.AddColumnAlias("Remark", "备注");
            this.winGridViewPager1.AddColumnAlias("Editor", "修改用户");
            this.winGridViewPager1.AddColumnAlias("EditTime", "更新日期");
            #endregion

            if (this.lblDictType.Tag != null)
            {
                string condition = GetCondtionSql();
                List<DictDataInfo> list = BLLFactory<DictData>.Instance.FindWithPager(condition, this.winGridViewPager1.PagerInfo);

                this.winGridViewPager1.DataSource = new WHC.Pager.WinControl.SortableBindingList<DictDataInfo>(list);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 初始化树信息
        /// </summary>
        private void InitTreeView()
        {
            this.treeView1.Nodes.Clear();
            this.treeView1.BeginUpdate();
            List<DictTypeNodeInfo> typeNodeList = BLLFactory<DictType>.Instance.GetTree();
            foreach (DictTypeNodeInfo info in typeNodeList)
            {
                AddTree(null, info);
            }
            this.treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        /// <summary>
        /// 根据节点数据，递归构建该层级以下的树节点
        /// </summary>
        /// <param name="pNode">父树节点</param>
        /// <param name="info">字典类型数据</param>
        private void AddTree(TreeNode pNode, DictTypeNodeInfo info)
        {
            TreeNode node = null;
            if (info.PID == "-1")
            {
                node = new TreeNode(info.Name, 1, 1);
                node.Tag = info.ID;
                this.treeView1.Nodes.Add(node);
            }
            else
            {
                node = new TreeNode(info.Name, 1, 1);
                node.Tag = info.ID;
                pNode.Nodes.Add(node);
            }

            foreach (DictTypeNodeInfo subInfo in info.Children)
            {
                AddTree(node, subInfo);
            }
        }

        /// <summary>
        /// 单击节点事件处理
        /// </summary>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.lblDictType.Text = e.Node.Text;
                this.lblDictType.Tag = e.Node.Tag;

                BindData();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnAddNew(this.winGridViewPager1.gridView1, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(this.winGridViewPager1.gridView1, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnDeleteSelected(this.winGridViewPager1.gridView1, null);
        }
                
        private string GetParentNodeIndex()
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                return node.Tag.ToString();
            }
            return "-1";
        }

        private void menu_AddType_Click(object sender, EventArgs e)
        {
            string pid = GetParentNodeIndex();
            FrmEditDictType dlg = new FrmEditDictType();
            dlg.PID = pid;
            dlg.LoginID = LoginID;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                InitTreeView();
            }
        }

        private void menu_DeleteType_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                string typeId = selectedNode.Tag.ToString();
                //int count = BLLFactory<DictData>.Instance.GetDictByTypeID(typeId).Count;
                //int typeCount = BLLFactory<DictType>.Instance.GetAllType(typeId).Count;
                //if (count > 0 || typeCount > 0)
                //{
                //    MessageDxUtil.ShowError("不能直接删除有项目内容的大类，请先删除其下面的项目。");
                //    return;
                //}

                var format = "您确定要删除节点：{0}，删除将子节点及其数据均一并删除，请谨慎操作。";
                format = JsonLanguage.Default.GetString(format);
                string message = string.Format(format, selectedNode.Text);
                if (MessageDxUtil.ShowYesNoAndWarning(message) == DialogResult.Yes)
                {
                    try
                    {
                        Dictionary<string, string> dict = BLLFactory<DictType>.Instance.GetAllType(typeId);
                        dict.Add(typeId, typeId);//增加一个自己，也需要删除

                        foreach (string key in dict.Keys)
                        {
                            string subTypeID = dict[key];
                            BLLFactory<DictType>.Instance.Delete(subTypeID);

                            string condition = string.Format("DictType_ID='{0}'", subTypeID);
                            BLLFactory<DictData>.Instance.DeleteByCondition(condition);
                        }
                        
                        InitTreeView();
                    }
                    catch (Exception ex)
                    {
                        LogTextHelper.Error(ex);
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        private void menu_Refresh_Click(object sender, EventArgs e)
        {
            InitTreeView();
        }

        private void menu_EditType_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                string typeId = selectedNode.Tag.ToString();
                DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(typeId);
                if (info != null)
                {
                    FrmEditDictType dlg = new FrmEditDictType();
                    dlg.ID = typeId;
                    dlg.PID = info.PID;
                    dlg.LoginID = LoginID;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        InitTreeView();
                        BindData();
                    }
                }
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                this.menu_EditType.Enabled = false;
            }
            else
            {
                this.menu_EditType.Enabled = true;
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            menu_EditType_Click(null, null);
        }

        private void menu_ClearData_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                string typeId = selectedNode.Tag.ToString();
                int count = BLLFactory<DictData>.Instance.GetDictByTypeID(typeId).Count;

                var format = "您确定要删除节点：{0}，该节点下面有【{1}】项数据";
                format = JsonLanguage.Default.GetString(format);
                string message = string.Format(format, selectedNode.Text, count);

                if (MessageDxUtil.ShowYesNoAndWarning(message) == DialogResult.Yes)
                {
                    try
                    {
                        BLLFactory<DictData>.Instance.DeleteByCondition(string.Format("DictType_ID='{0}'", typeId));
                        InitTreeView();
                        BindData();
                    }
                    catch (Exception ex)
                    {
                        LogTextHelper.Error(ex);
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }
    }

    public class LicenseCheckResult
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        public bool IsValided = false;

        /// <summary>
        /// 注册的用户名称
        /// </summary>
        public string Username = "";

        /// <summary>
        /// 注册的公司名称
        /// </summary>
        public string CompanyName = "";

        /// <summary>
        /// 是否显示授权信息
        /// </summary>
        public bool DisplayCopyright = true;
    }
}
