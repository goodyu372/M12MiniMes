using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WHC.Framework.Commons;
using Newtonsoft.Json;
using System.Data;

namespace WHC.Framework.ControlUtil.Facade
{
    /// <summary>
    /// Json字符串操作辅助类
    /// </summary>
    public class JsonHelper<T> where T : class
    {
        /// <summary>
        /// 检查返回的记录，如果返回没有错误，或者结果提示成功，则不抛出异常
        /// </summary>
        /// <param name="content">返回的结果</param>
        /// <returns></returns>
        private static bool VerifyErrorCode(string content)
        {
            if (content.Contains("errcode"))
            {
                BaseResultJson errorResult = JsonConvert.DeserializeObject<BaseResultJson>(content);
                //非成功操作才记录异常，因为有些操作是返回正常的结果（{"errcode": 0, "errmsg": "ok"}）
                if (errorResult != null && !errorResult.success)
                {
                    string error = string.Format("请求发生错误！错误代码：{0}，说明：{1}", (int)errorResult.errcode, errorResult.errmsg);
                    LogTextHelper.Error(errorResult.ToJson());

                    throw new Exception(error);//抛出错误
                }
            }
            return true;
        }

        /// <summary>
        /// 转换Json字符串到具体的对象
        /// </summary>
        /// <param name="url">返回Json数据的链接地址</param>
        /// <returns></returns>
        public static T ConvertJson(string url)
        {
            HttpHelper helper = new HttpHelper();
            helper.ContentType = "application/json";
            string content = helper.GetHtml(url);

            VerifyErrorCode(content);
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        /// <summary>
        /// 转换Json字符串到具体的对象
        /// </summary>
        /// <param name="url">返回Json数据的链接地址</param>
        /// <param name="postData">POST提交的数据</param>
        /// <returns></returns>
        public static T ConvertJson(string url, string postData)
        {
            HttpHelper helper = new HttpHelper();
            helper.ContentType = "application/json";

            string content = helper.GetHtml(url, postData, true);
            VerifyErrorCode(content);
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        /// <summary>
        /// 转换Json字符串到具体的对象
        /// </summary>
        /// <param name="url">返回Json数据的链接地址</param>
        /// <returns></returns>
        public static string ConvertString(string url)
        {
            HttpHelper helper = new HttpHelper();
            helper.ContentType = "application/json";

            string content = helper.GetHtml(url);
            VerifyErrorCode(content);

            string result = JsonConvert.DeserializeObject<string>(content);
            return result;
        }

        /// <summary>
        /// 转换Json字符串到具体的对象
        /// </summary>
        /// <param name="url">返回Json数据的链接地址</param>
        /// <param name="postData">POST提交的数据</param>
        /// <returns></returns>
        public static string ConvertString(string url, string postData)
        {
            HttpHelper helper = new HttpHelper();
            helper.ContentType = "application/json";

            string content = helper.GetHtml(url, postData, true);
            VerifyErrorCode(content);

            string result = JsonConvert.DeserializeObject<string>(content);
            return result;
        }

        /// <summary>
        /// 提交文件并解析返回的结果
        /// </summary>
        /// <param name="url">提交文件数据的链接地址</param>
        /// <param name="file">文件地址</param>
        /// <param name="nvc">名称集合对象</param>
        /// <returns></returns>
        public static T PostFile(string url, string file, NameValueCollection nvc = null)
        {
            HttpHelper helper = new HttpHelper();
            string content = helper.PostStream(url, new string[] { file }, nvc);
            VerifyErrorCode(content);

            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }

}