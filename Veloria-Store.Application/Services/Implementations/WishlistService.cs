using AutoMapper;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.ProductViewModels;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class WishlistService : IWishlistService
    {

        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task AddAsync(string userId, Guid productId)
        {
            if (await _wishlistRepository.ExistsAsync(userId, productId))
                return;

            await _wishlistRepository.AddAsync(new WishlistItem
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = productId
            });
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<int> CountAsync(string userId)
        {
            return await _wishlistRepository.CountAsync(w=>w.UserId ==userId);
        }

        public async Task<List<ProductCardVM>> GetAsync(string userId)
        {
            var items = await _wishlistRepository.GetByUserAsync(userId);

            return _mapper.Map<List<ProductCardVM>>
            (
                items.Select(x => x.Product)
            );
        }

        public async Task RemoveAsync(string userId, Guid productId)
        {
            await _wishlistRepository.RemoveAsync(userId, productId);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
