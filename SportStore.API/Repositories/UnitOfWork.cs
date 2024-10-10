using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Data;
using SportStore.API.Interfaces;

namespace SportStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportStoreContext _db;
        public UnitOfWork(SportStoreContext db)
        {
           _db = db; 
        }

        public async Task CommitChangedAsync(CancellationToken cancellationToken)
        {
            try{
             await _db.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex){
                Console.WriteLine(ex.ToString());
            }

        }
    }
}