namespace M12MiniMes.UI
{
    partial class Frm设备表
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
        	this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm设备表));
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
        	this.winGridViewPager1 = new WHC.Pager.WinControl.WinGridViewPager();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);            
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();    


            this.txt设备名称 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
 
            this.txtIp = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
 
            this.txt位置序号1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txt位置序号2 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
 
            this.txt启用状态 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
 
            this.txt生产状态 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
             
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();

            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();    

            ((System.ComponentModel.ISupportInitialize)(this.txt设备名称.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
 
            ((System.ComponentModel.ISupportInitialize)(this.txtIp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
 
            ((System.ComponentModel.ISupportInitialize)(this.txt位置序号1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt位置序号2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
 
            ((System.ComponentModel.ISupportInitialize)(this.txt启用状态.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
 
            ((System.ComponentModel.ISupportInitialize)(this.txt生产状态.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
 

 
 
 
 
 

	 	 	 	 	  			
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(698, 65);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(69, 22);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(773, 65);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(69, 22);
            this.btnAddNew.TabIndex = 15;
            this.btnAddNew.Text = "新建";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(848, 65);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(69, 22);
            this.btnImport.TabIndex = 15;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(923, 65);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(69, 22);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // winGridViewPager1
            // 
            this.winGridViewPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.winGridViewPager1.AppendedMenu = null;
            this.winGridViewPager1.DataSource = null;
            this.winGridViewPager1.DisplayColumns = "";
            this.winGridViewPager1.Location = new System.Drawing.Point(12, 95);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.Size = new System.Drawing.Size(980, 580);
            this.winGridViewPager1.TabIndex = 11;
            // 
            // layoutControl1
            //             
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(980, 53);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";


            this.layoutControl1.Controls.Add(this.txt设备名称);

 
            this.layoutControl1.Controls.Add(this.txtIp);

 
            this.layoutControl1.Controls.Add(this.txt位置序号1);
            this.layoutControl1.Controls.Add(this.txt位置序号2);

 
            this.layoutControl1.Controls.Add(this.txt启用状态);

 
            this.layoutControl1.Controls.Add(this.txt生产状态);

 

            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txt设备名称;
            this.layoutControlItem1.CustomizationFormText = "设备名称";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(150, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(80, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(150, 25);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutControlItem1.Text = "设备名称";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14); 
            // 
            // txt设备名称
            // 
            this.txt设备名称.Location = new System.Drawing.Point(64, 12);
            this.txt设备名称.Name = "txt设备名称";
            this.txt设备名称.Size = new System.Drawing.Size(122, 20);
            this.txt设备名称.StyleController = this.layoutControl1;
            this.txt设备名称.TabIndex = 1;


 
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtIp;
            this.layoutControlItem2.CustomizationFormText = "IP";
            this.layoutControlItem2.Location = new System.Drawing.Point(178, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(150, 25);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(80, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(150, 25);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutControlItem2.Text = "IP";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14); 
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(242, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(122, 20);
            this.txtIp.StyleController = this.layoutControl1;
            this.txtIp.TabIndex = 2;


 
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txt位置序号1;
            this.layoutControlItem3.CustomizationFormText = "位置序号1";
            this.layoutControlItem3.Location = new System.Drawing.Point(356, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 25);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(80, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 25);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutControlItem3.Text = "位置序号";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14); 
            // 
            // txt位置序号
            // 
            this.txt位置序号1.Location = new System.Drawing.Point(420, 12);
            this.txt位置序号1.Name = "txt位置序号1";
            this.txt位置序号1.Size = new System.Drawing.Size(80, 20);
            this.txt位置序号1.StyleController = this.layoutControl1;
            this.txt位置序号1.TabIndex = 3;
			this.txt位置序号1.ImeMode = System.Windows.Forms.ImeMode.Off;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txt位置序号2;
            this.layoutControlItem4.CustomizationFormText = "位置序号2";
            this.layoutControlItem4.Location = new System.Drawing.Point(534, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 25);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(80, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 25);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutControlItem4.Text = "位置序号";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14); 
            // 
            // txt位置序号
            // 
            this.txt位置序号2.Location = new System.Drawing.Point(598, 12);
            this.txt位置序号2.Name = "txt位置序号2";
            this.txt位置序号2.Size = new System.Drawing.Size(80, 20);
            this.txt位置序号2.StyleController = this.layoutControl1;
            this.txt位置序号2.TabIndex = 4;
			this.txt位置序号2.ImeMode = System.Windows.Forms.ImeMode.Off;


 
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txt启用状态;
            this.layoutControlItem5.CustomizationFormText = "启用状态";
            this.layoutControlItem5.Location = new System.Drawing.Point(712, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(150, 25);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(80, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(150, 25);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutControlItem5.Text = "启用状态";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14); 
            // 
            // txt启用状态
            // 
            this.txt启用状态.Location = new System.Drawing.Point(776, 12);
            this.txt启用状态.Name = "txt启用状态";
            this.txt启用状态.Size = new System.Drawing.Size(122, 20);
            this.txt启用状态.StyleController = this.layoutControl1;
            this.txt启用状态.TabIndex = 5;


 
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txt生产状态;
            this.layoutControlItem6.CustomizationFormText = "生产状态";
            this.layoutControlItem6.Location = new System.Drawing.Point(890, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(150, 25);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(80, 25);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(150, 25);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutControlItem6.Text = "生产状态";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 14); 
            // 
            // txt生产状态
            // 
            this.txt生产状态.Location = new System.Drawing.Point(954, 12);
            this.txt生产状态.Name = "txt生产状态";
            this.txt生产状态.Size = new System.Drawing.Size(122, 20);
            this.txt生产状态.StyleController = this.layoutControl1;
            this.txt生产状态.TabIndex = 6;


 
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {

							this.layoutControlItem1
			 
					       ,this.layoutControlItem2
			 
					       ,this.layoutControlItem3
				       ,this.layoutControlItem4
			 
					       ,this.layoutControlItem5
			 
					       ,this.layoutControlItem6
			        });
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(605, 363);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;          

            // 
            // Frm设备表
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.winGridViewPager1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Name = "Frm设备表";
            this.Text = "设备表";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);            
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.txt设备名称.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();            
             ((System.ComponentModel.ISupportInitialize)(this.txtIp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();            
             ((System.ComponentModel.ISupportInitialize)(this.txt位置序号1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit(); 
            ((System.ComponentModel.ISupportInitialize)(this.txt位置序号2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit(); 
             ((System.ComponentModel.ISupportInitialize)(this.txt启用状态.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();            
             ((System.ComponentModel.ISupportInitialize)(this.txt生产状态.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();            
 
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddNew;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private WHC.Pager.WinControl.WinGridViewPager winGridViewPager1;

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;


        private DevExpress.XtraEditors.TextEdit txt设备名称; 
 
        private DevExpress.XtraEditors.TextEdit txtIp; 
 
        private DevExpress.XtraEditors.TextEdit txt位置序号1;  
        private DevExpress.XtraEditors.TextEdit txt位置序号2;  
 
        private DevExpress.XtraEditors.TextEdit txt启用状态; 
 
        private DevExpress.XtraEditors.TextEdit txt生产状态; 
 
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;    
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;  
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;    
 
    }
}