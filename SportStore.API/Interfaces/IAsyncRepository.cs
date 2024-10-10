using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task Create(T entity);
        Task<T> Update(T entity);
        Task<bool> DeleteAsync(int id);
    }
}