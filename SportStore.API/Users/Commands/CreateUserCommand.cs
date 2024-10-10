using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;

using SportStore.Domain;

namespace SportStore.Application.Users.Commands;

public class CreateUserCommand : IRequest<ErrorOr<User>>
{
    public User? User {get ;set;}
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>{

    public CreateUserCommandValidator()
    {
        RuleFor(u => u.User!.Name).NotEmpty().WithMessage("У пользователя должно быть имя");
    }
}
