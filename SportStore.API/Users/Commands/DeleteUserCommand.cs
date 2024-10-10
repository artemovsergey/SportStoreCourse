using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.API.Entities;

namespace SportStore.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<ErrorOr<User>>
    {
        public int userId { get; set;}
    }
}