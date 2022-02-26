using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using WHC.Framework.Commons;

namespace WHC.Framework.ControlUtil.Facade
{
    /// <summary>
    /// 为非数据表API服务的处理基类
    /// </summary>
    public class NormalApiService
    {   
        /// <summary>
        /// 没有SignatureInfo数据的错误
        /// </summary>
        public const string NoSignatureInfo_MsgError = "没有在缓存里面设置SignatureInfo签名信息";    

        #region 构造函数及变量属性

        private SignatureInfo signatureInfo = null;
        
        /// <summary>
        /// 设置的接口访问签名信息（每次获取自动从缓存里面取）
        /// </summary>
        protected virtual SignatureInfo SignatureInfo
        {
            /*
             * 为了使这个属性可以在Winform中（使用Cache），也可以通过Web使用（使用Session）来获取对应的签名信息。
             * 需要一个配置项，指定是从那种方式获取签名信息。
             * 这样Web和Winform里面可以共用Web API Caller层的实现，不需要改动。
             */
            get
            {
                //为提高速度，首次加载首次
                if (signatureInfo == null)
                {
                    AppConfig settingConfig = new AppConfig();
                    string signatureType = settingConfig.AppConfigGet("SignatureType");//签名类型，默认是Winform的Cache，可以指定为Web的Session
                    if (!string.IsNullOrEmpty(signatureType) && signatureType.Equals("web", StringComparison.OrdinalIgnoreCase))
                    {
                        signatureInfo = HttpContext.Current.Session["SignatureInfo"] as SignatureInfo;
                    }
                    else
                    {
                        // 默认为Winform方式的Cache获取
                        //通过缓存获取签名对象信息
                        signatureInfo = Cache.Instance["SignatureInfo"] as SignatureInfo;
                    }
                    return signatureInfo;
                }
                else
                {
                    return signatureInfo;
                }
            }
        }

        /// <summary>
        /// 访问网络的辅助类
        /// </summary>
        protected HttpHelper helper = new HttpHelper();

        /// <summary>
        /// 访问配置文件的辅助类
        /// </summary>
        protected AppConfig config = new AppConfig();

        /// <summary>
        /// WCF配置文件, 默认为"ApiConfig.config"
        /// </summary>
        private string configurationPath = "ApiConfig.config";

        /// <summary>
        /// 构建属性，方便在值变化的时候，重新设置AppConfig属性
        /// </summary>
        protected virtual string ConfigurationPath
        {
            get { return configurationPath; }
            set 
            { 
                var serverRealPath = value;
                if (!Path.IsPathRooted(serverRealPath))
                {
                    //如果是相对目录，加上当前程序的目录才能定位文件地址
                    serverRealPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                }

                configurationPath = serverRealPath;
                config = new AppConfig(serverRealPath);
            }
        }

        /// <summary>
        /// API配置节点,在子类中配置
        /// </summary>
        protected string configurationName = "";

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NormalApiService()
        {
            helper.ContentType = "application/json";
        }

        /// <summary>
        /// 使用自定义配置
        /// </summary>
        /// <param name="configurationName">API配置项名称</param>
        /// <param name="configurationPath">配置路径</param>
        public NormalApiService(string configurationName, string configurationPath)
        {
            this.configurationName = configurationName;
            this.ConfigurationPath = configurationPath;//记得使用属性ConfigurationPath
        }

        #endregion

        #region URL签名的处理函数

        /// <summary>
        /// 根据当前时间和随机数，生成签名字符串的URL
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">渠道接入秘钥</param>
        /// <param name="token">访问令牌</param>
        protected virtual string GetSignatureUrl(string appId, string appSecret, string token = null)
        {
            string timestamp = DateTime.Now.DateTimeToInt().ToString();
            string nonce = new Random().NextDouble().ToString();
            string signature = SignatureString(appSecret, timestamp, nonce);

            string result = string.Format("?signature={0}&timestamp={1}&nonce={2}&appid={3}", signature, timestamp, nonce, appId);
            if (!string.IsNullOrEmpty(token))
            {
                result += string.Format("&token={0}", token);
            }
            return result;
        }

        /// <summary>
        /// 生成签名字符串的URL
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">渠道接入秘钥</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        protected virtual string GetSignatureUrl(string appId, string appSecret, string timestamp, string nonce)
        {
            string signature = SignatureString(appSecret, timestamp, nonce);
            return string.Format("?signature={0}&timestamp={1}&nonce={2}&appid={3}", signature, timestamp, nonce, appId);
        }

