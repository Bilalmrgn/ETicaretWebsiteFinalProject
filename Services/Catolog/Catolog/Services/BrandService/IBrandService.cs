using Catolog.DTOs.BrandDto;
using Catolog.DTOs.CategoryDTOs;

namespace Catolog.Services.BrandService
{
    public interface IBrandService
    {
        Task<List<ResultBrandDto>> GetAllBrandsAsync();
        Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        Task DeleteBrandAsync(string id);
        Task CreateBrandAsync(CreateBrandDto createBrandDto);
        Task<GetByIdBrandDto> GetByIdBrandAsync(string id);
    }
    
}
