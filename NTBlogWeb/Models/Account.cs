using NTBlog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlog.Models
{
    public class Account : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}