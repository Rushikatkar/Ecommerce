using DAL.Models;
using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // First, insert the order to generate the OrderId
                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();  // This commits the order and generates the OrderId

                    // Now, set the OrderId in the orderDetails
                    foreach (var detail in orderDetails)
                    {
                        detail.OrderId = order.OrderId;  // Set the OrderId for each order detail
                    }

                    // Add the order details
                    await _context.OrderDetails.AddRangeAsync(orderDetails);

                    // Decrease stock in the Products table
                    foreach (var detail in orderDetails)
                    {
                        var product = await _context.Products.FindAsync(detail.ProductId);
                        if (product != null && product.Stock >= detail.Quantity)
                        {
                            product.Stock -= detail.Quantity;
                        }
                        else
                        {
                            throw new Exception($"Insufficient stock for product: {detail.ProductId}");
                        }
                    }

                    // Save all changes and commit the transaction
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return order;
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.Error.WriteLine($"Exception: {ex.Message}");
                    Console.Error.WriteLine($"InnerException: {ex.InnerException?.Message}");
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }






    }
}
