using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Domain;

namespace SportStore.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string userName);
    }
}