using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;

namespace Veloria_Store.mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }



        public async Task<IActionResult> Index()
        {
            var model = await _dashboardService.GetAsync();

            return View(model);
        }
    }
}
