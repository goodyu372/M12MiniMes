using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WHC.Framework.ControlUtil.Facade
{
    /// <summary>
    /// 分页集合
    /// </summary>
    /// <typeparam name="T">对象</typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// 返回记录的总数
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        /// 列表集合
        /// </summary>
        public List<T> list { get; set; }
    }

    /// <summary>
    /// 分页的表格
    /// </summary>
    public class PageTableList
    {
        /// <summary>
        /// 返回记录的总数
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        /// 列表集合
        /// </summary>
        public DataTable list { get; set; }
    }

}
