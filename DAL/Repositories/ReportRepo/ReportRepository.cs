using DAL.DTOs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.ReportRepo
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SalesReport> GetSalesReport(string period)
        {
            return period.ToLower() switch
            {
                "day" => _context.Orders
                    .Where(o => o.Status == "Delivered")
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new SalesReport
                    {
                        Date = g.Key,
                        TotalSales = g.Sum(o => o.TotalAmount)
                    }).ToList(),

                "week" => _context.Orders
                    .Where(o => o.Status == "Delivered")
                    .AsEnumerable() // Switch to LINQ-to-Objects for manual week calculation
                    .GroupBy(o => new
                    {
                        Year = o.OrderDate.Year,
                        Week = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                            o.OrderDate,
                            System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                            DayOfWeek.Monday)
                    })
                    .Select(g => new SalesReport
                    {
                        Date = new DateTime(g.Key.Year, 1, 1).AddDays((g.Key.Week - 1) * 7),
                        TotalSales = g.Sum(o => o.TotalAmount)
                    }).ToList(),


                "month" => _context.Orders
                    .Where(o => o.Status == "Delivered")
                    .GroupBy(o => new
                    {
                        Year = o.OrderDate.Year,
                        Month = o.OrderDate.Month
                    })
                    .Select(g => new SalesReport
                    {
                        Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                        TotalSales = g.Sum(o => o.TotalAmount)
                    }).ToList(),

                _ => throw new ArgumentException("Invalid period. Use 'day', 'week', or 'month'.")
            };
        }

        public List<TopSellingProduct> GetTopSellingProducts(int top)
        {
            return _context.OrderDetails
                .GroupBy(od => od.Product.Name)
                .OrderByDescending(g => g.Sum(od => od.Quantity))
                .Take(top)
                .Select(g => new TopSellingProduct
                {
                    ProductName = g.Key,
                    QuantitySold = g.Sum(od => od.Quantity)
                }).ToList();
        }

        public List<ActiveUser> GetMostActiveUsers(int top)
        {
            return _context.Orders
                .GroupBy(o => o.User.Username)
                .OrderByDescending(g => g.Count())
                .Take(top)
                .Select(g => new ActiveUser
                {
                    UserName = g.Key,
                    OrdersPlaced = g.Count()
                }).ToList();
        }
    }
}