        /// <summary>
        /// 生成签名字符串
        /// </summary>
        /// <param name="appSecret">渠道接入秘钥</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        protected virtual string SignatureString(string appSecret, string timestamp, string nonce)
        {
            ArrayList ArrTmp = new ArrayList() { appSecret, timestamp, nonce };
            ArrTmp.Sort(new DictionarySort());

            string tmpStr = string.Join("", ArrTmp.ToArray());

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            return tmpStr.ToLower();
        }

        /// <summary>
        /// 字典排序对比器
        /// </summary>
        public class DictionarySort : System.Collections.IComparer
        {
            /// <summary>
            /// 对比两个数值
            /// </summary>
            /// <returns></returns>
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }

        /// <summary>
        /// 组合URL和Action的内容
        /// </summary>
        /// <param name="url">api的URL，如http://localhost/api/</param>
        /// <param name="action">操作动作，如insert</param>
        /// <returns></returns>
        protected virtual string CombindUrl(string url, string action)
        {
            string result = url;
            if (url.EndsWith("/") || url.EndsWith("\\"))
            {
                result += action;
            }
            else
            {
                result += "/" + action;
            }
            return result;
        }

        /// <summary>
        /// 获取API的配置路径并检查
        /// </summary>
        /// <returns></returns>
        private string GetBaseUrl()
        {
            string baseUrl = config.AppConfigGet(this.configurationName);//配置的API控制器基础路径
            if (string.IsNullOrEmpty(baseUrl))
            {
                string error = string.Format("请检查配置WebAPI的文件【{0}】是否存在，或配置项【{1}】是否存在！", this.configurationPath, this.configurationName);
                LogTextHelper.Error(error);

                throw new ArgumentNullException(error);
            }
            return baseUrl;
        }

        /// <summary>
        /// 获取处理的URL，并带上具体的处理方法
        /// </summary>
        /// <param name="action">控制器方法名称</param>
        /// <returns></returns>
        protected virtual string GetNormalUrl(string action)
        {
            string url = "";
            string baseUrl = GetBaseUrl();

            url = CombindUrl(baseUrl, action);//组合为完整的访问地址
            return url;
        }


        /// <summary>
        /// 获取处理的URL,不带Token信息，但是包含签名信息
        /// </summary>
        /// <param name="action">控制器方法名称</param>
        /// <returns></returns>
        protected virtual string GetPostUrl(string action)
        {
            string url = "";
            if (this.SignatureInfo != null)
            {
                var append = GetSignatureUrl(SignatureInfo.appid, SignatureInfo.appsecret);

                string baseUrl = GetBaseUrl();
                url = CombindUrl(baseUrl, action + append);//组合为完整的访问地址
            }
            else
            {
                LogHelper.Error(NoSignatureInfo_MsgError);
                throw new ArgumentNullException(NoSignatureInfo_MsgError);
            }
            return url;
        }

        /// <summary>
        /// 获取处理的URL,带Token和签名信息
        /// </summary>
        /// <param name="action">控制器方法名称</param>
        /// <returns></returns>
        protected virtual string GetPostUrlWithToken(string action)
        {
            string url = "";
            if (this.SignatureInfo != null)
            {
                var append = GetSignatureUrl(SignatureInfo.appid, SignatureInfo.appsecret, SignatureInfo.token);

                string baseUrl = GetBaseUrl();
                url = CombindUrl(baseUrl, action + append);//组合为完整的访问地址
            }
            else
            {
                LogHelper.Error(NoSignatureInfo_MsgError);
                throw new ArgumentNullException(NoSignatureInfo_MsgError);
            }
            return url;
        }

        /// <summary>
        /// 获取单纯包含token参数的连接
        /// </summary>
        /// <param name="action">控制器方法名称</param>
        /// <returns></returns>
        protected virtual string GetTokenUrl(string action)
        {
            string url = "";
            if (this.SignatureInfo != null)
            {
                var append = string.Format("?token={0}", SignatureInfo.token);

                string baseUrl = GetBaseUrl();
                url = CombindUrl(baseUrl, action + append);//组合为完整的访问地址
            }
            else
            {
                LogHelper.Error(NoSignatureInfo_MsgError);
                throw new ArgumentNullException(NoSignatureInfo_MsgError);
            }
            return url;
        }

        #endregion
    }
}
