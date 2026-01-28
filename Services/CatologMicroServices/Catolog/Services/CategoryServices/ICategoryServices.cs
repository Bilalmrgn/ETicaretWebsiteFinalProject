using Catolog.DTOs.CategoryDTOs;

namespace Catolog.Services.CategoryServices
{
    public interface ICategoryServices
    {
        /*Task<T> anlamı: Sana T tipinde veri döneceğim*/
        Task<List<ResultCategoryDTOs>> GetAllCategoryAsync();
        Task UpdateCategoryAsync(UpdateCategoryDTOs updateCategoryDTOs);
        Task CreateCategoryAsync(CreateCategoryDTOs categoryDTOs);//burada kullanıcıya geri veri döndürmemize gerek yok bu yüzden Task<T> yapmadık
        Task DeleteCategoryAsync(string id);
        Task<GetByIdCategoryDTOs> GetByIdCategoryAsync(string id);//burada kullanıcıya id ye göre bir değer döndüreceğimiz için Task<T> Yaptık



    }
}
