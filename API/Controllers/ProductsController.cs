    using API.Service.Dtos.ProductDtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ShopApp.core.Entities;
    using ShopApp.core.Repositories;
using ShopApp.Service.Dtos.ProductDtos;
using ShopApp.Service.Interfaces;
using ShoppApi.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices )
        {
            _productServices = productServices;
        }


        [HttpPost]
        [Route("")]
        public IActionResult Create([FromForm]ProductCreatDto dto)
        {
            return StatusCode(201, _productServices.Create(dto));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductGetDto> Get(int id)
        {

            return Ok(_productServices.GetById(id));
        }
        [HttpGet]
        [Route("all")]

        public ActionResult<List<ProductGetAllDto>> GetAll()
        
        { 
                return Ok(_productServices.GetAll());
        
        
        
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, ProductEditDto dto)
        {
            _productServices.Edit(id, dto);
            return NoContent();

        }



        [HttpDelete]
        [Route("{id}")]

        public IActionResult Delete(int id)
        {
            _productServices.Delete(id); 
            return NoContent();


        }


    }

}
