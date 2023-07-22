using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Service.Dtos.ProductDtos
{
    public class ProductGetAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string   ImageUrl  { get; set; }
        public string BrandName { get; set; }
    }
}
