﻿using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUnitOfWork unitOfWork ,
        IMapper mapper,
        IBasketRepository basketRepository
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork,mapper);
        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);
    }
}
