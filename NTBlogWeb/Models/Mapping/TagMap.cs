using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NTBlogWeb.Core;

namespace NTBlogWeb.Models.Mapping
{
    public class TagMap : BaseEntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            HasKey(p => p.Id);

            ToTable("Tags");

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.TagName).HasMaxLength(50);
        }
    }
}