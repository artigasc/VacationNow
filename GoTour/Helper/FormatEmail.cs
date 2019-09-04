using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GoTour.Helper {
    public static class FormatEmail {

        public static string GetMessageBodyPayRegistered(Payment valPayment) {
            string htmlBody = "";
            string vLanguageGeneral = string.Empty;
            switch (valPayment.LanguageInitials) {
                case "ES":
                    vLanguageGeneral = @"\Templates\PayRegistered" + valPayment.LanguageInitials + ".html";
                    //vLanguageGeneral = @"\1.html";

                    break;
                case "EN":
                    vLanguageGeneral = @"\Templates\PayRegistered" + valPayment.LanguageInitials + ".html";
                    break;
                case "PO":
                    vLanguageGeneral = @"\Templates\PayRegistered" + valPayment.LanguageInitials + ".html";
                    break;

            }

            try {
                htmlBody = FileHelper.ReadFile(vLanguageGeneral);

            } catch (Exception) {
                htmlBody = string.Empty;
            }

            htmlBody = htmlBody.Replace("@@TRANSACTIONNUMBER", valPayment.IdTransaction.ToString())
                                .Replace("@@DOCUMENTNUMBERTYPE", TypeDocument.GetList(valPayment.LanguageInitials).FirstOrDefault().Name)
                                .Replace("@@DOCUMENTNUMBER", valPayment.NumberDocument)
                                .Replace("@@FULLNAME", valPayment.FirstName + " " + valPayment.LastName)
                                .Replace("@@TOURNAME", valPayment.Name)
                                .Replace("@@PERSONS", valPayment.Persons.ToString())
                                .Replace("@@COMPANYNAME", valPayment.NameCompany)
                                .Replace("@@MOUNT", valPayment.TotalMount.ToString())
                                .Replace("@@CURRENCYSYMBOL", valPayment.Symbol)
                                .Replace("@@DATE", valPayment.DateCreate.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture));

            return htmlBody;
        }

        public static string GetMessageBodyNotifyCompanies(Payment valPayment) {
            string htmlBody = "";
            string vLanguageGeneral = string.Empty;
            switch (valPayment.LanguageInitials) {
                case "ES":
                    vLanguageGeneral = @"\Templates\NotifyBuy" + valPayment.LanguageInitials + ".html";
                    //vLanguageGeneral = @"\1.html";
                    break;
                case "EN":
                    vLanguageGeneral = @"\Templates\NotifyBuy" + valPayment.LanguageInitials + ".html";
                    break;
                case "PO":
                    vLanguageGeneral = @"\Templates\NotifyBuy" + valPayment.LanguageInitials + ".html";
                    break;

            }

            try {
                htmlBody = FileHelper.ReadFile(vLanguageGeneral);

            } catch (Exception) {
                htmlBody = string.Empty;
            }

            htmlBody = htmlBody.Replace("@@TRANSACTIONNUMBER", valPayment.IdTransaction.ToString())
                                .Replace("@@DOCUMENTNUMBERTYPE", TypeDocument.GetList(valPayment.LanguageInitials).FirstOrDefault().Name)
                                .Replace("@@DOCUMENTNUMBER", valPayment.NumberDocument)
                                .Replace("@@FULLNAME", valPayment.FirstName + " " + valPayment.LastName)
                                .Replace("@@TOURNAME", valPayment.Name)
                                .Replace("@@PERSONS", valPayment.Persons.ToString())
                                .Replace("@@COMPANYNAME", valPayment.NameCompany)
                                .Replace("@@MOUNT", valPayment.TotalMount.ToString())
                                .Replace("@@CURRENCYSYMBOL", valPayment.Symbol)
                                .Replace("@@DATE", valPayment.DateCreate.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture));

            return htmlBody;
        }

        public static string GetMessageBodyPayCancelAndRefund(Payment valPayment) {
            string htmlBody = "";
            string vLanguageGeneral = string.Empty;
            switch (valPayment.LanguageInitials) {
                case "ES":
                    vLanguageGeneral = @"\Templates\NotifyCancelRefund" + valPayment.LanguageInitials + ".html";
                    break;
                case "EN":
                    vLanguageGeneral = @"\Templates\NotifyCancelRefund" + valPayment.LanguageInitials + ".html";
                    break;
                case "PO":
                    vLanguageGeneral = @"\Templates\NotifyCancelRefund" + valPayment.LanguageInitials + ".html";
                    break;

            }

            try {
                htmlBody = FileHelper.ReadFile(vLanguageGeneral);

            } catch (Exception) {
                htmlBody = string.Empty;
            }

            htmlBody = htmlBody.Replace("@@RESERVENUMBER", valPayment.Id.ToString().Substring(0, 8))
                                .Replace("@@TRANSACTIONNUMBER", valPayment.IdTransaction.ToString())
                                .Replace("@@DOCUMENTNUMBERTYPE", TypeDocument.GetList(valPayment.LanguageInitials).FirstOrDefault().Name)
                                .Replace("@@DOCUMENTNUMBER", valPayment.NumberDocument)
                                .Replace("@@FULLNAME", valPayment.FirstName + " " + valPayment.LastName)
                                .Replace("@@TOURNAME", valPayment.Name)
                                .Replace("@@PERSONS", valPayment.Persons.ToString())
                                .Replace("@@COMPANYNAME", valPayment.NameCompany)
                                .Replace("@@MOUNT", valPayment.Mount.ToString())
                                .Replace("@@CURRENCYSYMBOL", valPayment.Symbol)
                                .Replace("@@DATE", valPayment.DateCreate.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture))
                                .Replace("@@STATEREFUND", valPayment.GetNameStateRefund(valPayment.LanguageInitials, "APPROVED"));

            return htmlBody;
        }

        public static string GetMessageBodyCompaniesCancelAndRefund(Payment valPayment) {
            string htmlBody = "";
            string vLanguageGeneral = string.Empty;
            switch (valPayment.LanguageInitials) {
                case "ES":
                    vLanguageGeneral = @"\Templates\NotifyCompaniesCancelRefund" + valPayment.LanguageInitials + ".html";
                    break;
                case "EN":
                    vLanguageGeneral = @"\Templates\NotifyCompaniesCancelRefund" + valPayment.LanguageInitials + ".html";
                    break;
                case "PO":
                    vLanguageGeneral = @"\Templates\NotifyCompaniesCancelRefund" + valPayment.LanguageInitials + ".html";
                    break;

            }

            try {
                htmlBody = FileHelper.ReadFile(vLanguageGeneral);

            } catch (Exception) {
                htmlBody = string.Empty;
            }

            htmlBody = htmlBody.Replace("@@RESERVENUMBER", valPayment.Id.ToString().Substring(0, 8))
                                 .Replace("@@TRANSACTIONNUMBER", valPayment.IdTransaction.ToString())
                                 .Replace("@@DOCUMENTNUMBERTYPE", TypeDocument.GetList(valPayment.LanguageInitials).FirstOrDefault().Name)
                                 .Replace("@@DOCUMENTNUMBER", valPayment.NumberDocument)
                                 .Replace("@@FULLNAME", valPayment.FirstName + " " + valPayment.LastName)
                                 .Replace("@@TOURNAME", valPayment.Name)
                                 .Replace("@@PERSONS", valPayment.Persons.ToString())
                                 .Replace("@@COMPANYNAME", valPayment.NameCompany)
                                 .Replace("@@MOUNT", valPayment.Mount.ToString())
                                 .Replace("@@CURRENCYSYMBOL", valPayment.Symbol)
                                 .Replace("@@DATE", valPayment.DateCreate.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture))
                                 .Replace("@@STATEREFUND", valPayment.GetNameStateRefund(valPayment.LanguageInitials, "APPROVED"));


            return htmlBody;
        }
    }
}