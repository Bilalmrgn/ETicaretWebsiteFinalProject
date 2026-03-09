using Catolog.DTOs.FeatureSliderDto;
using Catolog.DTOs.SpecialOfferDto;

namespace Catolog.Services.SpecialOfferService
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);//burada kullanıcıya geri veri döndürmemize gerek yok bu yüzden Task<T> yapmadık
        Task DeleteSpecialOfferAsync(string id);
        Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id);//burada kullanıcıya id ye göre bir değer döndüreceğimiz için Task<T> Yaptık

    }
}
