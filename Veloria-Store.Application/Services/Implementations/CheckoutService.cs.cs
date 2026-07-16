using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.Services.Interfaces.Cookies;
using Veloria_Store.Application.ViewModels.Checkout;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Enums;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICookieCartService _cookieCart;
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            ICookieCartService cookieCart,
            IProductRepository productRepository,
            IHttpContextAccessor http,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _cookieCart = cookieCart;
            _productRepository = productRepository;
            _http = http;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private bool IsAuthenticated =>
            _http.HttpContext!.User.Identity!.IsAuthenticated;

        private string UserId =>
            _http.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<CheckoutVM> GetCheckoutAsync()
        {
            var model = new CheckoutVM();

            if (IsAuthenticated)
            {
                var cartItems = await _cartRepository.GetAllAsync(UserId);

                model.Items = _mapper.Map<List<CheckoutItemVM>>(cartItems);
            }
            else
            {
                var cookieItems = _cookieCart.Get();

                foreach (var item in cookieItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);

                    if (product == null)
                        continue;

                    model.Items.Add(new CheckoutItemVM
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductImage = product.Images.FirstOrDefault()?.ImageUrl ?? "",
                        Price = product.Price,
                        Quantity = item.Quantity
                    });
                }
            }

            model.SubTotal = model.Items.Sum(x => x.Total);
            model.Shipping = 0;
            model.Total = model.SubTotal + model.Shipping;

            return model;
        }

        public async Task<string> PlaceOrderAsync(CheckoutVM model)
        {
            Order order;

            if (IsAuthenticated)
            {
                var cartItems = await _cartRepository.GetAllAsync(UserId);

                if (!cartItems.Any())
                    throw new Exception("Cart is empty.");

                order = _mapper.Map<Order>(model);

                order.Id = Guid.NewGuid();
                order.OrderNumber = GenerateOrderNumber();
                order.Status = OrderStatus.Pending;
                order.CreatedAt = DateTime.UtcNow;
                order.UserId = UserId;

                order.OrderItems = cartItems.Select(x => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price
                }).ToList();

                order.SubTotal = order.OrderItems.Sum(x => x.Total);
                order.ShippingCost = 0;
                order.Total = order.SubTotal;

                await _orderRepository.AddAsync(order);

                await _cartRepository.ClearAsync(UserId);

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var cookieItems = _cookieCart.Get();

                if (!cookieItems.Any())
                    throw new Exception("Cart is empty.");

                order = _mapper.Map<Order>(model);

                order.Id = Guid.NewGuid();
                order.OrderNumber = GenerateOrderNumber();
                order.Status = OrderStatus.Pending;
                order.CreatedAt = DateTime.UtcNow;
                order.UserId = null;

                order.OrderItems = new List<OrderItem>();

                foreach (var item in cookieItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);

                    if (product == null)
                        continue;

                    order.OrderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    });
                }

                order.SubTotal = order.OrderItems.Sum(x => x.Total);
                order.ShippingCost = 0;
                order.Total = order.SubTotal;

                await _orderRepository.AddAsync(order);

                _cookieCart.Clear();

                await _unitOfWork.SaveChangesAsync();
            }

            return order.OrderNumber;
        }

        private static string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}
