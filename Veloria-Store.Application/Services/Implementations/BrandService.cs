using AutoMapper;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.BrandViewModel;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(
            IBrandRepository brandRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BrandVM>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();

            return _mapper.Map<List<BrandVM>>(brands);
        }

        public async Task<BrandVM?> GetByIdAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);

            return _mapper.Map<BrandVM>(brand);
        }
        public async Task CreateAsync(BrandCreateVM model)
        {
            var brand = _mapper.Map<Brand>(model);

            brand.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brand);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(BrandUpdateVM model)
        {
            var brand = await _brandRepository.GetByIdAsync(model.Id);

            if (brand == null)
                throw new Exception("Brand not found.");

            _mapper.Map(model, brand);

            _brandRepository.update(brand);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);

            if (brand == null)
                throw new Exception("Brand not found.");

            _brandRepository.Delete(brand);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

