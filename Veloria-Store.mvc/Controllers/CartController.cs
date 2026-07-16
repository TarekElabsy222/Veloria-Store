using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;

namespace Veloria_Store.mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _cartService.GetAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            await _cartService.AddAsync(productId);

            var cart = await _cartService.GetAsync();

            return Json(new
            {
                success = true,
                message = "Product added to cart successfully.",
                count = await _cartService.CountAsync(),
                cartSubtotal = cart.SubTotal.ToString("0.00"),
                total = cart.Total.ToString("0.00")
            });
        }
        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId)
        {
            await _cartService.RemoveAsync(productId);

            var cart = await _cartService.GetAsync();

            return Json(new
            {
                success = true,
                message = "Product removed from cart.",
                count = await _cartService.CountAsync(),
                cartSubtotal = cart.SubTotal.ToString("0.00"),
                total = cart.Total.ToString("0.00")
            });
        }
        [HttpPost]
        public async Task<IActionResult> Increase(Guid productId)
        {
            await _cartService.IncreaseAsync(productId);

            var cart = await _cartService.GetAsync();

            var item = cart.Items.First(x => x.ProductId == productId);

            return Json(new
            {
                success = true,
                count = await _cartService.CountAsync(),
                quantity = item.Quantity,
                subtotal = item.Total.ToString("0.00"),
                cartSubtotal = cart.SubTotal.ToString("0.00"),
                total = cart.Total.ToString("0.00")
            });
        }
        [HttpPost]
        public async Task<IActionResult> Decrease(Guid productId)
        {
            await _cartService.DecreaseAsync(productId);

            var cart = await _cartService.GetAsync();

            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            return Json(new
            {
                success = true,
                count = await _cartService.CountAsync(),
                quantity = item?.Quantity ?? 0,
                subtotal = item?.Total.ToString("0.00") ?? "0.00",
                cartSubtotal = cart.SubTotal.ToString("0.00"),
                total = cart.Total.ToString("0.00"),
                removed = item == null
            });
        }

    }
}
