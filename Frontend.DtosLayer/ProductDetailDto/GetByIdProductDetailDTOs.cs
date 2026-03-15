using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.ProductDetailDto
{
    public class GetByIdProductDetailDTOs
    {
        public string ProductDetailId { get; set; }
        public string? ProductDetailInformation { get; set; }
        public string? ProductDetailDescription { get; set; }
        public string ProductId { get; set; }
    }
}
