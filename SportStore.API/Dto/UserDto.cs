using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Dto;

public class UserDto
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public record UserRecordDto(string Login, string Password);