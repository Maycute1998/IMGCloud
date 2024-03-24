using Amazon.Runtime.Internal.Util;
using IMGCloud.Domain.Options;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly MailOptions _mailOptions;
        private readonly ILogger<SendMailService> _logger;

        public SendMailService(MailOptions options, ILogger<SendMailService> logger)
        {
            _mailOptions = options;
            _logger = logger;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailOptions.DisplayName, _mailOptions.Mail);
            email.From.Add(new MailboxAddress(_mailOptions.DisplayName, _mailOptions.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_mailOptions.Host, _mailOptions.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailOptions.Mail, _mailOptions.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);

                _logger.LogInformation("Error - " + emailsavefile);
                _logger.LogError(ex.Message);
            }
            smtp.Disconnect(true);

            _logger.LogInformation("send mail to " + mailContent.To);

        }

        public async Task SendResetPasswordEmail(string email, string resetToken)
        {
            string resetPasswordLink = $"https://example.com/resetpassword?token={resetToken}";

            await SendMail(new MailContent()
            {
                To = email,
                Subject = "Reset Your Password",
                Body = $"Please click the following link to reset your password: {resetPasswordLink}"
            });
        }
    }
}
