using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.ControlUtil.Facade
{
    /// <summary>
    /// 签名信息
    /// </summary>
    public class SignatureInfo
    {
        ///// <summary>
        ///// 签名信息
        ///// </summary>
        //public string signature { get; set; }

        ///// <summary>
        ///// 时间戳
        ///// </summary>
        //public string timestamp { get; set; }

        ///// <summary>
        ///// 随机数
        ///// </summary>
        //public string nonce { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
        public string appsecret { get; set; }

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string token { get; set; }
    }

}
