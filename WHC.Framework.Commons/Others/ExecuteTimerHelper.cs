using System;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 记录程序执行时间的辅助类。
    /// </summary>
    public sealed class ExecuteTimerHelper
    {
        /// <summary>
        /// 执行开始时间
        /// </summary>
        public DateTime EnterTime { get; set; }

        /// <summary>
        /// 执行结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行的时间跨度TimeSpan
        /// </summary>
        public TimeSpan ExecuteTime { get; set; }

        /// <summary>
        /// 记录开始执行
        /// </summary>
        public void Begin()
        {
            this.EnterTime = DateTime.Now;
        }

        /// <summary>
        /// 结束执行
        /// </summary>
        /// <returns></returns>
        public TimeSpan End()
        {
            this.EndTime = DateTime.Now;
            this.ExecuteTime = EndTime - EnterTime;
            return ExecuteTime;
        }

        /// <summary>
        /// 使用执行次数，执行指定的函数
        /// </summary>
        /// <param name="totalCount">执行次数</param>
        /// <param name="action">执行函数</param>
        public void Execute(int totalCount, Action action)
        {
            Begin();
            while (--totalCount >= 0)
            {
                action.Invoke();
            }
            End();
        }

        /// <summary>
        /// 显示执行的毫秒数
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Total Milliseconds : {0} ", ExecuteTime.TotalMilliseconds);
        }
    }
}
