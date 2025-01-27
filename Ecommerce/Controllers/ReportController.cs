using BAL.Services.ReportService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("sales")]
        public IActionResult GetSalesReport([FromQuery] string period = "day")
        {
            try
            {
                var salesReport = _reportService.GetSalesReport(period);
                return Ok(salesReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("top-products")]
        public IActionResult GetTopSellingProducts([FromQuery] int top = 5)
        {
            var topProducts = _reportService.GetTopSellingProducts(top);
            return Ok(topProducts);
        }

        [HttpGet]
        [Route("active-users")]
        public IActionResult GetMostActiveUsers([FromQuery] int top = 5)
        {
            var activeUsers = _reportService.GetMostActiveUsers(top);
            return Ok(activeUsers);
        }
    }
}
