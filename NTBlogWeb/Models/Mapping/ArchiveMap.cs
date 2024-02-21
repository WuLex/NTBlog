using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTBlogWeb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Models.Mapping
{
    public class ArchiveMap : BaseEntityTypeConfiguration<Archive>
    {
        public ArchiveMap()
        {
        }

        public override void Map(EntityTypeBuilder<Archive> builder)
        {
            //Key
            builder.HasKey(p => p.Id);

            //Table
            builder.ToTable("Archives");
        }
    }
}