using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace SportStore.API.Exceptions;

    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();

            foreach (var error in validationResult.Errors){
                ValidationErrors.Add(error.ErrorMessage);
            }
            

        }
    }
