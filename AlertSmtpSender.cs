using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading.Tasks;

namespace MessagingServer.Core.Mail
{
    public class AlertSmtpSender : SmtpSender
    {
        public AlertSmtpSender(ISmtpSettings smtpSettings, IMailTemplates mailTemplates)
            : base(smtpSettings, mailTemplates) { }

        public Task<bool> SendAlertMail(string recipientAddress, string recipientName,
            bool usePlainText, string alertName, string machineId, DateTime creationTime, string alertInfo)
        {
            var contentReplacements = new Dictionary<string, string>
            {
                { "MACHINEID", machineId },
                { "ALERTTIME", creationTime.ToString(CultureInfo.CurrentUICulture) },
                { "ALERTNAME", alertName },
                { "ALERTINFO", alertInfo }
            };

            var mailSubject = $"Alert: {alertName}, machine name: {machineId}";
            var mailBody = MailTemplates.CreateMailContent("core", "alert", usePlainText, contentReplacements);

            return SendMail(recipientAddress, recipientName, mailSubject, mailBody);
        }
    }
}
