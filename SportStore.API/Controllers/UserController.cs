using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Data;
using SportStore.API.Dto;
using SportStore.API.Entities;
using SportStore.API.Interfaces;
using SportStore.API.Validations;

namespace SportStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    private readonly SportStoreContext _db;
    private readonly ITokenService _tokenService;

    public UserController(IUserRepository repo,
                          IMapper mapper,
                          SportStoreContext db,
                          ITokenService tokenService)
    {
        _mapper = mapper;
        _repo = repo;
        _db = db;
        _tokenService = tokenService;
    }

    [HttpGet("generate")]
    public ActionResult SeedUsers()
    {

        using var hmac = new HMACSHA512();

        Faker<UserRecordDto> _faker = new Faker<UserRecordDto>("en")
            .RuleFor(u => u.Login, f => GenerateLogin(f).Trim())
            .RuleFor(u => u.Password, f => GeneratePassword(f).Trim().Replace(" ", ""));


        string GenerateLogin(Faker faker)
        {
            return faker.Random.Word() + faker.Random.Number(3, 5);
        }

        string GeneratePassword(Faker faker)
        {
            return faker.Random.Word() + faker.Random.Number(3, 5);
        }

        var users = _faker.Generate(100).Where(u => u.Login.Length > 4 && u.Login.Length <= 10);

        List<User> userToDb = new List<User>();

        try
        {

            foreach (var user in users)
            {
                var u = new User()
                {
                    Login = user.Login,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key,
                    Token = _tokenService.CreateToken(user.Login)
                };

                userToDb.Add(u);

            }

            _db.Users.AddRange(userToDb);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.InnerException!.Message}");
        }

        return Ok(userToDb);
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    /// 
    [HttpPost("login")]
    public ActionResult Login(UserRecordDto userDto)
    {

        // находим пользователя в базе данных
        var user = _db.Users.FirstOrDefault(u => u.Login == userDto.Login);

        if (user == null) return NotFound($"Пользователя {userDto.Login} не существует");

        // проверка пароля
        return CheckPasswordHash(userDto, user);
    }

    [HttpPost]
    public ActionResult CreateUser(UserRecordDto user)
    {

        using var hmac = new HMACSHA512();

        // Ручной маппинг

        // var currentUser = new User(){
        //     Login = user.Login,
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
        //     PasswordSalt = hmac.Key,
        // };

        // AutoMapper
        var currentUser = _mapper.Map<User>(user);
        currentUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
        currentUser.PasswordSalt = hmac.Key;
        currentUser.Token = _tokenService.CreateToken(user.Login);

        // TODO: валидация модели User

        var validator = new FluentValidator();
        var result = validator.Validate(currentUser);
        if (!result.IsValid)
        {
            throw new Exception($"{result.Errors.First().ErrorMessage}");
        }

        _repo.CreateUser(currentUser);

        return Created("http://192.168.4.90/api/User/id", currentUser);
    }

    //[Authorize]
    [HttpGet]
    public ActionResult GetUser()
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
    /// Проверка пароля
    /// </summary>
    /// <param name="userDto"> Модель представления </param>
    /// <param name="user"> Пользователь из базы данных </param>
    /// <returns></returns>
    private ActionResult CheckPasswordHash(UserRecordDto userDto, User user)
    {
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized($"Неправильный пароль");
            }
        }
        return Ok(user);
    }

}