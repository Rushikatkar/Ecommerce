using BAL.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process-payment")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var paymentId = await _paymentService.ProcessPaymentAsync(request.OrderId, request.PaymentMethod, request.Amount);
                return Ok(new { PaymentId = paymentId, Message = "Payment processed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetPaymentDetails(int orderId)
        {
            try
            {
                var payment = await _paymentService.GetPaymentDetailsAsync(orderId);
                if (payment == null)
                    return NotFound(new { Message = "Payment not found." });

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }

    public class ProcessPaymentRequest
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
