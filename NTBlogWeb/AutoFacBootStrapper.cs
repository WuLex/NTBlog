using NTBlogWeb.Core.Engines;
using System.Linq;
using System.Reflection;
using Autofac;
using NTBlogWeb.Core;
using NTBlogWeb.Models;
using System;
using NTBlogWeb.Service.Interfaces;
using NTBlogWeb.Service.Implements;
using NTBlogWeb.Helper;
using System.Configuration;
using NTBlogWeb.Core.Logging;
using NTBlogWeb.Core.Email;

namespace NTBlogWeb
{
    public class AutoFacBootStrapper
    {
        public static void Register()
        {
            //var builder = new ContainerBuilder();

            //#region MVC
            //// Register your MVC controllers. (MvcApplication is the name of
            //// the class in Global.asax.)
            ////builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //#endregion


            //builder.RegisterType<AspFormsAuthentication>().As<IFormsAuthentication>().InstancePerLifetimeScope();


            //builder.RegisterType<Log4NetAdapter>().As<ILogger>().InstancePerLifetimeScope();
            //builder.RegisterType<SmtpService>().As<IEmailService>().InstancePerLifetimeScope();

            ////EF DbContextFactory
            ////builder.RegisterType<DbContextFactory>().As<IDbContext>().InstancePerLifetimeScope();

            ////业务类
            ////builder.RegisterType<EfRepository<Log>>().As<IRepository<Log>>().InstancePerLifetimeScope();
            ////builder.RegisterType<EfRepository<Article>>().As<IRepository<Article>>().InstancePerLifetimeScope();
            ////builder.RegisterType<EfRepository<Category>>().As<IRepository<Category>>().InstancePerLifetimeScope();

            ////自动注册
            ////var baseType = typeof (IDependency);
            ////var assembly = Assembly.GetExecutingAssembly();
            ////builder.RegisterAssemblyTypes(assembly).Where(p => baseType.IsAssignableFrom(p) && p != baseType).AsImplementedInterfaces().InstancePerLifetimeScope();

            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            //builder.Register<IDbContext>(c => new ObjectContext(connectionString)).InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            ////Service
            //builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();

            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //EngineContainerFactory.InitializeEngineContainerFactory(new EngineContainer(container));
        }

    }
}