using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Application.ViewModels.BrandViewModel
{
    public class BrandCreateVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
