using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.Checkout;

namespace Veloria_Store.mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _checkoutService.GetCheckoutAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutVM model)
        {
            if (!ModelState.IsValid)
            {
                model = await _checkoutService.GetCheckoutAsync();

                return View(model);
            }

            var orderNumber = await _checkoutService.PlaceOrderAsync(model);

            return RedirectToAction(nameof(Success), new { orderNumber });
        }

        public IActionResult Success(string orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;

            return View();
        }
    }
}
