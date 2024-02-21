/*
  Web.config
  
  <add key="EmailConfigPath" value="~/Config/EmailConfig.json"/>

  默认配置文件路径为：~/Config/EmailConfig.json
  数据结构：
    {
      "Host": "smtp.126.com",
      "Port": 25,
      "Form": "lex2022@126.com",
      "UserName": "lex2022",
      "Password": "这里填写你的邮箱密码",
      "EnableSsl": false,
      "EnablePwdAuthentication": false
    }

 */

using System.Threading.Tasks;

namespace NTBlogWeb.Core.Email
{
    /// <summary>
    /// 邮件服务接口
    /// </summary>
    /// <remarks>
    /// Web.config
    /// add key="EmailConfigPath" value="~/Config/EmailConfig.json"/>
    /// 默认配置文件路径为：~/Config/EmailConfig.json
    /// {      
    /// "Host": "smtp.126.com",      
    /// "Port": 25,      
    /// "Form": "lex2022@126.com",     
    /// "UserName": "lex2022",      
    /// "Password": "这里填写你的邮箱密码",      
    /// "EnableSsl": false,      
    /// "EnablePwdAuthentication": false    
    /// }
    /// </remarks>
    public interface IEmailService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容（可以以html格式进行设计）</param>
        /// <param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        void SendMail(string to, string subject, string body, string attachmentsPath = "");

        /// <summary>
        /// [异步]发送邮件
        /// </summary>
        /// <param name="to">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        Task SendMailAsync(string to, string subject, string body, string attachmentsPath = "");
    }
}