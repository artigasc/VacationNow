using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class PaymentGateway {
        public string language { get { return ConfigurationManager.AppSettings["LanguagePaymentGateway"]; } }
        public string command { get { return ConfigurationManager.AppSettings["CommandInsertPaymentGateway"]; } }
        public Merchant merchant { get; set; }
        public Transaction transaction { get; set; }
        public bool test { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["TestPaymentGateway"]); } }
    }

    public class Transaction {
        public Order order { get; set; }
        public Payer payer { get; set; }
        public CreditCard creditCard { get; set; }
        public string type { get { return ConfigurationManager.AppSettings["TypePaymentGateway"]; } }
        public string paymentMethod { get; set; }
        public string paymentCountry { get { return ConfigurationManager.AppSettings["CountryPaymentGateway"]; } }
        public string deviceSessionId { get { return ConfigurationManager.AppSettings["DeviceSessionIdPaymentGateway"]; } }
        public string ipAddress { get; set; }
        public string cookie { get; set; }
        public string userAgent { get; set; }
    }

    public class CreditCard {
        public string number { get; set; }
        public string securityCode { get; set; }
        public string expirationDate { get; set; }
        public string name { get; set; }
    }

    public class Payer {
        public string merchantPayerId { get; set; }
        public string fullName { get; set; }
        public string emailAddress { get; set; }
        public string contactPhone { get; set; }
        public string dniNumber { get; set; }
        public Address billingAddress { get; set; }
    }

    public class Order {
        public string accountId { get { return ConfigurationManager.AppSettings["AccountIdPaymentGateway"]; } }
        public string referenceCode { get { return ConfigurationManager.AppSettings["ReferenceCodePaymentGateway"]; } }
        public string description { get { return ConfigurationManager.AppSettings["DescriptionPaymentGateway"]; } }
        public string language { get { return ConfigurationManager.AppSettings["LanguagePaymentGateway"]; } }
        public string signature { get { return ConfigurationManager.AppSettings["SignaturePaymentGateway"]; } }
        public string notifyUrl { get; set; }
        public AdditionalValues additionalValues { get; set; }
        public Buyer buyer { get; set; }
        public Address shippingAddress { get; set; }
    }

    public class Buyer {
        public string merchantBuyerId { get { return ConfigurationManager.AppSettings["MerchantBuyerIdPaymentGateway"]; } }
        public string fullName { get; set; }
        public string emailAddress { get; set; }
        public string contactPhone { get; set; }
        public string dniNumber { get; set; }
        public Address shippingAddress { get; set; }
    }

    public class Address {
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get { return ConfigurationManager.AppSettings["CountryPaymentGateway"]; } }
        public string postalCode { get; set; }
        public string phone { get; set; }
    }

    public class AdditionalValues {
        public TX_VALUEType TX_VALUE { get; set; }
    }

    public class TX_VALUEType {
        public TX_VALUEType() { }
        public float value { get; set; }
        public string currency { get; set; }
    }

    public class Merchant {
        public Merchant() {

        }
        public string apiKey { get { return ConfigurationManager.AppSettings["KeyPaymentGateway"]; } }
        public string apiLogin { get { return ConfigurationManager.AppSettings["LoginPaymentGateway"]; } }
    }

    public class ResponseGateway {
        public string code { get; set; }
        public TransactionResponse transactionResponse { get; set; }
    }

    public class TransactionResponse {
        public string orderId { get; set; }
        public Guid transactionId { get; set; }
        public string state { get; set; }
        public string paymentNetworkResponseCode { get; set; }
        public string paymentNetworkResponseErrorMessage { get; set; }
        public string trazabilityCode { get; set; }
        public string authorizationCode { get; set; }
        public string pendingReason { get; set; }
        public string responseCode { get; set; }
        public string errorCode { get; set; }
        public string responseMessage { get; set; }
        public string transactionDate { get; set; }
        public string transactionTime { get; set; }
        public long operationDate { get; set; }
        public string referenceQuestionnaire { get; set; }
        public dynamic extraParameters { get; set; }
        public string additionalInfo { get; set; }
    }

    
    public class GatewayErrorMessage {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class GatewayRefund {
        public string language { get { return ConfigurationManager.AppSettings["LanguagePaymentGateway"]; } }
        public string command { get { return ConfigurationManager.AppSettings["CommandInsertPaymentGateway"]; } }
        public Merchant merchant { get; set; }
        public TransactionRefund transaction { get; set; }
       
        public bool test { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["TestPaymentGateway"]); } }
    }

    public class TransactionRefund {
        public OrderRefund order { get; set; }
        public string type {
            get { return ConfigurationManager.AppSettings["TypeRefundPaymentGateway"]; }
        }
        public string reason { get { return "Reason for requesting the refund or cancellation of the transaction"; } }
        public string parentTransactionId { get; set; }
    }

    public class OrderRefund {
        public string id { get; set; }
    }

    public class ResponseGatewayRefund {
        public string code { get; set; }
        public string error { get; set; }
        public TransactionResponse transactionResponse { get; set; }
    }
}