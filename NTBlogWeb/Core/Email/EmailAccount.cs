using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTBlogWeb.Core.Email
{
    /// <summary>
    /// 邮件配置实体
    /// </summary>
    public class EmailAccount
    {
        /// <summary>
        /// 发件箱的邮件服务器地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 发送邮件所用的端口号（htmp协议默认为25）
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 发件人邮箱密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// true表示对邮件内容进行socket层加密传输，false表示不加密
        /// 加密连接。
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// true表示对发件人邮箱进行密码验证，false表示不对发件人邮箱进行密码验证
        /// </summary>
        public bool EnablePwdAuthentication { get; set; }

        /// <summary>
        /// 发送者
        /// </summary>
        public string Form { get; set; }
    }
}