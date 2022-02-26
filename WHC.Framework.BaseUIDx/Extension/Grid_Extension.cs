using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.Language;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// GridView及其RepositoryItem编辑控件的扩展类
    /// </summary>
	public static class Grid_Extension
	{
        #region 创建列编辑控件

        /// <summary>
        /// 创建GridView的列
        /// </summary>
        public static GridColumn CreateColumn(this GridView gridView, string fieldName, string caption, int width = 80, bool allowEdit = true,
            UnboundColumnType unboundColumnType = UnboundColumnType.Bound, DefaultBoolean allowMerge = DefaultBoolean.False,
            FixedStyle fixedStyle = FixedStyle.None)
        {
            //使用多语言处理标题
            caption = JsonLanguage.Default.GetString(caption);

            GridColumn gridColumn = new GridColumn
            {
                FieldName = fieldName,
                Caption = caption,
                Width = width,
                UnboundType = unboundColumnType
            };

            gridView.Columns.Add(gridColumn);
            gridColumn.AbsoluteIndex = gridView.Columns.Count;
            gridColumn.VisibleIndex = gridView.Columns.Count;
            gridColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            gridColumn.OptionsColumn.AllowEdit = allowEdit;

            if (!allowEdit)
            {
                gridColumn.AppearanceHeader.ForeColor = Color.Gray;
            }

            bool allowCellMerge = !gridView.OptionsView.AllowCellMerge && allowMerge == DefaultBoolean.True;
            if (allowCellMerge)
            {
                gridView.OptionsView.AllowCellMerge = true;
            }
            gridColumn.OptionsColumn.AllowMerge = allowMerge;
            gridColumn.Fixed = fixedStyle;

            bool isTime = caption.Contains("时间");
            if (isTime)
            {
                gridColumn.DisplayFormat.FormatType = FormatType.DateTime;
                gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            }
            else
            {
                bool isDate = caption.Contains("日期");
                if (isDate)
                {
                    gridColumn.DisplayFormat.FormatType = FormatType.DateTime;
                    gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                }
                else
                {
                    bool isPercent = caption.Contains("百分比") || caption.Contains("率");
                    if (isPercent)
                    {
                        gridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                        gridColumn.DisplayFormat.FormatString = "P";
                    }
                }
            }
            return gridColumn;
        }

        /// <summary>
        /// 创建GridView的列编辑为LookUpEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemLookUpEdit CreateLookUpEdit(this GridColumn gridColumn)
        {
            RepositoryItemLookUpEdit repositoryItem = new RepositoryItemLookUpEdit
            {
                AutoHeight = false,
                NullText = ""
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为SearchLookUpEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemSearchLookUpEdit CreateSearchLookUpEdit(this GridColumn gridColumn)
        {
            RepositoryItemSearchLookUpEdit repositoryItem = new RepositoryItemSearchLookUpEdit
            {
                AutoHeight = false,
                NullText = ""
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为GridLookUpEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemGridLookUpEdit CreateGridLookUpEdit(this GridColumn gridColumn)
        {
            RepositoryItemGridLookUpEdit repositoryItem = new RepositoryItemGridLookUpEdit
            {
                AutoHeight = false
            };
            GridView repositoryItemGridLookUpEditView = new GridView
            {
                FocusRectStyle = DrawFocusRectStyle.RowFocus
            };
            repositoryItemGridLookUpEditView.OptionsSelection.EnableAppearanceFocusedCell = false;
            repositoryItemGridLookUpEditView.OptionsView.ShowGroupPanel = false;
            repositoryItem.View = repositoryItemGridLookUpEditView;
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }


        /// <summary>
        /// 创建GridView的列编辑为ComboBox
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemComboBox CreateComboBox(this GridColumn gridColumn)
        {
            RepositoryItemComboBox repositoryItem = new RepositoryItemComboBox
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为ImageComboBox
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemImageComboBox CreateImageComboBox(this GridColumn gridColumn)
        {
            RepositoryItemImageComboBox repositoryItem = new RepositoryItemImageComboBox
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为CheckedComboBoxEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemCheckedComboBoxEdit CreateCheckedComboBoxEdit(this GridColumn gridColumn)
        {
            RepositoryItemCheckedComboBoxEdit repositoryItem = new RepositoryItemCheckedComboBoxEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为PopupContainerEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <param name="popupContainerControl">弹出的容器控件</param>
        /// <returns></returns>
        public static RepositoryItemPopupContainerEdit CreatePopupContainerEdit(this GridColumn gridColumn, PopupContainerControl popupContainerControl)
        {
            RepositoryItemPopupContainerEdit repositoryItem = new RepositoryItemPopupContainerEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            repositoryItem.CloseUpKey = new KeyShortcut(Keys.Space);
            repositoryItem.PopupControl = popupContainerControl;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为PopupContainerEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemPopupContainerEdit CreatePopupContainerEdit(this GridColumn gridColumn)
        {
            RepositoryItemPopupContainerEdit repositoryItem = new RepositoryItemPopupContainerEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            repositoryItem.CloseUpKey = new KeyShortcut(Keys.Space);
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为TextEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemTextEdit CreateTextEdit(this GridColumn gridColumn)
        {
            RepositoryItemTextEdit repositoryItem = new RepositoryItemTextEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为SpinEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemSpinEdit CreateSpinEdit(this GridColumn gridColumn)
        {
            RepositoryItemSpinEdit repositoryItem = new RepositoryItemSpinEdit
            {
                Increment = decimal.One,
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为CheckEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemCheckEdit CreateCheckEdit(this GridColumn gridColumn)
        {
            RepositoryItemCheckEdit repositoryItem = new RepositoryItemCheckEdit
            {
                AutoHeight = false,
                ValueChecked = 1,
                ValueUnchecked = 0
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为DateEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemDateEdit CreateDateEdit(this GridColumn gridColumn)
        {
            RepositoryItemDateEdit repositoryItem = new RepositoryItemDateEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为TimeEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemTimeEdit CreateTimeEdit(this GridColumn gridColumn)
        {
            RepositoryItemTimeEdit repositoryItem = new RepositoryItemTimeEdit
            {
                AutoHeight = false,
                EditMask = "yyyy-MM-dd HH:mm:ss"
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为MemoEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemMemoEdit CreateMemoEdit(this GridColumn gridColumn)
        {
            RepositoryItemMemoEdit repositoryItem = new RepositoryItemMemoEdit
            {
                AutoHeight = false,
                LinesCount = 0
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为MemoExEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemMemoExEdit CreateMemoExEdit(this GridColumn gridColumn)
        {
            RepositoryItemMemoExEdit repositoryItem = new RepositoryItemMemoExEdit
            {
                AutoHeight = false,
                ShowIcon = false,
                PopupFormSize = new Size(400, 200)
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为ButtonEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemButtonEdit CreateButtonEdit(this GridColumn gridColumn, ButtonPredefines buttonPredefines = ButtonPredefines.Search)
        {
            RepositoryItemButtonEdit repositoryItem = new RepositoryItemButtonEdit
            {
                AutoHeight = false
            };
            repositoryItem.Buttons[0].Kind = buttonPredefines;
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            repositoryItem.Buttons[0].Tag = gridColumn;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为MRUEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemMRUEdit CreateMRUEdit(this GridColumn gridColumn)
        {
            RepositoryItemMRUEdit repositoryItem = new RepositoryItemMRUEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为PictureEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemPictureEdit CreatePictureEdit(this GridColumn gridColumn)
        {
            RepositoryItemPictureEdit repositoryItem = new RepositoryItemPictureEdit
            {
                SizeMode = PictureSizeMode.Zoom,
                PictureInterpolationMode = InterpolationMode.High,
                NullText = " "
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为RadioGroup
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemRadioGroup CreateRadioGroup(this GridColumn gridColumn)
        {
            RepositoryItemRadioGroup repositoryItem = new RepositoryItemRadioGroup();
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为HyperLinkEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemHyperLinkEdit CreateHyperLinkEdit(this GridColumn gridColumn)
        {
            RepositoryItemHyperLinkEdit repositoryItem = new RepositoryItemHyperLinkEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为ImageEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemImageEdit CreateImageEdit(this GridColumn gridColumn)
        {
            RepositoryItemImageEdit repositoryItem = new RepositoryItemImageEdit
            {
                AutoHeight = false,
                SizeMode = PictureSizeMode.Zoom
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为CalcEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemCalcEdit CreateCalcEdit(this GridColumn gridColumn)
        {
            RepositoryItemCalcEdit repositoryItem = new RepositoryItemCalcEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为ColorEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemColorEdit CreateColorEdit(this GridColumn gridColumn)
        {
            RepositoryItemColorEdit repositoryItem = new RepositoryItemColorEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        }

        /// <summary>
        /// 创建GridView的列编辑为FontEdit
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <returns></returns>
        public static RepositoryItemFontEdit CreateFontEdit(this GridColumn gridColumn)
        {
            RepositoryItemFontEdit repositoryItem = new RepositoryItemFontEdit
            {
                AutoHeight = false
            };
            gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
            gridColumn.ColumnEdit = repositoryItem;
            return repositoryItem;
        } 
        
        #endregion

        /// <summary>
        /// 初始化GridView对象
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="gridType">Grid类型，默认为可添加新项目</param>
        /// <param name="checkBoxSelect">是否出现勾选列</param>
        /// <param name="editorShowMode">编辑器显示模式</param>
        /// <param name="viewCaption">GridView标题</param>
		public static void InitGridView(this GridView gridView, GridType gridType = GridType.NewItem, bool checkBoxSelect = false, 
            EditorShowMode editorShowMode = EditorShowMode.MouseDownFocused, string viewCaption = "")
		{
			gridView.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
			gridView.OptionsDetail.AllowExpandEmptyDetails = true;
			gridView.OptionsDetail.SmartDetailExpandButtonMode = DetailExpandButtonMode.AlwaysEnabled;
			gridView.OptionsNavigation.AutoFocusNewRow = true;
			gridView.OptionsNavigation.EnterMoveNextColumn = true;
			gridView.OptionsView.ShowViewCaption = false;
			gridView.OptionsView.ColumnAutoWidth = false;
			gridView.OptionsView.RowAutoHeight = true;
			gridView.OptionsSelection.MultiSelect = true;
			gridView.OptionsBehavior.EditorShowMode = editorShowMode;
			gridView.OptionsView.EnableAppearanceOddRow = true;
			gridView.OptionsView.EnableAppearanceEvenRow = true;
			gridView.Appearance.OddRow.BackColor = Color.Transparent;
			gridView.Appearance.OddRow.BorderColor = Color.FromArgb(199, 209, 228);
			gridView.Appearance.EvenRow.BackColor = Color.FromArgb(239, 243, 250);
			gridView.Appearance.EvenRow.BorderColor = Color.FromArgb(199, 209, 228);

            if (gridType == GridType.NewItem)
			{
				gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
			}
            else if (gridType == GridType.ReadOnly)
            {
                gridView.OptionsBehavior.Editable = false;
            }

			if (checkBoxSelect)
			{
				gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
				gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
				gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DefaultBoolean.True;
				gridView.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DefaultBoolean.True;
			}

			if (!string.IsNullOrEmpty(viewCaption))
			{
                //多语言支持
                viewCaption = JsonLanguage.Default.GetString(viewCaption);

				gridView.ViewCaption = viewCaption;
				gridView.OptionsView.ShowViewCaption = true;
			}

            gridView.CustomColumnDisplayText += gridView_CustomColumnDisplayText;
		}

        static void gridView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.ColumnType == typeof(DateTime))
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");                        
                    }
                }
            }
        }

        /// <summary>
        /// 设置统计列内容
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="fieldName">统计字段</param>
        /// <param name="summaryItemType">统计类型</param>
        /// <param name="prefix">显示前缀</param>
        public static void SetSummaryColumn(this GridView gridView, string fieldName, SummaryItemType summaryItemType = SummaryItemType.Sum,
            string prefix = "")
        {
            if (!gridView.OptionsView.ShowFooter)
            {
                gridView.OptionsView.ShowFooter = true;
            }

            string upperFieldName = fieldName;
            gridView.Columns[upperFieldName].SummaryItem.FieldName = upperFieldName;
            gridView.Columns[upperFieldName].SummaryItem.DisplayFormat = gridView.Columns[upperFieldName].DisplayFormat.FormatString;
            gridView.Columns[upperFieldName].SummaryItem.SummaryType = summaryItemType;
            gridView.Columns[upperFieldName].SummaryItem.DisplayFormat = prefix + "{0}";
        }

        /// <summary>
        /// 设置GridView的列为只读
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="fieldName">操作字段名</param>
        /// <param name="allowEdit">是否允许编辑，默认为false，只读</param>
        public static void SetReadOnly(this GridView gridView, string fieldName, bool allowEdit = false)
        {
            var gridColumn = gridView.Columns[fieldName];
            if (gridColumn != null)
            {
                gridColumn.OptionsColumn.AllowEdit = allowEdit;
                gridColumn.OptionsColumn.ReadOnly = !allowEdit;
                if (!allowEdit)
                {
                    gridColumn.AppearanceHeader.ForeColor = Color.Gray;
                }
                else
                {
                    gridColumn.AppearanceHeader.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// 设置GridView的列为只读
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <param name="allowEdit">是否允许编辑，默认为false，只读</param>
        public static GridColumn SetReadOnly(this GridColumn gridColumn, bool allowEdit = false)
        {
            gridColumn.OptionsColumn.AllowEdit = allowEdit;
            gridColumn.OptionsColumn.ReadOnly = !allowEdit;
            if (!allowEdit)
            {
                gridColumn.AppearanceHeader.ForeColor = Color.Gray;
            }
            else
            {
                gridColumn.AppearanceHeader.ForeColor = Color.Black;
            }
            return gridColumn;
        }

        /// <summary>
        /// 是否使用密码字符（如*号）来隐藏内容
        /// </summary>
        /// <param name="gridColumn">GridColumn列对象</param>
        /// <param name="masked">是否使用隐藏</param>
        /// <param name="passwordChar">密码字符，默认为*</param>
        public static GridColumn SetPasswordChar(this GridColumn gridColumn, bool masked = true, char passwordChar = '*')
        {
            if(masked)
            {
                gridColumn.CreateTextEdit().PasswordChar = passwordChar;
            }
            return gridColumn;
        }

        /// <summary>
        /// 校验GridControl控件对象
        /// </summary>
        /// <param name="gridControl">GridControl控件对象</param>
        /// <returns></returns>
		public static bool ValidateGridEditor(this GridControl gridControl)
		{
			bool result;
			if (gridControl == null)
			{
				result = true;
			}
			else
			{
				GridView gridView = gridControl.FocusedView as GridView;
				if (gridView == null)
				{
					result = true;
				}
				else
				{
					if (!gridView.OptionsBehavior.Editable)
					{
						result = true;
					}
					else
					{
						gridView.PostEditor();
						if (!gridView.ValidateEditor())
						{
							result = false;
						}
						else
						{
							result = gridView.UpdateCurrentRow();
						}
					}
				}
			}
			return result;
		}

        /// <summary>
        /// 设置GridControl是否可以编辑
        /// </summary>
        /// <param name="gridControl">GridControl控件对象</param>
        /// <param name="editable">是否可以编辑</param>
		public static void SetGridEditable(this GridControl gridControl, bool editable)
		{
			if (gridControl != null)
			{
				foreach (GridView gridView in gridControl.ViewCollection)
				{
					gridView.OptionsBehavior.Editable = editable;
				}
			}
		}

        /// <summary>
        /// 设置GridView的列为只读与否
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="fieldNameString">字段列表，逗号分开,如果需要设置所有，那么用*代替</param>
        /// <param name="allowEdit">是否允许编辑</param>
        public static void SetColumnsReadOnly(this GridView gridView, string fieldNameString, bool allowEdit = false)
        {
            if (!string.IsNullOrEmpty(fieldNameString))
            {
                List<string> includeList = fieldNameString.ToDelimitedList<string>(",");
                foreach (GridColumn col in gridView.Columns)
                {
                    if (fieldNameString == "*")
                    {
                        col.OptionsColumn.AllowEdit = allowEdit;
                        col.OptionsColumn.ReadOnly = !allowEdit;
                        if (!allowEdit)
                        {
                            col.AppearanceHeader.ForeColor = Color.Gray;
                        }
                        else
                        {
                            col.AppearanceHeader.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        var include = includeList.Contains(col.FieldName);
                        if (include)
                        {
                            col.OptionsColumn.AllowEdit = allowEdit;
                            col.OptionsColumn.ReadOnly = !allowEdit;
                            if (!allowEdit)
                            {
                                col.AppearanceHeader.ForeColor = Color.Gray;
                            }
                            else
                            {
                                col.AppearanceHeader.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置GridView的列为显示与否
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="fieldNameString">字段列表，逗号分开,如果需要设置所有，那么用*代替</param>
        /// <param name="visible">是否允许编辑</param>
        public static void SetColumnsVisible(this GridView gridView, string fieldNameString, bool visible = false)
        {
            if (!string.IsNullOrEmpty(fieldNameString))
            {
                List<string> includeList = fieldNameString.ToDelimitedList<string>(",");
                foreach (GridColumn col in gridView.Columns)
                {
                    if (fieldNameString == "*")
                    {
                        col.Visible = visible;
                    }
                    else
                    {
                        var include = includeList.Contains(col.FieldName);
                        if (include)
                        {
                            col.Visible = visible;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否使用密码字符（如*号）来隐藏内容
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="fieldNameString">字段列表，逗号分开,如果需要设置所有，那么用*代替</param>
        /// <param name="masked">是否遮挡</param>
        /// <param name="passwordChar">遮挡字符，默认为*字符</param>
        public static void SetColumnsPasswordChar(this GridView gridView, string fieldNameString, bool masked = true, char passwordChar='*')
        {
            if (!string.IsNullOrEmpty(fieldNameString) && masked)
            {
                List<string> includeList = fieldNameString.ToDelimitedList<string>(",");
                foreach (GridColumn col in gridView.Columns)
                {
                    if (fieldNameString == "*")
                    {
                        col.CreateTextEdit().PasswordChar = passwordChar;
                    }
                    else
                    {
                        var include = includeList.Contains(col.FieldName);
                        if (include)
                        {
                            col.CreateTextEdit().PasswordChar = passwordChar;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 根据参数权限字典的值：0可读写，1只读，2隐藏值，3不显示，设置列的权限。
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="fieNamePermitDict">字段和权限字典，字典值为权限控制：0可读写，1只读，2隐藏值，3不显示</param>
        public static void SetColumnsPermit(this GridView gridView, Dictionary<string,int> fieNamePermitDict)
        {
            char passwordChar = '*';
            foreach (GridColumn col in gridView.Columns)
            {
                var include = fieNamePermitDict.ContainsKey(col.FieldName);
                if (include)
                {
                    int permit = fieNamePermitDict[col.FieldName];
                    switch (permit)
                    {
                        case 0://正常可见、可读写
                            col.OptionsColumn.AllowEdit = true;
                            col.OptionsColumn.ReadOnly = false;
                            col.AppearanceHeader.ForeColor = Color.Black;

                            col.Visible = true;
                            break;

                        case 1:
                            //只读
                            col.OptionsColumn.AllowEdit = false;
                            col.OptionsColumn.ReadOnly = true;
                            col.AppearanceHeader.ForeColor = Color.Gray;

                            col.Visible = true;
                            break;

                        case 2:
                            //隐藏值
                            var edit = col.CreateTextEdit();                            
                            col.Tag = string.Concat(passwordChar);//用来在界面端进行判断，避免设置DisplayText
                            edit.PasswordChar = passwordChar;
                            col.Visible = true;
                            break;

                        case 3:
                            //不可见
                            col.Visible = false;
                            break;
                    }
                }
            }            
        }
        
        /// <summary>
        /// 设置GridView的标题显示
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <param name="caption">显示标题</param>
		public static void SetGridViewCaption(this GridView gridView, string caption)
        {
            //使用多语言处理标题
            caption = JsonLanguage.Default.GetString(caption);

			if (!gridView.OptionsView.ShowViewCaption)
			{
				gridView.OptionsView.ShowViewCaption = true;
			}
			gridView.ViewCaption = caption;
		}

        /// <summary>
        /// 从GridView里面获取可见列并转换为DataTable对象
        /// </summary>
        /// <param name="gridView">GridView对象</param>
        /// <returns></returns>
        public static DataTable GetDataTableFromGridView(this GridView gridView)
        {
            DataTable dataTable = new DataTable();
            for (int c = 0; c < gridView.Columns.Count; c++)
            {
                if (gridView.Columns[c].Visible)
                {
                    dataTable.Columns.Add(gridView.Columns[c].FieldName);
                }
            }

            for (int r = 0; r < gridView.RowCount; r++)
            {
                DataRow drNew = dataTable.NewRow();
                for (int c2 = 0; c2 < dataTable.Columns.Count; c2++)
                {
                    drNew[dataTable.Columns[c2].ColumnName] = gridView.GetRowCellDisplayText(r, dataTable.Columns[c2].ColumnName);
                }
                dataTable.Rows.Add(drNew);
            }
            return dataTable;
        }

        /// <summary>
        /// 设置GridView列的宽度
        /// </summary>
        /// <param name="gridView">gridView对象</param>
        /// <param name="columnName">列名称，大小写要注意</param>
        /// <param name="width">宽度，默认我100</param>
        public static GridColumn SetGridColumWidth(this GridView gridView, string columnName, int width = 100)
        {
            GridColumn column = gridView.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
            else
            {
                column = gridView.Columns.ColumnByFieldName(columnName.ToUpper());
                if (column != null)
                {
                    column.Width = width;
                }
            }
            return column;
        }

        #region 控件验证

        /// <summary>
        /// 检查某行字段是否为空
        /// </summary>
        public static bool ValidateRowNull(this GridControl grd, ValidateRowEventArgs e, params string[] fieldNames)
        {
            GridView grv = grd.FocusedView as GridView;
            bool result = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                string fieldName = fieldNames[i];
                if (string.IsNullOrEmpty(string.Concat(grv.GetRowCellValue(e.RowHandle, fieldName))))
                {
                    e.Valid = false;
                    e.ErrorText = string.Format("{0}不能为空", grv.Columns[fieldName].Caption);
                    grv.FocusedColumn = grv.Columns[fieldName];
                    result = false;
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 检查指定行的字段是否为重复
        /// </summary>
        public static bool ValidateRowRepeat(this GridControl grd, BaseContainerValidateEditorEventArgs e, string primaryKey, params string[] fieldNames)
        {
            bool result = true;

            GridView grv = grd.FocusedView as GridView;
            string focuseKeyValue = string.Concat(grv.GetFocusedRowCellValue(primaryKey));
            for (int i = 0; i < fieldNames.Length; i++)
            {
                string fieldName = fieldNames[i];
                if (grv.FocusedColumn.FieldName == fieldName)
                {
                    for (int r = 0; r < grv.RowCount; r++)
                    {
                        string keyValue = string.Concat(grv.GetRowCellValue(r, primaryKey));
                        if (!string.IsNullOrEmpty(keyValue) && focuseKeyValue != keyValue
                            && string.Concat(e.Value) == string.Concat(grv.GetRowCellValue(r, fieldName)))
                        {
                            e.Valid = false;
                            e.ErrorText = string.Format("{0}不能重复", grv.Columns[fieldName].Caption);
                            grv.FocusedColumn = grv.Columns[fieldName];
                            result = false;
                            return result;
                        }
                    }
                }
            }
            return result;
        } 
        #endregion  
        
        #region Excel导出操作

        /// <summary>
        /// 从GridView导出到Excel文件，并可以打开文件
        /// </summary>
        /// <param name="grv">GridView</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <param name="open">是否打开</param>
        /// <param name="printSelectedRowsOnly">是否打印选定行</param>
        public static void ExportToExcel(this GridView grv, string fileName = "", bool open = true, bool printSelectedRowsOnly = false)
        {
            string filePath = FileDialogHelper.SaveExcel(fileName);
            if (!string.IsNullOrEmpty(filePath))
            {
                SetOptionPrint(grv, printSelectedRowsOnly);
                grv.ExportToXls(filePath);
                ShowOpenFileDialog(open, filePath);
            }
        }

        /// <summary>
        /// 从GridView集合导出到Excel文件，并可以打开文件
        /// </summary>        
        /// <param name="grd">GridControl对象</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <param name="open">是否打开</param>
        public static void ExportToExcel(this GridControl grd, string fileName = "", bool open = true)
        {
            string filePath = FileDialogHelper.SaveExcel(fileName);
            if (!string.IsNullOrEmpty(filePath))
            {
                for (int i = 0; i < grd.ViewCollection.Count; i++)
                {
                    if (grd.ViewCollection[i] != null)
                    {
                        GridView grv = grd.ViewCollection[i] as GridView;
                        SetOptionPrint(grv, false);
                    }
                }
                grd.ExportToXls(filePath);
                ShowOpenFileDialog(open, filePath);
            }
        }

        /// <summary>
        /// 设置打印选项
        /// </summary>
        /// <param name="grv">GridView对象</param>
        /// <param name="printSelectedRowsOnly">是否只打印选中行，默认为false</param>
        private static void SetOptionPrint(this GridView grv, bool printSelectedRowsOnly = false)
        {
            grv.OptionsPrint.AutoWidth = false;
            grv.OptionsPrint.ExpandAllDetails = true;
            grv.OptionsPrint.ExpandAllGroups = true;
            grv.OptionsPrint.PrintDetails = true;
            grv.OptionsPrint.PrintHorzLines = true;
            grv.OptionsPrint.PrintVertLines = true;
            grv.OptionsPrint.EnableAppearanceEvenRow = true;
            grv.OptionsPrint.EnableAppearanceOddRow = true;
            grv.OptionsPrint.PrintSelectedRowsOnly = printSelectedRowsOnly;
        }

        private static void ShowOpenFileDialog(bool open, string fileName)
        {
            if (open)
            {
                bool result = MessageDxUtil.ShowYesNoAndTips("是否打开文件?") == DialogResult.Yes;
                if (result)
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = fileName;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        process.Start();
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 根据行，列索引来获取RepositoryItem
        /// </summary>
        /// <param name="view">GridView</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <returns>RepositoryItem</returns>
        public static RepositoryItem GetRepositoryItem(this GridView view, int rowIndex, int columnIndex)
        {
            GridViewInfo _viewInfo = view.GetViewInfo() as GridViewInfo;
            GridDataRowInfo _viewRowInfo = _viewInfo.RowsInfo.FindRow(rowIndex) as GridDataRowInfo;
            return _viewRowInfo.Cells[columnIndex].Editor;
        }
    }   

    /// <summary>
    /// GridView的显示类型
    /// </summary>
    public enum GridType { NewItem, EditOnly, ReadOnly }
}
