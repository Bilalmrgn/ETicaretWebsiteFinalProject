using AutoMapper;
using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.CategoryDTOs;
using Catolog.Entities;
using Catolog.Settings;
using MongoDB.Driver;

namespace Catolog.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        //veritabanına kaydetmek için bir tane nesne oluşturmamız gerekiyor
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _CategoryCollection;
        private readonly IMongoCollection<Product> _productCollection;
        public CategoryServices(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _CategoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }


        public async Task CreateCategoryAsync(CreateCategoryDTOs createCategoryDto)
        {
            var values = _mapper.Map<Category>(createCategoryDto);
            await _CategoryCollection.InsertOneAsync(values);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _CategoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task<List<ResultCategoryDTOs>> GetAllCategoryAsync()
        {
            var categories = await _CategoryCollection.Find(x => true).ToListAsync();
            var products = await _productCollection.Find(x => true).ToListAsync();

            var result = categories.Select(c => new ResultCategoryDTOs
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                ImageUrl = c.ImageUrl,
                ProductCount = products.Count(p => p.CategoryId == c.CategoryId)
            }).ToList();

            return result;
        }

        public async Task<GetByIdCategoryDTOs> GetByIdCategoryAsync(string id)
        {
            var values = await _CategoryCollection.Find<Category>(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDTOs>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTOs dto)
        {
            var update = Builders<Category>.Update
                .Set(x => x.CategoryName, dto.CategoryName);

            await _CategoryCollection.UpdateOneAsync(
                x => x.CategoryId == dto.CategoryId,
                update);
        }
    }
}
