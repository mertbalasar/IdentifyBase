﻿using IdentifyBase.Application.Abstractions.Database;
using IdentifyBase.Domain.Entities;
using IdentifyBase.Infrastructure.Persistence;
using IdentifyBase.Infrastructure.Persistence.ContextRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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
        }
    }
}
