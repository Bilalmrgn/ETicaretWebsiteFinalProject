using Dapper;
using Discount.API.Context;
using Discount.API.Dtos;

namespace Discount.API.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)";

            var parameters = new DynamicParameters(); //SQL sorgusuna parametre eklemek için kullanılır
            parameters.Add("@code", createCouponDto.Code);
            parameters.Add("@rate", createCouponDto.Rate);
            parameters.Add("@isActive", createCouponDto.IsActive);
            parameters.Add("@validDate", createCouponDto.ValidDate);

            using (var connection = _context.CreateConnection()) //metodu veritabanı bağlantısını açar 
            {
                await connection.ExecuteAsync(query, parameters); //metodu ile SQL sorgusu çalıştırılır
            }

        }

        public async Task DeleteCouponAsync(int id)
        {
            string query = "Delete From Coupons where CouponId = @couponId";

            var parameters = new DynamicParameters();
            parameters.Add("@couponId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCouponDto>> GetAllCouponAsync()
        {
            string query = "Select * From Coupons";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDto>(query); //metodu belirtilen SQL sorgusunu çalıştırır ve sonuçları ResultCouponDto tipinde bir liste olarak döner
                return values.ToList();
            }
        }

        public async Task<GetByIdCouponDto> GetByIdCouponAsync(int id)
        {
            string query = "Select * From Coupons where CouponId = @couponId";

            var parameters = new DynamicParameters();
            parameters.Add("@couponId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query, parameters); //metodu belirtilen SQL sorgusunu çalıştırır ve CouponId değeri eşleşen ilk satırı GetByIdCouponDto tipinde döner
                return values;
            }
        }

        public async Task<ResultCouponDto?> GetByCodeAsync(string code)
        {
            string query = "Select * From Coupons where Code = @code";

            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<ResultCouponDto>(query, parameters);

                return value;
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
        {
            string query = "Update Coupons Set Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate where CouponId=@couponId";

            var parameters = new DynamicParameters(); //SQL sorgusuna parametre eklemek için kullanılır
            parameters.Add("@code", updateCouponDto.Code);
            parameters.Add("@rate", updateCouponDto.Rate);
            parameters.Add("@isActive", updateCouponDto.IsActive);
            parameters.Add("@validDate", updateCouponDto.ValidDate);
            parameters.Add("@couponId", updateCouponDto.CouponId);

            using (var connection = _context.CreateConnection()) //metodu veritabanı bağlantısını açar 
            {
                await connection.ExecuteAsync(query, parameters); //metodu ile SQL sorgusu çalıştırılır
            }
        }
    }
}
