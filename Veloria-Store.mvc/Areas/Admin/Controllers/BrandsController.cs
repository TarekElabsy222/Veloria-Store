using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.BrandViewModel;
using Veloria_Store.Infrastructure.Utilities;

namespace Veloria_Store.mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var brands = await _brandService.GetAllAsync();
            return Json(brands);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var brand = await _brandService.GetByIdAsync(id);

            if (brand is null)
                return NotFound();

            return Json(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _brandService.CreateAsync(model);

            return Json(new
            {
                success = true,
                message = "Brand added successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _brandService.UpdateAsync(model);

            return Json(new
            {
                success = true,
                message = "Brand updated successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _brandService.DeleteAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Brand deleted successfully."
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
