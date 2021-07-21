using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.ViewModels
{
    public class CommentModel
    {
        public int ArticleId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string CommentText { get; set; }
    }
}