using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;
using Shared.ErrorModels;
using Store.G02.Api.Extensions;
using Store.G02.Api.MiddleWares;


namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddleWares();
           
            app.Run();
        }
    }
}
