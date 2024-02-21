using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Helper
{
    public class DataHelper
    {
        public static Dictionary<string, object> GetResult(bool success, string message, object data = null)
        {
            var d = new Dictionary<string, object>
            {
                { "success", success },
                { "message", message }
            };
            if (data != null)
            {
                d.Add("data", data);
            }

            return d;
        }
    }
}