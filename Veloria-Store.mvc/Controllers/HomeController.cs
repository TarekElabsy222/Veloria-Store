using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Veloria_Store.Application.Services.Interfaces;

namespace Veloria_Store.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public HomeController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetHomeAsync();

            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetDetailsAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Shop(int page = 1)
        {
            var model = await _productService.GetShopProductsAsync(page);

            return View(model);
        }
        public async Task<IActionResult>Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return View( await _orderService.GetUserOrdersAsync(userId));
        }
        public async Task<IActionResult> Search(string keyword)
        {
            var products = await _productService.SearchAsync(keyword);

            ViewBag.Keyword = keyword;

            return View(products);
        }
    }
}
