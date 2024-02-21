using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTBlogWeb.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Models.Mapping
{
    public class CommentMap : BaseEntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
        }

        public override void Map(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Comments");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            builder.HasOne(p => p.Article)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.ArticleId)
                .OnDelete(DeleteBehavior.SetNull)
                //.WillCascadeOnDelete(false)
                .IsRequired();
        }
    }
}