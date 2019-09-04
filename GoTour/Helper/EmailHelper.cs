using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace GoTour.Helper {
    public class EmailHelper {
        private string MailAccount { get; set; }
        private string MailPassword { get; set; }
        private string SMTP { get; set; }
        private string MailDisplayName { get; set; }
        private string SystemUrl { get; set; }

        public EmailHelper(string mailAccount, string mailPassword, string sMTP, string mailDisplayName) {
            //MailAccount = "cristian.artigas@weeesystem.com";
            //MailPassword = "Kk9172799";
            //SMTP = "smtp.office365.com";
            //MailDisplayName = "GoTour";
            MailAccount = "artigasc@gmail.com";
            MailPassword = "kkk9172799";
            SMTP = "smtp.gmail.com";
            MailDisplayName = "GoTour";
            //MailAccount = mailAccount; // "colaborador@cmscloud.pe";
            //MailPassword = mailPassword; // "Cms@Cloud2016";
            //SMTP = sMTP; // "smtp.office365.com";
            //MailDisplayName = mailDisplayName; // "Pandero";
        }

        public async Task PostSendEmail(List<string> userNames, string subject, string body, bool isHtml, List<Attachment> attachments) {
            var smtpClient = new SmtpClient {
                Host = SMTP,
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(MailAccount, MailPassword),
                EnableSsl = true
            };

            #region Configurar Mail

            var mail = new MailMessage {
                From = new MailAddress(MailAccount, MailDisplayName)
            };

            foreach (var item in userNames) {
                mail.To.Add(new MailAddress(item.ToString()));
            }

            #endregion

            if (attachments != null) {
                foreach (var item in attachments) {
                    mail.Attachments.Add(item);
                }
            }

            #region Set values to mail

            mail.Subject = subject;
            mail.IsBodyHtml = isHtml;
            mail.Body = body;
            //mail.AlternateViews.Add(html);

            #endregion Set values to mail

            //smtpClient.SendAsync(mail, null);
            try {
                await smtpClient.SendMailAsync(mail);// mail, null);
                smtpClient.SendCompleted += (sender, args) => {
                    smtpClient.Dispose();
                    mail.Dispose();
                };
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
            }
            // smtpClient.SendCompleted += (sender, args) => {
            //    smtpClient.Dispose();
            //    mail.Dispose();
            //};
        }

    }
}
