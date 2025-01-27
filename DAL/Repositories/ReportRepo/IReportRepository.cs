using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.ReportRepo
{
    public interface IReportRepository
    {
        List<SalesReport> GetSalesReport(string period);
        List<TopSellingProduct> GetTopSellingProducts(int top);
        List<ActiveUser> GetMostActiveUsers(int top);
    }
}
