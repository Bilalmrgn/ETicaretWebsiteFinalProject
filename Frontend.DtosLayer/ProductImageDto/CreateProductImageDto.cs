using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.ProductImageDto
{
    public class CreateProductImageDto
    {
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
    }
}
