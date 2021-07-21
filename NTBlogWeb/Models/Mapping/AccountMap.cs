using NTBlog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlog.Models.Mapping
{
    public class AccountMap : BaseEntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            //Key
            HasKey(p => p.Id);

            //Table
            ToTable("Accounts");
        }
    }
}