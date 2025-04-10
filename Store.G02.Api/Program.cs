using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;
using AssemblyMapping = Services.AssemblyReference;

namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options => {

                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));

            });
            
            builder.Services.AddScoped<IDbInitializer, DbInitializer>(); //Allow DI For initializer
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyMapping).Assembly);
            builder.Services.AddScoped<IServiceManager,ServiceManger>();

            var app = builder.Build();

            #region Seeding
            
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); //Ask Clr to create object from DbInitializer
            await dbInitializer.InitializeAsync(); 
            
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
