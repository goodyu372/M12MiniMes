 		 		 		 		 		 		 		 		 		 		 		 		 		 		 
using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Dictionary;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using DevExpress.Data;

using M12MiniMes.BLL;
using WHC.Security.BLL;
using M12MiniMes.Entity;

namespace M12MiniMes.UI
{
    /// <summary>
    /// 物料ng替换记录表
    /// </summary>	
    public partial class Frm物料ng替换记录表 : BaseDock
    {
        public Frm物料ng替换记录表()
        {
            InitializeComponent();

            InitDictItem();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
			this.winGridViewPager1.gridView1.DataSourceChanged +=new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }
        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "OrderStatus")
            //{
            //    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
            //    Color color = Color.White;
            //    if (status == "已审核")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
        	string columnName = e.Column.FieldName;
			
            //如果字段权限不够，那么字段的标签设置为*的
            if (string.Concat(e.Column.Tag) != "*")
            {
                if (e.Column.ColumnType == typeof(DateTime))
				{   
					if (e.Value != null)
					{
						if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
						{
							e.DisplayText = "";
						}
						else
						{
							e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
						}
					}
				}
				//else if (columnName == "Age")
				//{
				//    e.DisplayText = string.Format("{0}岁", e.Value);
				//}
				//else if (columnName == "ReceivedMoney")
				//{
				//    if (e.Value != null)
				//    {
				//        e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
				//    }
				//}
			}
        }
        
        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                GridView gridView = this.winGridViewPager1.gridView1;
                if (gridView != null)
                {
					//Ng替换记录id,Ng替换时间,物料生产批次号,设备id,设备名称,工位号,物料guid,替换前治具guid,替换前治具rfid,替换前治具孔号,前治具生产批次号,替换后治具guid,替换后治具rfid,替换后治具孔号,后治具生产批次号
					//gridView.SetGridColumWidth("Note", 200);
                }
            }
        }

        private void SetGridColumWidth(string columnName, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = this.winGridViewPager1.gridView1.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();
        }
        
        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
			//初始化代码
			//this.txtCategory.BindDictItems("报销类型");
        }
		
        /// <summary>
        /// 添加数据
        /// </summary>		
        private void AddData()
        {
            FrmEdit物料ng替换记录表 dlg = new FrmEdit物料ng替换记录表();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息
            
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }
        /// <summary>
        /// 编辑列表数据
        /// </summary>
        private void EditData()
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("Ng替换记录id");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "Ng替换记录id");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEdit物料ng替换记录表 dlg = new FrmEdit物料ng替换记录表();
                dlg.ID = ID;
                dlg.IDList = IDList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息
                
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }			
		}
		
        /// <summary>
        /// 删除选中列表数据
        /// </summary>		
        private void DeleteData()
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Ng替换记录id");
                BLLFactory<物料ng替换记录表>.Instance.Delete(ID);
            }
             
            BindData();			
		}
		
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
        	//entity		

            //根据业务对象获取对应的显示字段，如果没有设置，那么根据FieldPermit表的配置获取字段权限列表(默认不使用字段权限)
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(物料ng替换记录表Info).FullName, LoginUserInfo.ID.ToInt32());
            //var displayColumns = BLLFactory<物料ng替换记录表>.Instance.GetDisplayColumns();
            //displayColumns = string.IsNullOrEmpty(displayColumns) ? string.Join(",", permitDict.Keys) : displayColumns;
            //this.winGridViewPager1.DisplayColumns = displayColumns; 
			
			this.winGridViewPager1.DisplayColumns = "Ng替换记录id,Ng替换时间,物料生产批次号,设备id,设备名称,工位号,物料guid,替换前治具guid,替换前治具rfid,替换前治具孔号,前治具生产批次号,替换后治具guid,替换后治具rfid,替换后治具孔号,后治具生产批次号";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<物料ng替换记录表>.Instance.GetColumnNameAlias();//字段列显示名称转义

            #region 添加别名解析

           //this.winGridViewPager1.AddColumnAlias("Ng替换记录id", "NG替换记录ID");
           //this.winGridViewPager1.AddColumnAlias("Ng替换时间", "NG替换时间");
           //this.winGridViewPager1.AddColumnAlias("物料生产批次号", "物料生产批次号");
           //this.winGridViewPager1.AddColumnAlias("设备id", "设备ID");
           //this.winGridViewPager1.AddColumnAlias("设备名称", "工序ID");
           //this.winGridViewPager1.AddColumnAlias("工位号", "工位号");
           //this.winGridViewPager1.AddColumnAlias("物料guid", "物料GUID");
           //this.winGridViewPager1.AddColumnAlias("替换前治具guid", "替换前治具GUID");
           //this.winGridViewPager1.AddColumnAlias("替换前治具rfid", "替换前治具RFID");
           //this.winGridViewPager1.AddColumnAlias("替换前治具孔号", "替换前治具孔号");
           //this.winGridViewPager1.AddColumnAlias("前治具生产批次号", "前治具生产批次号");
           //this.winGridViewPager1.AddColumnAlias("替换后治具guid", "替换后治具GUID");
           //this.winGridViewPager1.AddColumnAlias("替换后治具rfid", "替换后治具RFID");
           //this.winGridViewPager1.AddColumnAlias("替换后治具孔号", "替换后治具孔号");
           //this.winGridViewPager1.AddColumnAlias("后治具生产批次号", "后治具生产批次号");

            #endregion

            string where = GetConditionSql();
            PagerInfo pagerInfo = this.winGridViewPager1.PagerInfo;
	            List<物料ng替换记录表Info> list = BLLFactory<物料ng替换记录表>.Instance.FindWithPager(where, pagerInfo);
            this.winGridViewPager1.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<物料ng替换记录表Info>(list);
                this.winGridViewPager1.PrintTitle = "物料ng替换记录表报表";
 
			// 设置GridControl对应的下拉类别内容，方便转义
            SetRepositoryItems(this.winGridViewPager1.GridView1);

            //获取字段显示权限，并设置(默认不使用字段权限)
            //this.winGridViewPager1.gridView1.SetColumnsPermit(permitDict); 
       }
	   
        /// <summary>
        /// 设置GridControl对应的下拉类别内容，方便转义
        /// </summary>
        private void SetRepositoryItems(GridView gridview)
        {
			/*
            gridview.Columns.ColumnByFieldName("ID").Visible = false;//设置不可见
            gridview.Columns.ColumnByFieldName("Pallet").CreateCheckEdit();//创建复选框控件
            gridview.Columns.ColumnByFieldName("TradeMode").CreateLookUpEdit().BindDictItems("贸易方向");//创建列表并绑定字典
			gridview.Columns.ColumnByFieldName("OrganizationCode").CreateTextEdit();//文本控件
			gridview.CreateColumn("Remark", "备注", 300, true).CreateMemoEdit();//设置备件内容
			
			//设置按钮可选择机构
            var deptControl = gridview.Columns.ColumnByFieldName("OuName").CreateButtonEdit(ButtonPredefines.Search);
            deptControl.ButtonClick += (object sender, ButtonPressedEventArgs e) =>
            {
                if (gridview.GetFocusedRow() == null)
                {
                    gridview.AddNewRow();//一定要增加
                }

                FrmSelectOU dlg = new FrmSelectOU();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    gridview.SetFocusedRowCellValue("OuName", dlg.OuName);
                    gridview.SetFocusedRowCellValue("OuID", dlg.OuID);
                }
            };
			
			//设置可编辑
			gridview.OptionsBehavior.ReadOnly = false;
            gridview.OptionsBehavior.Editable = true;
			*/
        }      

        private string moduleName = "物料ng替换记录表";		
		
        /// <summary>
        /// 导入的操作
        /// </summary>
		private void ImportData()
		{
            string templateFile = string.Format("{0}-模板.xls", moduleName);
            FrmImportExcelData dlg = new FrmImportExcelData();
            dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
            dlg.OnDataSave += new FrmImportExcelData.SaveDataHandler(ExcelData_OnDataSave);
            dlg.OnRefreshData += new EventHandler(ExcelData_OnRefreshData);
            dlg.ShowDialog();			
		}
		
        /// <summary>
        /// 导出的操作
        /// </summary>		
        private void ExportData()
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<物料ng替换记录表Info> list = BLLFactory<物料ng替换记录表>.Instance.Find(where);
                 DataTable dtNew = DataTableHelper.CreateTable("序号|int,NG替换时间,物料生产批次号,设备ID,工序ID,工位号,物料GUID,替换前治具GUID,替换前治具RFID,替换前治具孔号,前治具生产批次号,替换后治具GUID,替换后治具RFID,替换后治具孔号,后治具生产批次号");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                     dr["NG替换时间"] = list[i].Ng替换时间;
                     dr["物料生产批次号"] = list[i].物料生产批次号;
                     dr["设备ID"] = list[i].设备id;
                     dr["工序ID"] = list[i].设备名称;
                     dr["工位号"] = list[i].工位号;
                     dr["物料GUID"] = list[i].物料guid;
                     dr["替换前治具GUID"] = list[i].替换前治具guid;
                     dr["替换前治具RFID"] = list[i].替换前治具rfid;
                     dr["替换前治具孔号"] = list[i].替换前治具孔号;
                     dr["前治具生产批次号"] = list[i].前治具生产批次号;
                     dr["替换后治具GUID"] = list[i].替换后治具guid;
                     dr["替换后治具RFID"] = list[i].替换后治具rfid;
                     dr["替换后治具孔号"] = list[i].替换后治具孔号;
                     dr["后治具生产批次号"] = list[i].后治具生产批次号;
                     dtNew.Rows.Add(dr);
                }

                try
                {
                    string error = "";
                    AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageDxUtil.ShowError(string.Format("导出Excel出现错误：{0}", error));
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }			
		}

        private FrmAdvanceSearch dlg;		
        /// <summary>
        /// 高级查询的操作
        /// </summary>		
        private void AdvanceSearch()
		{
            if (dlg == null)
            {
                dlg = new FrmAdvanceSearch();
                dlg.FieldTypeTable = BLLFactory<物料ng替换记录表>.Instance.GetFieldTypeList();
                dlg.ColumnNameAlias = BLLFactory<物料ng替换记录表>.Instance.GetColumnNameAlias();                
                 dlg.DisplayColumns = "NG替换时间,物料生产批次号,设备ID,设备名称,工位号,物料GUID,替换前治具GUID,替换前治具RFID,替换前治具孔号,前治具生产批次号,替换后治具GUID,替换后治具RFID,替换后治具孔号,后治具生产批次号";

                #region 下拉列表数据

                //dlg.AddColumnListItem("UserType", Portal.gc.GetDictData("人员类型"));//字典列表
                //dlg.AddColumnListItem("Sex", "男,女");//固定列表
                //dlg.AddColumnListItem("Credit", BLLFactory<物料ng替换记录表>.Instance.GetFieldList("Credit"));//动态列表

                #endregion

                dlg.ConditionChanged += new FrmAdvanceSearch.ConditionChangedEventHandler(dlg_ConditionChanged);
            }
            dlg.ShowDialog();			
		}
		
        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
			DeleteData();
        }
        
        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
			EditData();
        }        
        
        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            AddData();
        }
        
        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<物料ng替换记录表>.Instance.FindToDataTable(where);
         }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }        
        
        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
        	advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
            BindData();
        }
        
        /// <summary>
        /// 新增数据操作
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
			AddData();
        }
        
        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }        
		
        /// <summary>
        /// 导入Excel的操作
        /// </summary>          
        private void btnImport_Click(object sender, EventArgs e)
        {
			ImportData();
        }

        void ExcelData_OnRefreshData(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
			ExportData();
        }
         
        private void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
			AdvanceSearch();
        }
        
        /// <summary>
        /// 高级查询条件语句对象
        /// </summary>
        private SearchCondition advanceCondition;
        
        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = advanceCondition;
            if (condition == null)
            {
                condition = new SearchCondition();
                condition.AddDateCondition("NG替换时间", this.txtNg替换时间1, this.txtNg替换时间2); //日期类型
                condition.AddCondition("物料生产批次号", this.txt物料生产批次号.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("设备ID", this.txt设备id1, this.txt设备id2); //数值类型
                condition.AddCondition("设备名称", this.txt设备名称.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("工位号", this.txt工位号.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("物料GUID", this.txt物料guid.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("替换前治具GUID", this.txt替换前治具guid.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("替换前治具RFID", this.txt替换前治具rfid.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("替换前治具孔号", this.txt替换前治具孔号1, this.txt替换前治具孔号2); //数值类型
                condition.AddCondition("前治具生产批次号", this.txt前治具生产批次号.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("替换后治具GUID", this.txt替换后治具guid.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("替换后治具RFID", this.txt替换后治具rfid.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("替换后治具孔号", this.txt替换后治具孔号1, this.txt替换后治具孔号2); //数值类型
                condition.AddCondition("后治具生产批次号", this.txt后治具生产批次号.Text.Trim(), SqlOperator.Like);
            }
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }
		
        /// <summary>
        /// 如果字段存在，则获取对应的值，否则返回默认空
        /// </summary>
        /// <param name="row">DataRow对象</param>
        /// <param name="columnName">字段列名</param>
        /// <returns></returns>
        private string GetRowData(DataRow row, string columnName)
        {
            string result = "";
            if (row.Table.Columns.Contains(columnName))
            {
                result = row[columnName].ToString();
            }
            return result;
        }
        
        bool ExcelData_OnDataSave(DataRow dr)
        {
            bool success = false;
            bool converted = false;
            DateTime dtDefault = Convert.ToDateTime("1900-01-01");
            DateTime dt;
            物料ng替换记录表Info info = new 物料ng替换记录表Info();
 
            string Ng替换时间 = GetRowData(dr, "NG替换时间");
            if (!string.IsNullOrEmpty(Ng替换时间))
            {
				converted = DateTime.TryParse(Ng替换时间, out dt);
                if (converted && dt > dtDefault)
                {
                    info.Ng替换时间 = dt;
                }
			}
            else
            {
                info.Ng替换时间 = DateTime.Now;
            }

              info.物料生产批次号 = GetRowData(dr, "物料生产批次号");
              info.设备id = GetRowData(dr, "设备ID").ToInt32();
              info.设备名称 = GetRowData(dr, "工序ID");
              info.工位号 = GetRowData(dr, "工位号");
              info.物料guid = GetRowData(dr, "物料GUID");
              info.替换前治具guid = GetRowData(dr, "替换前治具GUID");
              info.替换前治具rfid = GetRowData(dr, "替换前治具RFID");
              info.替换前治具孔号 = GetRowData(dr, "替换前治具孔号").ToInt32();
              info.前治具生产批次号 = GetRowData(dr, "前治具生产批次号");
              info.替换后治具guid = GetRowData(dr, "替换后治具GUID");
              info.替换后治具rfid = GetRowData(dr, "替换后治具RFID");
              info.替换后治具孔号 = GetRowData(dr, "替换后治具孔号").ToInt32();
              info.后治具生产批次号 = GetRowData(dr, "后治具生产批次号");
  
            success = BLLFactory<物料ng替换记录表>.Instance.Insert(info);
             return success;
        }
		
        void dlg_ConditionChanged(SearchCondition condition)
        {
            advanceCondition = condition;
            BindData();
        }
    }
}
