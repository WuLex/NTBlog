using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NTBlogWeb.Core.Logging;
using NTBlogWeb.Core.Util;
using NTBlogWeb.Helper;

namespace NTBlogWeb.Core.Email
{
    public class SmtpService : IEmailService
    {
        private MailMessage mailMessage; //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient smtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等
        private readonly EmailAccount email;
        private readonly string filePath; //配置文件物理路径

        private readonly ILogger _logger;

        public SmtpService()
        {
            _logger = new Log4NetAdapter();
            //配置文件虚拟路径
            var path = ConfigHelper.AppSetting("EmailConfigPath", "");
            filePath = CoreHttpContext.MapPath(path == "" ? "Configs\\Files\\EmailConfig.json" : path);
            try
            {
               
                var config = FileHelper.ReadFile(filePath);
                email = JsonConvert.DeserializeObject<EmailAccount>(config);
            }
            catch (Exception ex)
            {
                _logger.Error("读取配置文件出错，请检查配置文件是否存在！", ex);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void SendMail(string to, string subject, string body, string attachmentsPath = "")
        {
            if (email == null)
            {
                _logger.Error("配置出错，请检查配置内容！文件路径：" + filePath);
                return;
            }

            try
            {
                mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(email.Form);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(attachmentsPath))
                    AddAttachments(attachmentsPath); //添加附件

                smtpClient = new SmtpClient();
                smtpClient.Host = email.Host;
                smtpClient.Port = email.Port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = email.EnableSsl;
                if (email.EnablePwdAuthentication)
                {
                    System.Net.NetworkCredential nc = new System.Net.NetworkCredential(email.UserName, email.Password);
                    //NTLM: Secure Password Authentication in Microsoft Outlook Express
                    smtpClient.Credentials = nc.GetCredential(smtpClient.Host, smtpClient.Port, "NTLM");
                }
                else
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(email.UserName, email.Password);
                }

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "发送邮件时发生错误！");
            }
        }

        /// <summary>
        /// [异步]发送邮件
        /// </summary>
        /// <param name="to">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public async Task SendMailAsync(string to, string subject, string body, string attachmentsPath = "")
        {
            if (email == null)
            {
                _logger.Error("配置出错，请检查配置内容！文件路径：" + filePath);
                return;
            }

            try
            {
                mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(email.Form);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(attachmentsPath))
                    AddAttachments(attachmentsPath); //添加附件

                smtpClient = new SmtpClient();
                smtpClient.Host = email.Host;
                smtpClient.Port = email.Port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = email.EnableSsl;
                if (email.EnablePwdAuthentication)
                {
                    System.Net.NetworkCredential nc = new System.Net.NetworkCredential(email.UserName, email.Password);
                    //NTLM: Secure Password Authentication in Microsoft Outlook Express
                    smtpClient.Credentials = nc.GetCredential(smtpClient.Host, smtpClient.Port, "NTLM");
                }
                else
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(email.UserName, email.Password);
                }

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "发送邮件时发生错误！");
            }
        }

        //异步电子邮件发送操作完成后执行该方法。
        private void Smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _logger.Information("异步发送邮件完成！");
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        private void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                for (int i = 0; i < path.Length; i++)
                {
                    var data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    var disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "发送邮件时发生错误！");
            }
        }
    }
}