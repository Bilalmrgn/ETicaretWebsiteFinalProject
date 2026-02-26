using Catolog.Entities;

namespace Catolog.DTOs.ProductDetailDTOs
{
    public class UpdateProductDetailDTOs
    {
        public string ProductDetailId { get; set; }
        public string ProductDetailInformation { get; set; }
        public string ProductDetailDescription { get; set; }
        public string ProductId { get; set; }
    }
}
