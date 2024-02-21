using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NTBlogWeb.Models;
using NTBlogWeb.Models.Mapping;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;
using static NTBlogWeb.Core.Extension.EfExt;

namespace NTBlogWeb.Core
{
    public class EntityContext : DbContext
    {
        #region 方式一:ApplicationDbContext 类必须公开具有 DbContextOptions<ApplicationDbContext> 参数的公共构造函数。 这是将 AddDbContext 的上下文配置传递到 DbContext 的方式。

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        #endregion

        #region 方式二:还可以轻松地通过 DbContext 构造函数传递配置（如连接字符串）

        //private readonly string _connectionString;
        //public EntityContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //} 

        #endregion

        //public EntityContext(string nameOrConnectionString) : base(nameOrConnectionString)
        //{
        //    Database.SetInitializer<EntityContext>(null);
        //    //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContext>());
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Type[] types = typeof(EntityTypeConfiguration<>).GetTypeInfo().Assembly.GetTypes();
            //IEnumerable<Type> typesToRegister = types
            //    .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
            //                    type.GetTypeInfo().BaseType != null &&
            //                    type.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType && type.GetTypeInfo().BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityTypeConfiguration<>).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityContext).Assembly);
        }

        public DbSet<Archive> Archives { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}