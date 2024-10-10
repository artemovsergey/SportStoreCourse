using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.Application.Dto;
using SportStore.Domain;

namespace SportStore.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<ErrorOr<List<UserDto>>>
    {

    }
}