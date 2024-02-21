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
    public class CategoryMap : BaseEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
        }

        public override void Map(EntityTypeBuilder<Category> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Property(p => p.CategoryName).IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            builder.ToTable("Categories");
        }
    }
}