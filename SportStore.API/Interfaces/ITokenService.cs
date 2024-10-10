using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string userName);
    }
}