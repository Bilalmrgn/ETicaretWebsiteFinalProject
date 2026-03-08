using AutoMapper;
using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.FeatureSliderDto;
using Catolog.Entities;
using Catolog.Settings;
using MongoDB.Driver;

namespace Catolog.Services.FeatureSliderService
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;

        public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            var values = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
            await _featureSliderCollection.InsertOneAsync(values);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == id);
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(values);
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            var values = await _featureSliderCollection.Find<FeatureSlider>(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureSliderDto>(values);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            var update = Builders<FeatureSlider>.Update
        .Set(x => x.Title, updateFeatureSliderDto.Title)
        .Set(x => x.Description, updateFeatureSliderDto.Description)
        .Set(x => x.ImageUrl, updateFeatureSliderDto.ImageUrl)
        .Set(x => x.Status, updateFeatureSliderDto.Status);

            await _featureSliderCollection.UpdateOneAsync(
                x => x.FeatureSliderId == updateFeatureSliderDto.FeatureSliderId,
                update);
        }


    }
}
