using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.Shared;

namespace Veloria_Store.mvc.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IWishlistService _wishlistService;

        public HeaderViewComponent(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new HeaderVM();

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!;

                model.WishlistCount = await _wishlistService.CountAsync(userId);
            }

            return View(model);
        }
    }
}
