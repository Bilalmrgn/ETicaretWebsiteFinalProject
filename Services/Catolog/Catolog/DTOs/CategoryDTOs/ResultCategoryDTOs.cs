namespace Catolog.DTOs.CategoryDTOs
{
    public class ResultCategoryDTOs
    {
        //bu sınıfın amacı: kategori işlemlerinde listelemek istediğim işlemleri tutmak. Yani kullanıcı category name ve id sini görebilmesini istiyorum
        public string CategoryId { get; set; }//mongodb de id ler string ile tutulur
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public int ProductCount { get; set; }
    }
}
