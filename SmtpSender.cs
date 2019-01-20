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
        private readonly ILogger _logger;
        private readonly ISmtpSettings _smtpSettings;

        protected IMailTemplates MailTemplates;

        public SmtpSender(ISmtpSettings smtpSettings, IMailTemplates mailTemplates)
        {
            _logger = LogManager.GetLogger(GetType().Name.ToUpper());

            _smtpSettings = smtpSettings;

            MailTemplates = mailTemplates;
        }

        public Task<bool> SendMail(string toAddr, string toName, string mailSubject, AlternateView mailContent)
        {
            return SendMail(toAddr, toName, mailSubject, mailContent, null, null, null);
        }

        public async Task<bool> SendMail(string toAddr, string toName, string mailSubject, AlternateView mailContent,
            string attachmentName, byte[] attachmentBytes, string attachmentMimeType)
        {
            if (String.IsNullOrEmpty(toAddr))
            {
                _logger.Error("The recipient's e-mail address is empty");

                return false;
            }

            try
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
                        mailMessage.IsBodyHtml = mailContent.ContentType.MediaType == MediaTypeNames.Text.Html;

                        mailMessage.AlternateViews.Add(mailContent);

                        if (!String.IsNullOrWhiteSpace(attachmentName) && attachmentBytes != null &&
                            attachmentMimeType != null)
                        {
                            memoryStream.Write(attachmentBytes, 0, attachmentBytes.Length);

                            memoryStream.Seek(0, SeekOrigin.Begin);

                            var mailAttachment = new Attachment(memoryStream, attachmentName, attachmentMimeType);

                            mailMessage.Attachments.Add(mailAttachment);
                        }

                        using (var smtpClient = new SmtpClient(_smtpSettings.ServerAddress, _smtpSettings.ServerPort))
                        {
                            var networkCredential = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.UserPassword);

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
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return false;
        }
    }
}

