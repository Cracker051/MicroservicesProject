using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Infrastructures.Persistance;

namespace Infrastructure.Persistense
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql($"server={Environment.GetEnvironmentVariable("DB_ADDRESS")};user=root;password=87dima87;database=drugstores", ServerVersion.AutoDetect($"server={Environment.GetEnvironmentVariable("DB_ADDRESS")};user=root;password=87dima87;database=drugstores")));
            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());
            return services;
        }
    }
}