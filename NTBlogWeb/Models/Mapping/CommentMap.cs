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
            HasKey(p => p.Id);
            ToTable("Comments");

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Article)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.ArticleId)
                .WillCascadeOnDelete(false);

        }
    }
}