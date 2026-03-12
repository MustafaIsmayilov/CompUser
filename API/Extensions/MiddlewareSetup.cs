using Microsoft.AspNetCore.Identity;
using Persistence.Seed;

namespace API.Extensions;

public static class MiddlewareSetup
{
    public static void UseMyMiddlewares(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            RoleSeeder.SeedAsync(roleManager)
                .GetAwaiter()
                .GetResult();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}