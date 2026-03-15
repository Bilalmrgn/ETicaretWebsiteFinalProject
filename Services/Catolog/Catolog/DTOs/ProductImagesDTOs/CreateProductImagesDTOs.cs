using Catolog.Entities;

namespace Catolog.DTOs.ProductImagesDTOs
{
    public class CreateProductImagesDTOs
    {
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
    }
}
