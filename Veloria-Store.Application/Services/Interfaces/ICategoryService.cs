

using Veloria_Store.Application.ViewModels.CategoryViiewModels;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM?> GetByIdAsync(Guid id);
        Task CreateAsync(CategoryCreateVM category);
        Task UpdateAsync(CategoryUpdateVM model);
        Task DeleteAsync(Guid id);

    }
}
