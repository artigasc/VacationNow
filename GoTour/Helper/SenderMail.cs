using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GoTour.Helper {
    public  class SenderMail {
        private readonly EmailHelper _vEmail;
        public SenderMail() {
            _vEmail = new EmailHelper("", "", "", "");
        }


        public  async Task SendEmailWithPayment(Payment vItem) {
            try {
                string vBody = FormatEmail.GetMessageBodyPayRegistered(vItem);

                List<string> vRecipients = new List<string>();
                vRecipients.Add(vItem.Email);
                //vRecipients.Add("brihan.bocanegra@weesystem.com");
                await _vEmail.PostSendEmail(vRecipients, "Comprobante de Pago Realizado", vBody, true, null);
            } catch (Exception) { }

        }
        public async Task SendEmailToCompanies(Payment vItem) {
            try {
                string vBody = FormatEmail.GetMessageBodyNotifyCompanies(vItem);

                List<string> vRecipients = new List<string>();
                vRecipients.Add(vItem.EmailCompany1);
                vRecipients.Add(vItem.EmailCompany2);
                //vRecipients.Add("brihan.bocanegra@weesystem.com");
                await _vEmail.PostSendEmail(vRecipients, vItem.FirstName + " " + vItem.LastName + " ha reservado un Tour", vBody, true, null);
            } catch (Exception) { }
        }

        public async Task SendEmailClientWithCancelAndRefund(Payment vItem) {
            try {
                string vBody = FormatEmail.GetMessageBodyPayCancelAndRefund(vItem);

                List<string> vRecipients = new List<string>();
                vRecipients.Add(vItem.Email);
                vRecipients.Add("cristian.artigas@weesystem.com");
                await _vEmail.PostSendEmail(vRecipients, "Su compra ha sido Cancelada", vBody, true, null);
            } catch (Exception) { }

        }

        public async Task SendEmailCompaniesWithCancelAndRefund(Payment vItem) {
            try {
                string vBody = FormatEmail.GetMessageBodyCompaniesCancelAndRefund(vItem);
                List<string> vRecipients = new List<string>();
                vRecipients.Add(vItem.EmailCompany1);
                if (!string.IsNullOrEmpty(vItem.EmailCompany2)) {
                    vRecipients.Add(vItem.EmailCompany2);
                }
                //vRecipients.Add("brihan.bocanegra@weesystem.com");
                await _vEmail.PostSendEmail(vRecipients, vItem.FirstName + " " + vItem.LastName + " ha cancelado un Tour", vBody, true, null);
            } catch (Exception) { }

        }
    }
}