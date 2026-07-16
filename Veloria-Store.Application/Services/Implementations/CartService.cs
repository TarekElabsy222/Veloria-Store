using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.Services.Interfaces.Cookies;
using Veloria_Store.Application.ViewModels.CartItemViewModel;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        private readonly ICookieCartService _cookieCart;

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IHttpContextAccessor _http;

        public CartService(
            ICartRepository cartRepository,
            ICookieCartService cookieCart,
            IProductRepository productRepository,
            IHttpContextAccessor http,
            IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _cookieCart = cookieCart;
            _productRepository = productRepository;
            _http = http;
            _unitOfWork = unitOfWork;
        }

        private bool IsAuthenticated => _http.HttpContext!.User.Identity!.IsAuthenticated;
        private string UserId =>  _http.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        private async Task AddDatabase(Guid productId)
        {
            var item = await _cartRepository.GetAsync(UserId, productId);

            if (item != null)
            {
                item.Quantity++;

                await _unitOfWork.SaveChangesAsync();

                return;
            }

            await _cartRepository.AddAsync(new CartItem
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                ProductId = productId,
                Quantity = 1
            });

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task AddCookie(Guid productId)
        {
            var cart = _cookieCart.Get();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.Quantity++;

                _cookieCart.Save(cart);

                return;
            }

            cart.Add(new CartCookieItemVM
            {
                ProductId = productId,
                Quantity = 1
            });

            _cookieCart.Save(cart);

            await Task.CompletedTask;
        }

        public async Task AddAsync(Guid productId)
        {
            if (IsAuthenticated)
            {
                await AddDatabase(productId);
            }
            else
            {
                await AddCookie(productId);
            }
        }

        public async Task<int> CountAsync()
        {
            if (IsAuthenticated)
            {
                return await _cartRepository.CountAsync(p=>p.UserId == UserId);
            }

            return _cookieCart.Count();
        }

        public async Task DecreaseAsync(Guid productId)
        {
            if (IsAuthenticated)
            {
                var item = await _cartRepository.GetAsync(UserId, productId);

                if (item == null)
                    return;

                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    _cartRepository.Delete(item);
                }

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var cart = _cookieCart.Get();

                var item = cart.FirstOrDefault(x => x.ProductId == productId);

                if (item == null)
                    return;

                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }

                _cookieCart.Save(cart);
            }
        }

        public async Task<CartVM> GetAsync()
        {
            if (IsAuthenticated)
            {
                var items = await _cartRepository.GetAllAsync(UserId);

                return new CartVM
                {
                    Items = items.Select(x => new CartItemVM
                    {
                        ProductId = x.ProductId,
                        Name = x.Product.Name,
                        Image = x.Product.Images.FirstOrDefault()?.ImageUrl ?? "",
                        Price = x.Product.Price,
                        Quantity = x.Quantity
                    }).ToList()
                };
            }

            var cookieItems = _cookieCart.Get();

            var cart = new CartVM();

            foreach (var item in cookieItems)
            {
                var product = await _productRepository.GetDetailsAsync(item.ProductId);

                if (product == null)
                    continue;

                cart.Items.Add(new CartItemVM
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Image = product.Images.FirstOrDefault()?.ImageUrl ?? "",
                    Price = product.Price,
                    Quantity = item.Quantity
                });
            }

            return cart;
        }

        public async Task IncreaseAsync(Guid productId)
        {
            if (IsAuthenticated)
            {
                var item = await _cartRepository.GetAsync(UserId, productId);

                if (item == null)
                    return;

                item.Quantity++;

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var cart = _cookieCart.Get();

                var item = cart.FirstOrDefault(x => x.ProductId == productId);

                if (item == null)
                    return;

                item.Quantity++;

                _cookieCart.Save(cart);
            }
        }

        public async Task RemoveAsync(Guid productId)
        {
            if (IsAuthenticated)
            {
                var item = await _cartRepository.GetAsync(UserId, productId);

                if (item == null)
                    return;

                 _cartRepository.Delete(item);

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var cart = _cookieCart.Get();

                var item = cart.FirstOrDefault(x => x.ProductId == productId);

                if (item != null)
                    cart.Remove(item);

                _cookieCart.Save(cart);
            }
        }


        public async Task MergeCartAsync()
        {
            if (!IsAuthenticated)
                return;

            var cookieItems = _cookieCart.Get();

            if (!cookieItems.Any())
                return;

            foreach (var cookieItem in cookieItems)
            {
                var dbItem = await _cartRepository.GetAsync(UserId!, cookieItem.ProductId);

                if (dbItem == null)
                {
                    await _cartRepository.AddAsync(new CartItem
                    {
                        Id = Guid.NewGuid(),

                        UserId = UserId!,

                        ProductId = cookieItem.ProductId,

                        Quantity = cookieItem.Quantity
                    });
                }
                else
                {
                    dbItem.Quantity += cookieItem.Quantity;
                }
            }

            await _unitOfWork.SaveChangesAsync();

            _cookieCart.Clear();
        }
    }
}
