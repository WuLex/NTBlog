using Microsoft.EntityFrameworkCore;
using NTBlogWeb.Models;

namespace NTBlogWeb.Entities
{
    public class BlogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //注入Sql链接字符串
            optionsBuilder.UseSqlServer(
                @"Data Source=.;Initial Catalog=BlogDB;Persist Security Info=True;User ID=sa;Password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Articles)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                //.WillCascadeOnDelete(false)
                .IsRequired();
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