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
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
           //var user = await _userRepository.GetUserByName(request.UserName);

           return new User();
        }
    }
}