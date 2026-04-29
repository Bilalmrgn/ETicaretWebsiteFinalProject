namespace Order.WebAPI.Models
{
    public enum OrderStatus
    {
        //siparişin hangi aşamada olduğunu gösterir.
        Pending,
        Processing,
        Completed,
        Failed,
        Cancelled
    }
}
