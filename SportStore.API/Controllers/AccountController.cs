using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Dto;
using SportStore.Application.Users;
using SportStore.Application.Users.Commands;
using SportStore.Application.Users.Queries;
using SportStore.Application.Features.Users.Commands.Queries;
using SportStore.API.Interfaces;
using SportStore.API.Entities;
using SportStore.API.Validations;

namespace SportStore.API.Controllers;

    public class AccountController : ApiController
    {
      private readonly ITokenService _tokenSerivice;
      public AccountController(IMediator mediator, ITokenService tokenSerivice) : base(mediator){
         _tokenSerivice = tokenSerivice;
      }

    /// <summary>
    /// Регистрация пользователя и хеширование пароля
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// 
    [HttpPost("register")]        
       public async Task<ActionResult<User>> Register(UserDto user){
 
         using var hmac = new HMACSHA512();

         var registerUser = new User(){

            Login = user.Login,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
            PasswordSalt = hmac.Key,
            Token = _tokenSerivice.CreateToken(user.Login)

         };

         var validator = new SportStore.API.Validations.FluentValidator();
         var validationResult = await validator.ValidateAsync(registerUser);
         if(validationResult.Errors.Count > 0){
               //return BadRequest(validationResult.Errors[0].ErrorMessage);
               //throw new SportStore.Application.Exceptions.ValidationException(validationResult);
         }

         var command = new CreateUserCommand();
         command.User = registerUser;

         var response = await _mediator.Send(command);

         return response.MatchFirst(
               user => Ok(user),
               error =>  BadRequest($"Невозможно создать пользователя. \n {error.Code}") as ObjectResult);
       }
    
      /// <summary>
      /// Авторизация пользователя
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      /// 
      [HttpPost]
      public async Task<ActionResult> Login(UserDto userDto){
         
         var query = new GetUserByNameQuery();
         query.UserName = userDto.Login;

         var response = await _mediator.Send(query);

         return response.MatchFirst(
               user => CheckPasswordHash(userDto,user),
               error => Unauthorized($"Неправильный логин. \n {error.Code}"));
      }

      private ActionResult CheckPasswordHash(UserDto userDto, User user){
         
         using var hmac = new HMACSHA512(user.PasswordSalt);
         var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

         for(int i=0; i< computedHash.Length;i++){
            if(computedHash[i] != user.PasswordHash[i]){
               return Unauthorized($"Неправильный пароль");
            }
         }

         return Ok(user);

      }
    

      [HttpGet("export", Name = "ExportUsers")]
      //[FileResultContentType("text/csv")]

      public async Task<IActionResult> Export(){

         var fileDto =  await _mediator.Send(new GetUsersExportQuery());

         return File(fileDto.Data!, fileDto.ContentType!, fileDto.UserExportFileName);

      }


    }


