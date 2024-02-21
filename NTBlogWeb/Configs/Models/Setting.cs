using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Configs.Models
{
    public class Setting
    {
        /// <summary>
        /// 前台列表分页大小
        /// </summary>
        public int WebsitePageSize { get; set; }
        /// <summary>
        /// 评论列表分页大小
        /// </summary>
        public int CommentPageSize { get; set; }
        /// <summary>
        /// 管理后台列表分页大小
        /// </summary>
        public int ManagePageSize { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebsiteName { get; set; }

    }
}