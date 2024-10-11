using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
           //var user = await _userRepository.GetUser(request.UserId);
           return await Task.FromResult(new User());
        }
    }
}