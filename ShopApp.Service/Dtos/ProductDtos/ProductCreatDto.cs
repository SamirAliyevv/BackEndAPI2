using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace API.Service.Dtos.ProductDtos
{
    public class ProductCreatDto
    {

        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal  CostPrice { get; set; }
        public decimal  SalePrice { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class ProductCreatDtoValidator : AbstractValidator<ProductCreatDto>
    {

        public ProductCreatDtoValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MinimumLength(2).MaximumLength(35);
            RuleFor(x => x.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(x=>x.CostPrice);
            RuleFor(x => x.BrandId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.ImageFile).NotNull();

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile != null)
                {

                    if (x.ImageFile.Length>2 * 1024 * 1024)
                        context.AddFailure("ImageFile", "Image file must be less or equal that 2MB ");
                    if (x.ImageFile.ContentType!="image/png" && x.ImageFile.ContentType!="image/jpg")
                        context.AddFailure("ImageFile", "Image file must be  png,jpg or jpeg  ");


                }
                

            });
        }
    }
}
