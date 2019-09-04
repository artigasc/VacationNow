using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces {
    public interface IPaymentData {
        Task<string> Insert(Payment valPayment);
        string SelectById(PaymentSearch valSearch);
        string SelectByUser(PaymentUserSearch valSearch);
        string UpdateRanking(Payment valPayment);
        Task<string> CancelAndRefund(Payment valPayment);
    }
}
