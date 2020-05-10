using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PharmacyNetwork.Infrastructure.Identity;

namespace PharmacyNetwork.Web.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<int?> GetPharmacyId(this UserManager<ApplicationUser> userManager, ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var user = await userManager.GetUserAsync(principal);
            var pharmId = user.PharmacyId;

            return pharmId;
        }
    }
}
