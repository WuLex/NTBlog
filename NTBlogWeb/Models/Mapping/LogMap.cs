using NTBlog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTBlog.Models.Mapping
{
    public class LogMap : BaseEntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            HasKey(p => p.Id);
            ToTable("Logs");
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}