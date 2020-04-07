using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PharmacyNetwork.ApplicationCore.Constants;

namespace PharmacyNetwork.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINSTRATORS));

            //create demo user
            var defaultUser = new ApplicationUser
            {
                UserName = "demo.user@gmail.com",
                Email = "demo.user@gmail.com"
            };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            //create demo admin
            var defaultAdmin = new ApplicationUser
            {
                UserName = "demo.admin@gmail.com",
                Email = "demo.admin@gmail.com"
            };
            await userManager.CreateAsync(defaultAdmin, AuthorizationConstants.DEFAULT_PASSWORD);

            //add admin role for demo admin
            defaultAdmin = await userManager.FindByEmailAsync(defaultAdmin.Email);
            await userManager.AddToRoleAsync(defaultAdmin, AuthorizationConstants.Roles.ADMINSTRATORS);
        }
    }
}
