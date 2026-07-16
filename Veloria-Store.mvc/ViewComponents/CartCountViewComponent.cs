using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;

namespace Veloria_Store.mvc.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = await _cartService.CountAsync();

            return View(count);
        }
    }
}
