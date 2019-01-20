using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading.Tasks;

namespace MessagingServer.Core.Mail
{
    public class AlertSmtpSender : SmtpSender
    {
        private readonly string _machineId;
        private readonly string _creationTime;
        private readonly string _alertName;
        private readonly string _alertInfo;
        private readonly bool _usePlainText;
        

        public string MachineId { get; set; }
        public string CreationTime { get; set; }
        public string AlertName { get; set; }
        public string AlertInfo { get; set; }
        public bool UsePlainText { get; set; }


        public AlertSmtpSender(ISmtpSettings smtpSettings, IMailTemplates mailTemplates)
            : base(smtpSettings, mailTemplates){}

        public override Task<bool> SendMail(string toAddr, string toName, string mailSubject, string body, string anotherParameter)
        {
            var contentReplacements = new Dictionary<string, string>
            {
                {"MACHINEID", _machineId},
                {"ALERTTIME", _creationTime.ToString(CultureInfo.CurrentUICulture)},
                {"ALERTNAME", _alertName},
                {"ALERTINFO", _alertInfo}
            };

            var subject = $"Alert: {_alertName}, machine name: {_machineId}. On subject: {mailSubject}";
            var mailBody = MailTemplates.CreateMailContent("core", "alert", _usePlainText,
                contentReplacements, body);

            return base.SendMail(toAddr, toName, subject, mailBody);
        }
    }
}
