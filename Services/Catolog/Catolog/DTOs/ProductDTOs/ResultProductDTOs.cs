using Catolog.DTOs.CategoryDTOs;

namespace Catolog.DTOs.ProductDTOs
{
    public class ResultProductDTOs
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ResultCategoryDTOs Category { get; set; }
    }
}
