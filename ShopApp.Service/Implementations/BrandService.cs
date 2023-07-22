using API.Service.Dtos.BrandDtos;
using AutoMapper;
using ShopApp.core.Entities;
using ShopApp.core.Repositories;
using ShopApp.Service.Dtos.Common;
using ShopApp.Service.Exceptions;
using ShopApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Service.Implementations
{
    public class BrandService : IBrandServices
    {
        private readonly IBrandRepositories _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepositories brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }  


        public CreatedResultDto Create(BrandCreatDto dto)
        {
            if (_brandRepository.IsExists(x=>x.Name == dto.Name)) 
            {
                throw new RestExceptions(System.Net.HttpStatusCode.BadRequest, "Name", "Name already taken");

            }
            var entity = _mapper.Map<Brand>(dto);
           

            _brandRepository.Add(entity);
            _brandRepository.Commit();

            return   new CreatedResultDto { Id = entity.Id };
        }







        public void Delete(int id)
        {

            var entity = _brandRepository.Get(x => x.Id == id, "Products");
            if (entity == null)
            {
                throw new RestExceptions(System.Net.HttpStatusCode.NotFound, $"Brand not found  by id : {id} ");
            }

            _brandRepository.Delete(entity);
            _brandRepository.Commit();


        }







        public List<BrandGetAllDto> GetAll()
        {
            
            var entites = _brandRepository.GetQueryable(x=>true).ToList();


            return _mapper.Map<List<BrandGetAllDto>>(entites);
        }








        public BrandGetDto GetById(int id)
        {

            var entity = _brandRepository.Get(x => x.Id == id, "Products");
            if (entity == null)
            {
                throw new RestExceptions(System.Net.HttpStatusCode.NotFound, $"Brand Not found  by id : {id} ");
            }
            var dto = _mapper.Map<BrandGetDto>(entity);
          

            return dto;

        }











        public void Update(int id, BrandEditDto dto)
        {

            var entity = _brandRepository.Get(x => x.Id == id);
            if (entity == null)
            {

                throw new RestExceptions(System.Net.HttpStatusCode.NotFound, $"Brand Not found  by id : {id} ");
            }
            if (entity.Name != dto.Name && _brandRepository.IsExists(x => x.Name == dto.Name))
            {
                throw new RestExceptions(System.Net.HttpStatusCode.BadRequest,"Name","Name already taken")

                { Code = System.Net.HttpStatusCode.NotFound,
                    Errors = new List<RestExceptionsErrorItem> { new RestExceptionsErrorItem("Name", "Name  already taken") } };



            } 

             


                
            entity.Name = dto.Name;
            _brandRepository.Commit();

        }


       


    }
}
