using Veloria_Store.Application.ViewModels.BrandViewModel;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
        Task<BrandVM?> GetByIdAsync(Guid id);
        Task CreateAsync(BrandCreateVM model);

        Task UpdateAsync(BrandUpdateVM model);

        Task DeleteAsync(Guid id);
    }
}
