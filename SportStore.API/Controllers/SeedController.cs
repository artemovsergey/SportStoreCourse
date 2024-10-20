using System.Security.Cryptography;
using System.Text;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Data;
using SportStore.API.Dto;
using SportStore.API.Entities;

namespace SportStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SeedController : ControllerBase
{
    private readonly SportStoreContext _db;
    public SeedController(SportStoreContext db)
    {
       _db = db;
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
                    PasswordSalt = hmac.Key
                    
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
}