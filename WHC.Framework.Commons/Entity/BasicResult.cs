using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 返回结果基类
    /// </summary>
    public abstract class BasicResult<T>
    {
        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public virtual T Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="data">返回数据</param>
        public BasicResult(string message, T data)
        {
            this.Message = message == null ? null : message.Trim();
            this.Data = data;
        }

        /// <summary>
        /// 默认无参构造函数
        /// </summary>
        public BasicResult()
        {
        }
    }
}
