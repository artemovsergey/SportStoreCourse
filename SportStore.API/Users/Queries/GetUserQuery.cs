using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.API.Entities;

namespace SportStore.Application.Users.Queries
{
    public class GetUserQuery : IRequest<ErrorOr<User>>
    {
        public int UserId {get;set;}
    }
}