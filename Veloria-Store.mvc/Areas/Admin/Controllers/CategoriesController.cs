using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.CategoryViiewModels;

namespace Veloria_Store.mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public CategoriesController(ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _categoryService = categoryService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetAllAsync();

            return Json(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model.Image != null)
            {
                var folder = Path.Combine(_environment.WebRootPath, "uploads", "categories");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);

                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await model.Image.CopyToAsync(stream);

                model.ImageUrl = "/uploads/categories/" + fileName;
            }

            await _categoryService.CreateAsync(model);

            return Json(new
            {
                success = true,
                message = "Category added successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            return Json(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model.Image != null)
            {
                var folder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "categories");

                Directory.CreateDirectory(folder);

                var fileName =
                    Guid.NewGuid() +
                    Path.GetExtension(model.Image.FileName);

                var filePath =
                    Path.Combine(folder, fileName);

                using var stream =
                    new FileStream(filePath, FileMode.Create);

                await model.Image.CopyToAsync(stream);

                model.ImageUrl =
                    "/uploads/categories/" + fileName;
            }

            await _categoryService.UpdateAsync(model);

            return Json(new
            {
                success = true,
                message = "Category updated successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                if (category is null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Category not found."
                    });
                }

                var imageUrl = category.ImageUrl;

                await _categoryService.DeleteAsync(id);

                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    var imagePath = Path.Combine(
                        _environment.WebRootPath,
                        imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                return Json(new
                {
                    success = true,
                    message = "Category deleted successfully."
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
