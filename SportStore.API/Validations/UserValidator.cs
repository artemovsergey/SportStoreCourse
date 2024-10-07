using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using SportStore.API.Data;
using SportStore.API.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace SportStore.API.Validations;

public class MaxLengthAttribute : ValidationAttribute
{
    private readonly int _maxLength;

    public MaxLengthAttribute(int maxLength) : base($"Name max {maxLength} ")
    {
        _maxLength = maxLength;
    }

    public override bool IsValid(object value)
    {
        return ((String)value).Length <= _maxLength;
    }
}