//using System.Web.Caching;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Text;

namespace NTBlogWeb.Helper
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
                result = MemoryCacheHelper.Cache.Get<string>(fileName); //(string)HttpContext.Current.Cache[fileName];
                if (result == null)
                {
                    //var cancellationTokenSource = new CancellationTokenSource();
                    //var changeToken = cancellationTokenSource.Token;
                    //var firstCancellationTokenSource = new CancellationTokenSource();
                    //var secondCancellationTokenSource = new CancellationTokenSource();
                    //var firstCancellationToken = firstCancellationTokenSource.Token;
                    //var secondCancellationToken = secondCancellationTokenSource.Token;

                    //回调，但不执行写入缓存逻辑 ，只读取
                    result = ReadFile(fileName, encoding, false);

                    #region 写入缓存

                    //理想情况下，文件提供程序的实例应该从 IOC 容器中获取
                    var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                    IChangeToken token = fileProvider.Watch(fileName);
                    //文件只有在更改时才会从缓存中删除
                    var cacheEntryOptions = new MemoryCacheEntryOptions().AddExpirationToken(token);
                    // 将文件名称放入缓存
                    MemoryCacheHelper.Cache.Set(fileName, result, cacheEntryOptions);

                    #endregion 写入缓存

                    ////写入缓存
                    //HttpContext.Current.Cache.Add(fileName, result, new CacheDependency(fileName), DateTime.MaxValue,TimeSpan.Zero, CacheItemPriority.High, null);
                    //回调，但不执行写入缓存逻辑 ，只读取
                    //result = ReadFile(fileName, encoding, false);
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