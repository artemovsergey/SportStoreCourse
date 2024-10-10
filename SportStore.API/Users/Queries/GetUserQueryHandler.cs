using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.Application.Interfaces;
using SportStore.Domain;

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
           var user = await _userRepository.GetUser(request.UserId);
           return user != null ? user : Error.NotFound();
        }
    }
}