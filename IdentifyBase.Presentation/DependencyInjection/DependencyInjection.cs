using IdentifyBase.Application.Abstractions.Database;
using IdentifyBase.Infrastructure.Persistence;
using IdentifyBase.Infrastructure.Persistence.ContextRepositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace IdentifyBase.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDatabaseConnection(this IServiceCollection services, ConfigurationManager app)
        {
            services.AddTransient(typeof(IMainContextRepository<>), typeof(MainContextRepository<>));
            services.AddEntityFrameworkNpgsql().AddDbContext<MainContext>(option =>
            {
                option.UseNpgsql(
                    app.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
