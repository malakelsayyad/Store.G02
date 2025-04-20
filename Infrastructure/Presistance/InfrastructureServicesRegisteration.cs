using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistance.Data;
using Presistance.Identity;
using Presistance.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public static class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options => {

                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]);
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));

            });

            services.AddDbContext<StoreIdentityDbContext>(options => {

                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]);
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));

            });
            services.AddScoped<IDbInitializer, DbInitializer>(); //Allow DI For initializer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();


            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) => 
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });
            

            return services;
        }
    }
}
