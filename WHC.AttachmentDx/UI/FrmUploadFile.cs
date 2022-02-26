using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Attachment.Entity;
using WHC.Attachment.BLL;
using WHC.Framework.BaseUI;
using WHC.Framework.Language;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// 文件上传管理
    /// </summary>
    public partial class FrmUploadFile : BaseForm
    {
        #region 字段属性

        private BackgroundWorker worker = null;

        /// <summary>
        /// 设置附件的存储目录分类
        /// </summary>
        public string AttachmentDirectory = "";

        /// <summary>
        /// 指定附件对应的GUID
        /// </summary>
        public string AttachmentGUID = Guid.NewGuid().ToString();

        /// <summary>
        /// 附件的编辑人
        /// </summary>
        public string UserId = string.Empty;

        /// <summary>
        /// 附件组所属的记录ID，如属于某个主表记录的ID
        /// </summary>
        public string OwerId = "";

        private List<string> fileList = new List<string>();
        private Dictionary<string, string> fileStatus = new Dictionary<string, string>();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmUploadFile()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);

            SetNotCopyToolTips();
        }

        private void SetNotCopyToolTips()
        {
            ToolTip tip = new ToolTip();
            tip.IsBalloon = true;
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.ToolTipTitle = JsonLanguage.Default.GetString("提示");
            tip.UseAnimation = true;
            var tips = JsonLanguage.Default.GetString("对Winform本地程序来说，有时候文件已经是存在的，不需要复制文件！");
            tip.SetToolTip(this.chkNotCopyFile, tips);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.EditValue = e.ProgressPercentage;

            BindData();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barTips.Caption = (string)e.Result;
            //MessageDxUtil.ShowTips(e.Result.ToString());
            this.ShowAlertControl(e.Result.ToString(), "", this.Owner);

            if (e.Result != null && e.Result.ToString() == "Success")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

                ProcessDataSaved(this.btnUpload, new EventArgs());
            }

            this.btnBrowse.Enabled = true;
            this.btnUpload.Enabled = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //对Winform本地程序来说，有时候文件已经是存在的，且路径已经设置好，
                //因此设置notCopyFile = true, 不需要复制文件从而导致文件路径变动
                bool notCopyFile = Convert.ToBoolean(e.Argument);

                int step = 0;
                int i = 0;
                string state = "";
                if (fileList.Count > 0)
                {
                    bool sucess = true;
                    string lastError = "";
                    foreach (string file in fileList)
                    {
                        FileUploadInfo info = new FileUploadInfo();
                        if (notCopyFile)
                        {
                            //如果不需要复制文件，那么记录文件的相对路径
                            info.BasePath = Path.GetDirectoryName(file);                            
                        }
                        else
                        {
                            //常规复制文件，需要记录文件的字节
                            info.FileData = FileUtil.FileToBytes(file);
                        }

                        info.FileName = FileUtil.GetFileName(file);
                        info.Category = this.AttachmentDirectory;
                        info.FileExtend = FileUtil.GetExtension(file);
                        info.FileSize = FileUtil.GetFileSize(file);
                        info.Editor = UserId;//登录人
                        info.Owner_ID = OwerId;//所属主表记录ID
                        info.AttachmentGUID = AttachmentGUID;

                        CommonResult result = BLLFactory<FileUpload>.Instance.Upload(info);
                        if (!result.Success)
                        {
                            sucess = false;
                            lastError = result.ErrorMessage;
                            fileStatus[file] = result.ErrorMessage;
                            state = string.Format("{0}|{1}", file, result.ErrorMessage);
                        }
                        else
                        {
                            fileStatus[file] = "成功";
                            state = string.Format("{0}|成功", file);
                        }

                        i++;
                        step = Convert.ToInt32((100.0 / (fileList.Count * 1.0)) * i);
                        worker.ReportProgress(step, state);
                    }
                    e.Result = sucess ? "Success" : "Failed:" + lastError;
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string fileString = FileDialogHelper.OpenFile(true);
            this.txtFile.Text = fileString;

            DisplayFiles();
        }

        /// <summary>
        /// 显示文件
        /// </summary>
        public void DisplayFiles()
        {
            string[] filesArray = this.txtFile.Text.Split(',');
            if (filesArray != null && filesArray.Length > 0)
            {
                foreach (string filePath in filesArray)
                {
                    if (!fileList.Contains(filePath))
                    {
                        fileList.Add(filePath);
                        fileStatus.Add(filePath, "");
                    }
                }
                BindData();
            }          
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            #region 检查
            if (fileList.Count == 0)
            {
                MessageDxUtil.ShowTips("请选择文件");
                this.txtFile.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(this.AttachmentDirectory))
            {
                MessageDxUtil.ShowTips("请设置目录分类");
                return;
            }
            #endregion

            if (!worker.IsBusy)
            {            
                this.btnBrowse.Enabled = false;
                this.btnUpload.Enabled = false;
                this.txtFile.Text = "";

                this.progressBar1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                object argument = this.chkNotCopyFile.Checked;
                worker.RunWorkerAsync(argument);
            }
        }

        private void FrmUploadFile_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BindData();
            }
        }

        private void BindData()
        {
            DataTable dt = DataTableHelper.CreateTable("序号|int,文件名称,状态");
            int i = 1;
            foreach (string key in fileStatus.Keys)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i++;
                dr[1] = key;
                dr[2] = fileStatus[key];
                dt.Rows.Add(dr);
            }
            this.winGridView1.AddColumnAlias("序号", "序号");
            this.winGridView1.AddColumnAlias("文件名称", "文件名称");
            this.winGridView1.AddColumnAlias("状态", "状态");

            this.winGridView1.DataSource = dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtFile.Text = "";
            fileList.Clear();
            fileStatus.Clear();

            BindData();
        }

        private void FrmUploadFile_DragDrop(object sender, DragEventArgs e)
        {
            var list = (String[])e.Data.GetData(DataFormats.FileDrop);
            if (list.Length > 0)
            {
                this.txtFile.Text = string.Join(",", list);

                DisplayFiles();
            }
            else
            {
                MessageUtil.ShowError("请选择文件");
            }
        }

        private void FrmUploadFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
