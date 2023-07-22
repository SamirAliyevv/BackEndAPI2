using API.Service.Dtos.BrandDtos;
using API.Service.Dtos.ProductDtos;
using ShopApp.Service.Dtos.Common;
using ShopApp.Service.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Service.Interfaces
{
    public  interface IProductServices
    {
        CreatedResultDto Create(ProductCreatDto dto);
        void Edit(int id, ProductEditDto dto);
        ProductGetDto GetById(int id);

         List<ProductGetAllDto> GetAll();
        void Delete(int id);

    }
}
