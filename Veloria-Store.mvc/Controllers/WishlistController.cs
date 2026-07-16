using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Veloria_Store.Application.Services.Interfaces;

namespace Veloria_Store.mvc.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var model = await _wishlistService.GetAsync(userId);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    login = true
                });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await _wishlistService.AddAsync(userId, productId);

            var count = await _wishlistService.CountAsync(userId);

            return Json(new
            {
                success = true,
                message = "Added to wishlist",
                count
            });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await _wishlistService.RemoveAsync(userId, productId);

            var count = await _wishlistService.CountAsync(userId);

            return Json(new
            {
                success = true,
                message = "Remove to wishList",
                count = count
            });
        }


    }
}
