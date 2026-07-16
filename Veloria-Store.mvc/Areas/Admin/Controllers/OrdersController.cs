using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.Order;

namespace Veloria_Store.mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Json(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Json(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(OrderStatusUpdateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _orderService.UpdateStatusAsync(model);

            return Json(new
            {
                success = true,
                message = "Order status updated successfully."
            });
        }
    }
}
