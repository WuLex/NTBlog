using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTBlogWeb.Core;

namespace NTBlogWeb.Models.Mapping
{
    public class ArticleMap : BaseEntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            ////Key
            //HasKey(p => p.Id);

            ////Table
            //ToTable("Articles");

            ////Properties
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(p => p.Title).HasMaxLength(200).IsRequired();


            //HasRequired(p=>p.Category)
            //    .WithMany(p=>p.Articles)
            //    .HasForeignKey(p=>p.CategoryId)
            //    .WillCascadeOnDelete(false);
        }

        public override void Map(EntityTypeBuilder<Article> builder)
        {
            //Key
            builder.HasKey(p => p.Id);

            //Table
            builder.ToTable("Articles");

            //Properties
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Property(p => p.Title).HasMaxLength(200).IsRequired();


            builder.HasOne(p => p.Category)
                .WithMany(p => p.Articles)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                //.WillCascadeOnDelete(false)
                .IsRequired();
        }
    }
}