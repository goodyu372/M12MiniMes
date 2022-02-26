 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 
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
    /// 生产批次生成表
    /// </summary>	
    public partial class Frm生产批次生成表 : BaseDock
    {
        public Frm生产批次生成表()
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
					//生产批次id,时间,班次,组装线体号,机种,镜框日期,镜筒模穴号,镜框批次,穴号105,穴号104,穴号102,日期105,日期104,日期102,角度,系列号,镜框投料数,隔圈模穴号113b,成型日113b,隔圈模穴号112,成型日112,隔圈投料数,G3来料供应商,G3镜片来料日期,G1来料供应商,G1来料日期,镜片105投料数,镜片104投料数,镜片g3投料数,镜片102投料数,镜片95b投料数,配对监控批次,计划投入数,上线数,下线数,状态,生产批次号
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
            FrmEdit生产批次生成表 dlg = new FrmEdit生产批次生成表();
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
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("生产批次id");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "生产批次id");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEdit生产批次生成表 dlg = new FrmEdit生产批次生成表();
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
                string str1 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "镜片105投料数");
                int i1 = int.Parse(str1);
                if (i1 > 0 )
                {
                    MessageDxUtil.ShowWarning($@"镜片105已投料{i1}个，不能删除！");
                    return;
                }
                string str2 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "镜片104投料数");
                int i2 = int.Parse(str2);
                if (i2 > 0)
                {
                    MessageDxUtil.ShowWarning($@"镜片104已投料{i2}个，不能删除！");
                    return;
                }
                string str3 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "镜片g3投料数");
                int i3 = int.Parse(str3);
                if (i3 > 0)
                {
                    MessageDxUtil.ShowWarning($@"镜片G3已投料{i3}个，不能删除！");
                    return;
                }
                string str4 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "镜片102投料数");
                int i4 = int.Parse(str4);
                if (i4 > 0)
                {
                    MessageDxUtil.ShowWarning($@"镜片102已投料{i4}个，不能删除！");
                    return;
                }
                string str5 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "镜片95b投料数");
                int i5 = int.Parse(str5);
                if (i5 > 0)
                {
                    MessageDxUtil.ShowWarning($@"镜片95B已投料{i5}个，不能删除！");
                    return;
                }

                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "生产批次id");
                BLLFactory<生产批次生成表>.Instance.Delete(ID);
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
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(生产批次生成表Info).FullName, LoginUserInfo.ID.ToInt32());
            //var displayColumns = BLLFactory<生产批次生成表>.Instance.GetDisplayColumns();
            //displayColumns = string.IsNullOrEmpty(displayColumns) ? string.Join(",", permitDict.Keys) : displayColumns;
            //this.winGridViewPager1.DisplayColumns = displayColumns; 
			
			this.winGridViewPager1.DisplayColumns = "生产批次id,时间,班次,组装线体号,机种,镜框日期,镜筒模穴号,镜框批次,穴号105,穴号104,穴号102,日期105,日期104,日期102,角度,系列号,镜框投料数,隔圈模穴号113b,成型日113b,隔圈模穴号112,成型日112,隔圈投料数,G3来料供应商,G3镜片来料日期,G1来料供应商,G1来料日期,镜片105投料数,镜片104投料数,镜片g3投料数,镜片102投料数,镜片95b投料数,配对监控批次,计划投入数,上线数,下线数,状态,生产批次号";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<生产批次生成表>.Instance.GetColumnNameAlias();//字段列显示名称转义

            #region 添加别名解析

           //this.winGridViewPager1.AddColumnAlias("生产批次id", "生产批次ID");
           //this.winGridViewPager1.AddColumnAlias("时间", "时间");
           //this.winGridViewPager1.AddColumnAlias("班次", "班次");
           //this.winGridViewPager1.AddColumnAlias("组装线体号", "组装线体号");
           //this.winGridViewPager1.AddColumnAlias("机种", "机种");
           //this.winGridViewPager1.AddColumnAlias("镜框日期", "镜框日期");
           //this.winGridViewPager1.AddColumnAlias("镜筒模穴号", "镜筒模穴号");
           //this.winGridViewPager1.AddColumnAlias("镜框批次", "镜框批次");
           //this.winGridViewPager1.AddColumnAlias("穴号105", "穴号105");
           //this.winGridViewPager1.AddColumnAlias("穴号104", "穴号104");
           //this.winGridViewPager1.AddColumnAlias("穴号102", "穴号102");
           //this.winGridViewPager1.AddColumnAlias("日期105", "日期105");
           //this.winGridViewPager1.AddColumnAlias("日期104", "日期104");
           //this.winGridViewPager1.AddColumnAlias("日期102", "日期102");
           //this.winGridViewPager1.AddColumnAlias("角度", "角度");
           //this.winGridViewPager1.AddColumnAlias("系列号", "系列号");
           //this.winGridViewPager1.AddColumnAlias("镜框投料数", "镜框投料数");
           //this.winGridViewPager1.AddColumnAlias("隔圈模穴号113b", "隔圈模穴号113B");
           //this.winGridViewPager1.AddColumnAlias("成型日113b", "成型日113B");
           //this.winGridViewPager1.AddColumnAlias("隔圈模穴号112", "隔圈模穴号112");
           //this.winGridViewPager1.AddColumnAlias("成型日112", "成型日112");
           //this.winGridViewPager1.AddColumnAlias("隔圈投料数", "隔圈投料数");
           //this.winGridViewPager1.AddColumnAlias("G3来料供应商", "G3来料供应商");
           //this.winGridViewPager1.AddColumnAlias("G3镜片来料日期", "G3镜片来料日期");
           //this.winGridViewPager1.AddColumnAlias("G1来料供应商", "G1来料供应商");
           //this.winGridViewPager1.AddColumnAlias("G1来料日期", "G1来料日期");
           //this.winGridViewPager1.AddColumnAlias("镜片105投料数", "镜片105投料数");
           //this.winGridViewPager1.AddColumnAlias("镜片104投料数", "镜片104投料数");
           //this.winGridViewPager1.AddColumnAlias("镜片g3投料数", "镜片G3投料数");
           //this.winGridViewPager1.AddColumnAlias("镜片102投料数", "镜片102投料数");
           //this.winGridViewPager1.AddColumnAlias("镜片95b投料数", "镜片95B投料数");
           //this.winGridViewPager1.AddColumnAlias("配对监控批次", "配对监控批次");
           //this.winGridViewPager1.AddColumnAlias("计划投入数", "计划投入数");
           //this.winGridViewPager1.AddColumnAlias("上线数", "上线数");
           //this.winGridViewPager1.AddColumnAlias("下线数", "下线数");
           //this.winGridViewPager1.AddColumnAlias("状态", "状态");
           //this.winGridViewPager1.AddColumnAlias("生产批次号", "生成出的生产批次号");

            #endregion

            string where = GetConditionSql();
            PagerInfo pagerInfo = this.winGridViewPager1.PagerInfo;
	            List<生产批次生成表Info> list = BLLFactory<生产批次生成表>.Instance.FindWithPager(where, pagerInfo);
            this.winGridViewPager1.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<生产批次生成表Info>(list);
                this.winGridViewPager1.PrintTitle = "生产批次生成表报表";
 
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

        private string moduleName = "生产批次生成表";		
		
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
                List<生产批次生成表Info> list = BLLFactory<生产批次生成表>.Instance.Find(where);
                 DataTable dtNew = DataTableHelper.CreateTable("序号|int,时间,班次,组装线体号,机种,镜框日期,镜筒模穴号,镜框批次,穴号105,穴号104,穴号102,日期105,日期104,日期102,角度,系列号,镜框投料数,隔圈模穴号113B,成型日113B,隔圈模穴号112,成型日112,隔圈投料数,G3来料供应商,G3镜片来料日期,G1来料供应商,G1来料日期,镜片105投料数,镜片104投料数,镜片G3投料数,镜片102投料数,镜片95B投料数,配对监控批次,计划投入数,上线数,下线数,状态,生成出的生产批次号");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                     dr["时间"] = list[i].时间;
                     dr["班次"] = list[i].班次;
                     dr["组装线体号"] = list[i].组装线体号;
                     dr["机种"] = list[i].机种;
                     dr["镜框日期"] = list[i].镜框日期;
                     dr["镜筒模穴号"] = list[i].镜筒模穴号;
                     dr["镜框批次"] = list[i].镜框批次;
                     dr["穴号105"] = list[i].穴号105;
                     dr["穴号104"] = list[i].穴号104;
                     dr["穴号102"] = list[i].穴号102;
                     dr["日期105"] = list[i].日期105;
                     dr["日期104"] = list[i].日期104;
                     dr["日期102"] = list[i].日期102;
                     dr["角度"] = list[i].角度;
                     dr["系列号"] = list[i].系列号;
                     dr["镜框投料数"] = list[i].镜框投料数;
                     dr["隔圈模穴号113B"] = list[i].隔圈模穴号113b;
                     dr["成型日113B"] = list[i].成型日113b;
                     dr["隔圈模穴号112"] = list[i].隔圈模穴号112;
                     dr["成型日112"] = list[i].成型日112;
                     dr["隔圈投料数"] = list[i].隔圈投料数;
                     dr["G3来料供应商"] = list[i].G3来料供应商;
                     dr["G3镜片来料日期"] = list[i].G3镜片来料日期;
                     dr["G1来料供应商"] = list[i].G1来料供应商;
                     dr["G1来料日期"] = list[i].G1来料日期;
                     dr["镜片105投料数"] = list[i].镜片105投料数;
                     dr["镜片104投料数"] = list[i].镜片104投料数;
                     dr["镜片G3投料数"] = list[i].镜片g3投料数;
                     dr["镜片102投料数"] = list[i].镜片102投料数;
                     dr["镜片95B投料数"] = list[i].镜片95b投料数;
                     dr["配对监控批次"] = list[i].配对监控批次;
                     dr["计划投入数"] = list[i].计划投入数;
                     dr["上线数"] = list[i].上线数;
                     dr["下线数"] = list[i].下线数;
                     dr["状态"] = list[i].状态;
                     dr["生成出的生产批次号"] = list[i].生产批次号;
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
                dlg.FieldTypeTable = BLLFactory<生产批次生成表>.Instance.GetFieldTypeList();
                dlg.ColumnNameAlias = BLLFactory<生产批次生成表>.Instance.GetColumnNameAlias();                
                 dlg.DisplayColumns = "时间,班次,组装线体号,机种,镜框日期,镜筒模穴号,镜框批次,穴号105,穴号104,穴号102,日期105,日期104,日期102,角度,系列号,镜框投料数,隔圈模穴号113B,成型日113B,隔圈模穴号112,成型日112,隔圈投料数,G3来料供应商,G3镜片来料日期,G1来料供应商,G1来料日期,镜片105投料数,镜片104投料数,镜片G3投料数,镜片102投料数,镜片95B投料数,配对监控批次,计划投入数,上线数,下线数,状态,生产批次号";

                #region 下拉列表数据

                //dlg.AddColumnListItem("UserType", Portal.gc.GetDictData("人员类型"));//字典列表
                //dlg.AddColumnListItem("Sex", "男,女");//固定列表
                //dlg.AddColumnListItem("Credit", BLLFactory<生产批次生成表>.Instance.GetFieldList("Credit"));//动态列表

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
            this.winGridViewPager1.AllToExport = BLLFactory<生产批次生成表>.Instance.FindToDataTable(where);
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
                condition.AddDateCondition("时间", this.txt时间1, this.txt时间2); //日期类型
                condition.AddCondition("班次", this.txt班次.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("组装线体号", this.txt组装线体号.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("机种", this.txt机种.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("镜框日期", this.txt镜框日期1, this.txt镜框日期2); //日期类型
                condition.AddCondition("镜筒模穴号", this.txt镜筒模穴号.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("镜框批次", this.txt镜框批次.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("穴号105", this.txt穴号105.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("穴号104", this.txt穴号104.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("穴号102", this.txt穴号102.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("日期105", this.txt日期1051, this.txt日期1052); //日期类型
                condition.AddDateCondition("日期104", this.txt日期1041, this.txt日期1042); //日期类型
                condition.AddDateCondition("日期102", this.txt日期1021, this.txt日期1022); //日期类型
                condition.AddCondition("角度", this.txt角度.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("系列号", this.txt系列号.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("镜框投料数", this.txt镜框投料数1, this.txt镜框投料数2); //数值类型
                condition.AddCondition("隔圈模穴号113B", this.txt隔圈模穴号113b.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("成型日113B", this.txt成型日113b1, this.txt成型日113b2); //日期类型
                condition.AddCondition("隔圈模穴号112", this.txt隔圈模穴号112.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("成型日112", this.txt成型日1121, this.txt成型日1122); //日期类型
                condition.AddNumericCondition("隔圈投料数", this.txt隔圈投料数1, this.txt隔圈投料数2); //数值类型
                condition.AddCondition("G3来料供应商", this.txtG3来料供应商.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("G3镜片来料日期", this.txtG3镜片来料日期1, this.txtG3镜片来料日期2); //日期类型
                condition.AddCondition("G1来料供应商", this.txtG1来料供应商.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("G1来料日期", this.txtG1来料日期1, this.txtG1来料日期2); //日期类型
                condition.AddNumericCondition("镜片105投料数", this.txt镜片105投料数1, this.txt镜片105投料数2); //数值类型
                condition.AddNumericCondition("镜片104投料数", this.txt镜片104投料数1, this.txt镜片104投料数2); //数值类型
                condition.AddNumericCondition("镜片G3投料数", this.txt镜片g3投料数1, this.txt镜片g3投料数2); //数值类型
                condition.AddNumericCondition("镜片102投料数", this.txt镜片102投料数1, this.txt镜片102投料数2); //数值类型
                condition.AddNumericCondition("镜片95B投料数", this.txt镜片95b投料数1, this.txt镜片95b投料数2); //数值类型
                condition.AddCondition("配对监控批次", this.txt配对监控批次.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("计划投入数", this.txt计划投入数1, this.txt计划投入数2); //数值类型
                condition.AddNumericCondition("上线数", this.txt上线数1, this.txt上线数2); //数值类型
                condition.AddNumericCondition("下线数", this.txt下线数1, this.txt下线数2); //数值类型
                condition.AddCondition("状态", this.txt状态.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("生产批次号", this.txt生产批次号.Text.Trim(), SqlOperator.Like);
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
            生产批次生成表Info info = new 生产批次生成表Info();
 
            string 时间 = GetRowData(dr, "时间");
            if (!string.IsNullOrEmpty(时间))
            {
				converted = DateTime.TryParse(时间, out dt);
                if (converted && dt > dtDefault)
                {
                    info.时间 = dt;
                }
			}
            else
            {
                info.时间 = DateTime.Now;
            }

              info.班次 = GetRowData(dr, "班次");
              info.组装线体号 = GetRowData(dr, "组装线体号");
              info.机种 = GetRowData(dr, "机种");
  
            string 镜框日期 = GetRowData(dr, "镜框日期");
            if (!string.IsNullOrEmpty(镜框日期))
            {
				converted = DateTime.TryParse(镜框日期, out dt);
                if (converted && dt > dtDefault)
                {
                    info.镜框日期 = dt;
                }
			}
            else
            {
                info.镜框日期 = DateTime.Now;
            }

              info.镜筒模穴号 = GetRowData(dr, "镜筒模穴号");
              info.镜框批次 = GetRowData(dr, "镜框批次");
              info.穴号105 = GetRowData(dr, "穴号105");
              info.穴号104 = GetRowData(dr, "穴号104");
              info.穴号102 = GetRowData(dr, "穴号102");
  
            string 日期105 = GetRowData(dr, "日期105");
            if (!string.IsNullOrEmpty(日期105))
            {
				converted = DateTime.TryParse(日期105, out dt);
                if (converted && dt > dtDefault)
                {
                    info.日期105 = dt;
                }
			}
            else
            {
                info.日期105 = DateTime.Now;
            }

  
            string 日期104 = GetRowData(dr, "日期104");
            if (!string.IsNullOrEmpty(日期104))
            {
				converted = DateTime.TryParse(日期104, out dt);
                if (converted && dt > dtDefault)
                {
                    info.日期104 = dt;
                }
			}
            else
            {
                info.日期104 = DateTime.Now;
            }

  
            string 日期102 = GetRowData(dr, "日期102");
            if (!string.IsNullOrEmpty(日期102))
            {
				converted = DateTime.TryParse(日期102, out dt);
                if (converted && dt > dtDefault)
                {
                    info.日期102 = dt;
                }
			}
            else
            {
                info.日期102 = DateTime.Now;
            }

              info.角度 = GetRowData(dr, "角度");
              info.系列号 = GetRowData(dr, "系列号");
              info.镜框投料数 = GetRowData(dr, "镜框投料数").ToInt32();
              info.隔圈模穴号113b = GetRowData(dr, "隔圈模穴号113B");
  
            string 成型日113b = GetRowData(dr, "成型日113B");
            if (!string.IsNullOrEmpty(成型日113b))
            {
				converted = DateTime.TryParse(成型日113b, out dt);
                if (converted && dt > dtDefault)
                {
                    info.成型日113b = dt;
                }
			}
            else
            {
                info.成型日113b = DateTime.Now;
            }

              info.隔圈模穴号112 = GetRowData(dr, "隔圈模穴号112");
  
            string 成型日112 = GetRowData(dr, "成型日112");
            if (!string.IsNullOrEmpty(成型日112))
            {
				converted = DateTime.TryParse(成型日112, out dt);
                if (converted && dt > dtDefault)
                {
                    info.成型日112 = dt;
                }
			}
            else
            {
                info.成型日112 = DateTime.Now;
            }

              info.隔圈投料数 = GetRowData(dr, "隔圈投料数").ToInt32();
              info.G3来料供应商 = GetRowData(dr, "G3来料供应商");
  
            string G3镜片来料日期 = GetRowData(dr, "G3镜片来料日期");
            if (!string.IsNullOrEmpty(G3镜片来料日期))
            {
				converted = DateTime.TryParse(G3镜片来料日期, out dt);
                if (converted && dt > dtDefault)
                {
                    info.G3镜片来料日期 = dt;
                }
			}
            else
            {
                info.G3镜片来料日期 = DateTime.Now;
            }

              info.G1来料供应商 = GetRowData(dr, "G1来料供应商");
  
            string G1来料日期 = GetRowData(dr, "G1来料日期");
            if (!string.IsNullOrEmpty(G1来料日期))
            {
				converted = DateTime.TryParse(G1来料日期, out dt);
                if (converted && dt > dtDefault)
                {
                    info.G1来料日期 = dt;
                }
			}
            else
            {
                info.G1来料日期 = DateTime.Now;
            }

              info.镜片105投料数 = GetRowData(dr, "镜片105投料数").ToInt32();
              info.镜片104投料数 = GetRowData(dr, "镜片104投料数").ToInt32();
              info.镜片g3投料数 = GetRowData(dr, "镜片G3投料数").ToInt32();
              info.镜片102投料数 = GetRowData(dr, "镜片102投料数").ToInt32();
              info.镜片95b投料数 = GetRowData(dr, "镜片95B投料数").ToInt32();
              info.配对监控批次 = GetRowData(dr, "配对监控批次");
              info.计划投入数 = GetRowData(dr, "计划投入数").ToInt32();
              info.上线数 = GetRowData(dr, "上线数").ToInt32();
              info.下线数 = GetRowData(dr, "下线数").ToInt32();
              info.状态 = GetRowData(dr, "状态");
              info.生产批次号 = GetRowData(dr, "生成出的生产批次号");
  
            success = BLLFactory<生产批次生成表>.Instance.Insert(info);
             return success;
        }
		
        void dlg_ConditionChanged(SearchCondition condition)
        {
            advanceCondition = condition;
            BindData();
        }
    }
}
