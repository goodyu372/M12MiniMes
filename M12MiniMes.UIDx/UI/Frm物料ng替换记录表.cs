 		 		 		 		 		 		 		 		 		 		 		 		 		 		 
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
    /// ����ng�滻��¼��
    /// </summary>	
    public partial class Frm����ng�滻��¼�� : BaseDock
    {
        public Frm����ng�滻��¼��()
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
            this.winGridViewPager1.BestFitColumnWith = false;//�Ƿ�����Ϊ�Զ�������ȣ�falseΪ������
			this.winGridViewPager1.gridView1.DataSourceChanged +=new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);

            //�����س������в�ѯ
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
            //    if (status == "�����")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
        	string columnName = e.Column.FieldName;
			
            //����ֶ�Ȩ�޲�������ô�ֶεı�ǩ����Ϊ*��
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
				//    e.DisplayText = string.Format("{0}��", e.Value);
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
        /// �����ݺ󣬷�����еĿ��
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //ͳһ����100���
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //�����������ر�Ŀ��
                GridView gridView = this.winGridViewPager1.gridView1;
                if (gridView != null)
                {
					//Ng�滻��¼id,Ng�滻ʱ��,�����������κ�,�豸id,�豸����,��λ��,����guid,�滻ǰ�ξ�guid,�滻ǰ�ξ�rfid,�滻ǰ�ξ߿׺�,ǰ�ξ��������κ�,�滻���ξ�guid,�滻���ξ�rfid,�滻���ξ߿׺�,���ξ��������κ�
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
        /// ��д��ʼ�������ʵ�֣���������ˢ��
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();
        }
        
        /// <summary>
        /// ��ʼ���ֵ��б�����
        /// </summary>
        private void InitDictItem()
        {
			//��ʼ������
			//this.txtCategory.BindDictItems("��������");
        }
		
        /// <summary>
        /// �������
        /// </summary>		
        private void AddData()
        {
            FrmEdit����ng�滻��¼�� dlg = new FrmEdit����ng�滻��¼��();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.InitFunction(LoginUserInfo, FunctionDict);//���Ӵ��帳ֵ�û�Ȩ����Ϣ
            
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }
        /// <summary>
        /// �༭�б�����
        /// </summary>
        private void EditData()
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("Ng�滻��¼id");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "Ng�滻��¼id");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEdit����ng�滻��¼�� dlg = new FrmEdit����ng�滻��¼��();
                dlg.ID = ID;
                dlg.IDList = IDList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                dlg.InitFunction(LoginUserInfo, FunctionDict);//���Ӵ��帳ֵ�û�Ȩ����Ϣ
                
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }			
		}
		
        /// <summary>
        /// ɾ��ѡ���б�����
        /// </summary>		
        private void DeleteData()
        {
            if (MessageDxUtil.ShowYesNoAndTips("��ȷ��ɾ��ѡ���ļ�¼ô��") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Ng�滻��¼id");
                BLLFactory<����ng�滻��¼��>.Instance.Delete(ID);
            }
             
            BindData();			
		}
		
        /// <summary>
        /// ���б�����
        /// </summary>
        private void BindData()
        {
        	//entity		

            //����ҵ������ȡ��Ӧ����ʾ�ֶΣ����û�����ã���ô����FieldPermit������û�ȡ�ֶ�Ȩ���б�(Ĭ�ϲ�ʹ���ֶ�Ȩ��)
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(����ng�滻��¼��Info).FullName, LoginUserInfo.ID.ToInt32());
            //var displayColumns = BLLFactory<����ng�滻��¼��>.Instance.GetDisplayColumns();
            //displayColumns = string.IsNullOrEmpty(displayColumns) ? string.Join(",", permitDict.Keys) : displayColumns;
            //this.winGridViewPager1.DisplayColumns = displayColumns; 
			
			this.winGridViewPager1.DisplayColumns = "Ng�滻��¼id,Ng�滻ʱ��,�����������κ�,�豸id,�豸����,��λ��,����guid,�滻ǰ�ξ�guid,�滻ǰ�ξ�rfid,�滻ǰ�ξ߿׺�,ǰ�ξ��������κ�,�滻���ξ�guid,�滻���ξ�rfid,�滻���ξ߿׺�,���ξ��������κ�";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<����ng�滻��¼��>.Instance.GetColumnNameAlias();//�ֶ�����ʾ����ת��

            #region ��ӱ�������

           //this.winGridViewPager1.AddColumnAlias("Ng�滻��¼id", "NG�滻��¼ID");
           //this.winGridViewPager1.AddColumnAlias("Ng�滻ʱ��", "NG�滻ʱ��");
           //this.winGridViewPager1.AddColumnAlias("�����������κ�", "�����������κ�");
           //this.winGridViewPager1.AddColumnAlias("�豸id", "�豸ID");
           //this.winGridViewPager1.AddColumnAlias("�豸����", "����ID");
           //this.winGridViewPager1.AddColumnAlias("��λ��", "��λ��");
           //this.winGridViewPager1.AddColumnAlias("����guid", "����GUID");
           //this.winGridViewPager1.AddColumnAlias("�滻ǰ�ξ�guid", "�滻ǰ�ξ�GUID");
           //this.winGridViewPager1.AddColumnAlias("�滻ǰ�ξ�rfid", "�滻ǰ�ξ�RFID");
           //this.winGridViewPager1.AddColumnAlias("�滻ǰ�ξ߿׺�", "�滻ǰ�ξ߿׺�");
           //this.winGridViewPager1.AddColumnAlias("ǰ�ξ��������κ�", "ǰ�ξ��������κ�");
           //this.winGridViewPager1.AddColumnAlias("�滻���ξ�guid", "�滻���ξ�GUID");
           //this.winGridViewPager1.AddColumnAlias("�滻���ξ�rfid", "�滻���ξ�RFID");
           //this.winGridViewPager1.AddColumnAlias("�滻���ξ߿׺�", "�滻���ξ߿׺�");
           //this.winGridViewPager1.AddColumnAlias("���ξ��������κ�", "���ξ��������κ�");

            #endregion

            string where = GetConditionSql();
            PagerInfo pagerInfo = this.winGridViewPager1.PagerInfo;
	            List<����ng�滻��¼��Info> list = BLLFactory<����ng�滻��¼��>.Instance.FindWithPager(where, pagerInfo);
            this.winGridViewPager1.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<����ng�滻��¼��Info>(list);
                this.winGridViewPager1.PrintTitle = "����ng�滻��¼����";
 
			// ����GridControl��Ӧ������������ݣ�����ת��
            SetRepositoryItems(this.winGridViewPager1.GridView1);

            //��ȡ�ֶ���ʾȨ�ޣ�������(Ĭ�ϲ�ʹ���ֶ�Ȩ��)
            //this.winGridViewPager1.gridView1.SetColumnsPermit(permitDict); 
       }
	   
        /// <summary>
        /// ����GridControl��Ӧ������������ݣ�����ת��
        /// </summary>
        private void SetRepositoryItems(GridView gridview)
        {
			/*
            gridview.Columns.ColumnByFieldName("ID").Visible = false;//���ò��ɼ�
            gridview.Columns.ColumnByFieldName("Pallet").CreateCheckEdit();//������ѡ��ؼ�
            gridview.Columns.ColumnByFieldName("TradeMode").CreateLookUpEdit().BindDictItems("ó�׷���");//�����б����ֵ�
			gridview.Columns.ColumnByFieldName("OrganizationCode").CreateTextEdit();//�ı��ؼ�
			gridview.CreateColumn("Remark", "��ע", 300, true).CreateMemoEdit();//���ñ�������
			
			//���ð�ť��ѡ�����
            var deptControl = gridview.Columns.ColumnByFieldName("OuName").CreateButtonEdit(ButtonPredefines.Search);
            deptControl.ButtonClick += (object sender, ButtonPressedEventArgs e) =>
            {
                if (gridview.GetFocusedRow() == null)
                {
                    gridview.AddNewRow();//һ��Ҫ����
                }

                FrmSelectOU dlg = new FrmSelectOU();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    gridview.SetFocusedRowCellValue("OuName", dlg.OuName);
                    gridview.SetFocusedRowCellValue("OuID", dlg.OuID);
                }
            };
			
			//���ÿɱ༭
			gridview.OptionsBehavior.ReadOnly = false;
            gridview.OptionsBehavior.Editable = true;
			*/
        }      

        private string moduleName = "����ng�滻��¼��";		
		
        /// <summary>
        /// ����Ĳ���
        /// </summary>
		private void ImportData()
		{
            string templateFile = string.Format("{0}-ģ��.xls", moduleName);
            FrmImportExcelData dlg = new FrmImportExcelData();
            dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
            dlg.OnDataSave += new FrmImportExcelData.SaveDataHandler(ExcelData_OnDataSave);
            dlg.OnRefreshData += new EventHandler(ExcelData_OnRefreshData);
            dlg.ShowDialog();			
		}
		
        /// <summary>
        /// �����Ĳ���
        /// </summary>		
        private void ExportData()
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<����ng�滻��¼��Info> list = BLLFactory<����ng�滻��¼��>.Instance.Find(where);
                 DataTable dtNew = DataTableHelper.CreateTable("���|int,NG�滻ʱ��,�����������κ�,�豸ID,����ID,��λ��,����GUID,�滻ǰ�ξ�GUID,�滻ǰ�ξ�RFID,�滻ǰ�ξ߿׺�,ǰ�ξ��������κ�,�滻���ξ�GUID,�滻���ξ�RFID,�滻���ξ߿׺�,���ξ��������κ�");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["���"] = j++;
                     dr["NG�滻ʱ��"] = list[i].Ng�滻ʱ��;
                     dr["�����������κ�"] = list[i].�����������κ�;
                     dr["�豸ID"] = list[i].�豸id;
                     dr["����ID"] = list[i].�豸����;
                     dr["��λ��"] = list[i].��λ��;
                     dr["����GUID"] = list[i].����guid;
                     dr["�滻ǰ�ξ�GUID"] = list[i].�滻ǰ�ξ�guid;
                     dr["�滻ǰ�ξ�RFID"] = list[i].�滻ǰ�ξ�rfid;
                     dr["�滻ǰ�ξ߿׺�"] = list[i].�滻ǰ�ξ߿׺�;
                     dr["ǰ�ξ��������κ�"] = list[i].ǰ�ξ��������κ�;
                     dr["�滻���ξ�GUID"] = list[i].�滻���ξ�guid;
                     dr["�滻���ξ�RFID"] = list[i].�滻���ξ�rfid;
                     dr["�滻���ξ߿׺�"] = list[i].�滻���ξ߿׺�;
                     dr["���ξ��������κ�"] = list[i].���ξ��������κ�;
                     dtNew.Rows.Add(dr);
                }

                try
                {
                    string error = "";
                    AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageDxUtil.ShowError(string.Format("����Excel���ִ���{0}", error));
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndTips("�����ɹ����Ƿ���ļ���") == System.Windows.Forms.DialogResult.Yes)
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
        /// �߼���ѯ�Ĳ���
        /// </summary>		
        private void AdvanceSearch()
		{
            if (dlg == null)
            {
                dlg = new FrmAdvanceSearch();
                dlg.FieldTypeTable = BLLFactory<����ng�滻��¼��>.Instance.GetFieldTypeList();
                dlg.ColumnNameAlias = BLLFactory<����ng�滻��¼��>.Instance.GetColumnNameAlias();                
                 dlg.DisplayColumns = "NG�滻ʱ��,�����������κ�,�豸ID,�豸����,��λ��,����GUID,�滻ǰ�ξ�GUID,�滻ǰ�ξ�RFID,�滻ǰ�ξ߿׺�,ǰ�ξ��������κ�,�滻���ξ�GUID,�滻���ξ�RFID,�滻���ξ߿׺�,���ξ��������κ�";

                #region �����б�����

                //dlg.AddColumnListItem("UserType", Portal.gc.GetDictData("��Ա����"));//�ֵ��б�
                //dlg.AddColumnListItem("Sex", "��,Ů");//�̶��б�
                //dlg.AddColumnListItem("Credit", BLLFactory<����ng�滻��¼��>.Instance.GetFieldList("Credit"));//��̬�б�

                #endregion

                dlg.ConditionChanged += new FrmAdvanceSearch.ConditionChangedEventHandler(dlg_ConditionChanged);
            }
            dlg.ShowDialog();			
		}
		
        /// <summary>
        /// ��ҳ�ؼ�ˢ�²���
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ�ɾ������
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
			DeleteData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ��༭�����
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
        /// ��ҳ�ؼ���������
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            AddData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ�ȫ����������ǰ�Ĳ���
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<����ng�滻��¼��>.Instance.FindToDataTable(where);
         }

        /// <summary>
        /// ��ҳ�ؼ���ҳ�Ĳ���
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }        
        
        /// <summary>
        /// ��ѯ���ݲ���
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
        	advanceCondition = null;//�������ò�ѯ������������ܻ�ʹ�ø߼���ѯ������
            BindData();
        }
        
        /// <summary>
        /// �������ݲ���
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
			AddData();
        }
        
        /// <summary>
        /// �ṩ���ؼ��س�ִ�в�ѯ�Ĳ���
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }        
		
        /// <summary>
        /// ����Excel�Ĳ���
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
        /// ����Excel�Ĳ���
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
        /// �߼���ѯ����������
        /// </summary>
        private SearchCondition advanceCondition;
        
        /// <summary>
        /// ���ݲ�ѯ���������ѯ���
        /// </summary> 
        private string GetConditionSql()
        {
            //������ڸ߼���ѯ������Ϣ����ʹ�ø߼���ѯ����������ʹ������������ѯ
            SearchCondition condition = advanceCondition;
            if (condition == null)
            {
                condition = new SearchCondition();
                condition.AddDateCondition("NG�滻ʱ��", this.txtNg�滻ʱ��1, this.txtNg�滻ʱ��2); //��������
                condition.AddCondition("�����������κ�", this.txt�����������κ�.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("�豸ID", this.txt�豸id1, this.txt�豸id2); //��ֵ����
                condition.AddCondition("�豸����", this.txt�豸����.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("��λ��", this.txt��λ��.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("����GUID", this.txt����guid.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("�滻ǰ�ξ�GUID", this.txt�滻ǰ�ξ�guid.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("�滻ǰ�ξ�RFID", this.txt�滻ǰ�ξ�rfid.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("�滻ǰ�ξ߿׺�", this.txt�滻ǰ�ξ߿׺�1, this.txt�滻ǰ�ξ߿׺�2); //��ֵ����
                condition.AddCondition("ǰ�ξ��������κ�", this.txtǰ�ξ��������κ�.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("�滻���ξ�GUID", this.txt�滻���ξ�guid.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("�滻���ξ�RFID", this.txt�滻���ξ�rfid.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("�滻���ξ߿׺�", this.txt�滻���ξ߿׺�1, this.txt�滻���ξ߿׺�2); //��ֵ����
                condition.AddCondition("���ξ��������κ�", this.txt���ξ��������κ�.Text.Trim(), SqlOperator.Like);
            }
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }
		
        /// <summary>
        /// ����ֶδ��ڣ����ȡ��Ӧ��ֵ�����򷵻�Ĭ�Ͽ�
        /// </summary>
        /// <param name="row">DataRow����</param>
        /// <param name="columnName">�ֶ�����</param>
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
            ����ng�滻��¼��Info info = new ����ng�滻��¼��Info();
 
            string Ng�滻ʱ�� = GetRowData(dr, "NG�滻ʱ��");
            if (!string.IsNullOrEmpty(Ng�滻ʱ��))
            {
				converted = DateTime.TryParse(Ng�滻ʱ��, out dt);
                if (converted && dt > dtDefault)
                {
                    info.Ng�滻ʱ�� = dt;
                }
			}
            else
            {
                info.Ng�滻ʱ�� = DateTime.Now;
            }

              info.�����������κ� = GetRowData(dr, "�����������κ�");
              info.�豸id = GetRowData(dr, "�豸ID").ToInt32();
              info.�豸���� = GetRowData(dr, "����ID");
              info.��λ�� = GetRowData(dr, "��λ��");
              info.����guid = GetRowData(dr, "����GUID");
              info.�滻ǰ�ξ�guid = GetRowData(dr, "�滻ǰ�ξ�GUID");
              info.�滻ǰ�ξ�rfid = GetRowData(dr, "�滻ǰ�ξ�RFID");
              info.�滻ǰ�ξ߿׺� = GetRowData(dr, "�滻ǰ�ξ߿׺�").ToInt32();
              info.ǰ�ξ��������κ� = GetRowData(dr, "ǰ�ξ��������κ�");
              info.�滻���ξ�guid = GetRowData(dr, "�滻���ξ�GUID");
              info.�滻���ξ�rfid = GetRowData(dr, "�滻���ξ�RFID");
              info.�滻���ξ߿׺� = GetRowData(dr, "�滻���ξ߿׺�").ToInt32();
              info.���ξ��������κ� = GetRowData(dr, "���ξ��������κ�");
  
            success = BLLFactory<����ng�滻��¼��>.Instance.Insert(info);
             return success;
        }
		
        void dlg_ConditionChanged(SearchCondition condition)
        {
            advanceCondition = condition;
            BindData();
        }
    }
}
