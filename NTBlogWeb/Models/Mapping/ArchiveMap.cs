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
            //Key
            HasKey(p => p.Id);

            //Table
            ToTable("Archives");
        }
    }
}