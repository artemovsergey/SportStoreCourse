using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SportStore.API.Entities;
using SportStore.API.Interfaces;


namespace SportStore.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
           _userRepository = userRepository;
           _unitOfWork = unitOfWork;   
        }

        public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            // var validator = new CreateUserCommandValidator();
            // var validatorResult = await validator.ValidateAsync(command);

            // if(validatorResult.Errors.Count > 0){
            //     throw new Exceptions.ValidationException(validatorResult);
            // }

            //var response = await _userRepository.CreateUserAsync(command.User!);

            return new User();
        }
    }
}