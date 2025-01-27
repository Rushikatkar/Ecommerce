using DAL.Models.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.PaymentRepo
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);

            // Update the ShoppingCart status to "Success" for the corresponding OrderId
            var cart = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == payment.OrderId);
            if (cart != null)
            {
                cart.Status = "Success";
            }

            await _context.SaveChangesAsync();
            return payment.PaymentId;
        }

        public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }
    }
}
