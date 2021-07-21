using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NTBlog.Core;

namespace NTBlog.Models.Mapping
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