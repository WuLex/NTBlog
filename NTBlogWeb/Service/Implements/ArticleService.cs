using NTBlogWeb.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTBlogWeb.Core;
using NTBlogWeb.Models;
using NTBlogWeb.Service.Messaging.Request;
using NTBlogWeb.Core.Messaging;
using NTBlogWeb.Core.Extension;

namespace NTBlogWeb.Service.Implements
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repository;
        private readonly IDbContext _dbContext;
        private readonly IRepository<Archive> _archiveRepository;

        public ArticleService(IRepository<Article> repository, IRepository<Archive> archiveRepository, IDbContext dbContext)
        {
            _repository = repository;
            _archiveRepository = archiveRepository;
            _dbContext = dbContext;
        }

        public GetPagesResponse<Article> GetPageArticles(GetPageArticlesRequest request)
        {
            var query = _repository.Table
                .HasWhere(request.Title, p => p.Title.Contains(request.Title))
                .HasWhere(request.CategoryId, p => p.CategoryId == request.CategoryId)
                .HasWhere(request.Status, p => p.State == request.Status)
                .Where(p => !p.IsDel);

            //排序
            if (request.Sort != null)
            {
                switch (request.Sort.PropertyName)
                {
                    case "CreateTime":
                        query = request.Sort.SortMode == SortMode.Asc ? query.OrderBy(p => p.CreateTime) : query.OrderByDescending(p => p.CreateTime);
                        break;
                    default:
                        query = query.OrderBy(p => p.Id);
                        break;
                }
                
            }
            else
            {
                query = query.OrderByDescending(p=>p.IsTop).ThenByDescending(p=>p.Sort).ThenByDescending(p => p.CreateTime);
            }
            var page = query.ToPagedList(request.PageIndex, request.PageSize, true);

            var response = new GetPagesResponse<Article>
            {
                IsSuccess = true,
                Message = "获取成功！",
                Pages = page
            };

            return response;
        }

        public ResponseBase Insert(Article entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var response = new ResponseBase();
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                var date = DateTime.Now.ToString("yyyy年MM月");
                var archiveContext = _dbContext.Set<Archive>();
                var articleContext = _dbContext.Set<Article>();
                var archive = archiveContext.SingleOrDefault(p => p.ArchiveDate == date);

                if (archive == null)
                {
                    archive = new Archive
                    {
                        ArchiveDate = date,
                        Count = 1
                    };
                    archiveContext.Add(archive);
                }
                else
                {
                    archive.Count++;
                }
                articleContext.Add(entity);
                _dbContext.SaveChanges();
                tran.Commit();
            }

            return response;
        }
    }
}