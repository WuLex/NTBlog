using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace NTBlog.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 读取文件，默认读取编码为UTF-8的编码，开启缓存
        /// </summary>
        /// <param name="fileName">文件文称，要求绝对路径</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string fileName)
        {
            return ReadFile(fileName, Encoding.UTF8, true);
        }

        /// <summary>
        /// 读取文件，默认读取编码为UTF-8的编码
        /// </summary>
        /// <param name="fileName">文件文称，要求绝对路径</param>
        /// <param name="isCache">是否启用缓存</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string fileName, bool isCache)
        {
            return ReadFile(fileName, Encoding.UTF8, isCache);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件文称，要求绝对路径</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isCache">是否启用缓存</param>    
        /// <returns>文件内容</returns>
        public static string ReadFile(string fileName, Encoding encoding, bool isCache)
        {
            string result; //返回结果
            if (isCache)
            {
                result = (string)HttpContext.Current.Cache[fileName];
                if (result == null)
                {
                    result = ReadFile(fileName, encoding, false);
                    HttpContext.Current.Cache.Add(fileName, result, new CacheDependency(fileName), DateTime.MaxValue,
                                                  TimeSpan.Zero, CacheItemPriority.High, null);
                }
            }
            else
            {
                using (var sr = new StreamReader(fileName, encoding))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }
    }
}