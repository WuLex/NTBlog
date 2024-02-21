using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace NTBlogWeb.Core.Middleware
{
    public class IgnoreRouteMiddleware
    {
        private readonly RequestDelegate next;

        // You can inject a dependency here that gives you access
        // to your ignored route configuration.
        public IgnoreRouteMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value != null && context.Request.Path.HasValue && context.Request.Path.Value.Contains(".axd"))
            {
                context.Response.StatusCode = 404;

                Console.WriteLine("Ignored!");

                return;
            }

            await next.Invoke(context);
        }
    }
}