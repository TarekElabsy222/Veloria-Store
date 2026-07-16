namespace Veloria_Store.Application.ViewModels.Dashboard
{
    public class DashboardVM
    {
        public decimal TotalRevenue { get; set; }

        public int TotalOrders { get; set; }

        public int TotalProducts { get; set; }

        public int TotalUsers { get; set; }

        public List<RecentOrderVM> RecentOrders { get; set; } = [];
    }
}
