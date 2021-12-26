using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NTBlogWeb.Core;

namespace NTBlogWeb.Models.Mapping
{
    public class CategoryMap : BaseEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            HasKey(p => p.Id);

            // Properties
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CategoryName).IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("Categories");

        }
    }
}