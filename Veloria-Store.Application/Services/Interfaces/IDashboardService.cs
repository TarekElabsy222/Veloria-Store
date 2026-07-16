using Veloria_Store.Application.ViewModels.Dashboard;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardVM> GetAsync();
    }
}
