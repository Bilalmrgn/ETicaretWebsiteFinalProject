using Catolog.DTOs.ProductImagesDTOs;

namespace Catolog.Services.ProductImagesServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImagesDTOs>> GetAllProductImagesAsync();
        Task UpdateProductImagesAsync(UpdateProductImagesDTOs updateProductImageDTOs);
        Task CreateProductImagesAsync(CreateProductImagesDTOs ProductImageDTOs);//burada kullanıcıya geri veri döndürmemize gerek yok bu yüzden Task<T> yapmadık
        Task DeleteProductImagesAsync(string id);
        Task<GetByIdProductImagesDTOs> GetByIdProductImagesAsync(string id);//burada kullanıcıya id ye göre bir değer döndüreceğimiz için Task<T> Yaptık

    }
}
