using AutoMapper;

using Catolog.DTOs.ProductDetailDTOs;
using Catolog.Entities;
using Catolog.Services.ProductDetailServices;
using Catolog.Settings;
using MongoDB.Driver;

namespace Catolog.Services.ProductDetailDetailServices
{
    public class ProductDetailServices : IProductDetailServices
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;

        public ProductDetailServices(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }


        public async Task CreateProductDetailAsync(CreateProductDetailDTOs createProductDetailDto)
        {
            var values = _mapper.Map<ProductDetail>(createProductDetailDto);
            await _productDetailCollection.InsertOneAsync(values);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _productDetailCollection.DeleteOneAsync(x => x.ProductDetailId == id);
        }

        public async Task<List<ResultProductDetailDTOs>> GetAllProductDetailAsync()
        {
            var values = await _productDetailCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDetailDTOs>>(values);
        }

        public async Task<GetByIdProductDetailDTOs> GetByIdProductDetailAsync(string id)
        {
            var values = await _productDetailCollection.Find<ProductDetail>(x => x.ProductDetailId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDetailDTOs>(values);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDTOs updateProductDetailDTOs)
        {
            var values = _mapper.Map<ProductDetail>(updateProductDetailDTOs);
            await _productDetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailId == updateProductDetailDTOs.ProductDetailId, values);
        }
    }
}
