﻿using FluentValidation;

namespace API.Service.Dtos.ProductDtos
{
   
        public class ProductEditDto
        {

            public string Name { get; set; }

             public int BrandId { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }


        }
        public class ProductEditDtoValidator : AbstractValidator<ProductEditDto>
        {
            public ProductEditDtoValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);
            }
        }
   
}
