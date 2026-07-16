using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.Dashboard;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardVM> GetAsync()
        {
            var model = new DashboardVM
            {
                TotalProducts = await _dashboardRepository.GetProductsCountAsync(),

                TotalOrders = await _dashboardRepository.GetOrdersCountAsync(),

                TotalUsers = await _dashboardRepository.GetUsersCountAsync(),

                TotalRevenue = await _dashboardRepository.GetRevenueAsync()
            };

            var orders = await _dashboardRepository.GetRecentOrdersAsync(5);

            foreach (var order in orders)
            {
                model.RecentOrders.Add(new RecentOrderVM
                {
                    OrderNumber = order.OrderNumber,

                    CustomerName = order.CustomerName,

                    Total = order.Total,

                    Status = order.Status.ToString(),

                    CreatedAt = order.CreatedAt
                });
            }

            return model;
        }
    }
}
