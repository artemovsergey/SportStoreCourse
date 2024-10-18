using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Dto;
using SportStore.API.Entities;
using SportStore.API.Interfaces;
using SportStore.API.Validations;

namespace SportStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly ITokenService _tokenService;
    HMACSHA256 hmac = new HMACSHA256();
    public UsersController(IUserRepository repo, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _repo = repo;
    }

    [HttpPost]
    public ActionResult CreateUser(UserDto userDto)
    {

        // формирование объекта из модели представления
        var user = new User()
        {
            Login = userDto.Login,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
            PasswordSalt = hmac.Key,
            Token = _tokenService.CreateToken(userDto.Login)
        };

        // проверка валидатором объекта user
        ValidUser(user);

        // сохранение пользователя
        return Ok(_repo.CreateUser(user));
    }

    // [Authorize]
    [HttpGet]
    public ActionResult GetUsers()
    {
        return Ok(_repo.GetUsers());
    }


    [HttpPut]
    public ActionResult UpdateUser(User user)
    {
        return Ok(_repo.EditUser(user, user.Id));
    }


    [HttpGet("{id}")]
    public ActionResult GetUserById(int id)
    {
        return Ok(_repo.FindUserById(id));
    }


    [HttpDelete]
    public ActionResult DeleteUser(int id)
    {
        return Ok(_repo.DeleteUser(id));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="Exception"> Ошибки проверки через FluentValidation</exception> <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    private void ValidUser(User user)
    {
        var validator = new FluentValidator();
        var result = validator.Validate(user);
        if (!result.IsValid)
        {
            throw new Exception($"{result.Errors.First().ErrorMessage}");
        }
    }

}