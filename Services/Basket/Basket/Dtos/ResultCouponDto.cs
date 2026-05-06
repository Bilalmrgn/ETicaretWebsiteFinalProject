namespace Basket.Dtos
{
    public class ResultCouponDto
    {
        public string Code { get; set; }
        public int Rate { get; set; }
        public bool IsFirstOrderOnly { get; set; }
    }
}
