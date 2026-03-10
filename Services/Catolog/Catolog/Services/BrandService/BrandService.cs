using AutoMapper;
using Catolog.DTOs.BrandDto;
using Catolog.DTOs.CategoryDTOs;
using Catolog.Entities;
using Catolog.Settings;
using MongoDB.Driver;

namespace Catolog.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Brand> _brandMongoCollection;
        public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _brandMongoCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
            _mapper = mapper;
        }
        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var values = _mapper.Map<Brand>(createBrandDto);

            await _brandMongoCollection.InsertOneAsync(values);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _brandMongoCollection.DeleteOneAsync(x => x.BrandId == id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandsAsync()
        {
            var categories = await _brandMongoCollection.Find(x => true).ToListAsync();

            var result = categories.Select(c => new ResultBrandDto
            {
                BrandId = c.BrandId,
                BrandName = c.BrandName,
                BrandImageUrl = c.BrandImageUrl,
            }).ToList();

            return result;
        }

        public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
        {
            var values = await _brandMongoCollection.Find<Brand>(x => x.BrandId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdBrandDto>(values);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var update = Builders<Brand>.Update
                .Set(x => x.BrandName, updateBrandDto.BrandName)
                .Set(x=>x.BrandImageUrl, updateBrandDto.BrandImageUrl);
            await _brandMongoCollection.UpdateOneAsync(
                x => x.BrandId == updateBrandDto.BrandId,
                update);
        }
    }
}
