using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using Faster.Core;
using System.IO;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;


namespace M12MiniMes.UIStart
{
    public partial class FormItemsView : DevExpress.XtraEditors.XtraForm
    {
        public FormItemsView()
        {
            InitializeComponent();

            //每个gridview显示行序号
            this.gridView1.CustomDrawRowIndicator += showRowIndex;
            this.gridView2.CustomDrawRowIndicator += showRowIndex;
            this.gridView3.CustomDrawRowIndicator += showRowIndex;

            //A multithreading issue is detected. The DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection option allows you to disable the exception, but it does not resolve the underlying issue.
            //DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = false;
        }

        private void showRowIndex(object sender, RowIndicatorCustomDrawEventArgs e) 
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void FormItemsView_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = ItemManager.Instance.MachineItems;
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //如果e.Valid等于false就会触发InvalidRowException事件
            
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ErrorText = $@"不允许存在重复名称！只能为字母开头！请检查 (按ESC键可取消编辑并退出)";
        }

        private void gridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int i = this.gridView1.FocusedRowHandle;
            if (i >= 0)
            {
                //如果e.Valid等于false就会触发InvalidRowException事件
                
            }
        }

        private void gridView2_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ErrorText = $@"不允许存在重复名称！只能为字母开头！请检查 (按ESC键可取消编辑并退出)";
        }

        /// <summary>
        /// 展开所有子级列表
        /// </summary>
        public void ExpandAllRow() 
        {
            for (int i = 0; i < this.gridView1.RowCount; i++)
            {
                this.gridView1.ExpandMasterRow(i);
            }
        }

        private void bt同步_Click(object sender, EventArgs e)
        {
            if (MessageService.AskQuestion("只建议在第一次配置好设备表后操作！此步会从数据库设备表中同步信息，且会清空当前内存的所有治具、物料信息！在警告后还要继续操作码？"))
            {
                ItemManager.Instance.GetMachineItems();
                this.gridControl1.DataSource = ItemManager.Instance.MachineItems;
                MessageService.ShowMessage("已从数据库设备表中同步信息，已清空当前内存的所有治具、物料信息！");
            }
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            int i = this.gridView1.FocusedRowHandle;
            if (i >= 0 )
            {
                
            }
        }

        private void bt刷新_Click(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = null;
            this.gridControl1.DataSource = ItemManager.Instance.MachineItems;
            this.ExpandAllRow();
        }
    }
}