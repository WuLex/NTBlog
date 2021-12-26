using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NTBlogWeb.Core.Util;
using Autofac;
using NTBlogWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using NTBlogWeb.Core.Logging;
using NTBlogWeb.Core.Email;
using System.Configuration;
using NTBlogWeb.Core;
using NTBlogWeb.Service.Implements;
using NTBlogWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using NTBlogWeb.Helper;
using Microsoft.EntityFrameworkCore;

namespace NTBlogWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllersWithViews()
                .AddControllersAsServices();  //将控制器添加为服务

            //使用依赖关系注入配置 ASP.NET Core 应用程序。 可以使用 Startup.cs 的 方法中的 AddDbContext 将 EF Core 添加到此配置。
            services.AddDbContext<EntityContext>(
               options =>
               {
                    //获取数据连接串
                    //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AspEFCoreDemo;Trusted_Connection=True;");
                    options.UseSqlServer(Configuration.GetConnectionString("BlogDBConnection"));
               });

            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            //在ConfigureServices中注册缓存服务
            services.AddMemoryCache();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
           
            builder.RegisterType<Log4NetAdapter>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<SmtpService>().As<IEmailService>().InstancePerLifetimeScope();

            //EF DbContextFactory
            //builder.RegisterType<DbContextFactory>().As<IDbContext>().InstancePerLifetimeScope();

            //业务类
            //builder.RegisterType<EfRepository<Log>>().As<IRepository<Log>>().InstancePerLifetimeScope();
            //builder.RegisterType<EfRepository<Article>>().As<IRepository<Article>>().InstancePerLifetimeScope();
            //builder.RegisterType<EfRepository<Category>>().As<IRepository<Category>>().InstancePerLifetimeScope();

            //自动注册
            //var baseType = typeof (IDependency);
            //var assembly = Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(assembly).Where(p => baseType.IsAssignableFrom(p) && p != baseType).AsImplementedInterfaces().InstancePerLifetimeScope();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            //builder.Register<IDbContext>(c => new ObjectContext(connectionString)).InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //Service
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();


            #region 注册所有控制器的关系及控制器实例化所需要的组件

            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();

            builder.RegisterTypes(controllersTypesInAssembly)
                .PropertiesAutowired();

            #endregion
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EntityContext db)
        {
            //赋值给静态类方便手动获取依赖注入对象
            WebHelper.Instance = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            db.Database.EnsureCreated();

            app.UseStaticHostEnviroment();
            app.UseSession();

          

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}