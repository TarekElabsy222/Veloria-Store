using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetWithProductsAsync();
        Task<List<Category>> GetCategoriesAsync();

        Task<Category?> GetDetailsAsync(Guid id);
    }
}
