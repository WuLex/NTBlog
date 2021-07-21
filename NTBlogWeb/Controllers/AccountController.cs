using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTBlog.Core.Authentication;
using NTBlog.ViewModels;
using NTBlog.Core;
using NTBlog.Models;
using NTBlog.Core.Extension;
using NTBlog.Helper;

namespace NTBlog.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IFormsAuthentication _authentication;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<LoginLog> _loginLogRepository;

        public AccountController(IFormsAuthentication authentication,
            IRepository<Account> accountRepository, IRepository<LoginLog> loginLogRepository)
        {
            _authentication = authentication;
            _accountRepository = accountRepository;
            _loginLogRepository = loginLogRepository;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //如果已经登录，直接跳转至文章管理列表页
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("List", "Article");

            var cookie = Request.Cookies["UserName"];
            if (cookie != null)
            {
                ViewBag.UserName = cookie;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            //验证验证码是否正确
            var pwdErrorCount = HttpContext.Session.GetString("PwdErrorCount");
            if (pwdErrorCount != null && Convert.ToInt32(pwdErrorCount) > 5)
            {
                var sessionVerifyCode = HttpContext.Session.GetString("VerifyCode");
                if (sessionVerifyCode == null || model.VerifyCode != sessionVerifyCode.ToString())
                    return Json(GetResult(false, "验证码输入错误，请刷新重试。", new {errorCount = pwdErrorCount}));
            }

            var entity = _accountRepository.Table.SingleOrDefault(p => p.UserName == model.UserName);
            if (entity == null)
            {
                return Json(GetResult(false, "用户不存在。"));
            }

            if (entity.Password != model.Password.ToMd5())
            {
                int count = 1;
                var errorCount = HttpContext.Session.GetString("PwdErrorCount");
                if (errorCount != null)
                    count = Convert.ToInt32(errorCount) + 1;
                HttpContext.Session.SetInt32("PwdErrorCount", count);

                return Json(GetResult(false, "用户名或密码输错了呢。", new {errorCount = count}));
            }

            //记录登录日志
            _loginLogRepository.Insert(new LoginLog
            {
                Ip = WebHelper.GetIp(),
                CreateTime = DateTime.Now,
                UserAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault() //Request.UserAgent
            });

            //重置错误次数
            HttpContext.Session.SetString("PwdErrorCount", null);
            //保存身份票据
            _authentication.SetAuthenticationToken(entity.UserName);
            //保存登录名
            if (model.RememberMe)
            {
                HttpContext.Response.Cookies.Append("UserName", entity.UserName, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(5)
                });

                //HttpCookie cookie = new HttpCookie("UserName");
                //cookie.Value = entity.UserName;
                //cookie.Expires = DateTime.Now.AddDays(5);
                //Response.Cookies.Set(cookie);
            }

            return Json(GetResult(true, "登录成功。"));
        }

        //登出
        public ActionResult SignOut()
        {
            _authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}