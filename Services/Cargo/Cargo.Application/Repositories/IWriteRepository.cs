using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        /*Task<bool> AddRangeAsync(List<T> list);//Koleksyon şeklinde eklemek için*/
        Task<bool> RemoveAsync(string id);
        bool Remove(T model);
        bool Update(T model);
        Task<int> SaveChangeAsync();
    }
}
