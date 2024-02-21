using NTBlogWeb.Configs.Models;
using NTBlogWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NTBlogWeb.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string GetUserName()
        {
            //如果HttpContext.User.Identity.IsAuthenticated为true，
            //或者HttpContext.User.Claims.Count()大于0表示用户已经登录
            if (HttpContext?.User?.Identity?.IsAuthenticated??false)
            {
                //这里通过 HttpContext.User.Claims 可以将我们在Login这个Action中存储到cookie中的所有
                //claims键值对都读出来，比如我们刚才定义的UserName的值Wangdacui就在这里读取出来了
                var userName = HttpContext.User.Claims.First().Value;
                return userName;
            }
            else
            {
                return "极客者";
            }
        }

        protected Setting GetSetting()
        {
            return Configs.ConfigHelper.GetBasicConfig();
        }

        protected Dictionary<string, object> GetResult(bool success, string message, object data = null)
        {
            return DataHelper.GetResult(success, message, data);
        }
    }
}