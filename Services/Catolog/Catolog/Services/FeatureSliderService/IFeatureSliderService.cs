using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.FeatureSliderDtos;

namespace Catolog.Services.FeatureSliderService
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);//burada kullanıcıya geri veri döndürmemize gerek yok bu yüzden Task<T> yapmadık
        Task DeleteFeatureSliderAsync(string id);
        Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id);//burada kullanıcıya id ye göre bir değer döndüreceğimiz için Task<T> Yaptık
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
