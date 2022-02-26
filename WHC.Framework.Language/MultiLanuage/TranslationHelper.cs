using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Diagnostics;

namespace WHC.Framework.Language
{
    /// <summary>
    /// 翻译相应的词汇
    /// </summary>
    public class TranslationHelper
    {       
        /// <summary>
        /// 接口翻译
        /// </summary>
        /// <param name="inputString">输入字符串</param>
        /// <param name="from">源内容语言</param>
        /// <param name="to">目标语言</param>
        /// <returns></returns>
        public static string Translate(string inputString, string from = "zh", string to = "en")
        {
            return BaiduTranslate(inputString, from, to);
        }


        /// <summary>
        /// 百度接口翻译
        /// </summary>
        /// <param name="inputString">输入字符串</param>
        /// <param name="from">源内容语言</param>
        /// <param name="to">目标语言</param>
        /// <returns></returns>
        private static string BaiduTranslate(string inputString, string from = "zh", string to = "en")
        {
            string content = "";

            string appId = "20180502000152401";
            string securityId = "rb0KjwtPfZccZinVAi6J";
            int salt = 0;

            StringBuilder signString = new StringBuilder();
            string md5Result = string.Empty;
            //1.拼接字符,为了生成sign
            signString.Append(appId);
            signString.Append(inputString);
            signString.Append(salt);
            signString.Append(securityId);

            //2.通过md5获取sign
            byte[] sourceMd5Byte = Encoding.UTF8.GetBytes(signString.ToString());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] destMd5Byte = md5.ComputeHash(sourceMd5Byte);
            md5Result = BitConverter.ToString(destMd5Byte).Replace("-", "");
            md5Result = md5Result.ToLower();

            try
            {
                //3.获取web翻译的json结果
                WebClient client = new WebClient();
                string url = string.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from=zh&to=en&appid={1}&salt={2}&sign={3}", inputString, appId, salt, md5Result);
                byte[] buffer = client.DownloadData(url);
                string result = Encoding.UTF8.GetString(buffer);

                var trans = JsonConvert.DeserializeObject<TranslationJson>(result);
                if (trans != null)
                {
                    content = trans.trans_result[0].dst;
                    content = StringUtil.ToProperCase(content);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return content;
        }


    }
    internal class TranslationJson
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<TranslationResult> trans_result { get; set; }
    }
    internal class TranslationResult
    {
        public string src { get; set; }
        public string dst { get; set; }
    }

}
