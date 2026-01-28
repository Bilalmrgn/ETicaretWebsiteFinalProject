using Dapper;
using Discount.Context;
using Discount.Dtos;

namespace Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _dapperContext;
        public DiscountService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            var sql = @"INSERT INTO Coupons (Code, Rate, IsActive, ValidDate)
                VALUES (@Code, @Rate, @IsActive, @ValidDate)";

            using var connection = _dapperContext.CreateConnection();

            await connection.ExecuteAsync(sql, createCouponDto);
        }

        public async Task DeleteCouponAsync(int couponId)
        {
            var query = @"DELETE FROM Coupons
                  WHERE CouponId = @CouponId";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { CouponId = couponId });
        }

        public async Task<List<ResultCouponDto>> GetAllCouponAsync()
        {
            var query = @"SELECT CouponId, Code, Rate, IsActive, ValidDate
                  FROM Coupons";

            using var connection = _dapperContext.CreateConnection();

            var coupons = await connection.QueryAsync<ResultCouponDto>(query);

            return coupons.ToList();
        }

        

        public async Task<GetByIdCoupon> GetByIdCouponAsync(int couponId)
        {
            var query = @"SELECT CouponId, Code, Rate, IsActive, ValidDate
                  FROM Coupons
                  WHERE CouponId = @CouponId";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdCoupon>(
                query, new { CouponId = couponId });
        }

        public async Task UpdateCouponAsync(UpdateCouponDto dto)
        {
            var query = @"UPDATE Coupons
                  SET Code = @Code,
                      Rate = @Rate,
                      IsActive = @IsActive,
                      ValidDate = @ValidDate
                  WHERE CouponId = @CouponId";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, dto);
        }

    }
}
