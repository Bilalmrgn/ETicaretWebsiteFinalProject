using Cargo.Application;
using Cargo.Domain;
using Cargo.Persistence.Context;
using Cargo.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Persistence
{
    public class CargoCompanyWriteRepository : WriteRepository<CargoCompany>, ICargoCompanyWriteRepository
    {
        public CargoCompanyWriteRepository(CargoAppDbcontext context) : base(context)
        {
            
        }
    }
}
