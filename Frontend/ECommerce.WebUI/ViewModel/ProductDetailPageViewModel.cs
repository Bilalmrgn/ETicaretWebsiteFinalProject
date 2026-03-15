using Frontend.DtosLayer.ProductDetailDto;
using Frontend.DtosLayer.ProductsDto;

namespace ECommerce.WebUI.ViewModel
{
    public class ProductDetailPageViewModel
    {
        public GetProductByIdDto Product { get; set; }
        public ResultProductDetailDto ProductDetail { get; set; }
    }
}
