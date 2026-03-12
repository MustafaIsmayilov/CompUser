using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
            await roleManager.CreateAsync(new IdentityRole(RoleNames.Admin));

        if (!await roleManager.RoleExistsAsync(RoleNames.Provider))
            await roleManager.CreateAsync(new IdentityRole(RoleNames.Provider));

        if (!await roleManager.RoleExistsAsync(RoleNames.User))
            await roleManager.CreateAsync(new IdentityRole(RoleNames.User));
    }
}
