using fileManagerCore.Interfaces;
using fileManagerCore.Services;
using fileManagerInfrastructure.Data;
using fileManagerInfrastructure.Models;
using fileManagerInfrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fileManagerInfrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFileItemService, FileItemService>();
            services.AddTransient<IFileItemRepo, FileItemRepo>();

            return services;
        }

        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder applicationBuilder, MultitenantDbContext appDbContext, IHttpContextAccessor accessor, IConfiguration configuration)
        {
            applicationBuilder.Use(async (context, next) => {
                using (var tenantDbContext = new MultitenantDbContext(accessor, configuration))
                {
                    if (accessor.HttpContext.User.Identity.IsAuthenticated)
                    {
                        tenantDbContext.Database.Migrate();
                    }                    
                }
                await next();
            });

            return applicationBuilder;
        }

    }
}
