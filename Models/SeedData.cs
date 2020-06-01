using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShare.Models
{
    public static class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager);
        }

        public static void SeedUser(UserManager<ApplicationUser> userManager)
        {
            var AdminResult = userManager.FindByNameAsync("admin@musicshare.it").Result;

            if(AdminResult == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = "admin@musicshare.it",
                    UserName = "admin@musicshare.it"
                };

                var result = userManager.CreateAsync(user, "Qwertyuiop123@").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
    }
}
