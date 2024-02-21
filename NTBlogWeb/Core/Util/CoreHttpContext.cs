using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NTBlogWeb.Helper;

namespace NTBlogWeb.Core.Util
{
    public static class CoreHttpContext
    {
        private static IWebHostEnvironment _wevHostEnviroment =
            WebHelper.Instance.GetRequiredService<IWebHostEnvironment>();

        public static string WebPath => _wevHostEnviroment.WebRootPath;

        public static string MapPath(string path)
        {
            return Path.Combine(_wevHostEnviroment.ContentRootPath, path);
        }

        internal static void Configure(IWebHostEnvironment webhostEnviroment)
        {
            _wevHostEnviroment = webhostEnviroment;
        }
    }

    public static class StaticWebHostEnviromentExtensions
    {
        public static IApplicationBuilder UseStaticHostEnviroment(this IApplicationBuilder app)
        {
            var webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            CoreHttpContext.Configure(webHostEnvironment);
            return app;
        }
    }
}