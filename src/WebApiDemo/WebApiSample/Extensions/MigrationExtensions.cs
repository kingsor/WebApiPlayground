using Microsoft.EntityFrameworkCore;

namespace WebApiSample.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations<TDbContext>(this WebApplication app)
        where TDbContext : DbContext
    {
        using IServiceScope scope = app.Services.CreateScope();

        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
