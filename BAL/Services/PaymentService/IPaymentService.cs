using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<int> ProcessPaymentAsync(int orderId, string paymentMethod, decimal amount);
        Task<Payment> GetPaymentDetailsAsync(int orderId);
    }
}
