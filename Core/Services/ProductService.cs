using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper ) : IProductService
    {
       
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters specParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpecifications(specParams);
            //Get All Products From Repository
            var products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);

            var specCount = new ProductWithCountSpecification(specParams);

            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount );

            //Mapping IEnumerable<Product> to IEnumerable<ProductResultDto> : AutoMapper
            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);
            
            return new PaginationResponse<ProductResultDto>(specParams.PageIndex,specParams.PageSize, count , result);
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecifications(id);
            var product = await unitOfWork.GetRepository<Product,int>().GetByIdAsync(spec);
            if(product is null) throw new ProductNotFoundException(id);

            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var result=mapper.Map< IEnumerable<TypeResultDto>>(types);
            return result;
        }

        
    }
}
