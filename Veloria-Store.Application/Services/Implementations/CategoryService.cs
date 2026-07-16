using AutoMapper;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.CategoryViiewModels;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CategoryCreateVM model)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),

                Name = model.Name,

                ImageUrl = model.ImageUrl
            };

            await _categoryRepository.AddAsync(category);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<List<CategoryVM>>(categories);
        }

        public async Task<CategoryVM?> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return _mapper.Map<CategoryVM>(category);
        }
        public async Task UpdateAsync(CategoryUpdateVM model)
        {
            var category =
                await _categoryRepository.GetByIdAsync(model.Id);

            if (category == null)
                throw new Exception();

            category.Name = model.Name;

            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                category.ImageUrl = model.ImageUrl;
            }

            _categoryRepository.update(category);

            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);


            _categoryRepository.Delete(category);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
