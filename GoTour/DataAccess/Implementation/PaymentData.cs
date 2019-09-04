using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace GoTour.DataAccess.Implementation {
    public class PaymentData : IPaymentData {
        public string vConnection = "Master";
        
        public async Task<string> Insert(Payment valPayment) {
            string vResult = string.Empty;
            try {
                ResponseGateway vProccessPayGateway = await InsertPaymentGateway(valPayment);
                GatewayService vGatewayService = new GatewayService();
                if (vProccessPayGateway != null && vProccessPayGateway.transactionResponse.state == "APPROVED") {
                    valPayment.IdTransaction = vProccessPayGateway.transactionResponse.transactionId;
                    valPayment.GatewayJsonData = JsonConvert.SerializeObject(vProccessPayGateway);
                    SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                    List<SqlParameter> vParameterList = new List<SqlParameter> {
                        new SqlParameter("@Id", valPayment.Id),
                        new SqlParameter("@IdTransaction", !(valPayment.IdTransaction==Guid.Empty) ? valPayment.IdTransaction : Guid.Empty),
                        new SqlParameter("@GatewayJsonGata", !string.IsNullOrEmpty(valPayment.GatewayJsonData) ? valPayment.GatewayJsonData : "{}"),
                        new SqlParameter("@IdUser", valPayment.IdUser != Guid.Empty ? valPayment.IdUser : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@IdActivity", valPayment.IdActivity != Guid.Empty ? valPayment.IdActivity : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@Mount", valPayment.Mount),
                        new SqlParameter("@IdCurrency", valPayment.IdCurrency != Guid.Empty ? valPayment.IdCurrency : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@FirstName", !string.IsNullOrEmpty(valPayment.FirstName) ? valPayment.FirstName : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@LastName", !string.IsNullOrEmpty(valPayment.LastName) ? valPayment.LastName : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@TypeNumberDocument", !string.IsNullOrEmpty(valPayment.TypeNumberDocument) ? valPayment.TypeNumberDocument : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@NumberDocument", !string.IsNullOrEmpty(valPayment.NumberDocument) ? valPayment.NumberDocument : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@Email", !string.IsNullOrEmpty(valPayment.Email) ? valPayment.Email : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@Phone", !string.IsNullOrEmpty(valPayment.Phone) ? valPayment.Phone : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@Persons", valPayment.Persons),
                        new SqlParameter("@DateReserve", valPayment.DateReserve),
                        new SqlParameter("@State", valPayment.State),
                        new SqlParameter("@UserCreate", !string.IsNullOrEmpty(valPayment.UserCreate) ? valPayment.UserCreate : string.Empty),
                        new SqlParameter("@DateCreate", valPayment.DateCreate)
                    };
                    bool vInsert = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Insert_Payment", vConnection);

                    if (vInsert) {
                        SenderMail vSenderMail = new SenderMail();
                        await vSenderMail.SendEmailWithPayment(valPayment);
                        await vSenderMail.SendEmailToCompanies(valPayment);
                        vResult = "1";
                        vResult = "1";
                    } else {
                        vResult = "3";
                    }
                } else {
                    vResult = GatewayUtils.ProccessingResponseGateway(vProccessPayGateway.transactionResponse.state);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "4";

            }
            return vResult;
        }


        public string SelectById(PaymentSearch valSearch) {
            string vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdPayment", valSearch.IdPayment.ToString()),
                    new SqlParameter("@IdLanguage", valSearch.IdLanguage.ToString())
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Payment", vConnection);
                Payment vData = DataTableToElement(vDatainTable);
                if (vData != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }

        public Payment SelectById(Guid valIdPayment,Guid valIdLanguage) {
            Payment vResult = new Payment();
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdPayment", valIdPayment.ToString()),
                    new SqlParameter("@IdLanguage", valIdLanguage.ToString())
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Payment", vConnection);
                vResult = DataTableToElement(vDatainTable);
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            
            return vResult;

        }


        public List<Payment> DataTableToList(DataTable table) {

            List<Payment> vResult = new List<Payment>();
            try {
                foreach (DataRow row in table.Rows) {
                    Payment vPayment = new Payment {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        IdTransaction = Guid.Parse(Convert.ToString(row["IdTransaction"])),
                        GatewayJsonData = Convert.ToString(row["GatewayJsonData"]),
                        IdUser = Guid.Parse(Convert.ToString(row["IdUser"])),
                        IdActivity = Guid.Parse(Convert.ToString(row["IdActivity"])),
                        Mount = float.Parse(Convert.ToString(row["Mount"])),
                        Persons = Convert.ToInt32(row["Persons"]),
                        DateReserve = row["DateReserve"] != DBNull.Value ? Convert.ToDateTime(row["DateReserve"]) : DateTime.MinValue,
                        IdCurrency = Guid.Parse(Convert.ToString(row["IdCurrency"])),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
                        Email = Convert.ToString(row["Email"]),
                        Phone = Convert.ToString(row["Phone"]),
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"]),
                        Symbol = Convert.ToString(row["Symbol"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = row["UserUpdate"] != DBNull.Value ? Convert.ToString(row["UserUpdate"]) : string.Empty,

                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vPayment);
                }

        
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Payment>();
            }
            return vResult;
        }

        public Payment DataTableToElement(DataTable table) {

            Payment vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    vResult = new Payment {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        IdTransaction = !string.IsNullOrEmpty(Convert.ToString(row["IdTransaction"])) ? Guid.Parse(Convert.ToString(row["IdTransaction"])) : Guid.Empty,
                        GatewayJsonData = Convert.ToString(row["GatewayJsonData"]),
                        IdUser = Guid.Parse(Convert.ToString(row["IdUser"])),
                        IdActivity = Guid.Parse(Convert.ToString(row["IdActivity"])),
                        Mount = float.Parse(Convert.ToString(row["Mount"])),
                        Persons = Convert.ToInt32(row["Persons"]),
                        DateReserve = row["DateReserve"] != DBNull.Value ? Convert.ToDateTime(row["DateReserve"]) : DateTime.MinValue,
                        IdCurrency = Guid.Parse(Convert.ToString(row["IdCurrency"])),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
                        Email = Convert.ToString(row["Email"]),
                        Phone = Convert.ToString(row["Phone"]),
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"]),
                        Symbol = Convert.ToString(row["Symbol"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = row["UserUpdate"] != DBNull.Value ? Convert.ToString(row["UserUpdate"]) : string.Empty,
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public string SelectByUser(PaymentUserSearch valSearch) {
            string vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdUser", valSearch.IdUser.ToString()),
                    new SqlParameter("@IdLanguage", valSearch.IdLanguage.ToString()),
                    new SqlParameter("@RowsPerPage", valSearch.RowsPerPage),
                    new SqlParameter("@PageNumber", valSearch.PageNumber)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Payment_User", vConnection);
                List<Payment> vData = DataTableToListSearchUser(vDatainTable);
                SetIfUserHasRankingActivity(ref vData);
                long vTotalRows = GetTotalByUser(valSearch);
                PaymentResponse vResponseObject = new PaymentResponse();
                vResponseObject.Payments = vData;
                vResponseObject.TotalRows = vTotalRows;
                if (vResponseObject != null) {
                    vResult = JsonConvert.SerializeObject(vResponseObject, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }

        private void SetIfUserHasRankingActivity(ref List<Payment> valData) {
            if (valData != null && valData.Count > 0) {
                foreach (Payment vItem in valData) {
                    vItem.IsRankingForUser= GetIfUserHAsRankingActivity(vItem.UserCreate, vItem.IdActivity, vItem.Id) > 0;
                }
            }
        }

        private long GetTotalByUser(PaymentUserSearch valSearch) {
            long vResult = 0;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdUser", valSearch.IdUser.ToString()),
                    new SqlParameter("@IdLanguage", valSearch.IdLanguage.ToString()),
                    new SqlParameter("@RowsPerPage", valSearch.RowsPerPage),
                    new SqlParameter("@PageNumber", valSearch.PageNumber)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Payment_User_Count", vConnection);
                vResult = DataTableToListSearchTotal(vDatainTable);
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
            }
            return vResult;
        }
        private long DataTableToListSearchTotal(DataTable valDatainTableCount) {
            long vResult = 0;
            try {
                foreach (DataRow vRow in valDatainTableCount.Rows) {
                    vResult = Convert.ToInt64(vRow["TOTAL_ROWS"]);
                };
            } catch (Exception) { }
            return vResult;
        }

        public List<Payment> DataTableToListSearchUser(DataTable table) {

            List<Payment> vResult = new List<Payment>();
            try {
                foreach (DataRow row in table.Rows) {
                    Payment vPayment = new Payment {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        IdTransaction = !string.IsNullOrEmpty(Convert.ToString(row["IdTransaction"])) ? Guid.Parse(Convert.ToString(row["IdTransaction"])) :Guid.Empty ,
                        GatewayJsonData = Convert.ToString(row["GatewayJsonData"]),
                        IdUser = Guid.Parse(Convert.ToString(row["IdUser"])),
                        IdActivity = Guid.Parse(Convert.ToString(row["IdActivity"])),
                        Mount = float.Parse(Convert.ToString(row["Mount"])),
                        Persons = Convert.ToInt32(row["Persons"]),
                        DateReserve = row["DateReserve"] != DBNull.Value ? Convert.ToDateTime(row["DateReserve"]) : DateTime.MinValue,
                        FreeCancelation = Convert.ToInt16(row["FreeCancelation"]),
                        IdCurrency = Guid.Parse(Convert.ToString(row["IdCurrency"])),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
                        Email = Convert.ToString(row["Email"]),
                        Phone = Convert.ToString(row["Phone"]),
                        Name = Convert.ToString(row["Name"]),
                        Description = row["Description"] != DBNull.Value ? Convert.ToString(row["Description"]) : string.Empty,
                        Ranking = row["Ranking"] != DBNull.Value ? Convert.ToInt32(row["Ranking"]): 0,
                        Duration = Convert.ToInt32(row["Duration"]),
                        MinimumPeople = Convert.ToInt32(row["MinimumPeople"]),
                        SellTimeAdvance =  Convert.ToInt32(row["SellTimeAdvance"]),
                        Symbol = Convert.ToString(row["Symbol"]),
                        NameCompany = Convert.ToString(row["NameCompany"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = row["UserUpdate"] != DBNull.Value ? Convert.ToString(row["UserUpdate"]) : string.Empty,
                         DateUpdate = row["DateUpdate"] != DBNull.Value ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vPayment);
                }


            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Payment>();
            }
            return vResult;
        }

        public string UpdateRanking(Payment valPayment) {
            string vResult = string.Empty;
            try {
               
                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valPayment.Id),
                    new SqlParameter("@IdActivity", valPayment.IdActivity != Guid.Empty ? valPayment.IdActivity : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Ranking", valPayment.Ranking),
                    new SqlParameter("@UserUpdate", !string.IsNullOrEmpty(valPayment.UserUpdate) ? valPayment.UserUpdate : string.Empty),
                    new SqlParameter("@DateUpdate", DateTime.Now)
                };
                bool vInsert = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Update_Payment_Activity_Ranking", vConnection);
                if (vInsert) {

                    vResult = "1";
                } else {
                    vResult = "4";
                }

            } catch (Exception) {
                vResult = "4";

            }
            return vResult;
        }

        
        private async Task<ResponseGateway> InsertPaymentGateway(Payment valPayment) {
            ResponseGateway vResult = new ResponseGateway();
            try {
                PaymentGateway vPaymentGateway = InstanceTransaction();
                GatewayService vGatewayService = new GatewayService();
                ICurrencyData vCurrency = new CurrencyData();
                vPaymentGateway.transaction.creditCard.name = valPayment.FirstName + " " + valPayment.LastName;
                vPaymentGateway.transaction.creditCard.name = "APPROVED";
                vPaymentGateway.transaction.creditCard.number = valPayment.CardNumber;
                vPaymentGateway.transaction.creditCard.expirationDate = valPayment.Year + "/" + valPayment.Month;
                vPaymentGateway.transaction.creditCard.securityCode = valPayment.SecurityCode;
                vPaymentGateway.transaction.order.additionalValues.TX_VALUE.value = valPayment.TotalMount;
                vPaymentGateway.transaction.order.additionalValues.TX_VALUE.value = 100;
                vPaymentGateway.transaction.order.additionalValues.TX_VALUE.currency = vCurrency.SelectById(valPayment.IdCurrency).Code;
                //vPaymentGateway.transaction.order.additionalValues.TX_VALUE.currency = vCurrency.SelectById(Guid.Parse("2AC154DA-120F-4BBA-B4E2-DB728AC89DA0")).Code;
                vPaymentGateway.transaction.paymentMethod = valPayment.PayMethod;
                //vPaymentGateway.transaction.paymentMethod = "VISA";
                vResult = await vGatewayService.Create(vPaymentGateway);
            } catch (Exception) {

            }
            return vResult;
        }

        private PaymentGateway InstanceTransaction() {
            PaymentGateway vResult = new PaymentGateway();
            vResult.transaction = new Transaction();
            vResult.merchant = new Merchant();
            vResult.transaction.creditCard = new CreditCard();
            vResult.transaction.order = new Order();
            vResult.transaction.order.additionalValues = new AdditionalValues();
            vResult.transaction.order.additionalValues.TX_VALUE = new TX_VALUEType();
            vResult.transaction.order.buyer = new Buyer();
            vResult.transaction.order.buyer.shippingAddress = new Address();
            vResult.transaction.order.shippingAddress = new Address();
            vResult.transaction.payer = new Payer();
            vResult.transaction.payer.billingAddress = new Address();
            return vResult;
        }

        public async Task<string> CancelAndRefund(Payment valPayment) {
            string vResult = string.Empty;
            try {
                ResponseGatewayRefund vProccessPayGateway = await Refund(valPayment);
                GatewayService vGatewayService = new GatewayService();
               // if (vProccessPayGateway != null && vProccessPayGateway.transactionResponse.state == "PENDING" && vProccessPayGateway.code != "ERROR") {
                    
                    valPayment.GatewayJsonData = JsonConvert.SerializeObject(vProccessPayGateway);
                    SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                    List<SqlParameter> vParameterList = new List<SqlParameter> {
                        new SqlParameter("@Id", valPayment.Id),
                        new SqlParameter("@GatewayJsonData", !string.IsNullOrEmpty(valPayment.GatewayJsonData) ? valPayment.GatewayJsonData : "{}"),
                        new SqlParameter("@UserUpdate", valPayment.UserUpdate),
                        new SqlParameter("@DateUpdate", valPayment.DateUpdate)
                    };
                    bool vUpdate = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Cancel_Payment", vConnection);

                    if (vUpdate) {
                        SenderMail vSenderMail = new SenderMail();
                        Payment vPaymenNew = SelectById(valPayment.Id, valPayment.IdLanguage);
                        vPaymenNew.LanguageInitials = valPayment.LanguageInitials;
                        await vSenderMail.SendEmailClientWithCancelAndRefund(vPaymenNew);
                        ICompanyData vCompanyData = new CompanyData();
                        Company vCompany = vCompanyData.SelectCompanyByPayment(valPayment.Id);
                        vPaymenNew.EmailCompany1 = vCompany.Email1;
                        vPaymenNew.EmailCompany2 = vCompany.Email2;
                        await vSenderMail.SendEmailCompaniesWithCancelAndRefund(vPaymenNew);
                        vResult = "1";
                    } else {
                        vResult = "3";
                    }
                //} else {
                //    vResult = GatewayUtils.ProccessingResponseGateway(vProccessPayGateway.transactionResponse.state);
                //}
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "4";

            }
            return vResult;
        }

        private async Task<ResponseGatewayRefund> Refund(Payment valPayment) {
            ResponseGatewayRefund vResult = new ResponseGatewayRefund();
            try {
                GatewayRefund vPaymentGateway = InstanceClassRefound();
                GatewayService vGatewayService = new GatewayService();
                ICurrencyData vCurrency = new CurrencyData();
                ResponseGateway vInfoPayment = JsonConvert.DeserializeObject<ResponseGateway>(valPayment.GatewayJsonData);
                vPaymentGateway.transaction.order.id = vInfoPayment.transactionResponse.orderId;
                vPaymentGateway.transaction.parentTransactionId = valPayment.IdTransaction.ToString();
                vResult = await vGatewayService.Refund(vPaymentGateway);
            } catch (Exception) {

            }
            return vResult;
        }

        private GatewayRefund InstanceClassRefound() {
            GatewayRefund vResult = new GatewayRefund();
            vResult.merchant = new Merchant();
            vResult.transaction = new TransactionRefund();
            vResult.transaction.order = new OrderRefund();

            return vResult;
        }

        private int GetIfUserHAsRankingActivity(string valUserCreate, Guid valIdActivity, Guid valIdPayment) {
            int vResult = 0;
            try {

                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@UserCreate", valUserCreate),
                    new SqlParameter("@IdActivity", valIdActivity.ToString()),
                    new SqlParameter("@IdPayment", valIdPayment.ToString())
                   
                };
                DataTable vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_If_User_Has_Ranking_Activity", vConnection);
                foreach (DataRow vRow in vDatainTable.Rows) {
                    vResult = Convert.ToInt32(vRow["TOTAL_RANKING"]);
                };
            } catch (Exception) {
                vResult = 0;
            }
            return vResult;
        }

    }
}