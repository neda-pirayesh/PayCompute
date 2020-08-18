using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Persistence
{
   public static class DataSeedingInitializer
    {
        public static async Task UserANdRoleSeedAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        { 
            string[] roles={"Admin","Manager","Staff" };
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }

            }
            //Create Admin
            if (userManager.FindByEmailAsync("pirayeshn@gmail.com").Result==null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "pirayeshn@gmail.com",
                    Email = "pirayeshn@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            //Create Manager
            if (userManager.FindByEmailAsync("Manager@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Manager@gmail.com",
                    Email = "Manager@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }
            //Create Staff
            if (userManager.FindByEmailAsync("Saya@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Saya@gmail.com",
                    Email = "Saya@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }
            //Create no role user
            if (userManager.FindByEmailAsync("samira@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "samira@gmail.com",
                    Email = "samira@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                //No Role Assigned

            }
        }
    }
}
