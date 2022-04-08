namespace M12MiniMes.UIStart
{
    partial class FormItemsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormItemsView));
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_设备ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_设备名称 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_IP地址 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_下发批次时固定减少数量 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_下发批次时打折百分比 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_生产数据id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_生产时间 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_物料生产批次号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_治具生产批次号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_物料guid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_治具guid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_治具rfid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_治具孔号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_设备id2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_工位号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_工序数据 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_结果ok = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bt同步 = new DevExpress.XtraEditors.SimpleButton();
            this.bt刷新 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView2.FixedLineWidth = 3;
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.IndicatorWidth = 60;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gridView2.OptionsFilter.AllowFilterEditor = false;
            this.gridView2.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridView2.OptionsFind.ShowFindButton = false;
            this.gridView2.OptionsMenu.EnableColumnMenu = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gridView2_InvalidRowException);
            this.gridView2.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView2_ValidateRow);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "治具RFID";
            this.gridColumn1.FieldName = "RFID";
            this.gridColumn1.MinWidth = 25;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 94;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "治具GUID";
            this.gridColumn2.FieldName = "FixtureGuid";
            this.gridColumn2.MinWidth = 25;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 94;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "治具生产批次号";
            this.gridColumn3.FieldName = "治具生产批次号";
            this.gridColumn3.MinWidth = 25;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 94;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.First.Enabled = false;
            this.gridControl1.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.gridControl1.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.gridControl1.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gridControl1.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl1_EmbeddedNavigator_ButtonClick);
            gridLevelNode1.LevelTemplate = this.gridView2;
            gridLevelNode2.LevelTemplate = this.gridView3;
            gridLevelNode3.LevelTemplate = this.gridView4;
            gridLevelNode3.RelationName = "生产数据";
            gridLevelNode2.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode3});
            gridLevelNode2.RelationName = "MaterialItems";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            gridLevelNode1.RelationName = "CurrentFixtureItems";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2260, 805);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView3,
            this.gridView4,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_设备ID,
            this.gridColumn_设备名称,
            this.gridColumn_IP地址,
            this.gridColumn_下发批次时固定减少数量,
            this.gridColumn_下发批次时打折百分比});
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 60;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridView1.OptionsFind.ShowFindButton = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gridView1_InvalidRowException);
            this.gridView1.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView1_ValidateRow);
            // 
            // gridColumn_设备ID
            // 
            this.gridColumn_设备ID.Caption = "设备ID";
            this.gridColumn_设备ID.FieldName = "设备id";
            this.gridColumn_设备ID.MinWidth = 25;
            this.gridColumn_设备ID.Name = "gridColumn_设备ID";
            this.gridColumn_设备ID.OptionsColumn.ReadOnly = true;
            this.gridColumn_设备ID.Visible = true;
            this.gridColumn_设备ID.VisibleIndex = 0;
            this.gridColumn_设备ID.Width = 94;
            // 
            // gridColumn_设备名称
            // 
            this.gridColumn_设备名称.Caption = "设备名称";
            this.gridColumn_设备名称.FieldName = "设备名称";
            this.gridColumn_设备名称.MinWidth = 25;
            this.gridColumn_设备名称.Name = "gridColumn_设备名称";
            this.gridColumn_设备名称.OptionsColumn.ReadOnly = true;
            this.gridColumn_设备名称.Visible = true;
            this.gridColumn_设备名称.VisibleIndex = 1;
            this.gridColumn_设备名称.Width = 94;
            // 
            // gridColumn_IP地址
            // 
            this.gridColumn_IP地址.Caption = "IP";
            this.gridColumn_IP地址.FieldName = "Ip";
            this.gridColumn_IP地址.MinWidth = 25;
            this.gridColumn_IP地址.Name = "gridColumn_IP地址";
            this.gridColumn_IP地址.OptionsColumn.ReadOnly = true;
            this.gridColumn_IP地址.Visible = true;
            this.gridColumn_IP地址.VisibleIndex = 2;
            this.gridColumn_IP地址.Width = 94;
            // 
            // gridColumn_下发批次时固定减少数量
            // 
            this.gridColumn_下发批次时固定减少数量.Caption = "下发批次时固定减少数量";
            this.gridColumn_下发批次时固定减少数量.FieldName = "ReduceOffsets";
            this.gridColumn_下发批次时固定减少数量.MinWidth = 25;
            this.gridColumn_下发批次时固定减少数量.Name = "gridColumn_下发批次时固定减少数量";
            this.gridColumn_下发批次时固定减少数量.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gridColumn_下发批次时固定减少数量.Visible = true;
            this.gridColumn_下发批次时固定减少数量.VisibleIndex = 3;
            this.gridColumn_下发批次时固定减少数量.Width = 94;
            // 
            // gridColumn_下发批次时打折百分比
            // 
            this.gridColumn_下发批次时打折百分比.Caption = "下发批次时打折百分比";
            this.gridColumn_下发批次时打折百分比.FieldName = "ReduceOffsetsPercent";
            this.gridColumn_下发批次时打折百分比.MinWidth = 25;
            this.gridColumn_下发批次时打折百分比.Name = "gridColumn_下发批次时打折百分比";
            this.gridColumn_下发批次时打折百分比.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gridColumn_下发批次时打折百分比.Visible = true;
            this.gridColumn_下发批次时打折百分比.VisibleIndex = 4;
            this.gridColumn_下发批次时打折百分比.Width = 94;
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5});
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.IndicatorWidth = 60;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.ReadOnly = true;
            this.gridView3.OptionsCustomization.AllowColumnMoving = false;
            this.gridView3.OptionsCustomization.AllowFilter = false;
            this.gridView3.OptionsCustomization.AllowSort = false;
            this.gridView3.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gridView3.OptionsFilter.AllowFilterEditor = false;
            this.gridView3.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridView3.OptionsFind.ShowFindButton = false;
            this.gridView3.OptionsMenu.EnableColumnMenu = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "物料GUID";
            this.gridColumn4.FieldName = "MaterialGuid";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 94;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "物料生产批次号";
            this.gridColumn5.FieldName = "物料生产批次号";
            this.gridColumn5.MinWidth = 25;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 94;
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_生产数据id,
            this.gridColumn_生产时间,
            this.gridColumn_物料生产批次号,
            this.gridColumn_治具生产批次号,
            this.gridColumn_物料guid,
            this.gridColumn_治具guid,
            this.gridColumn_治具rfid,
            this.gridColumn_治具孔号,
            this.gridColumn_设备id2,
            this.gridColumn_工位号,
            this.gridColumn_工序数据,
            this.gridColumn_结果ok});
            this.gridView4.GridControl = this.gridControl1;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.ReadOnly = true;
            this.gridView4.OptionsCustomization.AllowColumnMoving = false;
            this.gridView4.OptionsCustomization.AllowFilter = false;
            this.gridView4.OptionsCustomization.AllowSort = false;
            this.gridView4.OptionsFilter.AllowFilterEditor = false;
            this.gridView4.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridView4.OptionsFind.ShowFindButton = false;
            this.gridView4.OptionsMenu.EnableColumnMenu = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn_生产数据id
            // 
            this.gridColumn_生产数据id.Caption = "生产数据id";
            this.gridColumn_生产数据id.FieldName = "生产数据id";
            this.gridColumn_生产数据id.MinWidth = 25;
            this.gridColumn_生产数据id.Name = "gridColumn_生产数据id";
            this.gridColumn_生产数据id.Visible = true;
            this.gridColumn_生产数据id.VisibleIndex = 0;
            this.gridColumn_生产数据id.Width = 94;
            // 
            // gridColumn_生产时间
            // 
            this.gridColumn_生产时间.Caption = "生产时间";
            this.gridColumn_生产时间.FieldName = "生产时间";
            this.gridColumn_生产时间.MinWidth = 25;
            this.gridColumn_生产时间.Name = "gridColumn_生产时间";
            this.gridColumn_生产时间.Visible = true;
            this.gridColumn_生产时间.VisibleIndex = 1;
            this.gridColumn_生产时间.Width = 94;
            // 
            // gridColumn_物料生产批次号
            // 
            this.gridColumn_物料生产批次号.Caption = "物料生产批次号";
            this.gridColumn_物料生产批次号.FieldName = "物料生产批次号";
            this.gridColumn_物料生产批次号.MinWidth = 25;
            this.gridColumn_物料生产批次号.Name = "gridColumn_物料生产批次号";
            this.gridColumn_物料生产批次号.Visible = true;
            this.gridColumn_物料生产批次号.VisibleIndex = 2;
            this.gridColumn_物料生产批次号.Width = 94;
            // 
            // gridColumn_治具生产批次号
            // 
            this.gridColumn_治具生产批次号.Caption = "治具生产批次号";
            this.gridColumn_治具生产批次号.FieldName = "治具生产批次号";
            this.gridColumn_治具生产批次号.MinWidth = 25;
            this.gridColumn_治具生产批次号.Name = "gridColumn_治具生产批次号";
            this.gridColumn_治具生产批次号.Visible = true;
            this.gridColumn_治具生产批次号.VisibleIndex = 3;
            this.gridColumn_治具生产批次号.Width = 94;
            // 
            // gridColumn_物料guid
            // 
            this.gridColumn_物料guid.Caption = "物料guid";
            this.gridColumn_物料guid.FieldName = "物料guid";
            this.gridColumn_物料guid.MinWidth = 25;
            this.gridColumn_物料guid.Name = "gridColumn_物料guid";
            this.gridColumn_物料guid.Visible = true;
            this.gridColumn_物料guid.VisibleIndex = 4;
            this.gridColumn_物料guid.Width = 94;
            // 
            // gridColumn_治具guid
            // 
            this.gridColumn_治具guid.Caption = "治具guid";
            this.gridColumn_治具guid.FieldName = "治具guid";
            this.gridColumn_治具guid.MinWidth = 25;
            this.gridColumn_治具guid.Name = "gridColumn_治具guid";
            this.gridColumn_治具guid.Visible = true;
            this.gridColumn_治具guid.VisibleIndex = 5;
            this.gridColumn_治具guid.Width = 94;
            // 
            // gridColumn_治具rfid
            // 
            this.gridColumn_治具rfid.Caption = "治具rfid";
            this.gridColumn_治具rfid.FieldName = "治具rfid";
            this.gridColumn_治具rfid.MinWidth = 25;
            this.gridColumn_治具rfid.Name = "gridColumn_治具rfid";
            this.gridColumn_治具rfid.Visible = true;
            this.gridColumn_治具rfid.VisibleIndex = 6;
            this.gridColumn_治具rfid.Width = 94;
            // 
            // gridColumn_治具孔号
            // 
            this.gridColumn_治具孔号.Caption = "治具孔号";
            this.gridColumn_治具孔号.FieldName = "治具孔号";
            this.gridColumn_治具孔号.MinWidth = 25;
            this.gridColumn_治具孔号.Name = "gridColumn_治具孔号";
            this.gridColumn_治具孔号.Visible = true;
            this.gridColumn_治具孔号.VisibleIndex = 7;
            this.gridColumn_治具孔号.Width = 94;
            // 
            // gridColumn_设备id2
            // 
            this.gridColumn_设备id2.Caption = "设备id";
            this.gridColumn_设备id2.FieldName = "设备id";
            this.gridColumn_设备id2.MinWidth = 25;
            this.gridColumn_设备id2.Name = "gridColumn_设备id2";
            this.gridColumn_设备id2.Visible = true;
            this.gridColumn_设备id2.VisibleIndex = 8;
            this.gridColumn_设备id2.Width = 94;
            // 
            // gridColumn_工位号
            // 
            this.gridColumn_工位号.Caption = "工位号";
            this.gridColumn_工位号.FieldName = "工位号";
            this.gridColumn_工位号.MinWidth = 25;
            this.gridColumn_工位号.Name = "gridColumn_工位号";
            this.gridColumn_工位号.Visible = true;
            this.gridColumn_工位号.VisibleIndex = 9;
            this.gridColumn_工位号.Width = 94;
            // 
            // gridColumn_工序数据
            // 
            this.gridColumn_工序数据.Caption = "工序数据";
            this.gridColumn_工序数据.FieldName = "工序数据";
            this.gridColumn_工序数据.MinWidth = 25;
            this.gridColumn_工序数据.Name = "gridColumn_工序数据";
            this.gridColumn_工序数据.Visible = true;
            this.gridColumn_工序数据.VisibleIndex = 10;
            this.gridColumn_工序数据.Width = 94;
            // 
            // gridColumn_结果ok
            // 
            this.gridColumn_结果ok.Caption = "结果ok";
            this.gridColumn_结果ok.FieldName = "结果ok";
            this.gridColumn_结果ok.MinWidth = 25;
            this.gridColumn_结果ok.Name = "gridColumn_结果ok";
            this.gridColumn_结果ok.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.gridColumn_结果ok.Visible = true;
            this.gridColumn_结果ok.VisibleIndex = 11;
            this.gridColumn_结果ok.Width = 94;
            // 
            // bt同步
            // 
            this.bt同步.Appearance.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt同步.Appearance.ForeColor = System.Drawing.Color.Red;
            this.bt同步.Appearance.Options.UseFont = true;
            this.bt同步.Appearance.Options.UseForeColor = true;
            this.bt同步.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bt同步.Location = new System.Drawing.Point(0, 904);
            this.bt同步.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bt同步.Name = "bt同步";
            this.bt同步.Size = new System.Drawing.Size(2260, 99);
            this.bt同步.TabIndex = 12;
            this.bt同步.Text = "【慎点】：从数据库同步设备信息并清空当前内存数据";
            this.bt同步.Click += new System.EventHandler(this.bt同步_Click);
            // 
            // bt刷新
            // 
            this.bt刷新.Appearance.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt刷新.Appearance.ForeColor = System.Drawing.Color.Red;
            this.bt刷新.Appearance.Options.UseFont = true;
            this.bt刷新.Appearance.Options.UseForeColor = true;
            this.bt刷新.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bt刷新.Location = new System.Drawing.Point(0, 805);
            this.bt刷新.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bt刷新.Name = "bt刷新";
            this.bt刷新.Size = new System.Drawing.Size(2260, 99);
            this.bt刷新.TabIndex = 13;
            this.bt刷新.Text = "刷新";
            this.bt刷新.Click += new System.EventHandler(this.bt刷新_Click);
            // 
            // FormItemsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(2281, 979);
            this.Controls.Add(this.bt刷新);
            this.Controls.Add(this.bt同步);
            this.Controls.Add(this.gridControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("FormItemsView.IconOptions.Icon")));
            this.Name = "FormItemsView";
            this.Text = "生产内存数据总览";
            this.Load += new System.EventHandler(this.FormItemsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_设备ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_设备名称;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_IP地址;
        private DevExpress.XtraEditors.SimpleButton bt同步;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.SimpleButton bt刷新;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_生产数据id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_生产时间;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_物料生产批次号;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_治具生产批次号;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_物料guid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_治具guid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_治具rfid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_治具孔号;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_设备id2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_工位号;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_工序数据;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_结果ok;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_下发批次时固定减少数量;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_下发批次时打折百分比;
    }
}