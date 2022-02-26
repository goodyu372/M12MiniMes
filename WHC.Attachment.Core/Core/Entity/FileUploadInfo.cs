using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using WHC.Framework.ControlUtil;

namespace WHC.Attachment.Entity
{
    /// <summary>
    /// 上传附件信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class FileUploadInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public FileUploadInfo()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.FileSize = 0;//文件大小   
            this.DeleteFlag = 0;//删除标志，1为删除，0为正常
            this.AddTime = System.DateTime.Now; //添加时间    
        }

        #region Property Members

        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 附件组所属记录ID
        /// </summary>
        [DataMember]
        public virtual string Owner_ID { get; set; }

        /// <summary>
        /// 附件组GUID
        /// </summary>
        [DataMember]
        public virtual string AttachmentGUID { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public virtual string FileName { get; set; }

        /// <summary>
        /// 基础路径，在单机版的情况下，路径为本地物理路径
        /// </summary>
        [DataMember]
        public virtual string BasePath { get; set; }

        /// <summary>
        /// 文件保存相对路径
        /// </summary>
        [DataMember]
        public virtual string SavePath { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        [DataMember]
        public virtual string Category { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public virtual int FileSize { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        [DataMember]
        public virtual string FileExtend { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        [DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember]
        public virtual DateTime AddTime { get; set; }

        /// <summary>
        /// 删除标志，1为删除，0为正常
        /// </summary>
        [DataMember]
        public virtual int DeleteFlag { get; set; }

        /// <summary>
        /// 文件流数据
        /// </summary>
        [DataMember]
        public byte[] FileData { get; set; }

        #endregion

    }

    /// <summary>
    /// FTP配置信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class FTPInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FTPInfo()
        {

        }

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public FTPInfo(string server, string user, string password, string baseUrl)
        {
            this.Server = server;
            this.User = user;
            this.Password = password;
            this.BaseUrl = baseUrl;
        }

        /// <summary>
        /// FTP服务地址
        /// </summary>
        [DataMember]
        public string Server { get; set; }

        /// <summary>
        /// FTP用户名
        /// </summary>
        [DataMember]
        public string User { get; set; }

        /// <summary>
        /// FTP密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// FTP的基础路径，如可以指定为IIS的路径：http://www.iqidi.com:8000 ,方便下载打开
        /// </summary>
        [DataMember]
        public string BaseUrl { get; set; }
    }
}