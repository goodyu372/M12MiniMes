using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Attachment.Entity;
using WHC.Attachment.IDAL;
using WHC.Pager.Entity;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using FluentFTP;
using System.Net;

namespace WHC.Attachment.BLL
{
    /// <summary>
    /// 上传文件信息
    /// </summary>
	public class FileUpload : BaseBLL<FileUploadInfo>
    {
        AppConfig config = new AppConfig();

        public FileUpload() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }


        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUser(string userId)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetAllByUser(userId);
        }
               
        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="category">附件分类：个人附件，业务附件</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUser(string userId, string category, PagerInfo pagerInfo)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetAllByUser(userId, category, pagerInfo);
        }

        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGUID(string attachmentGUID, PagerInfo pagerInfo)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetByAttachGUID(attachmentGUID, pagerInfo);
        }
                        
        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGUID(string attachmentGUID)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetByAttachGUID(attachmentGUID);
        }

        /// <summary>
        /// 根据文件的相对路径，删除文件
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public bool DeleteByFilePath(string relativeFilePath, string userId)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.DeleteByFilePath(relativeFilePath, userId);
        }

        /// <summary>
        /// 根据Owner获取对应的附件列表
        /// </summary>
        /// <param name="ownerID">拥有者ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwner(string ownerID)
        {
            string condition = string.Format("Owner_ID ='{0}' ", ownerID);
            return base.Find(condition);
        }

        /// <summary>
        /// 根据Owner获取对应的附件列表
        /// </summary>
        /// <param name="ownerID">拥有者ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwner(string ownerID, PagerInfo pagerInfo)
        {
            string condition = string.Format("Owner_ID ='{0}' ", ownerID);
            return base.FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// 根据Owner获取对应的附件列表
        /// </summary>
        /// <param name="ownerID">拥有者ID</param>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwnerAndAttachGUID(string ownerID, string attachmentGUID)
        {
            string condition = string.Format("Owner_ID ='{0}' AND AttachmentGUID='{1}' ", ownerID, attachmentGUID);
            return base.Find(condition);
        }

        /// <summary>
        /// 根据Owner获取对应的附件列表
        /// </summary>
        /// <param name="ownerID">拥有者ID</param>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwnerAndAttachGUID(string ownerID, string attachmentGUID, PagerInfo pagerInfo)
        {
            string condition = string.Format("Owner_ID ='{0}' AND AttachmentGUID='{1}' ", ownerID, attachmentGUID);
            return base.FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// 根据附件组GUID获取对应的文件名列表，方便列出文件名
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns>返回ID和文件名的列表</returns>
        public Dictionary<string, string> GetFileNames(string attachmentGUID)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetFileNames(attachmentGUID);
        }

        /// <summary>
        /// 标记为删除（不直接删除)
        /// </summary>
        /// <param name="id">文件的ID</param>
        /// <returns></returns>
        public bool SetDeleteFlag(string id)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.SetDeleteFlag(id);
        }

        /// <summary>
        /// 删除指定的ID记录，如果是相对目录的文件则移除文件到DeletedFiles文件夹里面
        /// </summary>
        /// <param name="key">记录ID</param>
        /// <returns></returns>
        public override bool Delete(object key, DbTransaction trans = null)
        {
            //删除记录前，需要把文件移动到删除目录下面
            FileUploadInfo info = FindByID(key, trans);
            DeleteFile(info);

            return base.Delete(key, trans);
        }

        /// <summary>
        /// 删除指定OwnerID的数据记录
        /// </summary>
        /// <param name="owerID">所属者的ID</param>
        /// <returns></returns>
        public bool DeleteByOwerID(string owerID)
        {
            string condition = string.Format("Owner_ID ='{0}' ", owerID);
            List<FileUploadInfo> list = base.Find(condition);
            foreach (FileUploadInfo info in list)
            {
                Delete(info.ID);
            }
            return true;
        }

        /// <summary>
        /// 删除指定Attachment_GUID的数据记录
        /// </summary>
        /// <param name="attachment_GUID">附件的attachmentGUID</param>
        /// <returns></returns>
        public bool DeleteByAttachGUID(string attachment_GUID)
        {
            string condition = string.Format("AttachmentGUID ='{0}' ", attachment_GUID);
            List<FileUploadInfo> list = base.Find(condition);
            foreach (FileUploadInfo info in list)
            {
                Delete(info.ID);
            }
            return true;
        }


        /// <summary>
        /// 上传文件（根据配置文件选择合适的上传方式）
        /// </summary>
        /// <param name="info">文件信息（包含流数据）</param>
        /// <returns></returns>
        public CommonResult Upload(FileUploadInfo info)
        {
            string uploadType = config.AppConfigGet("AttachmentUploadType");
            if(string.IsNullOrEmpty(uploadType))
            {
                return UploadByNormal(info);
            }
            else if(uploadType.Equals("ftp", StringComparison.OrdinalIgnoreCase))
            {
                return UploadByFTP(info);
            }
            else
            {
                throw new ArgumentException("AttachmentUploadType配置指定了无效的值， 请置空或者填写ftp。");
            }
        }

        /// <summary>
        /// 获取配置的FTP配置参数
        /// </summary>
        /// <returns></returns>
        private FTPInfo GetFTPConfig()
        {
            var ftp_server = config.AppConfigGet("ftp_server");
            var ftp_user = config.AppConfigGet("ftp_user");
            var ftp_pass = config.AppConfigGet("ftp_password");
            var ftp_baseurl = config.AppConfigGet("ftp_baseurl");

            return new FTPInfo(ftp_server, ftp_user, ftp_pass, ftp_baseurl);
        }

                
        /// <summary>
        /// 上传文件(以FTP方式上传）
        /// </summary>
        /// <param name="info">文件信息（包含流数据）</param>
        /// <returns></returns>
        public CommonResult UploadByFTP(FileUploadInfo info)
        {
            CommonResult result = new CommonResult();

            try
            {
                //构造对应的FTP辅助类
                var ftpInfo = GetFTPConfig();
                if (!string.IsNullOrEmpty(ftpInfo.BaseUrl))
                {
                    info.BasePath = ftpInfo.BaseUrl;
                }

                //使用FluentFTP操作FTP文件
                FtpClient client = new FtpClient(ftpInfo.Server, ftpInfo.User, ftpInfo.Password);

                //如果配置指定了端口，则使用特定端口
                if (!string.IsNullOrEmpty(ftpInfo.Server) && ftpInfo.Server.Contains(":"))
                {
                    string port = ftpInfo.Server.Split(':')[1];
                    if(!string.IsNullOrEmpty(port))
                    {
                        client.Port = port.ToInt32();
                    }
                }


                //使用FTP上传获得的相对路径
                string category = info.Category;
                if (string.IsNullOrEmpty(category))
                {
                    category = "Photo";
                }

                //确定日期时间目录（格式：yyyy-MM），不存在则创建
                string savePath = string.Format("/{0}-{1:D2}/{2}", DateTime.Now.Year, DateTime.Now.Month, category);
                bool isExistDir = client.DirectoryExists(savePath);
                if(!isExistDir)
                {
                    client.CreateDirectory(savePath);
                }

                //使用FTP上传文件
                //避免文件重复，使用GUID命名
                var ext = FileUtil.GetExtension(info.FileName);
                var newFileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ext);//FileUtil.GetFileName(file);

                savePath = savePath.UriCombine(newFileName);
                bool uploaded = client.Upload(info.FileData, savePath, FtpExists.Overwrite, true);

                //成功后，写入数据库
                if (uploaded)
                {
                    //记录返回值
                    result.Data1 = info.BasePath.UriCombine(savePath);
                    result.Data2 = savePath;

                    info.SavePath = savePath;
                    info.AddTime = DateTime.Now;

                    bool success = base.Insert(info);
                    if (success)
                    {
                        result.Success = success;
                    }
                    else
                    {
                        result.ErrorMessage = "数据写入数据库出错。";
                    }
                }
                else
                {
                    result.ErrorMessage = "文件上传不成功";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 上传文件（以文件方式上传）
        /// </summary>
        /// <param name="info">文件信息（包含流数据）</param>
        /// <returns></returns>
        public CommonResult UploadByNormal(FileUploadInfo info)
        {            
            CommonResult result = new CommonResult();

            try
            {
                #region 确定相对目录，然后上传文件

                string relativeSavePath = "";

                //如果上传的时候 ，指定了基础路径，那么就不需修改
                if (string.IsNullOrEmpty(info.BasePath))
                {
                    //如果没指定基础路径，则以配置为主，如果没有配置项AttachmentBasePath，默认一个相对目录
                    string AttachmentBasePath = config.AppConfigGet("AttachmentBasePath");//配置的基础路径
                    if (string.IsNullOrEmpty(AttachmentBasePath))
                    {
                        //默认以根目录下的UploadFiles目录为上传目录， 例如"C:\SPDTPatientMisService\UploadFiles";
                        AttachmentBasePath = "UploadFiles";//Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
                    }
                    info.BasePath = AttachmentBasePath;

                    //如果没指定基础路径,就表明文件须上传
                    relativeSavePath = UploadFile(info);
                }
                else
                {
                    //如果指定了基础路径，那么属于Winform本地程序复制链接，不需要文件上传,相对路径就是文件名
                    relativeSavePath = info.FileName;
                }

                #endregion

                if (!string.IsNullOrEmpty(relativeSavePath))
                {
                    info.SavePath = relativeSavePath.Replace('\\', '/');
                    info.AddTime = DateTime.Now;

                    bool success = base.Insert(info);
                    if (success)
                    {
                        result.Success = success;
                    }
                    else
                    {
                        result.ErrorMessage = "数据写入数据库出错。";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 将两个路径组合一个合适的路径
        /// </summary>
        /// <param name="path1">路径1</param>
        /// <param name="path2">追加的路径</param>
        /// <returns></returns>
        private string PathCombine(string path1, string path2)
        {
            if (Path.IsPathRooted(path2))
            {
                path2 = path2.TrimStart(Path.DirectorySeparatorChar);
                path2 = path2.TrimStart(Path.AltDirectorySeparatorChar);
            }

            return Path.Combine(path1, path2);
        }

        /// <summary>
        /// 对附件组包含的文件进行归档处理，归档文件重新移动到指定的归档目录中
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <param name="basePath">基础路径</param>
        /// <param name="archiveCategory">归档目录</param>
        /// <returns></returns>
        public CommonResult ArchiveFile(string attachmentGUID, string basePath, string archiveCategory)
        {
            var result = new CommonResult();
            if(string.IsNullOrEmpty(basePath))
            {
                result.ErrorMessage = "基础路径basePath没有指定，无法归档";
                return result;
            }

            var fileList = BLLFactory<FileUpload>.Instance.GetByAttachGUID(attachmentGUID);
            foreach (var info in fileList)
            {
                if (info != null && !string.IsNullOrEmpty(info.SavePath))
                {
                    if (!string.IsNullOrEmpty(info.BasePath))
                    {
                        var url = info.BasePath.UriCombine(info.SavePath);
                        if (info.BasePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        {
                            result.ErrorMessage = "HTTP路径文件无法归档";
                        }
                        else if (info.BasePath.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                        {
                            result.ErrorMessage = "FTP路径文件无法归档";
                        }
                        else
                        {
                            //相对目录或者本地目录可以归档

                            //归档实际路径
                            string realArchiveBaseDir = basePath;
                            if (!IsPhysicalPath(realArchiveBaseDir))
                            {
                                //如果没指定基础路径，则以配置为主，如果没有配置项AttachmentBasePath，默认一个相对目录
                                string AttachmentBasePath = config.AppConfigGet("AttachmentBasePath");//配置的基础路径
                                if (string.IsNullOrEmpty(AttachmentBasePath))
                                {
                                    //默认以根目录下的UploadFiles目录为上传目录， 例如"C:\SPDTPatientMisService\UploadFiles";
                                    AttachmentBasePath = AppDomain.CurrentDomain.BaseDirectory.PathCombine("UploadFiles");
                                }
                                //和附件上传目录，组合为一个正确路径
                                realArchiveBaseDir = AttachmentBasePath.PathCombine(realArchiveBaseDir);
                            }
                            //包含分类目录的路径
                            string realArchivePath = realArchiveBaseDir.PathCombine(archiveCategory);

                            //实际文件路径                            
                            string serverRealPath = PathCombine(info.BasePath, info.SavePath.Replace('\\', '/'));
                            if (!IsPhysicalPath(serverRealPath))
                            {
                                //如果是相对目录，加上当前程序的目录才能定位文件地址
                                serverRealPath = AppDomain.CurrentDomain.BaseDirectory.PathCombine(serverRealPath);
                            }

                            try
                            {
                                //确保移动的目录存在，不存在则创建
                                DirectoryUtil.AssertDirExist(realArchivePath);
                                //移动后的路径
                                var movedFilePath = realArchivePath.PathCombine(info.FileName);

                                if (File.Exists(serverRealPath) && !File.Exists(movedFilePath))
                                {
                                    //移动文件
                                    File.Move(serverRealPath, movedFilePath);

                                    //修改数据库的文件路径并更新
                                    string filePath = movedFilePath.Replace(realArchiveBaseDir, "");
                                    Hashtable ht = new Hashtable();
                                    ht.Add("BasePath", realArchiveBaseDir);
                                    ht.Add("SavePath", filePath);
                                    baseDal.UpdateFields(ht, info.ID);
                                }
                            }
                            catch(Exception ex)
                            {
                                var tips = "文件归档出现错误：" + ex.Message;
                                LogTextHelper.Error(tips);
                                result.ErrorMessage = tips;
                            }
                        }
                    }
                }
            }
            result.Success = true;

            return result;
        }

        /// <summary>
        /// 判断集合中的附件列表是否需要归档，需要返回True，否则为False
        /// </summary>
        /// <param name="attachmentGUIDList">附件组GUID列表</param>
        /// <returns></returns>
        public bool NeedToArchive(List<string> attachmentGUIDList)
        {
            bool result = false;
            if(attachmentGUIDList != null)
            {
                foreach(string guid in attachmentGUIDList)
                {
                    var list = GetByAttachGUID(guid);
                    foreach(var info in list)
                    {
                        if (!IsPhysicalPath(info.BasePath))
                        {
                            result = true;//如果有任意路径非物理路径
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取单一的文件数据（包含文件字节数据）,如果指定了图片的高度，宽度，那么下载缩略图
        /// </summary>
        /// <param name="id">附件记录的ID</param>
        /// <returns></returns>
        public FileUploadInfo Download(string id, int? width = null, int? height = null)
        {
            FileUploadInfo info = FindByID(id);
            try
            {
                if (info != null && !string.IsNullOrEmpty(info.SavePath))
                {
                    if (!string.IsNullOrEmpty(info.BasePath))
                    {
                        var url = info.BasePath.UriCombine(info.SavePath);
                        if (info.BasePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        {
                            var client = new NewWebClient();
                            info.FileData = client.DownloadData(url);
                        }
                        else if (info.BasePath.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                        {
                            //获取FTP配置信息
                            var ftpInfo = GetFTPConfig();

                            var client = new NewWebClient();
                            client.Credentials = new NetworkCredential(ftpInfo.User, ftpInfo.Password);
                            info.FileData = client.DownloadData(url);
                        }
                        else
                        {
                            string serverRealPath = PathCombine(info.BasePath, info.SavePath.Replace('\\', '/'));
                            if (!IsPhysicalPath(serverRealPath))
                            {
                                //如果是相对目录，加上当前程序的目录才能定位文件地址
                                serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                            }

                            if (File.Exists(serverRealPath))
                            {
                                byte[] bytes = FileUtil.FileToBytes(serverRealPath);
                                if (width.HasValue && height.HasValue)
                                {
                                    //控制图片的最大尺寸
                                    int newwidth = width.Value > 1024 ? 1024 : width.Value;
                                    int newheight = height.Value > 768 ? 768 : height.Value;

                                    Image image = ImageHelper.BitmapFromBytes(bytes);
                                    Image smallImage = ImageHelper.ChangeImageSize(image, newwidth, newheight);
                                    info.FileData = ImageHelper.ImageToBytes(smallImage);
                                }
                                else
                                {
                                    info.FileData = bytes;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
            return info;
        }

        /// <summary>
        /// 根据文件名获取单一的文件数据（包含文件字节数据）,如果指定了图片的高度，宽度，那么下载缩略图
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public FileUploadInfo DownloadByPath(string filePath, int? width = null, int? height = null)
        {
            FileUploadInfo info = null;
            if (File.Exists(filePath))
            {
                try
                {
                    info = new FileUploadInfo();
                    info.FileExtend = FileUtil.GetExtension(filePath);
                    info.FileName = FileUtil.GetFileName(filePath);
                    info.FileSize = FileUtil.GetFileSize(filePath);
                    info.AddTime = FileUtil.GetFileCreateTime(filePath);

                    byte[] bytes = FileUtil.FileToBytes(filePath);
                    if (width.HasValue && height.HasValue)
                    {
                        //控制图片的最大尺寸
                        int newwidth = width.Value > 1024 ? 1024 : width.Value;
                        int newheight = height.Value > 768 ? 768 : height.Value;

                        Image image = ImageHelper.BitmapFromBytes(bytes);
                        Image smallImage = ImageHelper.ChangeImageSize(image, newwidth, newheight);
                        info.FileData = ImageHelper.ImageToBytes(smallImage);
                    }
                    else
                    {
                        info.FileData = bytes;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            return info;
        }

        /// <summary>
        /// 列出目录的文件路径或者目录
        /// </summary>
        /// <param name="filePath">指定文件路径</param>
        /// <param name="isDir">是否列出目录，默认为false，返回文件列表</param>
        /// <returns></returns>
        public List<string> GetFileOrDirList(string filePath, bool isDir = false)
        {
            List<string> list = new List<string>();

            try
            {
                var array = isDir ? DirectoryUtil.GetDirectories(filePath) : DirectoryUtil.GetFileNames(filePath);
                if (array != null)
                {
                    list.AddRange(array);
                }
            }
            catch (Exception ex)
            {
                LogTextHelper.Error("GetFileOrDirList列出目录的文件路径出错:" + ex.Message);
            }

            return list;
        }


        /// <summary>
        /// 检查输入及组合路径
        /// </summary>
        /// <param name="info">上传文件信息</param>
        /// <returns></returns>
        public string GetFilePath(FileUploadInfo info)
        {
            string fileName = info.FileName;
            string category = info.Category;

            if (string.IsNullOrEmpty(category))
            {
                category = "Photo";
            }

            //以类别进行目录区分,并增加日期
            string uploadFolder = string.Format("{0}/{1}-{2:D2}/{3}", info.BasePath, DateTime.Now.Year, DateTime.Now.Month, category);
            string realFolderPath = uploadFolder;

            //如果目录为相对目录，那么转换为实际目录，并创建
            if (!IsPhysicalPath(uploadFolder))
            {
                realFolderPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, uploadFolder);

                if (!Directory.Exists(realFolderPath))
                {
                    Directory.CreateDirectory(realFolderPath);
                }
            }

            //返回相对目录
            string filePath = PathCombine(uploadFolder, fileName);
            return filePath;
        }

        /// <summary>
        /// 是否为绝对地址（该方法代替Path.IsPathRooted进行判断，因Path.IsPathRooted把路径包含\\也作为物理路径
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        private bool IsPhysicalPath(string path)
        {
            bool result = false;
            if(!string.IsNullOrEmpty(path) && path.Contains(":"))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据attachmentGUID的参数获取对应的第一个文件路径
        /// </summary>
        /// <param name="attachmentGUID">附件的attachmentGUID</param>
        /// <returns></returns>
        public string GetFirstFilePath(string attachmentGUID)
        {
            string serverRealPath = "";
            if (!string.IsNullOrEmpty(attachmentGUID))
            {
                List<FileUploadInfo> fileList = BLLFactory<FileUpload>.Instance.GetByAttachGUID(attachmentGUID);
                if (fileList != null && fileList.Count > 0)
                {
                    FileUploadInfo fileInfo = fileList[0];
                    if (fileInfo != null)
                    {
                        serverRealPath = PathCombine(fileInfo.BasePath, fileInfo.SavePath.Replace('\\', '/'));
                        if (!IsPhysicalPath(serverRealPath))
                        {
                            //如果是相对目录，加上当前程序的目录才能定位文件地址
                            serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                        }
                    }
                }
            }
            return serverRealPath;
        }


        /// <summary>
        /// 把文件保存到指定目录,并返回相对基础目录的路径
        /// </summary>
        /// <param name="info">文件上传信息</param>
        /// <returns>成功返回相对基础目录的路径，否则返回空字符</returns>
        private string UploadFile(FileUploadInfo info)
        {
            //检查输入及组合路径
            string filePath = GetFilePath(info);
            string relativeSavePath = filePath.Replace(info.BasePath, "").Replace('\\', '/');//替换掉起始目录即为相对路径


            string serverRealPath = filePath;
            if (!IsPhysicalPath(filePath))
            {
                serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, filePath);
            }

            //通过实际文件名去查找对应的文件名称
            serverRealPath = GetRightFileName(serverRealPath, 1);

            //当文件已存在，而重新命名时，修改Filename及relativeSavePath
            relativeSavePath = relativeSavePath.Substring(0, relativeSavePath.LastIndexOf(info.FileName)) + FileUtil.GetFileName(serverRealPath);
            info.FileName = FileUtil.GetFileName(serverRealPath);

            //根据实际文件名创建文件
            FileUtil.CreateFile(serverRealPath, info.FileData);

            bool success = FileUtil.IsExistFile(serverRealPath);
            if (success)
            {
                return relativeSavePath;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 查找文件名，如果存在则在文件名后面加(i)，i从1开始计算
        /// </summary>
        /// <param name="originalFilePath">原文件名</param>
        /// <param name="i">计数值</param>
        /// <returns></returns>
        private string GetRightFileName(string originalFilePath, int i)
        {
            bool fileExist = FileUtil.IsExistFile(originalFilePath);
            if (fileExist)
            {
                string onlyFileName = FileUtil.GetFileName(originalFilePath, true);
                int idx = originalFilePath.LastIndexOf(onlyFileName);
                string firstPath = originalFilePath.Substring(0, idx);
                string onlyExt = FileUtil.GetExtension(originalFilePath);
                string newFileName = string.Format("{0}{1}({2}){3}", firstPath, onlyFileName, i, onlyExt);
                if (FileUtil.IsExistFile(newFileName))
                {
                    i++;
                    return GetRightFileName(originalFilePath, i);
                }
                else
                {
                    return newFileName;
                }
            }
            else
            {
                return originalFilePath;
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        /// <param name="info">文件信息</param>
        private void DeleteFile(FileUploadInfo info)
        {
            try
            {
                if (info != null && !string.IsNullOrEmpty(info.SavePath))
                {
                    if (info.BasePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                    {

                    }
                    else if (info.BasePath.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                    {
                    }
                    else
                    {
                        //普通文件删除操作
                        string serverRealPath = PathCombine(info.BasePath, info.SavePath.Replace('\\', '/'));
                        if (!IsPhysicalPath(serverRealPath))
                        {
                            //如果是相对目录，加上当前程序的目录才能定位文件地址
                            serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);

                            //如果是相对目录的，移动到删除目录里面
                            if (File.Exists(serverRealPath))
                            {
                                try
                                {
                                    string deletedPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, Path.Combine(info.BasePath, "DeletedFiles"));
                                    DirectoryUtil.AssertDirExist(deletedPath);

                                    string newFilePath = PathCombine(deletedPath, info.FileName);
                                    newFilePath = GetRightFileName(newFilePath, 1);
                                    File.Move(serverRealPath, newFilePath);
                                }
                                catch (Exception ex)
                                {
                                    LogTextHelper.Error(ex);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //记录删除操作错误
                LogHelper.Error(ex);
            }

        }

        /// <summary>
        /// 根据唯一的GUID获取一个唯一的记录
        /// </summary>
        /// <param name="attachmentGUID">附件GUID</param>
        /// <returns></returns>
        public FileUploadInfo FindSingleByAttachGUID(string attachmentGUID)
        {
            string condition = string.Format("AttachmentGUID = '{0}'", attachmentGUID);
            return baseDal.FindSingle(condition);
        }
    }

    /// <summary>
    /// 为WebClient增加超时时间
    /// <para>从WebClient派生一个新的类，重载GetWebRequest方法</para>
    /// </summary>
    internal class NewWebClient : WebClient
    {
        private int _timeout;

        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public NewWebClient()
        {
            this._timeout = 10000;
        }

        public NewWebClient(int timeout)
        {
            this._timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
