using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTBlogWeb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Models.Mapping
{
    public class LoginLogMap : BaseEntityTypeConfiguration<LoginLog>
    {
        public LoginLogMap()
        {
        }

        public override void Map(EntityTypeBuilder<LoginLog> builder)
        {
            //Key
            builder.HasKey(p => p.Id);

            //Table
            builder.ToTable("LoginLogs");
        }
    }
}