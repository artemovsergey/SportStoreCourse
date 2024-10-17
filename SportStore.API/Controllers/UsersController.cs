using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Entities;
using SportStore.API.Interfaces;
using SportStore.API.Validations;

namespace SportStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    public UsersController(IUserRepository repo)
    {
       _repo = repo;
    }

    [HttpPost]
    public ActionResult CreateUser(User user){

        var validator = new FluentValidator();
        var result = validator.Validate(user);
        if(!result.IsValid){
            throw new Exception($"{result.Errors.First().ErrorMessage}");
        }

        return Ok(_repo.CreateUser(user));
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
    public ActionResult GetUserById(int id){
       return Ok(_repo.FindUserById(id));
    }


    [HttpDelete]
    public ActionResult DeleteUser(int id){
        return Ok(_repo.DeleteUser(id));
    }

}