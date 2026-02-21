using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Domain
{
    //kargo operasyonları için
    public class CargoOperation : BaseEntity
    {
        
        public string Barcode { get; set; }
        public string Description { get; set; }
        public DateTime OperationDate { get; set; }

    }
}
