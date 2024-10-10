using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Dto;

public class UserDto
{
    [MinLength(5,ErrorMessage = "Минимальное длина 5")]
    [SportStore.API.Validations.MaxLength(10)]
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public record UserRecordDto {
    
    [MinLength(5,ErrorMessage = "Минимальное длина 5")]
    [SportStore.API.Validations.MaxLength(10)]
    public string Login {get; init;}
    public string Password {get; init;}

};