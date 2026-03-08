namespace Catolog.Entities
{
    public class ProductWithCategory : Product
    {
        public List<Category> Categories { get; set; }
    }
}
