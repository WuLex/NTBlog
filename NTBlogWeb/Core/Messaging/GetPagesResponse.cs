using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Core.Messaging
{
    public class GetPagesResponse<T> : ResponseBase where T : BaseEntity
    {
        public PagedList<T> Pages { get; set; }
    }
}