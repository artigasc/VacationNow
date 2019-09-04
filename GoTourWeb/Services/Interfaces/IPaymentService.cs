using GoTourWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Interfaces {
	public interface IPaymentService {
		Task<PaymentViewModel> SearchPay(PaymentSearchViewModel valPaymenSearch);
        Task<ClientResponseViewModel> Register(PaymentViewModel valModel);
        Task<PaymentResponseViewModel> SearchPayByUser(PaymentSearchUserViewModel valPaymentSearch);
        Task<ClientResponseViewModel> UpdateRanking(PaymentViewModel valModel);
        Task<ClientResponseViewModel> CancelAndRefund(PaymentViewModel valModel);
    }
}
