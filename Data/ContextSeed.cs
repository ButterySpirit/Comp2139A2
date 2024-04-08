using Assign1.Models;
using Microsoft.AspNetCore.Identity;

namespace Assign1.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Traveler.ToString()));
            
        }

        public static async Task SuperSeedRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var superUser = new ApplicationUser
            {
                UserName = "superAdmin",
                Email = "adminsupport@domain.com",
                FirstName = "Super",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!userManager.Users.Any(u => u.Id == superUser.Id))
            {
                var user = await userManager.FindByEmailAsync(superUser.Email);
                if (user == null)
                {
                    var createPowerUser = await userManager.CreateAsync(superUser, "P@ssword123");
                    if (createPowerUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(superUser, Enum.Roles.Traveler.ToString());
                        await userManager.AddToRoleAsync(superUser, Enum.Roles.Admin.ToString());
                        await userManager.AddToRoleAsync(superUser, Enum.Roles.SuperAdmin.ToString());
                    }
                }
            }
        }
    }
}
