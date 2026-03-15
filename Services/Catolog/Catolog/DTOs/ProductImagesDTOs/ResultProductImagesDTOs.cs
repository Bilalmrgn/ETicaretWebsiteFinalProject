using Catolog.Entities;

namespace Catolog.DTOs.ProductImagesDTOs
{
    public class ResultProductImagesDTOs
    {
        public string ProductImagesId { get; set; }
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
    }
}
