using BAL.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(int userId)
        {
            try
            {
                var order = await _orderService.PlaceOrderAsync(userId);
                return Ok(new { Message = "Order placed successfully.", OrderId = order.OrderId });
            }
            catch (Exception ex)
            {
                // Log the error to get more details
                Console.Error.WriteLine($"Error: {ex.Message}");
                Console.Error.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

                return BadRequest(new { Error = ex.Message, InnerError = ex.InnerException?.Message });
            }
        }


        [HttpGet("GetUserOrders/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            try
            {
                var orders = await _orderService.GetUserOrdersAsync(userId);

                // Define custom serialization options
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    MaxDepth = 32
                };

                // Serialize the response with custom options
                string json = JsonSerializer.Serialize(orders, options);

                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("UpdateOrderStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(orderId, status);
                return Ok(new { Message = "Order status updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
