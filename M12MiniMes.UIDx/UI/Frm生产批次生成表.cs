 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 
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
    /// �����������ɱ�
    /// </summary>	
    public partial class Frm�����������ɱ� : BaseDock
    {
        public Frm�����������ɱ�()
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
					//��������id,ʱ��,���,��װ�����,����,��������,��ͲģѨ��,��������,Ѩ��105,Ѩ��104,Ѩ��102,����105,����104,����102,�Ƕ�,ϵ�к�,����Ͷ����,��ȦģѨ��113b,������113b,��ȦģѨ��112,������112,��ȦͶ����,G3���Ϲ�Ӧ��,G3��Ƭ��������,G1���Ϲ�Ӧ��,G1��������,��Ƭ105Ͷ����,��Ƭ104Ͷ����,��Ƭg3Ͷ����,��Ƭ102Ͷ����,��Ƭ95bͶ����,��Լ������,�ƻ�Ͷ����,������,������,״̬,�������κ�
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
            FrmEdit�����������ɱ� dlg = new FrmEdit�����������ɱ�();
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
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("��������id");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "��������id");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEdit�����������ɱ� dlg = new FrmEdit�����������ɱ�();
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
                string str1 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "��Ƭ105Ͷ����");
                int i1 = int.Parse(str1);
                if (i1 > 0 )
                {
                    MessageDxUtil.ShowWarning($@"��Ƭ105��Ͷ��{i1}��������ɾ����");
                    return;
                }
                string str2 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "��Ƭ104Ͷ����");
                int i2 = int.Parse(str2);
                if (i2 > 0)
                {
                    MessageDxUtil.ShowWarning($@"��Ƭ104��Ͷ��{i2}��������ɾ����");
                    return;
                }
                string str3 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "��Ƭg3Ͷ����");
                int i3 = int.Parse(str3);
                if (i3 > 0)
                {
                    MessageDxUtil.ShowWarning($@"��ƬG3��Ͷ��{i3}��������ɾ����");
                    return;
                }
                string str4 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "��Ƭ102Ͷ����");
                int i4 = int.Parse(str4);
                if (i4 > 0)
                {
                    MessageDxUtil.ShowWarning($@"��Ƭ102��Ͷ��{i4}��������ɾ����");
                    return;
                }
                string str5 = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "��Ƭ95bͶ����");
                int i5 = int.Parse(str5);
                if (i5 > 0)
                {
                    MessageDxUtil.ShowWarning($@"��Ƭ95B��Ͷ��{i5}��������ɾ����");
                    return;
                }

                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "��������id");
                BLLFactory<�����������ɱ�>.Instance.Delete(ID);
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
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(�����������ɱ�Info).FullName, LoginUserInfo.ID.ToInt32());
            //var displayColumns = BLLFactory<�����������ɱ�>.Instance.GetDisplayColumns();
            //displayColumns = string.IsNullOrEmpty(displayColumns) ? string.Join(",", permitDict.Keys) : displayColumns;
            //this.winGridViewPager1.DisplayColumns = displayColumns; 
			
			this.winGridViewPager1.DisplayColumns = "��������id,ʱ��,���,��װ�����,����,��������,��ͲģѨ��,��������,Ѩ��105,Ѩ��104,Ѩ��102,����105,����104,����102,�Ƕ�,ϵ�к�,����Ͷ����,��ȦģѨ��113b,������113b,��ȦģѨ��112,������112,��ȦͶ����,G3���Ϲ�Ӧ��,G3��Ƭ��������,G1���Ϲ�Ӧ��,G1��������,��Ƭ105Ͷ����,��Ƭ104Ͷ����,��Ƭg3Ͷ����,��Ƭ102Ͷ����,��Ƭ95bͶ����,��Լ������,�ƻ�Ͷ����,������,������,״̬,�������κ�";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<�����������ɱ�>.Instance.GetColumnNameAlias();//�ֶ�����ʾ����ת��

            #region ��ӱ�������

           //this.winGridViewPager1.AddColumnAlias("��������id", "��������ID");
           //this.winGridViewPager1.AddColumnAlias("ʱ��", "ʱ��");
           //this.winGridViewPager1.AddColumnAlias("���", "���");
           //this.winGridViewPager1.AddColumnAlias("��װ�����", "��װ�����");
           //this.winGridViewPager1.AddColumnAlias("����", "����");
           //this.winGridViewPager1.AddColumnAlias("��������", "��������");
           //this.winGridViewPager1.AddColumnAlias("��ͲģѨ��", "��ͲģѨ��");
           //this.winGridViewPager1.AddColumnAlias("��������", "��������");
           //this.winGridViewPager1.AddColumnAlias("Ѩ��105", "Ѩ��105");
           //this.winGridViewPager1.AddColumnAlias("Ѩ��104", "Ѩ��104");
           //this.winGridViewPager1.AddColumnAlias("Ѩ��102", "Ѩ��102");
           //this.winGridViewPager1.AddColumnAlias("����105", "����105");
           //this.winGridViewPager1.AddColumnAlias("����104", "����104");
           //this.winGridViewPager1.AddColumnAlias("����102", "����102");
           //this.winGridViewPager1.AddColumnAlias("�Ƕ�", "�Ƕ�");
           //this.winGridViewPager1.AddColumnAlias("ϵ�к�", "ϵ�к�");
           //this.winGridViewPager1.AddColumnAlias("����Ͷ����", "����Ͷ����");
           //this.winGridViewPager1.AddColumnAlias("��ȦģѨ��113b", "��ȦģѨ��113B");
           //this.winGridViewPager1.AddColumnAlias("������113b", "������113B");
           //this.winGridViewPager1.AddColumnAlias("��ȦģѨ��112", "��ȦģѨ��112");
           //this.winGridViewPager1.AddColumnAlias("������112", "������112");
           //this.winGridViewPager1.AddColumnAlias("��ȦͶ����", "��ȦͶ����");
           //this.winGridViewPager1.AddColumnAlias("G3���Ϲ�Ӧ��", "G3���Ϲ�Ӧ��");
           //this.winGridViewPager1.AddColumnAlias("G3��Ƭ��������", "G3��Ƭ��������");
           //this.winGridViewPager1.AddColumnAlias("G1���Ϲ�Ӧ��", "G1���Ϲ�Ӧ��");
           //this.winGridViewPager1.AddColumnAlias("G1��������", "G1��������");
           //this.winGridViewPager1.AddColumnAlias("��Ƭ105Ͷ����", "��Ƭ105Ͷ����");
           //this.winGridViewPager1.AddColumnAlias("��Ƭ104Ͷ����", "��Ƭ104Ͷ����");
           //this.winGridViewPager1.AddColumnAlias("��Ƭg3Ͷ����", "��ƬG3Ͷ����");
           //this.winGridViewPager1.AddColumnAlias("��Ƭ102Ͷ����", "��Ƭ102Ͷ����");
           //this.winGridViewPager1.AddColumnAlias("��Ƭ95bͶ����", "��Ƭ95BͶ����");
           //this.winGridViewPager1.AddColumnAlias("��Լ������", "��Լ������");
           //this.winGridViewPager1.AddColumnAlias("�ƻ�Ͷ����", "�ƻ�Ͷ����");
           //this.winGridViewPager1.AddColumnAlias("������", "������");
           //this.winGridViewPager1.AddColumnAlias("������", "������");
           //this.winGridViewPager1.AddColumnAlias("״̬", "״̬");
           //this.winGridViewPager1.AddColumnAlias("�������κ�", "���ɳ����������κ�");

            #endregion

            string where = GetConditionSql();
            PagerInfo pagerInfo = this.winGridViewPager1.PagerInfo;
	            List<�����������ɱ�Info> list = BLLFactory<�����������ɱ�>.Instance.FindWithPager(where, pagerInfo);
            this.winGridViewPager1.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<�����������ɱ�Info>(list);
                this.winGridViewPager1.PrintTitle = "�����������ɱ���";
 
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

        private string moduleName = "�����������ɱ�";		
		
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
                List<�����������ɱ�Info> list = BLLFactory<�����������ɱ�>.Instance.Find(where);
                 DataTable dtNew = DataTableHelper.CreateTable("���|int,ʱ��,���,��װ�����,����,��������,��ͲģѨ��,��������,Ѩ��105,Ѩ��104,Ѩ��102,����105,����104,����102,�Ƕ�,ϵ�к�,����Ͷ����,��ȦģѨ��113B,������113B,��ȦģѨ��112,������112,��ȦͶ����,G3���Ϲ�Ӧ��,G3��Ƭ��������,G1���Ϲ�Ӧ��,G1��������,��Ƭ105Ͷ����,��Ƭ104Ͷ����,��ƬG3Ͷ����,��Ƭ102Ͷ����,��Ƭ95BͶ����,��Լ������,�ƻ�Ͷ����,������,������,״̬,���ɳ����������κ�");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["���"] = j++;
                     dr["ʱ��"] = list[i].ʱ��;
                     dr["���"] = list[i].���;
                     dr["��װ�����"] = list[i].��װ�����;
                     dr["����"] = list[i].����;
                     dr["��������"] = list[i].��������;
                     dr["��ͲģѨ��"] = list[i].��ͲģѨ��;
                     dr["��������"] = list[i].��������;
                     dr["Ѩ��105"] = list[i].Ѩ��105;
                     dr["Ѩ��104"] = list[i].Ѩ��104;
                     dr["Ѩ��102"] = list[i].Ѩ��102;
                     dr["����105"] = list[i].����105;
                     dr["����104"] = list[i].����104;
                     dr["����102"] = list[i].����102;
                     dr["�Ƕ�"] = list[i].�Ƕ�;
                     dr["ϵ�к�"] = list[i].ϵ�к�;
                     dr["����Ͷ����"] = list[i].����Ͷ����;
                     dr["��ȦģѨ��113B"] = list[i].��ȦģѨ��113b;
                     dr["������113B"] = list[i].������113b;
                     dr["��ȦģѨ��112"] = list[i].��ȦģѨ��112;
                     dr["������112"] = list[i].������112;
                     dr["��ȦͶ����"] = list[i].��ȦͶ����;
                     dr["G3���Ϲ�Ӧ��"] = list[i].G3���Ϲ�Ӧ��;
                     dr["G3��Ƭ��������"] = list[i].G3��Ƭ��������;
                     dr["G1���Ϲ�Ӧ��"] = list[i].G1���Ϲ�Ӧ��;
                     dr["G1��������"] = list[i].G1��������;
                     dr["��Ƭ105Ͷ����"] = list[i].��Ƭ105Ͷ����;
                     dr["��Ƭ104Ͷ����"] = list[i].��Ƭ104Ͷ����;
                     dr["��ƬG3Ͷ����"] = list[i].��Ƭg3Ͷ����;
                     dr["��Ƭ102Ͷ����"] = list[i].��Ƭ102Ͷ����;
                     dr["��Ƭ95BͶ����"] = list[i].��Ƭ95bͶ����;
                     dr["��Լ������"] = list[i].��Լ������;
                     dr["�ƻ�Ͷ����"] = list[i].�ƻ�Ͷ����;
                     dr["������"] = list[i].������;
                     dr["������"] = list[i].������;
                     dr["״̬"] = list[i].״̬;
                     dr["���ɳ����������κ�"] = list[i].�������κ�;
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
                dlg.FieldTypeTable = BLLFactory<�����������ɱ�>.Instance.GetFieldTypeList();
                dlg.ColumnNameAlias = BLLFactory<�����������ɱ�>.Instance.GetColumnNameAlias();                
                 dlg.DisplayColumns = "ʱ��,���,��װ�����,����,��������,��ͲģѨ��,��������,Ѩ��105,Ѩ��104,Ѩ��102,����105,����104,����102,�Ƕ�,ϵ�к�,����Ͷ����,��ȦģѨ��113B,������113B,��ȦģѨ��112,������112,��ȦͶ����,G3���Ϲ�Ӧ��,G3��Ƭ��������,G1���Ϲ�Ӧ��,G1��������,��Ƭ105Ͷ����,��Ƭ104Ͷ����,��ƬG3Ͷ����,��Ƭ102Ͷ����,��Ƭ95BͶ����,��Լ������,�ƻ�Ͷ����,������,������,״̬,�������κ�";

                #region �����б�����

                //dlg.AddColumnListItem("UserType", Portal.gc.GetDictData("��Ա����"));//�ֵ��б�
                //dlg.AddColumnListItem("Sex", "��,Ů");//�̶��б�
                //dlg.AddColumnListItem("Credit", BLLFactory<�����������ɱ�>.Instance.GetFieldList("Credit"));//��̬�б�

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
            this.winGridViewPager1.AllToExport = BLLFactory<�����������ɱ�>.Instance.FindToDataTable(where);
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
                condition.AddDateCondition("ʱ��", this.txtʱ��1, this.txtʱ��2); //��������
                condition.AddCondition("���", this.txt���.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("��װ�����", this.txt��װ�����.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("����", this.txt����.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("��������", this.txt��������1, this.txt��������2); //��������
                condition.AddCondition("��ͲģѨ��", this.txt��ͲģѨ��.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("��������", this.txt��������.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Ѩ��105", this.txtѨ��105.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Ѩ��104", this.txtѨ��104.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Ѩ��102", this.txtѨ��102.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("����105", this.txt����1051, this.txt����1052); //��������
                condition.AddDateCondition("����104", this.txt����1041, this.txt����1042); //��������
                condition.AddDateCondition("����102", this.txt����1021, this.txt����1022); //��������
                condition.AddCondition("�Ƕ�", this.txt�Ƕ�.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("ϵ�к�", this.txtϵ�к�.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("����Ͷ����", this.txt����Ͷ����1, this.txt����Ͷ����2); //��ֵ����
                condition.AddCondition("��ȦģѨ��113B", this.txt��ȦģѨ��113b.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("������113B", this.txt������113b1, this.txt������113b2); //��������
                condition.AddCondition("��ȦģѨ��112", this.txt��ȦģѨ��112.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("������112", this.txt������1121, this.txt������1122); //��������
                condition.AddNumericCondition("��ȦͶ����", this.txt��ȦͶ����1, this.txt��ȦͶ����2); //��ֵ����
                condition.AddCondition("G3���Ϲ�Ӧ��", this.txtG3���Ϲ�Ӧ��.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("G3��Ƭ��������", this.txtG3��Ƭ��������1, this.txtG3��Ƭ��������2); //��������
                condition.AddCondition("G1���Ϲ�Ӧ��", this.txtG1���Ϲ�Ӧ��.Text.Trim(), SqlOperator.Like);
                condition.AddDateCondition("G1��������", this.txtG1��������1, this.txtG1��������2); //��������
                condition.AddNumericCondition("��Ƭ105Ͷ����", this.txt��Ƭ105Ͷ����1, this.txt��Ƭ105Ͷ����2); //��ֵ����
                condition.AddNumericCondition("��Ƭ104Ͷ����", this.txt��Ƭ104Ͷ����1, this.txt��Ƭ104Ͷ����2); //��ֵ����
                condition.AddNumericCondition("��ƬG3Ͷ����", this.txt��Ƭg3Ͷ����1, this.txt��Ƭg3Ͷ����2); //��ֵ����
                condition.AddNumericCondition("��Ƭ102Ͷ����", this.txt��Ƭ102Ͷ����1, this.txt��Ƭ102Ͷ����2); //��ֵ����
                condition.AddNumericCondition("��Ƭ95BͶ����", this.txt��Ƭ95bͶ����1, this.txt��Ƭ95bͶ����2); //��ֵ����
                condition.AddCondition("��Լ������", this.txt��Լ������.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("�ƻ�Ͷ����", this.txt�ƻ�Ͷ����1, this.txt�ƻ�Ͷ����2); //��ֵ����
                condition.AddNumericCondition("������", this.txt������1, this.txt������2); //��ֵ����
                condition.AddNumericCondition("������", this.txt������1, this.txt������2); //��ֵ����
                condition.AddCondition("״̬", this.txt״̬.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("�������κ�", this.txt�������κ�.Text.Trim(), SqlOperator.Like);
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
            �����������ɱ�Info info = new �����������ɱ�Info();
 
            string ʱ�� = GetRowData(dr, "ʱ��");
            if (!string.IsNullOrEmpty(ʱ��))
            {
				converted = DateTime.TryParse(ʱ��, out dt);
                if (converted && dt > dtDefault)
                {
                    info.ʱ�� = dt;
                }
			}
            else
            {
                info.ʱ�� = DateTime.Now;
            }

              info.��� = GetRowData(dr, "���");
              info.��װ����� = GetRowData(dr, "��װ�����");
              info.���� = GetRowData(dr, "����");
  
            string �������� = GetRowData(dr, "��������");
            if (!string.IsNullOrEmpty(��������))
            {
				converted = DateTime.TryParse(��������, out dt);
                if (converted && dt > dtDefault)
                {
                    info.�������� = dt;
                }
			}
            else
            {
                info.�������� = DateTime.Now;
            }

              info.��ͲģѨ�� = GetRowData(dr, "��ͲģѨ��");
              info.�������� = GetRowData(dr, "��������");
              info.Ѩ��105 = GetRowData(dr, "Ѩ��105");
              info.Ѩ��104 = GetRowData(dr, "Ѩ��104");
              info.Ѩ��102 = GetRowData(dr, "Ѩ��102");
  
            string ����105 = GetRowData(dr, "����105");
            if (!string.IsNullOrEmpty(����105))
            {
				converted = DateTime.TryParse(����105, out dt);
                if (converted && dt > dtDefault)
                {
                    info.����105 = dt;
                }
			}
            else
            {
                info.����105 = DateTime.Now;
            }

  
            string ����104 = GetRowData(dr, "����104");
            if (!string.IsNullOrEmpty(����104))
            {
				converted = DateTime.TryParse(����104, out dt);
                if (converted && dt > dtDefault)
                {
                    info.����104 = dt;
                }
			}
            else
            {
                info.����104 = DateTime.Now;
            }

  
            string ����102 = GetRowData(dr, "����102");
            if (!string.IsNullOrEmpty(����102))
            {
				converted = DateTime.TryParse(����102, out dt);
                if (converted && dt > dtDefault)
                {
                    info.����102 = dt;
                }
			}
            else
            {
                info.����102 = DateTime.Now;
            }

              info.�Ƕ� = GetRowData(dr, "�Ƕ�");
              info.ϵ�к� = GetRowData(dr, "ϵ�к�");
              info.����Ͷ���� = GetRowData(dr, "����Ͷ����").ToInt32();
              info.��ȦģѨ��113b = GetRowData(dr, "��ȦģѨ��113B");
  
            string ������113b = GetRowData(dr, "������113B");
            if (!string.IsNullOrEmpty(������113b))
            {
				converted = DateTime.TryParse(������113b, out dt);
                if (converted && dt > dtDefault)
                {
                    info.������113b = dt;
                }
			}
            else
            {
                info.������113b = DateTime.Now;
            }

              info.��ȦģѨ��112 = GetRowData(dr, "��ȦģѨ��112");
  
            string ������112 = GetRowData(dr, "������112");
            if (!string.IsNullOrEmpty(������112))
            {
				converted = DateTime.TryParse(������112, out dt);
                if (converted && dt > dtDefault)
                {
                    info.������112 = dt;
                }
			}
            else
            {
                info.������112 = DateTime.Now;
            }

              info.��ȦͶ���� = GetRowData(dr, "��ȦͶ����").ToInt32();
              info.G3���Ϲ�Ӧ�� = GetRowData(dr, "G3���Ϲ�Ӧ��");
  
            string G3��Ƭ�������� = GetRowData(dr, "G3��Ƭ��������");
            if (!string.IsNullOrEmpty(G3��Ƭ��������))
            {
				converted = DateTime.TryParse(G3��Ƭ��������, out dt);
                if (converted && dt > dtDefault)
                {
                    info.G3��Ƭ�������� = dt;
                }
			}
            else
            {
                info.G3��Ƭ�������� = DateTime.Now;
            }

              info.G1���Ϲ�Ӧ�� = GetRowData(dr, "G1���Ϲ�Ӧ��");
  
            string G1�������� = GetRowData(dr, "G1��������");
            if (!string.IsNullOrEmpty(G1��������))
            {
				converted = DateTime.TryParse(G1��������, out dt);
                if (converted && dt > dtDefault)
                {
                    info.G1�������� = dt;
                }
			}
            else
            {
                info.G1�������� = DateTime.Now;
            }

              info.��Ƭ105Ͷ���� = GetRowData(dr, "��Ƭ105Ͷ����").ToInt32();
              info.��Ƭ104Ͷ���� = GetRowData(dr, "��Ƭ104Ͷ����").ToInt32();
              info.��Ƭg3Ͷ���� = GetRowData(dr, "��ƬG3Ͷ����").ToInt32();
              info.��Ƭ102Ͷ���� = GetRowData(dr, "��Ƭ102Ͷ����").ToInt32();
              info.��Ƭ95bͶ���� = GetRowData(dr, "��Ƭ95BͶ����").ToInt32();
              info.��Լ������ = GetRowData(dr, "��Լ������");
              info.�ƻ�Ͷ���� = GetRowData(dr, "�ƻ�Ͷ����").ToInt32();
              info.������ = GetRowData(dr, "������").ToInt32();
              info.������ = GetRowData(dr, "������").ToInt32();
              info.״̬ = GetRowData(dr, "״̬");
              info.�������κ� = GetRowData(dr, "���ɳ����������κ�");
  
            success = BLLFactory<�����������ɱ�>.Instance.Insert(info);
             return success;
        }
		
        void dlg_ConditionChanged(SearchCondition condition)
        {
            advanceCondition = condition;
            BindData();
        }
    }
}
