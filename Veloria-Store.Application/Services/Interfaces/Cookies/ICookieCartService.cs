using Veloria_Store.Application.ViewModels.CartItemViewModel;

namespace Veloria_Store.Application.Services.Interfaces.Cookies
{
    public interface ICookieCartService
    {
        List<CartCookieItemVM> Get();

        void Save(List<CartCookieItemVM> items);

        void Clear();

        int Count();
    }
}
