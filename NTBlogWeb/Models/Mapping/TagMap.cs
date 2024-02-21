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
    public class TagMap : BaseEntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
        }

        public override void Map(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("Tags");

            builder.Property(p => p.Id).ValueGeneratedOnAdd(); //(DatabaseGeneratedOption.Identity);
            builder.Property(p => p.TagName).HasMaxLength(50);
        }
    }
}