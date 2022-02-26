using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.ControlUtil.Facade
{

    /// <summary>
    /// 接口返回数据结果基类
    /// </summary>
    public class BaseResultJson
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseResultJson()
        {
            this.errcode = -1;
            this.errmsg = "";
        }

        /// <summary>
        /// 普通参数构造
        /// </summary>
        /// <param name="errmsg">错误信息</param>
        /// <param name="success">是否成功</param>
        /// <param name="errcode">错误代码</param>
        public BaseResultJson(string errmsg, bool success = false, int errcode = 0)
        {
            this.errmsg = errmsg;
            this.success = success;
            this.errcode = errcode;
        }

        /// <summary>
        /// 通过异常对象进行构造
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="errcode">错误代码</param>
        public BaseResultJson(Exception ex, int errcode = 0)
        {
            this.errmsg = ex.Message;
            this.success = false;
            this.errcode = errcode;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int errcode { get; set; }

        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string errmsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }
    }

    /// <summary>
    /// 用户校验结果
    /// </summary>
    public class CheckResult : BaseResultJson
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 用户登录来源渠道，0为网站，1为微信，2为安卓APP，3为苹果APP
        /// </summary>
        public string channel { get; set; }

        /// <summary>
        /// 所属公司ID（可选）
        /// </summary>
        public string corpid { get; set; }
    }

    /// <summary>
    /// 返回终端的Token信息
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// 接口访问令牌
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 令牌失效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }

    /// <summary>
    /// 接口访问常见错误
    /// </summary>
    public class SystemCommonError
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误说明
        /// </summary>
        public string MessageDetail { get; set; }
    }
}
