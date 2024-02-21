using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace NTBlogWeb.Helper
{
    public class WebHelper
    {
        public static IServiceProvider Instance { get; set; }


        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIp()
        {
            IHttpContextAccessor httpContext = (IHttpContextAccessor)Instance.GetService(typeof(IHttpContextAccessor));
            string result = httpContext.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"];
            //string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = httpContext.HttpContext.Request
                    .Headers["REMOTE_ADDR"]; //HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
            }

            result = result == "::1" ? "127.0.0.1" : result;

            if (string.IsNullOrEmpty(result) || !IsIp4(result))
            {
                return "0.0.0.0";
            }

            return result;
        }

        /// <summary>
        /// 判断字符串是否为 IP4 地址格式，是返回 true，否返回 false
        /// </summary>
        /// <param name="value">需要判断的字符串</param>
        public static bool IsIp4(string value)
        {
            return Regex.IsMatch(value, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 根据相对路径或绝对路径获取绝对路径
        /// </summary>
        /// <param name="localPath">相对路径或绝对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetFilePath(string localPath)
        {
            IWebHostEnvironment _env = (IWebHostEnvironment)Instance.GetService(typeof(IWebHostEnvironment));
            var path = Path.Combine(_env.ContentRootPath + "\\Configs\\Files\\", localPath);
            return Regex.IsMatch(localPath, @"([A-Za-z]):\\([\S]*)") ? localPath : path;
        }
    }
}