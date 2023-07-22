using API.Service.Dtos.BrandDtos;
using API.Service.Dtos.ProductDtos;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.core.Repositories;
using ShopApp.Service.Exceptions;
using ShopApp.Service.Implementations;
using ShopApp.Service.Interfaces;
using ShopApp.Service.Profiles;
using ShoppApi.Data;
using ShoppApi.Data.Repositories;
using System.Linq;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
         var errors = context.ModelState.Where(x=>x.Value.Errors.Count()>0)
        .Select(x => new RestExceptionsErrorItem(x.Key, x.Value.Errors.First().ErrorMessage)).ToList();
        return new BadRequestObjectResult(new { message = (string)null,errors=errors });
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopDbContext>(opt =>
{   
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


 builder.Services.AddScoped<IBrandRepositories,BrandRepositories>();

builder.Services.AddScoped<IProductRepositories, ProductRepositories>();
builder.Services.AddScoped<IBrandServices, BrandService>();
builder.Services.AddScoped<IProductServices, ProductService>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<BrandCreatDtoValidator>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(provider =>

   new MapperConfiguration(opt =>
   {
       opt.AddProfile(new MappingProfile(provider.GetService<IHttpContextAccessor>()));
   }).CreateMapper());




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();

