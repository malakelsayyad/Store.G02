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
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpGet] //Get:/api/baskets?id=smsms
        public async Task<IActionResult>GetBasketById(string id) 
        {
            var result = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost] //Post:/api/baskets
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }

        [HttpDelete] //Delete:/api/baskets
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result = await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
