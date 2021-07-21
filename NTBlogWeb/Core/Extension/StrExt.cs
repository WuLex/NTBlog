using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace NTBlogWeb.Core.Extension
{
    public static class StrExt
    {
        /// <summary>
        /// 指定使用 MD5 哈希算法加密字符(默认32位)
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的字符串(32位)</returns>
        public static string ToMd5(this string str)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
#pragma warning restore CS0618 // 类型或成员已过时
        }
    }
}