using NTBlogWeb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Models
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment : BaseEntity
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 评论者IP
        /// </summary>
        public string CommentIp { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        public virtual Article Article { get; set; }
    }
}