using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Attachment.Entity;
using WHC.Attachment.BLL;
using WHC.Framework.BaseUI;
using System.Diagnostics;
using WHC.Framework.Language;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// 附件信息管理
    /// </summary>
    public partial class FrmAttachmentGroupView : BaseForm
    {
        #region public属性变量

        /// <summary>
        /// 附件组所属的记录ID，如属于某个主表记录的ID
        /// </summary>
        public string OwerId = "";

        /// <summary>
        /// 操作用户ID，当前登录用户
        /// </summary>
        public string UserId = "";

        /// <summary>
        /// 设置附件的存储目录分类
        /// </summary>
        public string AttachmentDirectory = "业务附件";

        /// <summary>
        /// 设置附件组的GUID
        /// </summary>
        public string AttachmentGUID;

        /// <summary>
        /// 是否显示上传按钮
        /// </summary>
        public bool ShowUpload { get; set; }

        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool ShowDelete { get; set; }

        /// <summary>
        /// 是否显示下载按钮
        /// </summary>
        public bool ShowDownload { get; set; }

        #endregion

        /// <summary>
        /// 附件列表中文件后缀名图片列表（自动从本地读取加载）
        /// </summary>
        private Dictionary<string, int> imageDict = new Dictionary<string, int>();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FrmAttachmentGroupView()
        {
            InitializeComponent();

            //初始化字典
            InitDictItem();

            //默认可以操作各个功能
            InitButtonStatus();
        }  
        
        /// <summary>
        /// 初始化字典
        /// </summary>
        private void InitDictItem()
        {
            //大图标 小图标 列表
            var list = new List<string>() { "大图标", "小图标", "列表" };
            this.txtViewType.Properties.Items.Clear();
            foreach (var item in list)
            {
                //对内容进行多语言处理
                var newItem = JsonLanguage.Default.GetString(item);

                this.txtViewType.Properties.Items.Add(newItem);
            }
        }

        /// <summary>
        /// 初始化按钮的显示状态
        /// </summary>
        /// <param name="showUpload">是否显示上传按钮</param>
        /// <param name="showDelete">是否显示删除按钮</param>
        /// <param name="showDownload">是否显示下载按钮</param>
        public void InitButtonStatus(bool showUpload = true, bool showDelete = true, bool showDownload = true)
        {
            this.btnUpload.Visible = showUpload;
            this.btnDelete.Visible = showDelete;
            this.btnDownload.Visible = showDownload;

            this.menuDelete.Visible = showDelete;
            this.menuDownload.Visible = showDownload;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            FrmUploadFile dlg = new FrmUploadFile();
            dlg.UserId = UserId;
            dlg.OwerId = OwerId;
            dlg.AttachmentDirectory = this.AttachmentDirectory;
            dlg.AttachmentGUID = this.AttachmentGUID;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.Show(this);
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();

            ProcessDataSaved(this.btnUpload, new EventArgs());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.CheckedItems.Count == 0)
            {
                MessageDxUtil.ShowTips("请勾选删除的文件！");
                return;
            }

            string lastError = "";
            bool sucess = false;
            foreach (ListViewItem item in this.listView1.CheckedItems)
            {
                if (item != null && item.Tag != null)
                {
                    string id = item.Tag.ToString();
                    try
                    {
                        sucess = BLLFactory<FileUpload>.Instance.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        lastError += ex.Message + "\r\n";
                        LogTextHelper.Error(ex);
                    }
                }
            }
            //MessageDxUtil.ShowTips(sucess ? "删除操作成功" : "操作失败" );
            this.ShowAlertControl(sucess ? "删除操作成功" : "操作失败" );
            ProcessDataSaved(this.btnDelete, new EventArgs());

            BindData();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (this.listView1.CheckedItems.Count == 0)
            {
                MessageDxUtil.ShowTips("请勾选下载的文件！");
                return;
            }

            string path = FileDialogHelper.OpenDir();
            if (!string.IsNullOrEmpty(path))
            {
                DirectoryUtil.AssertDirExist(Path.GetDirectoryName(path));

                #region 下载保存图片
                bool hasError = false;

                foreach (ListViewItem item in this.listView1.CheckedItems)
                {
                    if (item != null && item.Tag != null)
                    {
                        string id = item.Tag.ToString();

                        try
                        {
                            FileUploadInfo fileInfo = BLLFactory<FileUpload>.Instance.Download(id);
                            if (fileInfo != null && fileInfo.FileData != null)
                            {
                                string filePath = Path.Combine(path, fileInfo.FileName);
                                FileUtil.CreateFile(filePath, fileInfo.FileData);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogTextHelper.Error(ex);
                            hasError = true;
                        }
                    }
                }
                #endregion

                if (hasError)
                {
                    MessageDxUtil.ShowError("保存文件出现错误。具体请查看日志文件！");
                }
                else
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
        }

        private void FrmAttachmentGroupView_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BindData();
            }
        }

        private void BindData()
        {
            this.listView1.CheckBoxes = chkSelect.Checked;
            this.listView1.Items.Clear();
            this.imageList1.Images.Clear();
            this.imageList2.Images.Clear();

            List<FileUploadInfo> fileList = BLLFactory<FileUpload>.Instance.GetByAttachGUID(this.AttachmentGUID);

            int k = 0;
            Icon icon = null;
            foreach (FileUploadInfo fileInfo in fileList)
            {
                string file = fileInfo.FileName;
                string extension = FileUtil.GetExtension(file);

                #region 取缩略图存到 imageList1 的操作
                //如果是图片，取得它的图片数据作为缩略图
                bool isImage = MyHelper.IsImageFile(fileInfo.FileExtend);
                if (isImage)
                {
                    try
                    {
                        FileUploadInfo tmpInfo = BLLFactory<FileUpload>.Instance.Download(fileInfo.ID, 48, 48);
                        if (tmpInfo != null && tmpInfo.FileData != null)
                        {
                            this.imageList1.Images.Add(ImageHelper.BitmapFromBytes(tmpInfo.FileData));
                            this.imageList2.Images.Add(ImageHelper.BitmapFromBytes(tmpInfo.FileData));
                        }
                        else
                        {
                            icon = IconReaderHelper.ExtractIconForExtension(extension, true); //大图标
                            this.imageList1.Images.Add(icon);
                            icon = IconReaderHelper.ExtractIconForExtension(extension, false);//小图标
                            this.imageList2.Images.Add(icon);
                        }
                    }
                    catch
                    {
                        icon = IconReaderHelper.ExtractIconForExtension(extension, true); //大图标
                        this.imageList1.Images.Add(icon);
                        icon = IconReaderHelper.ExtractIconForExtension(extension, false);//小图标
                        this.imageList2.Images.Add(icon);
                    }
                }
                else
                {
                    icon = IconReaderHelper.ExtractIconForExtension(extension, true); //大图标
                    this.imageList1.Images.Add(icon);
                    icon = IconReaderHelper.ExtractIconForExtension(extension, false);//小图标
                    this.imageList2.Images.Add(icon);
                }
                #endregion
            }

            int i = 0;
            foreach (FileUploadInfo fileInfo in fileList)
            {
                ListViewItem item = listView1.Items.Add(fileInfo.FileName);

                double fileSize = ConvertHelper.ToDouble(fileInfo.FileSize / 1024, 1);
                item.SubItems.Add(fileSize.ToString("#,#KB"));
                item.SubItems.Add(fileInfo.AddTime.ToShortDateString());
                item.ImageIndex = i++;
                item.Tag = fileInfo.ID;
            }
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //对中文进行处理才能适应多语言
            var bigIcon = JsonLanguage.Default.GetString("大图标");
            var smallIcon = JsonLanguage.Default.GetString("小图标");

            if (txtViewType.Text == bigIcon)
            {
                this.listView1.View = View.LargeIcon;
            }
            else if (txtViewType.Text == smallIcon)
            {
                this.listView1.View = View.SmallIcon;
            }
            else
            {
                this.listView1.View = View.Details;
            }
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.listView1.CheckBoxes = chkSelect.Checked;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void DownloadOpen(string id, string name)
        {                
            string tempFilePath = "";
            try
            {
                FileUploadInfo fileInfo = BLLFactory<FileUpload>.Instance.Download(id);
                if (fileInfo != null && fileInfo.FileData != null)
                {
                    string extension = fileInfo.FileExtend.ToLower();
                    string fileName = fileInfo.FileName;

                    this.SendToBack();

                    FileUtil.OpenFileInProcess(fileName, fileInfo.FileData);

                    this.BringToFront();
                }
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError("下载文件出现错误。具体如下：\r\n" + ex.Message);
            }
            finally
            {
                bool flag2 = File.Exists(tempFilePath);
                if (flag2)
                {
                    File.Delete(tempFilePath);
                }
            }
        }
        private void DownloadOrViewFile(string id, string name)
        {
            try
            {
                FileUploadInfo fileInfo = BLLFactory<FileUpload>.Instance.Download(id);
                if (fileInfo != null && fileInfo.FileData != null)
                {
                    string extension = fileInfo.FileExtend.ToLower();
                    bool isImage = MyHelper.IsImageFile(extension);
                    if (isImage)
                    {
                        FrmPicturePreview frm = new FrmPicturePreview();
                        Bitmap bitmap = ImageHelper.BitmapFromBytes(fileInfo.FileData);
                        frm.ImageObj = bitmap;
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (extension.Contains(".pdf"))
                        {
                            FrmPDFView dlg = new FrmPDFView();
                            dlg.Extension = extension;
                            dlg.Stream = FileUtil.BytesToStream(fileInfo.FileData);
                            dlg.ShowDialog();
                        }
                        else if (extension.Contains(".xls") || extension.Contains(".xlsx") || extension.Contains(".csv"))
                        {
                            FrmExcelView dlg = new FrmExcelView();
                            dlg.Extension = extension;
                            dlg.Stream = FileUtil.BytesToStream(fileInfo.FileData);
                            dlg.ShowDialog();
                        }
                        else if (extension.Contains(".doc") || extension.Contains(".docx") || extension.Contains(".rtf"))
                        {
                            FrmWordView dlg = new FrmWordView();
                            dlg.Extension = extension;
                            dlg.Stream = FileUtil.BytesToStream(fileInfo.FileData);
                            dlg.ShowDialog();
                        }
                        else
                        {
                            #region 非图片文件下载到本地
                            string saveFile = FileDialogHelper.SaveFile(name);
                            if (!string.IsNullOrEmpty(saveFile))
                            {
                                FileUtil.CreateFile(saveFile, fileInfo.FileData);
                                if (File.Exists(saveFile))
                                {
                                    if (MessageDxUtil.ShowYesNoAndTips("文件下载成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        System.Diagnostics.Process.Start(saveFile);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError("保存文件出现错误。具体如下：\r\n" + ex.Message);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
            if (item != null && item.Tag != null)
            {
                string id = item.Tag.ToString();
                DownloadOrViewFile(id, item.Text);   

                //DownloadOpen(id, item.Text);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkStats = chkSelectAll.Checked;
            this.listView1.CheckBoxes = checkStats;
            foreach (ListViewItem item in this.listView1.Items)
            {
                item.Checked = checkStats;
            }
        }

        private void menuDownload_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                if (item != null && item.Tag != null)
                {
                    string id = item.Tag.ToString();
                    DownloadOrViewFile(id, item.Text);
                }
            }
        }



        private void menuDelete_Click(object sender, EventArgs e)
        {
            bool sucess = false;
            string lastError = "";
            try
            {
                if (this.listView1.CheckedItems.Count > 0)
                {
                    foreach (ListViewItem item in this.listView1.CheckedItems)
                    {
                        if (item != null && item.Tag != null)
                        {
                            string id = item.Tag.ToString();
                            sucess = BLLFactory<FileUpload>.Instance.Delete(id);
                        }
                    }
                }
                //否则是右键选择的，则删除选定的
                else if (this.listView1.SelectedItems.Count == 1)
                {
                    var item = this.listView1.SelectedItems[0];
                    if (item != null && item.Tag != null)
                    {
                        string id = item.Tag.ToString();
                        sucess = BLLFactory<FileUpload>.Instance.Delete(id);
                    }

                }
            }
            catch (Exception ex)
            {
                lastError += ex.Message + "\r\n";
                LogTextHelper.Error(ex);
            }

            this.ShowAlertControl(sucess ? "操作成功" : "操作失败" );
            //MessageDxUtil.ShowTips(sucess ? "删除操作成功" : "操作失败" );
            ProcessDataSaved(this.btnDelete, new EventArgs());
            BindData();
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void FrmAttachmentGroupView_DragDrop(object sender, DragEventArgs e)
        {
            var list = (String[])e.Data.GetData(DataFormats.FileDrop);
            if (list.Length > 0)
            {
                FrmUploadFile dlg = new FrmUploadFile();
                dlg.UserId = UserId;
                dlg.OwerId = OwerId;
                dlg.AttachmentDirectory = this.AttachmentDirectory;
                dlg.AttachmentGUID = this.AttachmentGUID;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

                //显示相关的文件
                dlg.txtFile.Text = string.Join(",", list);
                dlg.DisplayFiles();

                dlg.Show();
            }
            else
            {
                MessageUtil.ShowError("请选择文件");
            }
        }

        private void FrmAttachmentGroupView_DragEnter(object sender, DragEventArgs e)
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
