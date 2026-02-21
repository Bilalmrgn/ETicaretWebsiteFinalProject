using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        //base repository sınıfım. Yani ben buarda bütün repository sınıflarımda ortak olacak şeyleri yazacağım (BaseEntity gibi)
        DbSet<T> Table { get; }
    }
}
