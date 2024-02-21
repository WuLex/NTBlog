using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTBlogWeb.Core;

namespace NTBlogWeb.Models
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Category : BaseEntity
    {
        private ICollection<Article> _articles;

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 文章列表
        /// </summary>
        public virtual ICollection<Article> Articles
        {
            get { return _articles ?? (_articles = new List<Article>()); }
            set { _articles = value; }
        }
    }
}