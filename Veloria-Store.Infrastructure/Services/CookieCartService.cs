using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Veloria_Store.Application.Services.Interfaces.Cookies;
using Veloria_Store.Application.ViewModels.CartItemViewModel;

namespace Veloria_Store.Infrastructure.Services
{
    public class CookieCartService : ICookieCartService
    {
        private readonly IHttpContextAccessor _http;

        private const string CartKey = "VeloriaCart";

        // Cache the cart for the current request
        private List<CartCookieItemVM>? _cachedCart;

        public CookieCartService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public List<CartCookieItemVM> Get()
        {
            if (_cachedCart != null)
                return _cachedCart;

            var cookie = _http.HttpContext?.Request.Cookies[CartKey];

            if (string.IsNullOrWhiteSpace(cookie))
            {
                _cachedCart = new List<CartCookieItemVM>();
                return _cachedCart;
            }

            _cachedCart = JsonSerializer.Deserialize<List<CartCookieItemVM>>(cookie)
                          ?? new List<CartCookieItemVM>();

            return _cachedCart;
        }

        public void Save(List<CartCookieItemVM> items)
        {
            // Update cache immediately
            _cachedCart = items;

            var json = JsonSerializer.Serialize(items);

            _http.HttpContext?.Response.Cookies.Append(
                CartKey,
                json,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = false
                });
        }

        public void Clear()
        {
            _cachedCart = new List<CartCookieItemVM>();

            _http.HttpContext?.Response.Cookies.Delete(CartKey);
        }

        public int Count()
        {
            return Get().Sum(x => x.Quantity);
        }
    }
}

