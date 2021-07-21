using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace NTBlogWeb.Core.Authentication
{
    public class AspFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthenticationToken(string token, bool createPersistentCookie = false)
        {
            FormsAuthentication.SetAuthCookie(token, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}