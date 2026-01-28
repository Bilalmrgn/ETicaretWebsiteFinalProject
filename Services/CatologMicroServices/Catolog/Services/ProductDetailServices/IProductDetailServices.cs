using Catolog.DTOs.ProductDetailDTOs;

namespace Catolog.Services.ProductDetailServices
{
    public interface IProductDetailServices
    {
        Task<List<ResultProductDetailDTOs>> GetAllProductDetailAsync();
        Task UpdateProductDetailAsync(UpdateProductDetailDTOs updateProductDetailDTOs);
        Task CreateProductDetailAsync(CreateProductDetailDTOs ProductDetailDTOs);//burada kullanıcıya geri veri döndürmemize gerek yok bu yüzden Task<T> yapmadık
        Task DeleteProductDetailAsync(string id);
        Task<GetByIdProductDetailDTOs> GetByIdProductDetailAsync(string id);//burada kullanıcıya id ye göre bir değer döndüreceğimiz için Task<T> Yaptık

    }
}
