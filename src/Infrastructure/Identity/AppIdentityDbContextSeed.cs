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
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.USERS));

            // Create demo user
            var defaultUser = new ApplicationUser
            {
                UserName = "demo.user@gmail.com",
                Email = "demo.user@gmail.com"
            };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            // Create demo admin
            var defaultAdmin = new ApplicationUser
            {
                UserName = "demo.admin@gmail.com",
                Email = "demo.admin@gmail.com"
            };
            await userManager.CreateAsync(defaultAdmin, AuthorizationConstants.DEFAULT_PASSWORD);

            // Add admin role for demo admin
            defaultAdmin = await userManager.FindByEmailAsync(defaultAdmin.Email);
            await userManager.AddToRoleAsync(defaultAdmin, AuthorizationConstants.Roles.ADMINSTRATORS);

            // Add user role for demo user
            defaultUser = await userManager.FindByEmailAsync(defaultUser.Email);
            await userManager.AddToRoleAsync(defaultUser, AuthorizationConstants.Roles.USERS);
        }
    }
}
