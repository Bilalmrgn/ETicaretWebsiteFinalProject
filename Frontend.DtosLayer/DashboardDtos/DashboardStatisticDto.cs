namespace Frontend.DtosLayer.DashboardDtos
{
    public class DashboardStatisticDto
    {
        public int TotalOrderCount { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; } 
        public int TotalUserCount { get; set; }

        public List<TopSellingProductDto> TopSellingProducts { get; set; } = new();
        public List<SalesByCityDto> SalesByCities { get; set; } = new();
    }

    public class TopSellingProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalSold { get; set; }
        public decimal Price { get; set; }
    }

    public class SalesByCityDto
    {
        public string City { get; set; }
        public int OrderCount { get; set; }
        public int ProductCount { get; set; }
    }
}
