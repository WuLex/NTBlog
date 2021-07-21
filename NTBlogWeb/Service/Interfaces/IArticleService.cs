using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NTBlog.Core;
using NTBlog.Models;
using NTBlog.Service.Messaging.Request;
using NTBlog.Core.Messaging;

namespace NTBlog.Service.Interfaces
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetPagesResponse<Article> GetPageArticles(GetPageArticlesRequest request);

        ResponseBase Insert(Article entity);
    }
}
