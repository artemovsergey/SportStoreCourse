using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.Domain;

namespace SportStore.Application.Users.Queries
{
    public class GetUserByNameQuery : IRequest<ErrorOr<User>>
    {
        public string UserName {get; set;} = default!;
    }
}