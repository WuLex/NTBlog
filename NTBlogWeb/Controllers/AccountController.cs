using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTBlogWeb.Core;
using NTBlogWeb.Core.Extension;
using NTBlogWeb.Helper;
using NTBlogWeb.Models;
using NTBlogWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NTBlogWeb.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<LoginLog> _loginLogRepository;

        public AccountController(
            IRepository<Account> accountRepository, IRepository<LoginLog> loginLogRepository)
        {
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
            // 判断用户是否已经登录，如果已经登录，那么读取登录用户的用户名
            //如果HttpContext.User.Identity.IsAuthenticated为true，
            //或者HttpContext.User.Claims.Count()大于0表示用户已经登录
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    //这里通过 HttpContext.User.Claims 可以将我们在Login这个Action中存储到cookie中的所有
            //    //claims键值对都读出来，比如我们刚才定义的UserName的值Wangdacui就在这里读取出来了
            //    var userName = HttpContext.User.Claims.First().Value;
            //}

            //如果已经登录，直接跳转至文章管理列表页
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("List", "Article");
            }

            ViewBag.UserName = HttpContext.User.Claims.First().Value;

            #region NET框架的旧方法

            //var cookie = Request.Cookies["UserName"];
            //if (cookie != null)
            //{
            //    ViewBag.UserName = cookie;
            //}

            #endregion NET框架的旧方法

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            //验证验证码是否正确
            var pwdErrorCount = HttpContext.Session.GetString("PwdErrorCount");
            if (pwdErrorCount != null && Convert.ToInt32(pwdErrorCount) > 5)
            {
                var sessionVerifyCode = HttpContext.Session.GetString("VerifyCode");
                if (sessionVerifyCode == null || model.VerifyCode != sessionVerifyCode.ToString())
                {
                    return Json(GetResult(false, "验证码输入错误，请刷新重试。", new { errorCount = pwdErrorCount }));
                }
            }

            var entity = _accountRepository.Table.SingleOrDefault(p => p.UserName == model.UserName);
            if (entity == null)
            {
                return Json(GetResult(false, "用户不存在。"));
            }

            #region 密码

            if (entity.Password != model.Password.ToMd5())
            {
                int count = 1;
                var errorCount = HttpContext.Session.GetString("PwdErrorCount");
                if (errorCount != null)
                {
                    count = Convert.ToInt32(errorCount) + 1;
                }

                HttpContext.Session.SetInt32("PwdErrorCount", count);

                return Json(GetResult(false, "用户名或密码输错了呢。", new { errorCount = count }));
            }

            #endregion 密码

            //记录登录日志
            _loginLogRepository.Insert(new LoginLog
            {
                Ip = WebHelper.GetIp(),
                CreateTime = DateTime.Now,
                UserAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault() //Request.UserAgent
            });

            //重置错误次数
            HttpContext.Session.SetString("PwdErrorCount", null);

            if (ModelState.IsValid)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, entity.UserName));

                //string[] roles = entity.Roles.Split(",");
                //foreach (string role in roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, role));
                //}

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                if (model.RememberMe)
                {
                    //保存登录名
                    var props = new AuthenticationProperties()
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.Now.AddDays(5),
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                    #region NET框架的旧方法

                    //HttpContext.Response.Cookies.Append("UserName", entity.UserName, new CookieOptions
                    //{
                    //    Expires = DateTime.Now.AddDays(5)
                    //});

                    //HttpCookie cookie = new HttpCookie("UserName");
                    //cookie.Value = entity.UserName;
                    //cookie.Expires = DateTime.Now.AddDays(5);
                    //Response.Cookies.Set(cookie);

                    #endregion NET框架的旧方法
                }
                else
                {
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                }
                return Json(GetResult(true, "登录成功。"));
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                //ViewData["message"] = "Invalid UserName or Password!";
                return Json(GetResult(false, "Invalid UserName or Password!"));
            }

            //return Json(GetResult(true, "登录成功。"));
        }

        //登出
        public ActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
            //_authentication.SignOut();
            //return RedirectToAction("Login");
        }
    }
}