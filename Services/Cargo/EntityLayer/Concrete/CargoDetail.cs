using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CargoDetail
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; }//gönderen müşteri (kim gönderdi)
        public CargoCustomer CargoCustomer { get; set; }//gönderen müşterinin bilgilerini almak için
        public string RecieverCustomer { get; set; }//Alan müşteri
        public int BarcodNumber { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }//compay bilgileri 
    }
}
