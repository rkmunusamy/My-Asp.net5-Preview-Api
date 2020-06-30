using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services  = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<BackendDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
