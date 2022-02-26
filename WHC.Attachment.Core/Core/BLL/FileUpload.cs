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
    /// �ϴ��ļ���Ϣ
    /// </summary>
	public class FileUpload : BaseBLL<FileUploadInfo>
    {
        AppConfig config = new AppConfig();

        public FileUpload() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }


        /// <summary>
        /// ��ȡָ���û����ϴ���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUser(string userId)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetAllByUser(userId);
        }
               
        /// <summary>
        /// ��ȡָ���û����ϴ���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="category">�������ࣺ���˸�����ҵ�񸽼�</param>
        /// <param name="pagerInfo">��ҳ��Ϣ</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUser(string userId, string category, PagerInfo pagerInfo)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetAllByUser(userId, category, pagerInfo);
        }

        /// <summary>
        /// ��ȡָ��������GUID�ĸ�����Ϣ
        /// </summary>
        /// <param name="attachmentGUID">������GUID</param>
        /// <param name="pagerInfo">��ҳ��Ϣ</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGUID(string attachmentGUID, PagerInfo pagerInfo)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetByAttachGUID(attachmentGUID, pagerInfo);
        }
                        
        /// <summary>
        /// ��ȡָ��������GUID�ĸ�����Ϣ
        /// </summary>
        /// <param name="attachmentGUID">������GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGUID(string attachmentGUID)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetByAttachGUID(attachmentGUID);
        }

        /// <summary>
        /// �����ļ������·����ɾ���ļ�
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public bool DeleteByFilePath(string relativeFilePath, string userId)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.DeleteByFilePath(relativeFilePath, userId);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="ownerID">ӵ����ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwner(string ownerID)
        {
            string condition = string.Format("Owner_ID ='{0}' ", ownerID);
            return base.Find(condition);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="ownerID">ӵ����ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwner(string ownerID, PagerInfo pagerInfo)
        {
            string condition = string.Format("Owner_ID ='{0}' ", ownerID);
            return base.FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="ownerID">ӵ����ID</param>
        /// <param name="attachmentGUID">������GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwnerAndAttachGUID(string ownerID, string attachmentGUID)
        {
            string condition = string.Format("Owner_ID ='{0}' AND AttachmentGUID='{1}' ", ownerID, attachmentGUID);
            return base.Find(condition);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="ownerID">ӵ����ID</param>
        /// <param name="attachmentGUID">������GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwnerAndAttachGUID(string ownerID, string attachmentGUID, PagerInfo pagerInfo)
        {
            string condition = string.Format("Owner_ID ='{0}' AND AttachmentGUID='{1}' ", ownerID, attachmentGUID);
            return base.FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// ���ݸ�����GUID��ȡ��Ӧ���ļ����б������г��ļ���
        /// </summary>
        /// <param name="attachmentGUID">������GUID</param>
        /// <returns>����ID���ļ������б�</returns>
        public Dictionary<string, string> GetFileNames(string attachmentGUID)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.GetFileNames(attachmentGUID);
        }

        /// <summary>
        /// ���Ϊɾ������ֱ��ɾ��)
        /// </summary>
        /// <param name="id">�ļ���ID</param>
        /// <returns></returns>
        public bool SetDeleteFlag(string id)
        {
            IFileUpload dal = baseDal as IFileUpload;
            return dal.SetDeleteFlag(id);
        }

        /// <summary>
        /// ɾ��ָ����ID��¼����������Ŀ¼���ļ����Ƴ��ļ���DeletedFiles�ļ�������
        /// </summary>
        /// <param name="key">��¼ID</param>
        /// <returns></returns>
        public override bool Delete(object key, DbTransaction trans = null)
        {
            //ɾ����¼ǰ����Ҫ���ļ��ƶ���ɾ��Ŀ¼����
            FileUploadInfo info = FindByID(key, trans);
            DeleteFile(info);

            return base.Delete(key, trans);
        }

        /// <summary>
        /// ɾ��ָ��OwnerID�����ݼ�¼
        /// </summary>
        /// <param name="owerID">�����ߵ�ID</param>
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
        /// ɾ��ָ��Attachment_GUID�����ݼ�¼
        /// </summary>
        /// <param name="attachment_GUID">������attachmentGUID</param>
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
        /// �ϴ��ļ������������ļ�ѡ����ʵ��ϴ���ʽ��
        /// </summary>
        /// <param name="info">�ļ���Ϣ�����������ݣ�</param>
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
                throw new ArgumentException("AttachmentUploadType����ָ������Ч��ֵ�� ���ÿջ�����дftp��");
            }
        }

        /// <summary>
        /// ��ȡ���õ�FTP���ò���
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
        /// �ϴ��ļ�(��FTP��ʽ�ϴ���
        /// </summary>
        /// <param name="info">�ļ���Ϣ�����������ݣ�</param>
        /// <returns></returns>
        public CommonResult UploadByFTP(FileUploadInfo info)
        {
            CommonResult result = new CommonResult();

            try
            {
                //�����Ӧ��FTP������
                var ftpInfo = GetFTPConfig();
                if (!string.IsNullOrEmpty(ftpInfo.BaseUrl))
                {
                    info.BasePath = ftpInfo.BaseUrl;
                }

                //ʹ��FluentFTP����FTP�ļ�
                FtpClient client = new FtpClient(ftpInfo.Server, ftpInfo.User, ftpInfo.Password);

                //�������ָ���˶˿ڣ���ʹ���ض��˿�
                if (!string.IsNullOrEmpty(ftpInfo.Server) && ftpInfo.Server.Contains(":"))
                {
                    string port = ftpInfo.Server.Split(':')[1];
                    if(!string.IsNullOrEmpty(port))
                    {
                        client.Port = port.ToInt32();
                    }
                }


                //ʹ��FTP�ϴ���õ����·��
                string category = info.Category;
                if (string.IsNullOrEmpty(category))
                {
                    category = "Photo";
                }

                //ȷ������ʱ��Ŀ¼����ʽ��yyyy-MM�����������򴴽�
                string savePath = string.Format("/{0}-{1:D2}/{2}", DateTime.Now.Year, DateTime.Now.Month, category);
                bool isExistDir = client.DirectoryExists(savePath);
                if(!isExistDir)
                {
                    client.CreateDirectory(savePath);
                }

                //ʹ��FTP�ϴ��ļ�
                //�����ļ��ظ���ʹ��GUID����
                var ext = FileUtil.GetExtension(info.FileName);
                var newFileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ext);//FileUtil.GetFileName(file);

                savePath = savePath.UriCombine(newFileName);
                bool uploaded = client.Upload(info.FileData, savePath, FtpExists.Overwrite, true);

                //�ɹ���д�����ݿ�
                if (uploaded)
                {
                    //��¼����ֵ
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
                        result.ErrorMessage = "����д�����ݿ����";
                    }
                }
                else
                {
                    result.ErrorMessage = "�ļ��ϴ����ɹ�";
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
        /// �ϴ��ļ������ļ���ʽ�ϴ���
        /// </summary>
        /// <param name="info">�ļ���Ϣ�����������ݣ�</param>
        /// <returns></returns>
        public CommonResult UploadByNormal(FileUploadInfo info)
        {            
            CommonResult result = new CommonResult();

            try
            {
                #region ȷ�����Ŀ¼��Ȼ���ϴ��ļ�

                string relativeSavePath = "";

                //����ϴ���ʱ�� ��ָ���˻���·������ô�Ͳ����޸�
                if (string.IsNullOrEmpty(info.BasePath))
                {
                    //���ûָ������·������������Ϊ�������û��������AttachmentBasePath��Ĭ��һ�����Ŀ¼
                    string AttachmentBasePath = config.AppConfigGet("AttachmentBasePath");//���õĻ���·��
                    if (string.IsNullOrEmpty(AttachmentBasePath))
                    {
                        //Ĭ���Ը�Ŀ¼�µ�UploadFilesĿ¼Ϊ�ϴ�Ŀ¼�� ����"C:\SPDTPatientMisService\UploadFiles";
                        AttachmentBasePath = "UploadFiles";//Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
                    }
                    info.BasePath = AttachmentBasePath;

                    //���ûָ������·��,�ͱ����ļ����ϴ�
                    relativeSavePath = UploadFile(info);
                }
                else
                {
                    //���ָ���˻���·������ô����Winform���س��������ӣ�����Ҫ�ļ��ϴ�,���·�������ļ���
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
                        result.ErrorMessage = "����д�����ݿ����";
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
        /// ������·�����һ�����ʵ�·��
        /// </summary>
        /// <param name="path1">·��1</param>
        /// <param name="path2">׷�ӵ�·��</param>
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
        /// �Ը�����������ļ����й鵵�����鵵�ļ������ƶ���ָ���Ĺ鵵Ŀ¼��
        /// </summary>
        /// <param name="attachmentGUID">������GUID</param>
        /// <param name="basePath">����·��</param>
        /// <param name="archiveCategory">�鵵Ŀ¼</param>
        /// <returns></returns>
        public CommonResult ArchiveFile(string attachmentGUID, string basePath, string archiveCategory)
        {
            var result = new CommonResult();
            if(string.IsNullOrEmpty(basePath))
            {
                result.ErrorMessage = "����·��basePathû��ָ�����޷��鵵";
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
                            result.ErrorMessage = "HTTP·���ļ��޷��鵵";
                        }
                        else if (info.BasePath.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                        {
                            result.ErrorMessage = "FTP·���ļ��޷��鵵";
                        }
                        else
                        {
                            //���Ŀ¼���߱���Ŀ¼���Թ鵵

                            //�鵵ʵ��·��
                            string realArchiveBaseDir = basePath;
                            if (!IsPhysicalPath(realArchiveBaseDir))
                            {
                                //���ûָ������·������������Ϊ�������û��������AttachmentBasePath��Ĭ��һ�����Ŀ¼
                                string AttachmentBasePath = config.AppConfigGet("AttachmentBasePath");//���õĻ���·��
                                if (string.IsNullOrEmpty(AttachmentBasePath))
                                {
                                    //Ĭ���Ը�Ŀ¼�µ�UploadFilesĿ¼Ϊ�ϴ�Ŀ¼�� ����"C:\SPDTPatientMisService\UploadFiles";
                                    AttachmentBasePath = AppDomain.CurrentDomain.BaseDirectory.PathCombine("UploadFiles");
                                }
                                //�͸����ϴ�Ŀ¼�����Ϊһ����ȷ·��
                                realArchiveBaseDir = AttachmentBasePath.PathCombine(realArchiveBaseDir);
                            }
                            //��������Ŀ¼��·��
                            string realArchivePath = realArchiveBaseDir.PathCombine(archiveCategory);

                            //ʵ���ļ�·��                            
                            string serverRealPath = PathCombine(info.BasePath, info.SavePath.Replace('\\', '/'));
                            if (!IsPhysicalPath(serverRealPath))
                            {
                                //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                                serverRealPath = AppDomain.CurrentDomain.BaseDirectory.PathCombine(serverRealPath);
                            }

                            try
                            {
                                //ȷ���ƶ���Ŀ¼���ڣ��������򴴽�
                                DirectoryUtil.AssertDirExist(realArchivePath);
                                //�ƶ����·��
                                var movedFilePath = realArchivePath.PathCombine(info.FileName);

                                if (File.Exists(serverRealPath) && !File.Exists(movedFilePath))
                                {
                                    //�ƶ��ļ�
                                    File.Move(serverRealPath, movedFilePath);

                                    //�޸����ݿ���ļ�·��������
                                    string filePath = movedFilePath.Replace(realArchiveBaseDir, "");
                                    Hashtable ht = new Hashtable();
                                    ht.Add("BasePath", realArchiveBaseDir);
                                    ht.Add("SavePath", filePath);
                                    baseDal.UpdateFields(ht, info.ID);
                                }
                            }
                            catch(Exception ex)
                            {
                                var tips = "�ļ��鵵���ִ���" + ex.Message;
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
        /// �жϼ����еĸ����б��Ƿ���Ҫ�鵵����Ҫ����True������ΪFalse
        /// </summary>
        /// <param name="attachmentGUIDList">������GUID�б�</param>
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
                            result = true;//���������·��������·��
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ��ȡ��һ���ļ����ݣ������ļ��ֽ����ݣ�,���ָ����ͼƬ�ĸ߶ȣ���ȣ���ô��������ͼ
        /// </summary>
        /// <param name="id">������¼��ID</param>
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
                            //��ȡFTP������Ϣ
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
                                //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                                serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                            }

                            if (File.Exists(serverRealPath))
                            {
                                byte[] bytes = FileUtil.FileToBytes(serverRealPath);
                                if (width.HasValue && height.HasValue)
                                {
                                    //����ͼƬ�����ߴ�
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
        /// �����ļ�����ȡ��һ���ļ����ݣ������ļ��ֽ����ݣ�,���ָ����ͼƬ�ĸ߶ȣ���ȣ���ô��������ͼ
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
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
                        //����ͼƬ�����ߴ�
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
        /// �г�Ŀ¼���ļ�·������Ŀ¼
        /// </summary>
        /// <param name="filePath">ָ���ļ�·��</param>
        /// <param name="isDir">�Ƿ��г�Ŀ¼��Ĭ��Ϊfalse�������ļ��б�</param>
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
                LogTextHelper.Error("GetFileOrDirList�г�Ŀ¼���ļ�·������:" + ex.Message);
            }

            return list;
        }


        /// <summary>
        /// ������뼰���·��
        /// </summary>
        /// <param name="info">�ϴ��ļ���Ϣ</param>
        /// <returns></returns>
        public string GetFilePath(FileUploadInfo info)
        {
            string fileName = info.FileName;
            string category = info.Category;

            if (string.IsNullOrEmpty(category))
            {
                category = "Photo";
            }

            //��������Ŀ¼����,����������
            string uploadFolder = string.Format("{0}/{1}-{2:D2}/{3}", info.BasePath, DateTime.Now.Year, DateTime.Now.Month, category);
            string realFolderPath = uploadFolder;

            //���Ŀ¼Ϊ���Ŀ¼����ôת��Ϊʵ��Ŀ¼��������
            if (!IsPhysicalPath(uploadFolder))
            {
                realFolderPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, uploadFolder);

                if (!Directory.Exists(realFolderPath))
                {
                    Directory.CreateDirectory(realFolderPath);
                }
            }

            //�������Ŀ¼
            string filePath = PathCombine(uploadFolder, fileName);
            return filePath;
        }

        /// <summary>
        /// �Ƿ�Ϊ���Ե�ַ���÷�������Path.IsPathRooted�����жϣ���Path.IsPathRooted��·������\\Ҳ��Ϊ����·��
        /// </summary>
        /// <param name="path">Ŀ¼·��</param>
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
        /// ����attachmentGUID�Ĳ�����ȡ��Ӧ�ĵ�һ���ļ�·��
        /// </summary>
        /// <param name="attachmentGUID">������attachmentGUID</param>
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
                            //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                            serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                        }
                    }
                }
            }
            return serverRealPath;
        }


        /// <summary>
        /// ���ļ����浽ָ��Ŀ¼,��������Ի���Ŀ¼��·��
        /// </summary>
        /// <param name="info">�ļ��ϴ���Ϣ</param>
        /// <returns>�ɹ�������Ի���Ŀ¼��·�������򷵻ؿ��ַ�</returns>
        private string UploadFile(FileUploadInfo info)
        {
            //������뼰���·��
            string filePath = GetFilePath(info);
            string relativeSavePath = filePath.Replace(info.BasePath, "").Replace('\\', '/');//�滻����ʼĿ¼��Ϊ���·��


            string serverRealPath = filePath;
            if (!IsPhysicalPath(filePath))
            {
                serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, filePath);
            }

            //ͨ��ʵ���ļ���ȥ���Ҷ�Ӧ���ļ�����
            serverRealPath = GetRightFileName(serverRealPath, 1);

            //���ļ��Ѵ��ڣ�����������ʱ���޸�Filename��relativeSavePath
            relativeSavePath = relativeSavePath.Substring(0, relativeSavePath.LastIndexOf(info.FileName)) + FileUtil.GetFileName(serverRealPath);
            info.FileName = FileUtil.GetFileName(serverRealPath);

            //����ʵ���ļ��������ļ�
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
        /// �����ļ�����������������ļ��������(i)��i��1��ʼ����
        /// </summary>
        /// <param name="originalFilePath">ԭ�ļ���</param>
        /// <param name="i">����ֵ</param>
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
        /// ɾ�������ļ�
        /// </summary>
        /// <param name="info">�ļ���Ϣ</param>
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
                        //��ͨ�ļ�ɾ������
                        string serverRealPath = PathCombine(info.BasePath, info.SavePath.Replace('\\', '/'));
                        if (!IsPhysicalPath(serverRealPath))
                        {
                            //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                            serverRealPath = PathCombine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);

                            //��������Ŀ¼�ģ��ƶ���ɾ��Ŀ¼����
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
                //��¼ɾ����������
                LogHelper.Error(ex);
            }

        }

        /// <summary>
        /// ����Ψһ��GUID��ȡһ��Ψһ�ļ�¼
        /// </summary>
        /// <param name="attachmentGUID">����GUID</param>
        /// <returns></returns>
        public FileUploadInfo FindSingleByAttachGUID(string attachmentGUID)
        {
            string condition = string.Format("AttachmentGUID = '{0}'", attachmentGUID);
            return baseDal.FindSingle(condition);
        }
    }

    /// <summary>
    /// ΪWebClient���ӳ�ʱʱ��
    /// <para>��WebClient����һ���µ��࣬����GetWebRequest����</para>
    /// </summary>
    internal class NewWebClient : WebClient
    {
        private int _timeout;

        /// <summary>
        /// ��ʱʱ��(����)
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
