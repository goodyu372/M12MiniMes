// Developer Express Code Central Example:
// How to use an unbound check box column to select grid rows
// 
// This example demonstrates how to select grid rows via an additional check box
// column. The example is similar to the one from the
// http://www.devexpress.com/scid=A371 article. The difference is that this example
// employs the grid's build-in multi-selection and keeps it in sync with the check
// boxes. All code related to selection is placed into the GridCheckMarksSelection
// helper class, so it must be easy to port this functionality to your
// application.
// Please note if you use AppearanceEvenRow
// (ms-help://DevExpress.NETv10.2/DevExpress.WindowsForms/DevExpressXtraGridViewsGridGridViewAppearances_EvenRowtopic.htm)
// and AppearanceOddRow
// (ms-help://DevExpress.NETv10.2/DevExpress.WindowsForms/DevExpressXtraGridViewsGridGridViewAppearances_OddRowtopic.htm)
// styles, by default they have a higher priority then RowStyle
// (ms-help://DevExpress.NETv10.2/DevExpress.WindowsForms/DevExpressXtraGridViewsGridGridView_RowStyletopic.htm)
// used for the selection in this example. To avoid incorrect selected rows drawing
// enable the e.HighPriority
// (ms-help://DevExpress.NETv10.2/DevExpress.WindowsForms/DevExpressXtraGridViewsGridRowStyleEventArgs_HighPrioritytopic.htm)
// option. See Also:
// http://www.devexpress.com/scid=E1271
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E990

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace WHC.Pager.WinControl
{
	internal class GridCheckMarksSelection
	{
		protected GridView view;
		protected ArrayList selection;
		private GridColumn column;
		private RepositoryItemCheckEdit edit;

		public GridCheckMarksSelection() : base()
		{
			selection = new ArrayList();
		}

		public GridCheckMarksSelection(GridView view) : this()
		{
			View = view;
		}

		protected virtual void Attach(GridView view)
		{
			if ( view == null ) return;
			selection.Clear();
			this.view = view;
			edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
			edit.EditValueChanged += new EventHandler(edit_EditValueChanged);

			column = view.Columns.Add();
			column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			column.VisibleIndex = int.MaxValue;
			column.FieldName = "CheckMarkSelection";
			column.Caption = "Mark";
			column.OptionsColumn.ShowCaption = false;
			column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
			column.ColumnEdit = edit;

			view.CustomDrawColumnHeader += new ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
			view.CustomDrawGroupRow += new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
			view.CustomUnboundColumnData += new CustomColumnDataEventHandler(view_CustomUnboundColumnData);
			view.RowStyle += new RowStyleEventHandler(view_RowStyle);
			view.MouseDown += new MouseEventHandler(view_MouseDown); // clear selection
		}

		protected virtual void Detach()
		{
			if ( view == null ) return;
			if ( column != null )
				column.Dispose();

			if ( edit != null )
			{
				view.GridControl.RepositoryItems.Remove(edit);
				edit.Dispose();
			}

			view.CustomDrawColumnHeader -= new ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
			view.CustomDrawGroupRow -= new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
			view.CustomUnboundColumnData -= new CustomColumnDataEventHandler(view_CustomUnboundColumnData);
			view.RowStyle -= new RowStyleEventHandler(view_RowStyle);
			view.MouseDown -= new MouseEventHandler(view_MouseDown);
			view = null;
		}

		protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
		{
			DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
			DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
			DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
			info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
			painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
			info.EditValue = Checked;
			info.Bounds = r;
			info.CalcViewInfo(g);
			args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
			painter.Draw(args);
			args.Cache.Dispose();
		}

		private void view_MouseDown(object sender, MouseEventArgs e)
		{
			if ( e.Clicks == 1 && e.Button == MouseButtons.Left )
			{
				GridHitInfo info;
				Point pt = view.GridControl.PointToClient(Control.MousePosition);
				info = view.CalcHitInfo(pt);
				if ( info.InRow && info.Column != column && view.IsDataRow(info.RowHandle) )
				{
					ClearSelection();
					SelectRow(info.RowHandle, true);
				}

				if ( info.InColumn && info.Column == column )
				{
					if ( SelectedCount == view.DataRowCount )
						ClearSelection();
					else
						SelectAll();
				}

				if ( info.InRow && view.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton )
				{
					bool selected = IsGroupRowSelected(info.RowHandle);
					SelectGroup(info.RowHandle, !selected);
				}
			}
		}

		private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
		{
			if ( e.Column == column )
			{
				e.Info.InnerElements.Clear();
				e.Painter.DrawObject(e.Info);
				DrawCheckBox(e.Graphics, e.Bounds, SelectedCount == view.DataRowCount);
				e.Handled = true;
			}
		}

		private void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
		{
			DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
			info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

			info.GroupText = "         " + info.GroupText.TrimStart();
			e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
			e.Painter.DrawObject(e.Info);

			Rectangle r = info.ButtonBounds;
			r.Offset(r.Width * 2, 0);
			DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle));
			e.Handled = true;
		}

		private void view_RowStyle(object sender, RowStyleEventArgs e)
		{
            bool selected = IsRowSelected(e.RowHandle);
            if (selected)
            {
                this.view.SelectRow(e.RowHandle);
            }
            else
            {
                //this.view.UnselectRow(e.RowHandle);
            }
            this.view.RefreshEditor(false);
		}

		public GridView View
		{
			get
			{
				return view;
			}
			set
			{
				if ( view != value )
				{
					Detach();
					Attach(value);
				}
			}
		}

		public GridColumn CheckMarkColumn
		{
			get
			{
				return column;
			}
		}

		public int SelectedCount
		{
			get
			{
				return selection.Count;
			}
		}

		public object GetSelectedRow(int index)
		{
			return selection[index];
		}

		public int GetSelectedIndex(object row)
		{
			return selection.IndexOf(row);
		}

		public void ClearSelection()
		{
			selection.Clear();
			Invalidate();
		}

        private void Invalidate()
        {
            view.CloseEditor();
            view.BeginUpdate();
            view.EndUpdate();
        }

        public void SelectAll()
        {
	        selection.Clear();
            ICollection dataSource = view.DataSource as ICollection;
            if (dataSource != null && dataSource.Count == view.DataRowCount)
            {
                selection.AddRange(dataSource);  // fast
            }
            else
            {
                for (int i = 0; i < view.DataRowCount; i++)  // slow
                {
                    selection.Add(view.GetRow(i));
                }
            }

	        Invalidate();
        }

		public void SelectGroup(int rowHandle, bool select)
		{
			if ( IsGroupRowSelected(rowHandle) && select ) return;
			for ( int i = 0; i < view.GetChildRowCount(rowHandle); i++ )
			{
				int childRowHandle = view.GetChildRowHandle(rowHandle, i);
				if ( view.IsGroupRow(childRowHandle) )
					SelectGroup(childRowHandle, select);
				else
					SelectRow(childRowHandle, select, false);
			}
			Invalidate();
		}

		public void SelectRow(int rowHandle, bool select)
		{
			SelectRow(rowHandle, select, true);
		}

		private void SelectRow(int rowHandle, bool select, bool invalidate)
		{
			if ( IsRowSelected(rowHandle) == select ) return;
			object row = view.GetRow(rowHandle);
			if ( select )
				selection.Add(row);
			else
				selection.Remove(row);

			if ( invalidate )
			{
				Invalidate();
			}
		}

		public bool IsGroupRowSelected(int rowHandle)
		{
			for ( int i = 0; i < view.GetChildRowCount(rowHandle); i++ )
			{
				int row = view.GetChildRowHandle(rowHandle, i);
				if ( view.IsGroupRow(row) )
				{
					if ( !IsGroupRowSelected(row) ) return false;
				} else
					if ( !IsRowSelected(row) ) return false;
			}
			return true;
		}

		public bool IsRowSelected(int rowHandle)
		{
			if ( view.IsGroupRow(rowHandle) )
				return IsGroupRowSelected(rowHandle);

			object row = view.GetRow(rowHandle);
			return GetSelectedIndex(row) != -1;
		}

		private void view_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			if ( e.Column == CheckMarkColumn )
			{
				if ( e.IsGetData )
                    e.Value = IsRowSelected(View.GetRowHandle(e.ListSourceRowIndex));
				else
                    SelectRow(View.GetRowHandle(e.ListSourceRowIndex), (bool)e.Value);
			}
		}

		private void edit_EditValueChanged(object sender, EventArgs e)
		{
			view.PostEditor();
		}
	}
}
