using NTBlogWeb.Configs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NTBlogWeb.Controllers
{
    public class SettingController : BaseController
    {
        // GET: Setting
        public ActionResult BasicSetting()
        {
            var setting = Configs.ConfigHelper.GetBasicConfig();

            #region 获取用户名

            ViewBag.UserName = GetUserName();

            #endregion

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBasicSetting(Setting model)
        {
            Configs.ConfigHelper.SetBasicConfig(model);
            return RedirectToAction("BasicSetting");
        }
    }
}