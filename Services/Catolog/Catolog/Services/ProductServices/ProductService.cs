using AutoMapper;
using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.ProductDTOs;
using Catolog.Entities;
using Catolog.Settings;
using MongoDB.Driver;

namespace Catolog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        //Create Product
        public async Task CreateProductAsync(CreateProductDTOs createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(values);
        }

        //Delete Product
        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        //Get All Products
        public async Task<List<ResultProductDTOs>> GetAllProductAsync()
        {
            var result = await _productCollection.Aggregate()
                .Lookup<Product, Category, ProductWithCategory>(
                    _categoryCollection,
                    p => p.CategoryId,    
                    c => c.CategoryId,    
                    x => x.Categories 
                )
                .ToListAsync();
            
            return _mapper.Map<List<ResultProductDTOs>>(result);
        }

        //Get By Id Product
        public async Task<GetByIdProductDTOs> GetByIdProductAsync(string id)
        {
            var values = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDTOs>(values);
        }

        //Get Last 10 Product
        public async Task<List<ResultProductDTOs>> GetLast10ProductsAsync()
        {
            var values = await _productCollection.Find(x => true).SortByDescending(x => x.CreatedDate).Limit(10).ToListAsync();
            return _mapper.Map<List<ResultProductDTOs>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDTOs updateProductDTOs)
        {
            var update = Builders<Product>.Update
                .Set(x => x.ProductName, updateProductDTOs.ProductName)
                .Set(x => x.ProductPrice, updateProductDTOs.ProductPrice)
                .Set(x => x.ProductImageUrl, updateProductDTOs.ProductImageUrl)
                .Set(x => x.ProductDescription, updateProductDTOs.ProductDescription)
                .Set(x => x.CategoryId, updateProductDTOs.CategoryId);

            await _productCollection.UpdateOneAsync(
                x => x.ProductId == updateProductDTOs.ProductId,
                update
            );
        }



    }
}
