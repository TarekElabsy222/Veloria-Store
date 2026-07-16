using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<List<Brand>> GetWithProductsAsync();
        Task<List<Brand>> GetBrandsAsync();

        Task<Brand?> GetDetailsAsync(Guid id);
    }
}
