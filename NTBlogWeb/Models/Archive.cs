using NTBlogWeb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Models
{
    /// <summary>
    /// 归档
    /// </summary>
    public class Archive : BaseEntity
    {
        /// <summary>
        /// 归档日期 yyyy-MM-dd
        /// </summary>
        public string ArchiveDate { get; set; }

        /// <summary>
        /// 文章数
        /// </summary>
        public int Count { get; set; }
    }
}