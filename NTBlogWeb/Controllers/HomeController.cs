using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTBlogWeb.Core;
using NTBlogWeb.Core.Email;
using NTBlogWeb.Core.Extension;
using NTBlogWeb.Helper;
using NTBlogWeb.Models;
using NTBlogWeb.ViewModels;
using System;
using System.Linq;

namespace NTBlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Archive> _archiveRepository;
        private readonly IRepository<Log> _logRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Comment> _commentRepository;

        public HomeController(IRepository<Article> articleRepository, IRepository<Log> logRepository,
            IRepository<Category> categoryRepository, IRepository<Comment> commentRepository,
            IRepository<Archive> archiveRepository)
        {
            _articleRepository = articleRepository;
            _logRepository = logRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _archiveRepository = archiveRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(GetList(new SearchInfo(1)));
        }

        //加载数据列表
        [HttpGet]
        public ActionResult AJaxLoadList()
        {
            //获取页码
            var page = Request.Query["page"];
            var searchInfo = new SearchInfo(Convert.ToInt32(page));
            //获取查询条件
            var url = Convert.ToString(Request.Query["p"]);
            var pIndex = url.LastIndexOf('/');
            if (pIndex > 0)
            {
                var arr = url.TrimStart('/').Split('/');
                if (arr.Length == 2)
                {
                    if (arr[0] == "categories")
                        searchInfo.CategoryName = arr[1];
                    if (arr[0] == "archive")
                        searchInfo.ArchiveName = arr[1];
                    if (arr[0] == "tag")
                        searchInfo.TagName = arr[1];
                }
            }
            else
            {
                if (url.IndexOf('?') >= 0)
                {
                    var arr = url.TrimStart('?').Split('=');
                    if (arr.Length == 2)
                    {
                        searchInfo.Keywork = arr[1];
                    }
                }
            }

            var pages = GetList(searchInfo, false);
            var items = pages.Items;
            var list = items.Select(p =>
            {
                return new { p.Title, p.IsTop, CreateTime = p.CreateTime.ToString("yyyy-MM-dd HH:mm"), p.Id };
            });

            return Json(new { data = list, page = pages.TotalPages });
        }

        //关键字搜索
        public ActionResult Search(string keywork)
        {
            var pages = GetList(new SearchInfo(1) { Keywork = keywork });

            ViewBag.Highlight = keywork;
            return View("List", pages);
        }

        //文章详情
        public ActionResult Detail(string articleId)
        {
            int id;
            if (int.TryParse(articleId, out id))
            {
                var article = _articleRepository.FindById(id);
                article.Category = _categoryRepository.FindById(article.CategoryId);
                if (article != null)
                {
                    ViewBag.CommentList = _commentRepository.Table
                        .Where(p => p.ArticleId == id)
                        .OrderByDescending(p => p.CreateTime).ToList();

                    //浏览量+1
                    article.Hits += 1;
                    _articleRepository.Update(article);
                    var cookie = Request.Cookies["CommentInfo"]?.FromLegacyCookieString();
                    if (cookie != null)
                    {
                        ViewBag.NickName = cookie["NickName"];
                        ViewBag.Email = cookie["Email"];
                    }

                    return View(article);
                }
            }

            return RedirectToAction("NotFound", "Message");
        }

        //关于
        public ActionResult About()
        {
            return View();
        }

        //标签
        public ActionResult Tag(string tagName)
        {
            var pages = GetList(new SearchInfo(1) { TagName = tagName });

            if (pages.Items.Count > 0)
                ViewBag.Highlight = tagName;
            return View("List", pages);
        }

        //分类
        public ActionResult CategorySearch(string categoryName)
        {
            var pages = GetList(new SearchInfo(1) { CategoryName = categoryName });
            if (pages.Items.Count > 0)
                ViewBag.Highlight = categoryName;
            return View("List", pages);
        }

        //文章评论
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(CommentModel model)
        {
            var comment = new Comment
            {
                ArticleId = model.ArticleId,
                Nickname = model.NickName,
                CommentText = model.CommentText,
                State = 1,
                CreateTime = DateTime.Now,
                CommentIp = WebHelper.GetIp(),
                Email = model.Email
            };
            _commentRepository.Insert(comment);

            CookieOptions options = new CookieOptions
            {
                // 设置过期时间
                Expires = DateTime.Now.AddDays(7)
            };

            HttpContext.Response.Cookies.Append("NickName", model.NickName, options);
            HttpContext.Response.Cookies.Append("Email", model.NickName, options);
            //HttpCookie cookie = new HttpCookie("CommentInfo");
            //cookie.Values["NickName"] = model.NickName;
            //cookie.Values["Email"] = model.Email;
            //cookie.Expires = DateTime.Now.AddDays(7);
            //Response.Cookies.Set(cookie);

            //发送邮件通知
            var subject = model.NickName + "评论了你的文章";
            EmailServiceFactory.GetEmailService().SendMail("827******@qq.com", subject, "这里有好多好多的内容。耍花枪顶替在无可奈何花落去 在s");

            return RedirectToAction("Detail", new { articleId = model.ArticleId });
        }

        //文章归档
        public ActionResult Archive(string keywork)
        {
            try
            {
                var pages = GetList(new SearchInfo(1) { ArchiveName = keywork });

                return View("List", pages);
            }
            catch (Exception)
            {
                return RedirectToAction("List");
            }
        }

        /// <summary>
        /// 加载侧边数据
        /// </summary>
        private void LoadSideInfo()
        {
            var categorylist = _categoryRepository.Table.Include(category => category.Articles)
                .OrderByDescending(p => p.IsTop).ThenByDescending(p => p.Sort).ToList();
            //for (int i = 0; i < categorylist.Count; i++)
            //{
            //    categorylist[i].Articles.Add = _articleRepository.TableNoTracking.Where(a => a.CategoryId == categorylist[i].Id).ToList();
            //}

            //获取分类信息
            ViewBag.Categories = categorylist;

            //获取归档信息
            ViewBag.Archives = _archiveRepository.Table.OrderByDescending(p => p.Id).ToList();

            var topQuery = from a in _articleRepository.Table
                where a.State == (int)ArticleStatus.Normal
                select new ArticleTopModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Hits = a.Hits,
                    IsTop = a.IsTop
                };
            //获取阅读排行榜TOP7
            ViewBag.Top = topQuery.OrderByDescending(p => p.Hits).ThenByDescending(p => p.IsTop).Skip((1 - 1) * 7)
                .Take(7).ToList();
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="info">查询条件</param>
        /// <param name="isLoadSideInfo">是否加载侧边数据</param>
        /// <returns></returns>
        private PagedList<Article> GetList(SearchInfo info, bool isLoadSideInfo = true)
        {
            var pageSize = 15; //默认
            var setting = Configs.ConfigHelper.GetBasicConfig();
            if (setting != null)
            {
                pageSize = setting.WebsitePageSize;
            }

            if (isLoadSideInfo)
            {
                LoadSideInfo();
            }

            var query = _articleRepository.Table.Where(p => p.State == (int)ArticleStatus.Normal);

            //关键字
            if (!string.IsNullOrEmpty(info.Keywork))
                query = query.Where(p => p.Title.Contains(info.Keywork));
            //标签
            if (!string.IsNullOrEmpty(info.TagName))
            {
                query = query.Where(p => p.Tags.Contains(info.TagName));
            }

            //分类
            if (!string.IsNullOrEmpty(info.CategoryName))
            {
                var category = _categoryRepository.Table
                    .FirstOrDefault(p => p.CategoryName == info.CategoryName);
                if (category != null)
                    query = query.Where(p => p.CategoryId == category.Id);
            }

            //归档
            if (!string.IsNullOrEmpty(info.ArchiveName))
            {
                var date = Convert.ToDateTime(info.ArchiveName);
                int days = DateTime.DaysInMonth(date.Year, date.Month);
                var maxDate = Convert.ToDateTime(info.ArchiveName + days + "日");
                query = query.Where(p => p.CreateTime >= date && p.CreateTime <= maxDate);
            }

            query = query.OrderByDescending(p => p.IsTop)
                .ThenByDescending(p => p.Sort)
                .ThenByDescending(p => p.CreateTime);
            return query.ToPagedList(info.PageIndex, pageSize, true);
        }
    }

    /// <summary>
    /// 搜索信息类
    /// </summary>
    public class SearchInfo
    {
        public SearchInfo(int pageIndex)
        {
            PageIndex = pageIndex;
        }

        /// <summary>
        /// 页索引值
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywork { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 归档日期
        /// </summary>
        public string ArchiveName { get; set; }
    }
}