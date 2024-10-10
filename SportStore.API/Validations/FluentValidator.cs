using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SportStore.API.Entities;

namespace SportStore.API.Validations;

    public class FluentValidator : AbstractValidator<User>
    {
        public FluentValidator()
        {
            RuleFor(u => u.Login).Must(StartsWithCapitalLetter).WithMessage("Логин пользователя должен начинаться с заглавной буквы");
        }
        
        private bool StartsWithCapitalLetter(string login)
        {
            return char.IsUpper(login[0]);
        }
    }