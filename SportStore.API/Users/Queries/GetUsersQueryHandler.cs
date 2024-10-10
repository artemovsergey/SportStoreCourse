using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using MediatR;
using SportStore.Application.Dto;
using SportStore.Application.Interfaces;
using SportStore.Domain;

namespace SportStore.Application.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await _userRepository.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);
        
            return users != null ? usersDto : Error.NotFound();
        }
    }
}