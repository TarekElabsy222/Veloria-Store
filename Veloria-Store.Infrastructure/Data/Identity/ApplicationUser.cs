using Microsoft.AspNetCore.Identity;
using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}
