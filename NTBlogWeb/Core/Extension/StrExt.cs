using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToUpper();
            }
        }
    }
}