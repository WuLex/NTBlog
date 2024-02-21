using NTBlogWeb.Core;
using NTBlogWeb.Core.Messaging;
using NTBlogWeb.Models;
using NTBlogWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NTBlogWeb.Service.Messaging.Request
{
    public class GetPageArticlesRequest : PageRequest
    {
        public GetPageArticlesRequest(int pageIndex, int pageSize) : base(pageIndex, pageSize)
        {
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Sort Sort { get; set; }
    }
}