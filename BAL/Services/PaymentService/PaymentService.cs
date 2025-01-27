using DAL.Models.Entities;
using DAL.Repositories.PaymentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<int> ProcessPaymentAsync(int orderId, string paymentMethod, decimal amount)
        {
            // Simulate payment processing logic
            string paymentStatus = "Success"; // Assume success for mock integration

            var payment = new Payment
            {
                OrderId = orderId,
                PaymentMethod = paymentMethod,
                PaymentDate = DateTime.Now,
                Amount = amount,
                Status = paymentStatus
            };

            return await _paymentRepository.AddPaymentAsync(payment);
        }

        public async Task<Payment> GetPaymentDetailsAsync(int orderId)
        {
            return await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
        }
    }
}
