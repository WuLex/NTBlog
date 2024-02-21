using System;
using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NTBlogWeb.Core;
using NTBlogWeb.Core.Email;
using NTBlogWeb.Core.Engines;
using NTBlogWeb.Core.Logging;
using NTBlogWeb.Core.Middleware;
using NTBlogWeb.Core.Util;
using NTBlogWeb.Helper;
using NTBlogWeb.Models;
using NTBlogWeb.Service.Implements;
using NTBlogWeb.Service.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;

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
                .AddControllersAsServices(); //�����������Ϊ����

            //ʹ��������ϵע������ ASP.NET Core Ӧ�ó��� ����ʹ�� Startup.cs �� �����е� AddDbContext �� EF Core ��ӵ������á�
            services.AddDbContext<EntityContext>(
                options =>
                {
                    //��ȡ�������Ӵ�
                    //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AspEFCoreDemo;Trusted_Connection=True;");
                    options.UseSqlServer(Configuration.GetConnectionString("BlogDBConnection"));
                });

            services.AddSession();

            //���cookie��֤
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    //options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    options.Cookie.Name = "user-session";
                    options.SlidingExpiration = true;
                });


            services.AddHttpContextAccessor();
            //��ConfigureServices��ע�Ỻ�����
            services.AddMemoryCache();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<Log4NetAdapter>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<SmtpService>().As<IEmailService>().InstancePerLifetimeScope();

            //EF DbContextFactory
            //builder.RegisterType<DbContextFactory>().As<IDbContext>().InstancePerLifetimeScope();

            //ҵ����
            builder.RegisterType<EfRepository<Log>>().As<IRepository<Log>>().InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<Article>>().As<IRepository<Article>>().InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<Category>>().As<IRepository<Category>>().InstancePerLifetimeScope();

            //�Զ�ע��
            var baseType = typeof(IDependency);
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).Where(p => baseType.IsAssignableFrom(p) && p != baseType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //var connectionString = ConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;
            //builder.Register<IDbContext>(c => new ObjectContext(connectionString)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //Service
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();

            #region ע�����п������Ĺ�ϵ��������ʵ��������Ҫ�����

            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();

            builder.RegisterTypes(controllersTypesInAssembly)
                .PropertiesAutowired();

            #endregion ע�����п������Ĺ�ϵ��������ʵ��������Ҫ�����
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EntityContext db)
        {
            //��ֵ����̬�෽���ֶ���ȡ����ע�����
            WebHelper.Instance = app.ApplicationServices;

            //��ʼ���ʼ�
            EmailServiceFactory.InitializeEmailServiceFactory(new SmtpService());


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

            app.UseMiddleware<IgnoreRouteMiddleware>();

            #region ��̬�ļ�

            app.UseStaticFiles();

            // ӳ��·��һ
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Plugins")),
                RequestPath = "/Plugins"
            });

            // ӳ��·����
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Content")),
                RequestPath = "/Content",
            });

            // ӳ��·����
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images",
            });

            // ӳ��·����
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Scripts")),
                RequestPath = "/Scripts",
            });


            // ӳ��·����
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "fonts")),
                RequestPath = "/fonts",
            });

            #endregion ��̬�ļ�

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllerRoute("wangEditor", "wangEditor", new { controller = "Upload", action = "WangEditor" });

                // //Error pages
                // endpoints.MapControllerRoute("404", "404", new { controller = "ErrorPage", action = "Error404" });
                // endpoints.MapControllerRoute("500", "500", new { controller = "ErrorPage", action = "Error500" });
                // endpoints.MapControllerRoute("illegal", "illegal", new { controller = "ErrorPage", action = "IllegalOperation" });
                // endpoints.MapControllerRoute("lackauthority", "lackauthority", new { controller = "ErrorPage", action = "LackAuthority" });

                // //Login
                // endpoints.MapControllerRoute("Login2", "manage/login", new { controller = "Account", action = "Login" });
                // endpoints.MapControllerRoute("Login1", "manage/login.html", new { controller = "Account", action = "Login" });
                // //SignOut
                // endpoints.MapControllerRoute("SignOut1", "manage/signout", new { controller = "Account", action = "SignOut" });

                // //validate code
                // endpoints.MapControllerRoute("ValidateCode1", "manage/validatecode", new { controller = "ValidateCode", action = "GetValidateCode" });

                // //Detail
                // endpoints.MapControllerRoute("ArticleDetail2", "detail-{articleId}", new { controller = "Home", action = "Detail" });
                // endpoints.MapControllerRoute("ArticleDetail1", "detail-{articleId}.html", new { controller = "Home", action = "Detail" });
                // endpoints.MapControllerRoute("ArticleDetail4", "detail/{articleId}", new { controller = "Home", action = "Detail" });
                // endpoints.MapControllerRoute("ArticleDetail3", "detail/{articleId}.html", new { controller = "Home", action = "Detail" });

                // //About
                // endpoints.MapControllerRoute("About2", "about", new { controller = "Home", action = "About" });
                // endpoints.MapControllerRoute("About1", "about.html", new { controller = "Home", action = "About" });

                // //Archive
                // endpoints.MapControllerRoute("Archive1", "archive/{keywork}", new { controller = "Home", action = "Archive" });
                // endpoints.MapControllerRoute("Archive2", "archive/{keywork}.html", new { controller = "Home", action = "Archive" });

                // //Tag
                // endpoints.MapControllerRoute("Tag2", "tag/{tagName}", new { controller = "Home", action = "Tag" });
                // endpoints.MapControllerRoute("Tag1", "tag/{tagName}.html", new { controller = "Home", action = "Tag" });

                // //List
                // endpoints.MapControllerRoute("List2", "all", new { controller = "Home", action = "List" });
                // endpoints.MapControllerRoute("List1", "all.html", new { controller = "Home", action = "List" });

                // //404
                // endpoints.MapControllerRoute("404-1", "404", new { controller = "Message", action = "NotFound" });
                // endpoints.MapControllerRoute("404-2", "404.html", new { controller = "Message", action = "NotFound" });

                // //Category
                // endpoints.MapControllerRoute("cagegory1", "categories/{categoryName}", new { controller = "Home", action = "CategorySearch" });
                // endpoints.MapControllerRoute("cagegory2", "categories/{categoryName}.html", new { controller = "Home", action = "CategorySearch" });

                // //Search
                // endpoints.MapControllerRoute("search1", "search/t={keywork}", new { controller = "Home", action = "Search" });
                // endpoints.MapControllerRoute("search2", "search/t={keywork}.html", new { controller = "Home", action = "Search" });


                // #region Manage

                // //basic setting
                // endpoints.MapControllerRoute("manage_setting", "manage/setting", new { controller = "Setting", action = "BasicSetting" });

                //// endpoints.MapControllerRoute("manage_index", "manage", new { controller = "Article", action = "List" });

                // //article
                // endpoints.MapControllerRoute("manage_article_list", "manage/article_list_{pageIndex}_{pageSize}", new { controller = "Article", action = "List" });
                // endpoints.MapControllerRoute("manage_article_create", "manage/article_create", new { controller = "Article", action = "Create" });
                // endpoints.MapControllerRoute("manage_article_edit", "manage/article_edit_{articleId}", new { controller = "Article", action = "Edit" });

                // //Category
                // endpoints.MapControllerRoute("manage_category_list", "manage/category_list", new { controller = "Category", action = "List" });
                // endpoints.MapControllerRoute("manage_category_create", "manage/category_create", new { controller = "Category", action = "Create" });
                // endpoints.MapControllerRoute("manage_category_edit", "manage/category_edit", new { controller = "Category", action = "Edit" });

                // //Log
                // endpoints.MapControllerRoute("manage_log_list1", "manage/log_list", new { controller = "Log", action = "List" });
                // endpoints.MapControllerRoute("manage_log_list2", "manage/log_list_{pageIndex}_{pageSize}", new { controller = "Log", action = "List" });

                // #endregion Manage

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}