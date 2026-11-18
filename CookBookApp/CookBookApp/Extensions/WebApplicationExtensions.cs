using CookBookApp.Data.Seeding;

namespace CookBookApp.Web.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedIdentityAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await IdentitySeeder.SeedAsync(scope.ServiceProvider);
    }
}
