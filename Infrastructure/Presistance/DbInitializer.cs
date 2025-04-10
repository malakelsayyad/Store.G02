using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DbInitializer : IDbInitializer
    {

        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
            //Create Db If Its Not Created && Apply Any Pending Migrations
            
            if (_context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }

            //DataSeeding

            //Seeding ProductTypes From Json Files

            if (!_context.ProductTypes.Any())
            {
                //1.Read All Data From Types Json File As String
                //D:\course\Backend\StoreApi\Store.G02\Infrastructure\Presistance\Data\Seeding\types.json
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\types.json");

                //2.Convert to C# Object [List<ProductType>]
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                //3.Add List<ProductTypes> To Db
                if (types is not null && types.Any())
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }

            //Seeding ProductBrand From Json Files

            if (!_context.ProductBrands.Any())
            {
                //1.Read All Data From Brands Json File As String
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\brands.json");

                //2.Convert to C# Object [List<ProductBrand>]
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                //3.Add List<ProductBrand> To Db
                if (brands is not null && brands.Any())
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }

            }

            //Seeding Products From Json Files

            if (!_context.Products.Any())
            {
                //1.Read All Data From Product Json File As String
                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\products.json");

                //2.Convert to C# Object [List<Product>]
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                //3.Add List<ProductTypes> To Db
                if (products is not null && products.Any())
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
