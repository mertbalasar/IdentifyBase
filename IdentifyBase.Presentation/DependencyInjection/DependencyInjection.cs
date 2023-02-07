using IdentifyBase.Application.Abstractions.Database;
using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Entities;
using IdentifyBase.Infrastructure.Persistence;
using IdentifyBase.Infrastructure.Persistence.ContextRepositories;
using IdentifyBase.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace IdentifyBase.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDatabaseConnectionDI(this IServiceCollection services, ConfigurationManager app)
        {
            services.AddTransient(typeof(IMainContextRepository<>), typeof(MainContextRepository<>));
            services.AddEntityFrameworkNpgsql().AddDbContext<MainContext>(option =>
            {
                option.UseNpgsql(
                    app.GetConnectionString("DefaultConnection"));
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<IdentityContext>(option =>
            {
                option.UseNpgsql(
                    app.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddIdentityDI(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://google.com",
                    ValidIssuer = "http://google.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"))
                };
            });
        }

        public static void AddServicesDI(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program));
            var assembly = AppDomain.CurrentDomain.Load("IdentifyBase.Application");
            services.AddMediatR(assembly);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
