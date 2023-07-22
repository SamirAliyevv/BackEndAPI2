        using API.Service.Dtos.BrandDtos;
using API.Service.Dtos.ProductDtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopApp.core.Entities;
using ShopApp.Service.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Service.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile(IHttpContextAccessor _httpContextAccessor)
        {

            var  baseUrl = new UriBuilder(_httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Host, _httpContextAccessor.HttpContext.Request.Host.Port ?? -1);
            CreateMap<BrandCreatDto, Brand>();
            CreateMap<Brand, BrandGetDto>();
            CreateMap<Brand, BrandGetAllDto>();
            CreateMap<ProductCreatDto, Product>();
            //.ForMember(dest => dest.CreadAt, s => s.MapFrom(m => DateTime.UtcNow));
            CreateMap<Brand, ProductGetDtoBrandIn>(); 
            CreateMap<Product, ProductGetDto>()
                .ForMember(d => d.Profit, s => s.MapFrom(m => m.SalePrice - m.CostPrice))
              .ForMember(d => d.ImageUrl, s => s.MapFrom(m => baseUrl + "uploads/products" + m.ImageURl));


            CreateMap<Product,ProductGetAllDto>()
                .ForMember(d => d.ImageUrl, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageURl)? null :(baseUrl + "uploads/products" + m.ImageURl));

        }

    }
}
