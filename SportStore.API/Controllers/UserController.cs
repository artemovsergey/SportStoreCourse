using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Data;
using SportStore.API.Dto;
using SportStore.API.Entities;
using SportStore.API.Interfaces;
using SportStore.API.Validations;

namespace SportStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;
    public UserController(IUserRepository repo)
    {
       _repo = repo;
    }

    [HttpPost]
    public ActionResult Login(UserRecordDto user){

        //logic 
        
        return Ok();
    }

    [HttpPost]
    public ActionResult CreateUser(UserRecordDto user){
 
        // TODO: применить AutoMapper
        var currentUser = new User(){
            Name = user.Login
        };

        // TODO: валидация модели User

        var validator = new FluentValidator();
        var result = validator.Validate(currentUser);
        if(!result.IsValid){
            throw new Exception($"{result.Errors.First().ErrorMessage}");
        }

        _repo.CreateUser(currentUser);

        return Created("http://192.168.4.90/api/users/id", currentUser);
    }
    
    [HttpGet]
    public ActionResult GetUser(){
        return Ok(_repo.GetUsers());
    }
    

    [HttpPut]
    public ActionResult UpdateUser(User user){
       return Ok(_repo.EditUser(user, user.Id));
    }


    [HttpGet("{id}")]
    public ActionResult GetUserById(Guid id){
       return Ok(_repo.FindUserById(id));
    }


    [HttpDelete]
    public ActionResult DeleteUser(Guid id){
        return Ok(_repo.DeleteUser(id));
    }

}