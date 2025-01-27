using BAL.Services.ShoppingCartService;
using DAL.Models.Entities;
using DAL.Repositories.OrderRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingService _shoppingService;

        public OrderService(IOrderRepository orderRepository, IShoppingService shoppingService)
        {
            _orderRepository = orderRepository;
            _shoppingService = shoppingService;
        }

        public async Task<Order> PlaceOrderAsync(int userId)
        {
            // Get items from the cart
            var cartItems = await _shoppingService.GetCartItemsAsync(userId);
            if (cartItems == null || !cartItems.Any()) throw new Exception("Cart is empty.");

            // Validate stock availability
            foreach (var item in cartItems)
            {
                if (item.Product.Stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for product: {item.Product.Name}");
                }
            }

            // Create order and order details
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cartItems.Sum(item => item.Quantity * item.Product.Price)
            };

            var orderDetails = cartItems.Select(item => new OrderDetail
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Product.Price
            }).ToList();

            // Save order
            return await _orderRepository.CreateOrderAsync(order, orderDetails);
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _orderRepository.GetUserOrdersAsync(userId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
        }
    }
}
