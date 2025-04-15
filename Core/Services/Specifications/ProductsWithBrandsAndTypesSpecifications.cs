using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductsWithBrandsAndTypesSpecifications:BaseSpecifications<Product,int>
    {
        public ProductsWithBrandsAndTypesSpecifications(int id):base( P => P.Id == id)
        {
            ApplyIncludes();
        }

        public ProductsWithBrandsAndTypesSpecifications(ProductSpecificationsParameters specParams) 
            : base(
                    P=>
                    (string.IsNullOrEmpty(specParams.Search)||P.Name.ToLower().Contains(specParams.Search.ToLower()))&&
                    (!specParams.BrandId.HasValue||P.BrandId == specParams.BrandId) &&
                    (!specParams.TypeId.HasValue || P.TypeId == specParams.TypeId)
                  )
        {
            ApplyIncludes();

            ApplySorting(specParams.Sort);

            ApplyPagination(specParams.PageIndex,specParams.PageSize);
        }

        private void ApplyIncludes()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(P => P.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }
    }
}
