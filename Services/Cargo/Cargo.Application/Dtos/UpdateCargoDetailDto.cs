using Cargo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Dtos
{
    public class UpdateCargoDetailDto
    {
        public string CargoDetailId { get; set; }
        public string SenderCustomer { get; set; }//gönderen müşteri (kim gönderdi)
        public string RecieverCustomer { get; set; }//Alan müşteri
        public int BarcodNumber { get; set; }
        public string CargoCompanyId { get; set; }
        
    }
}
