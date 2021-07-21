using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string VerifyCode { get; set; }
    }
}