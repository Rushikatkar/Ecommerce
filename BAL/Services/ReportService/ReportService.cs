using DAL.DTOs;
using DAL.Repositories.ReportRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<SalesReport> GetSalesReport(string period)
        {
            return _reportRepository.GetSalesReport(period);
        }

        public List<TopSellingProduct> GetTopSellingProducts(int top)
        {
            return _reportRepository.GetTopSellingProducts(top);
        }

        public List<ActiveUser> GetMostActiveUsers(int top)
        {
            return _reportRepository.GetMostActiveUsers(top);
        }
    }
}
