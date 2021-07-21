using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using NTBlog.Core;

namespace NTBlog.Models.Mapping
{
    public class ArticleMap : BaseEntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            //Key
            HasKey(p => p.Id);

            //Table
            ToTable("Articles");

            //Properties
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).HasMaxLength(200).IsRequired();


            HasRequired(p=>p.Category)
                .WithMany(p=>p.Articles)
                .HasForeignKey(p=>p.CategoryId)
                .WillCascadeOnDelete(false);

            
        }
    }
}