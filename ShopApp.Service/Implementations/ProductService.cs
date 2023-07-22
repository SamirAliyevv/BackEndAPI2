using API.Service.Dtos.ProductDtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopApp.core.Entities;
using ShopApp.core.Repositories;
using ShopApp.Service.Dtos.Common;
using ShopApp.Service.Dtos.ProductDtos;
using ShopApp.Service.Exceptions;
using ShopApp.Service.Helpers;
using ShopApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Service.Implementations
{
    public class ProductService : IProductServices
    {
        private readonly IBrandRepositories _brandRepositories;
        private readonly IProductRepositories _productRepositories;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IBrandRepositories brandRepositories,IProductRepositories productRepositories,IMapper mapper  )
        {
            _brandRepositories = brandRepositories;
            _productRepositories = productRepositories;
            _mapper = mapper;
        }
        public CreatedResultDto Create(ProductCreatDto dto)
        {

            if (!_brandRepositories.IsExists(x => x.Id == dto.BrandId))
                throw new RestExceptions(System.Net.HttpStatusCode.BadRequest,"BrandId",$"Brand not found by id {dto.BrandId}"  );

            if (_productRepositories.IsExists(x=>x.Name==dto.Name))
                throw new RestExceptions(System.Net.HttpStatusCode.BadRequest, "Name", $"Name already taken  {dto.Name}");


              var entity = _mapper.Map<Product>(dto);

            string rootPath = Directory.GetCurrentDirectory()+"/wwwroot";
            entity.ImageURl = FileManager.Save(dto.ImageFile,rootPath,"uploads/products");
            _productRepositories.Add(entity);
            _productRepositories.Commit();
            return new CreatedResultDto { Id = entity.Id };
        }
            
        public void Delete(int id)
        {



            var entity = _productRepositories.Get(x => x.Id == id);
            if (entity == null)
            {
                throw new RestExceptions(System.Net.HttpStatusCode.NotFound, $"Product not found  by id : {id} ");
            }

        _productRepositories.Delete(entity);
            _productRepositories.Commit();

        }

        public List<ProductGetAllDto> GetAll()
        {
            var entities = _productRepositories.GetQueryable(x => true, "Brand").ToList();

            return _mapper.Map<List<ProductGetAllDto>>(entities);   
        }

        public ProductGetDto GetById(int id)
        {



            var entity = _productRepositories.Get(x => x.Id == id, "Brand");
            if (entity==null)
                throw new RestExceptions(System.Net.HttpStatusCode.NotFound,$"Product not found by id : {id}");
            var dto = _mapper.Map<ProductGetDto>(entity);
             
            return dto;
          
        }

        public void Edit(int id, ProductEditDto dto)
        {

            var entity = _productRepositories.Get(x => x.Id == id);


            if (entity == null)
                throw new RestExceptions(System.Net.HttpStatusCode.NotFound, $"Product not found by id : {id}");

            if (entity.BrandId != dto.BrandId && !_brandRepositories.IsExists(x => x.Id == dto.BrandId))
                throw new RestExceptions(System.Net.HttpStatusCode.BadRequest, "BrandId",$"BrandId not found ");

            if (entity.Name != dto.Name && !_productRepositories.IsExists(x => x.Name == dto.Name))
                throw new RestExceptions(System.Net.HttpStatusCode.BadRequest, "Name", $"Name already taken");

            entity.Name= dto.Name;
            entity.BrandId= dto.BrandId;
            entity.CostPrice= dto.CostPrice;
            entity.SalePrice= dto.SalePrice;
            entity.ModifiedAt = DateTime.UtcNow;

        }
    }
}
