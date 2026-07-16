using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Application.ViewModels.BrandViewModel
{
    public class BrandUpdateVM
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
