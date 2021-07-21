using NTBlog.Core.VerifyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NTBlog.Controllers
{
    public class ValidateCodeController : Controller
    {
        // GET: ValidateCode
        public ActionResult GetValidateCode()
        {
            var vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            HttpContext.Session.SetString("VerifyCode",code);
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}