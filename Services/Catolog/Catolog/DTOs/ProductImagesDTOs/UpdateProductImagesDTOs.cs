using Catolog.Entities;

namespace Catolog.DTOs.ProductImagesDTOs
{
    public class UpdateProductImagesDTOs
    {
        public string ProductImagesId { get; set; }
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
    }
}
