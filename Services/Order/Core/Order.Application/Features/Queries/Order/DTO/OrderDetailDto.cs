using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Order.DTO
{
    //bu sınıfı oluşturmamın amacı şu:
    /*benim OrderQueryResponse nesnemde Order details var yani ben aslında orderDetails i order ın içerisinde göstermek istiyorum.
     Bu yüzden de OrderDetails kısmını QueryHandler da map lemem gerekiyor. Bu yüzden domain deki orderDetail i kullanmayacağıma göre
    ekstra bir tane sınıf oluştururum ve Dto olur ve bu sınıfı map lemek için kullanırım
     */
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductTotalPrice { get; set; }
        //public int OrderingId { get; set; }
        //public Ordering Ordering { get; set; }
        //yukarıdaki iki nesneye gerek yok çünkü zaten üstte Order duracak bir daa burada yazmanın mantığı yok

    }
}
