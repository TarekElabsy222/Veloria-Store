using AutoMapper;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.CategoryViiewModels;
using Veloria_Store.Application.ViewModels.HomeViewModel;
using Veloria_Store.Application.ViewModels.ProductViewModels;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductCardVM>> GetFeaturedAsync(int count)
        {
            var products = await _productRepository.GetFeaturedAsync(count);

            return _mapper.Map<List<ProductCardVM>>(products);
        }

        public async Task<List<ProductCardVM>> GetPopularAsync(int count)
        {
            var products = await _productRepository.GetPopularAsync(count);

            return _mapper.Map<List<ProductCardVM>>(products);
        }

        public async Task<List<ProductCardVM>> GetNewestAsync(int count)
        {
            var products = await _productRepository.GetNewestAsync(count);

            return _mapper.Map<List<ProductCardVM>>(products);
        }

        public async Task<List<ProductCardVM>> GetTrendyAsync(int count)
        {
            var products = await _productRepository.GetTrendyAsync(count);

            return _mapper.Map<List<ProductCardVM>>(products);
        }

        public async Task<ProductDetailsPageVM?> GetDetailsAsync(Guid id)
        {
            var product = await _productRepository.GetDetailsAsync(id);
            if (product == null) return null;

            var related = await _productRepository.GetRelatedProductsAsync(
                product.Id,
                product.CategoryId,
                4);

            return new ProductDetailsPageVM
            {
                Product = _mapper.Map<ProductDetailsVM>(product),
                RelatedProducts = _mapper.Map<List<ProductCardVM>>(related)
            };
        }

        

        public async Task<List<ProductCardVM>> GetPagedProductsAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetPagedProductsAsync(page, pageSize);

            return _mapper.Map<List<ProductCardVM>>(products);
        }

        public async Task<List<ProductCardVM>> SearchAsync(string keyword)
        {
            var products = await _productRepository.SearchAsync(keyword);

            return _mapper.Map<List<ProductCardVM>>(products);
        }

        public async Task<HomeVM> GetHomeAsync()
        {
            var featured = await _productRepository.GetFeaturedAsync(8);

            var popular = await _productRepository.GetPopularAsync(8);

            var newest = await _productRepository.GetNewestAsync(8);

            var trendy = await _productRepository.GetTrendyAsync(3);
            var category = await _categoryRepository.GetAllAsync();
            var topSelling = await _productRepository.GetTopSellingProductsAsync();
            var deals = await _productRepository.GetDealsOutletProductsAsync();

            return new HomeVM
            {
                Categories = _mapper.Map<List<CategoryVM>>(category),
                FeaturedProducts = _mapper.Map<List<ProductCardVM>>(featured),

                PopularProducts = _mapper.Map<List<ProductCardVM>>(popular),

                NewProducts = _mapper.Map<List<ProductCardVM>>(newest),

                TrendyProducts = _mapper.Map<List<ProductShowCaseVM>>(trendy),

                TopSellingProducts = _mapper.Map<List<ProductShowCaseVM>>(topSelling),

                DealsOutletProducts = _mapper.Map<List<ProductShowCaseVM>>(deals)
            };
        }

        public async Task<List<ProductShowCaseVM>> GetTopSellingAsync()
        {
            var products = await _productRepository.GetTopSellingProductsAsync();

            return _mapper.Map<List<ProductShowCaseVM>>(products);
        }

        public async Task<List<ProductShowCaseVM>> GetDealsOutletAsync()
        {

            var products = await _productRepository.GetDealsOutletProductsAsync();

            return _mapper.Map<List<ProductShowCaseVM>>(products);

        }

        public async Task<ShopVM> GetShopProductsAsync(int page)
        {
            const int pageSize = 12;

            var products = await _productRepository.GetPagedProductsAsync(page, pageSize);

            var totalProducts = await _productRepository.CountAsync();

            return new ShopVM
            {
                Products = _mapper.Map<List<ProductCardVM>>(products),

                CurrentPage = page,

                PageSize = pageSize,

                TotalProducts = totalProducts,

                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize)
            };
        }

        public async Task<IEnumerable<ProductVM>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<ProductVM?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetDetailsAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductVM>(product);
        }

        public async Task CreateAsync(ProductCreateVM model)
        {
            var product = _mapper.Map<Product>(model);

            product.Id = Guid.NewGuid();

            foreach (var image in model.ImageUrls)
            {
                product.Images.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = image
                });
            }

            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductUpdateVM model)
        {
            var product = await _productRepository.GetDetailsAsync(model.Id);

            if (product == null)
                throw new Exception("Product not found.");

            _mapper.Map(model, product);

            product.Images.Clear();

            foreach (var image in model.ImageUrls)
            {
                product.Images.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ImageUrl = image
                });
            }

            _productRepository.update(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetDetailsAsync(id);

            if (product == null)
                throw new Exception("Product not found.");

            _productRepository.Delete(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
