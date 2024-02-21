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
    public class LogMap : BaseEntityTypeConfiguration<Log>
    {
        public LogMap()
        {
        }

        public override void Map(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Logs");
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}