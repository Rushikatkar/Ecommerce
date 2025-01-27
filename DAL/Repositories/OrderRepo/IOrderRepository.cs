using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.OrderRepo
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order, List<OrderDetail> orderDetails);
        Task<List<Order>> GetUserOrdersAsync(int userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, string status);

    }
}
