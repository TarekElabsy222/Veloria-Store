using Veloria_Store.Application.ViewModels.Checkout;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<CheckoutVM> GetCheckoutAsync();

        Task<string> PlaceOrderAsync(CheckoutVM model);
    }
}
