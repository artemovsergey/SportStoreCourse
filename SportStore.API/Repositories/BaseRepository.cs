using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Application.Interfaces;
using SportStore.Infrastructure.Data;

namespace SportStore.Infrastructure.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{

    protected readonly SportStoreContext _db;
    public BaseRepository(SportStoreContext db)
    {
        _db = db;
    }

    public Task Create(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T> GetByIdAsync(int id){
        return await _db.Set<T>().FindAsync(id);
    }

    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }
}
