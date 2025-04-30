using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Presistance.Data;
using Presistance.Identity;
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
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            StoreDbContext context,
            StoreIdentityDbContext identityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

            //Seeding Delivery From Json Files


            if (!_context.DeliveryMethods.Any())
            {
                //1.Read All Data From delivery Json File As String
                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\delivery.json");

                //2.Convert to C# Object [List<DeliveryMethod>]
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                //3.Add List<ProductTypes> To Db
                if (deliveryMethods is not null && deliveryMethods.Any())
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task InitializeIdentityAsync()
        {
            //Create Db If Its Not Created && Apply Any Pending Migrations

            if (_identityDbContext.Database.GetPendingMigrations().Any())
            { 
               await _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() 
                { 
                  Name="Admin"                
                }); 
                
                await _roleManager.CreateAsync(new IdentityRole() 
                { 
                  Name="SuperAdmin"                
                });
            }

            //Seeding
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName="Super Admin User",
                    Email="SuperAdmin@gmail.com",
                    UserName="SuperAdmin",
                    PhoneNumber="0123456789"
                };

                var adminUser = new AppUser()
                {
                    DisplayName = "Admi",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789"
                };
               await _userManager.CreateAsync(superAdminUser, "P@ssw0rd");
               await _userManager.CreateAsync(adminUser,"P@ssw0rd");

               await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
               await _userManager.AddToRoleAsync(adminUser, "Admin");
            
            }
        }
    }
}
