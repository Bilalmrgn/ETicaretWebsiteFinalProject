using Frontend.DtosLayer.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.ProductsDto
{
    public class ProductListDto
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public CategoryListDto Category { get; set; }

    }
}
