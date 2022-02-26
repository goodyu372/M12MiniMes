using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WHC.Framework.Commons;
using Newtonsoft.Json;
using System.Collections;
using System.Web.Security;

namespace WHC.Framework.ControlUtil.Facade
{
    /// <summary>
    /// Web API签名操作的辅助类
    /// </summary>
    public class SignatureHelper
    {
        /// <summary>
        /// 根据当前时间和随机数，生成签名字符串的URL
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">渠道接入秘钥</param>
        /// <param name="token">访问令牌</param>
        public static string GetSignatureUrl(string appId, string appSecret, string token = null)
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
        /// 生成签名字符串
        /// </summary>
        /// <param name="appSecret">渠道接入秘钥</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        public static string SignatureString(string appSecret, string timestamp, string nonce)
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
        internal class DictionarySort : System.Collections.IComparer
        {
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
    }

}