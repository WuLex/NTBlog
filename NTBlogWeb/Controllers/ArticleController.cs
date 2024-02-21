using NTBlogWeb.Core;
using NTBlogWeb.Models;
using NTBlogWeb.Service.Interfaces;
using NTBlogWeb.Service.Messaging.Request;
using NTBlogWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Mapster;

namespace NTBlogWeb.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IArticleService _articleService;

        public ArticleController(IRepository<Article> articleRepository, IRepository<Category> categoryRepository,
            IArticleService articleService)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _articleService = articleService;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult List(int pageIndex = 1, int pageSize = 15)
        {
            //var query = _articleRepository.Table;

            #region 获取用户名

            ViewBag.UserName = GetUserName();

            #endregion

            var setting = GetSetting();
            if (setting != null)
            {
                pageSize = setting.ManagePageSize;
            }


            var request = new GetPageArticlesRequest(pageIndex, pageSize);

            #region Filter

            var title = Request.Query["title"];
            var category = Request.Query["category"];
            var state = Request.Query["state"];
            int cId;
            int s;

            if (!string.IsNullOrEmpty(title))
            {
                request.Title = title;
                ViewBag.ArticleTitle = title;
            }

            if (int.TryParse(category, out cId) && cId != 0)
            {
                request.CategoryId = cId;
                ViewBag.CagegotyId = cId;
            }

            if (int.TryParse(state, out s) && s != 0)
            {
                request.Status = s;
                ViewBag.State = s;
            }

            //排序
            var field = Request.Query["field"];
            var sort = Request.Query["sort"];

            var createTimeSort = "desc";

            if (!StringValues.IsNullOrEmpty(field) && !StringValues.IsNullOrEmpty(sort))
            {
                if (field == "CreateTime" && (sort == "asc" || sort == "desc"))
                {
                    request.Sort = new Sort("CreateTime", sort == "asc" ? SortMode.Asc : SortMode.Desc);
                    createTimeSort = sort == "asc" ? "desc" : "asc";
                }
            }

            ViewBag.CreateTimeSort = createTimeSort;

            #endregion

            var response = _articleService.GetPageArticles(request);

            ViewBag.Categories = _categoryRepository.FindAll();

            return View(response.Pages);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.FindAll();
            return View();
        }


        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Create(ArticleModel model)
        {
            #region 旧的手动赋值

            //var article = new Article
            //{
            //    Title = model.Title,
            //    Content = model.Content,
            //    Author = model.Author,
            //    CreateTime = DateTime.Now,
            //    IsTop = model.IsTop,
            //    State = model.State,
            //    Hits = model.Hits,
            //    Tags = model.Tags,
            //    CategoryId = model.CategoryId,
            //    Sort = model.Sort,
            //    MetaTitle = model.MetaTitle,
            //    MetaKeywords = model.MetaKeywords,
            //    MetaDescription = model.MetaDescription
            //}; 

            #endregion

            var article = model.Adapt<Article>();
            _articleService.Insert(article);

            return RedirectToAction("List");
        }


        /// <summary>
        /// 编辑文章页面
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public ActionResult Edit(string articleId)
        {
            if (int.TryParse(articleId, out int id))
            {
                var article = _articleRepository.FindById(id);
                ViewBag.Categories = _categoryRepository.FindAll();
                if (article != null)
                    return View(article);
            }

            return RedirectToAction("NotFound", "Message");
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Edit(ArticleModel model)
        {
            var article = _articleRepository.FindById(model.Id);

            if (article != null)
            {
                #region 旧的赋值方法

                //article.Title = model.Title;
                //article.Content = model.Content;
                //article.Author = model.Author;
                //article.IsTop = model.IsTop;
                //article.State = model.State;
                //article.Hits = model.Hits;
                //article.Tags = model.Tags;
                //article.CategoryId = model.CategoryId;
                //article.Sort = model.Sort;
                //article.MetaTitle = model.MetaTitle;
                //article.MetaKeywords = model.MetaKeywords;
                //article.MetaDescription = model.MetaDescription; 

                #endregion

                model.Adapt(article);
                article.CreateTime = DateTime.Now;
                _articleRepository.Update(article);
            }

            return RedirectToAction("List");
        }
    }
}