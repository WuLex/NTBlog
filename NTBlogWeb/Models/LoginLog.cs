using NTBlog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlog.Models
{
    public class LoginLog : BaseEntity
    {
        public string Ip { get; set; }
        public DateTime CreateTime { get; set; }
        public string UserAgent { get; set; }
    }
}