using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    //Api Conttoller
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // Endpoint : public non-static method

        [HttpGet] //Get:/api/Products
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }

        [HttpGet("{id}")] //Get:/api/Products/id
        public async Task<IActionResult>GetProductById(int id)
        {
            var result= await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound();//404
            return Ok(result);
        }

        #region Task [GetAllBrands - GetAllTypes]

        [HttpGet("Brands")] //Get:/api/Products/Brands
        public async Task<IActionResult> GetAllBrands()
        {
           var result = await serviceManager.ProductService.GetAllBrandsAsync();
           if(result is null) return BadRequest();  
           return Ok(result);
        }

        [HttpGet("Types")]//Get:/api/Products/Types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        #endregion
    }
}
