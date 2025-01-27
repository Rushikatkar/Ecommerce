using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.PaymentRepo
{
    public interface IPaymentRepository
    {
        Task<int> AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    }
}
