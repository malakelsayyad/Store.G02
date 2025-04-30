using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
         IProductService ProductService { get;}
         IBasketService BasketService { get;}
         ICacheService CacheService { get;}
         IAuthService  AuthService { get;}
         IOrderService  OrderService { get;}

    }
}
