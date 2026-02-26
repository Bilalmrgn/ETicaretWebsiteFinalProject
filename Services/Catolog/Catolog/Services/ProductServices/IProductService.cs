using Catolog.DTOs.ProductDTOs;

namespace Catolog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDTOs>> GetAllProductAsync();
        Task UpdateProductAsync(UpdateProductDTOs updateProductDTOs);
        Task CreateProductAsync(CreateProductDTOs createProductDto);//burada kullanıcıya geri veri döndürmemize gerek yok bu yüzden Task<T> yapmadık
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDTOs> GetByIdProductAsync(string id);//burada kullanıcıya id ye göre bir değer döndüreceğimiz için Task<T> Yaptık

    }
}
