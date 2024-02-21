using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTBlogWeb.Core.Util;

namespace NTBlogWeb.Controllers
{
    public class UploadController : Controller
    {
        //目前只支持单文件
        [HttpPost]
        public ActionResult UploadImage(IFormFile f)
        {
            var files = Request.Form.Files;

            if (files.Count == 0)
            {
                var json = new
                {
                    code = 1,
                    msg = "没有找到要上传的文件呢，请重新选择吧。"
                };
                return Json(json);
            }

            try
            {
                var curFile = files[0];
                //扩展文件夹 e.g. /2016/10/
                var extPath = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\";
                //获取图片文件保存的完整路径
                var fileSavePath = CoreHttpContext.MapPath("uploads\\images\\" + extPath);
                //路径不存在则创建
                if (!Directory.Exists(fileSavePath))
                    Directory.CreateDirectory(fileSavePath);

                //获取文件名
                var fileName = string.Empty;

                //获取文件后缀名
                var filePostfixName = fileName.Substring(fileName.LastIndexOf('.'));
                //获取新文件名
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + filePostfixName;
                //合并路径
                var path = Path.Combine(fileSavePath, newFileName);
                //保存文件
                using (var fs = System.IO.File.Create(path))
                {
                    curFile.CopyTo(fs);
                    fs.Flush();
                }
                //curFile.CopyToAsync(path);

                var resourcesSystemUrl = ConfigurationManager.AppSettings["ResourcesSystemUrl"];

                return Json(new
                {
                    code = 0,
                    msg = "咦？好像上传成功了呢。",
                    data = new
                    {
                        src = resourcesSystemUrl + "uploads/images/" + extPath + newFileName,
                        title = "这是图片的标题啊。"
                    }
                });
            }
            catch (Exception ex)
            {
                var json = new
                {
                    code = 1,
                    msg = "出错了，错误信息：" + ex.Message
                };
                return Json(json);
            }
        }

        public ActionResult WangEditor()
        {
            return View();
        }
    }
}