using AutoMapper;
using Catolog.DTOs.ProductImagesDTOs;
using Catolog.Entities;
using Catolog.Services.ProductImagesServices;
using Catolog.Settings;
using MongoDB.Driver;

namespace Catolog.Services.ProductImagesServices
{
    public class ProductImagesService : IProductImageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductImages> _ProductImagesCollection;

        public ProductImagesService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _ProductImagesCollection = database.GetCollection<ProductImages>(_databaseSettings.ProductImagesCollectionName);
            _mapper = mapper;
        }


        public async Task CreateProductImagesAsync(CreateProductImagesDTOs createProductImagesDto)
        {
            var values = _mapper.Map<ProductImages>(createProductImagesDto);
            await _ProductImagesCollection.InsertOneAsync(values);
        }

        public async Task DeleteProductImagesAsync(string id)
        {
            await _ProductImagesCollection.DeleteOneAsync(x => x.ProductImagesId == id);
        }

        public async Task<List<ResultProductImagesDTOs>> GetAllProductImagesAsync()
        {
            var values = await _ProductImagesCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductImagesDTOs>>(values);
        }

        public async Task<GetByIdProductImagesDTOs> GetByIdProductImagesAsync(string id)
        {
            var values = await _ProductImagesCollection.Find<ProductImages>(x => x.ProductImagesId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImagesDTOs>(values);
        }

        public async Task UpdateProductImagesAsync(UpdateProductImagesDTOs updateProductImagesDTOs)
        {
            var values = _mapper.Map<ProductImages>(updateProductImagesDTOs);
            await _ProductImagesCollection.FindOneAndReplaceAsync(x => x.ProductImagesId == updateProductImagesDTOs.ProductImagesId, values);
        }
    }
}
