using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        // Get Order By Id
        Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        
        // Get Order
        Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);

        //Create Order
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest,string userEmail );

        //Get All Delivery Methods
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();

    }
}
