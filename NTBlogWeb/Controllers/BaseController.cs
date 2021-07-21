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