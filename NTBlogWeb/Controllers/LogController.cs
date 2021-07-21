using NTBlog.Core;
using NTBlog.Core.Extension;
using NTBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NTBlog.Controllers
{
    public class LogController : BaseController
    {
        private readonly IRepository<Log> _logRepository;
        public LogController(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        // GET: Log
        public ActionResult List(int pageIndex = 1, int pageSize = 15)
        {
            var setting = GetSetting();
            if (setting != null)
                pageSize = setting.ManagePageSize;

            var query = _logRepository.Table;

            var list = query.OrderByDescending(p => p.Date).ToPagedList(pageIndex, pageSize, true);

            return View(list);
        }

        public ActionResult Detail(int id)
        {
            return Json(GetResult(true, "获取成功", _logRepository.FindById(id)));
        }
    }
}