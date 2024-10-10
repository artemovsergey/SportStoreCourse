using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Entities; 

public class User
{
    public int Id {get ;set;}
    public string Login {get ;set;} = string.Empty;
    public byte[] PasswordHash {get ;set;} = default!;
    public byte[] PasswordSalt {get ;set;} = default!;
    

    [MinLength(5,ErrorMessage = "Минимальное длина имени 5")]
    [SportStore.API.Validations.MaxLength(10)]
    public string Name {get ;set;} = string.Empty;

    public string Token {get; set; } = default!;

}