using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.ProductViewModels;

namespace Veloria_Store.mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IBrandService brandService,
            IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Brands = await _brandService.GetAllAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await _productService.GetAllProductsAsync();

            return Json(products);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Json(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model.Images != null)
            {
                var folder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "products");

                Directory.CreateDirectory(folder);

                model.ImageUrls = new List<string>();

                foreach (var image in model.Images)
                {
                    var fileName =
                        Guid.NewGuid() +
                        Path.GetExtension(image.FileName);

                    var filePath =
                        Path.Combine(folder, fileName);

                    using var stream =
                        new FileStream(filePath, FileMode.Create);

                    await image.CopyToAsync(stream);

                    model.ImageUrls.Add("/uploads/products/" + fileName);
                }
            }

            await _productService.CreateAsync(model);

            return Json(new
            {
                success = true,
                message = "Product added successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model.Images != null && model.Images.Any())
            {
                var folder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "products");

                Directory.CreateDirectory(folder);

                model.ImageUrls = new List<string>();

                foreach (var image in model.Images)
                {
                    var fileName =
                        Guid.NewGuid() +
                        Path.GetExtension(image.FileName);

                    var filePath =
                        Path.Combine(folder, fileName);

                    using var stream =
                        new FileStream(filePath, FileMode.Create);

                    await image.CopyToAsync(stream);

                    model.ImageUrls.Add("/uploads/products/" + fileName);
                }
            }

            await _productService.UpdateAsync(model);

            return Json(new
            {
                success = true,
                message = "Product updated successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                    return Json(new
                    {
                        success = false,
                        message = "Product not found."
                    });

                if (product.Images != null)
                {
                    foreach (var image in product.Images)
                    {
                        if (string.IsNullOrWhiteSpace(image))
                            continue;

                        var path = Path.Combine(
                            _environment.WebRootPath,
                            image.TrimStart('/')
                                 .Replace('/', Path.DirectorySeparatorChar));

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                }

                await _productService.DeleteAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Product deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
