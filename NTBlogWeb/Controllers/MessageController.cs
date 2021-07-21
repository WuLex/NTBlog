using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NTBlog.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult NotFound()
        {
            return View();
        }
    }
}