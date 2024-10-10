using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.Domain;

namespace SportStore.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<ErrorOr<User>>
    {
        public User? User {get;set;}
        public int userId {get ;set;}
    }
}