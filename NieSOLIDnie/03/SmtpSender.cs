using NLog;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MessagingServer.Core.Mail
{
    public class SmtpSender
    {
        private readonly ISmtpSettings _smtpSettings;

        protected IMailTemplates MailTemplates;

        public SmtpSender(ISmtpSettings smtpSettings, IMailTemplates mailTemplates)
        {
            _smtpSettings = smtpSettings;

            MailTemplates = mailTemplates;
        }

        public async Task<bool> SendMail(string toAddr, string toName, string mailSubject, string body)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_smtpSettings.SenderAddress, _smtpSettings.SenderName);
                    mailMessage.To.Add(new MailAddress(toAddr, toName));
                    mailMessage.Subject = mailSubject;
                    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    mailMessage.Body = body;

                    using (var smtpClient = new SmtpClient(_smtpSettings.ServerAddress, _smtpSettings.ServerPort))
                    {
                        var networkCredential =
                            new NetworkCredential(_smtpSettings.UserName, _smtpSettings.UserPassword);

                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = _smtpSettings.UseSsl;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = networkCredential;

                        await smtpClient.SendMailAsync(mailMessage);

                        return true;
                    }
                }
            }
        }
    }
}

