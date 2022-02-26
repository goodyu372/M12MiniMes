using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 用来传递常规操作的结果内容
    /// </summary>
    [DataContract]
    [Serializable]
    public class CommonResult
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CommonResult() { }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public CommonResult(bool success, string error = null)
        {
            this.Success = success;
            this.ErrorMessage = error;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        [DataMember]
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        [DataMember]
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 用来传递的内容
        /// </summary>
        [DataMember]
        public string Data1
        {
            get;
            set;
        }

        /// <summary>
        /// 用来传递的内容
        /// </summary>
        [DataMember]
        public string Data2
        {
            get;
            set;
        }

        /// <summary>
        /// 用来传递的内容
        /// </summary>
        [DataMember]
        public string Data3
        {
            get;
            set;
        }
    }
}
