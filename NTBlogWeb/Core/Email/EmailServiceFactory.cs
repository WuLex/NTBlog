using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTBlogWeb.Core.Email
{
    /// <summary>
    /// 邮箱服务工厂
    /// </summary>
    public class EmailServiceFactory
    {
        private static IEmailService _emailService;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="emailService"></param>
        public static void InitializeEmailServiceFactory(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// 获取邮箱服务对象
        /// </summary>
        /// <returns></returns>
        public static IEmailService GetEmailService()
        {
            return _emailService;
        }
    }
}