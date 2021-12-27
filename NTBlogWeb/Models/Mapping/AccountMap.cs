using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTBlogWeb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Models.Mapping
{
    public class AccountMap : BaseEntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            ////Key
            //HasKey(p => p.Id);

            ////Table
            //ToTable("Accounts");
        }

        public override void Map(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.Property(p => p.Id).IsRequired();

            //throw new NotImplementedException();
        }
    }
}