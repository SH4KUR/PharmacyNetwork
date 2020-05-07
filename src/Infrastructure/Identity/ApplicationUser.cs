using Microsoft.AspNetCore.Identity;

namespace PharmacyNetwork.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int? PharmacyId { get; set; }
    }
}
