using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.Application.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<User>>
    {

        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            return  await Task.FromResult(new User()); //await _userRepository.DeleteUserAsync(command.userId);
        }
    }
}