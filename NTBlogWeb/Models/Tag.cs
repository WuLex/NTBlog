using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTBlog.Core;

namespace NTBlog.Models
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}