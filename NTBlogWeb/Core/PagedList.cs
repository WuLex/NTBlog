using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Core
{
    /// <summary>
    /// 分页结果集
    /// </summary>
    /// <typeparam name="TEntity">实体对象</typeparam>
    public class PagedList<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public long ItemsPerPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// 结果集
        /// </summary>
        public List<TEntity> Items { get; set; }
    }
}