using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Application.Interfaces;

    public interface IUnitOfWork
    {
        public Task CommitChangedAsync(CancellationToken cancellationToken = default);
    }
