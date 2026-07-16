using Microsoft.AspNetCore.Http;

namespace Veloria_Store.Application.ViewModels.CategoryViiewModels
{
    public class CategoryUpdateVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }
    }
}
