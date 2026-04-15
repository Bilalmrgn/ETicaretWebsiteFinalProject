using Frontend.DtosLayer.ProductsDto;

namespace ECommerce.WebUI.ViewModel
{
    public class ProductWithFavoriteViewModel
    {
        public ProductListDto Product { get; set; }
        public bool IsFavorite { get; set; }
    }
}
