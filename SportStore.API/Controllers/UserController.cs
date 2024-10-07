using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Data;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _db;
    public UserController(IUserRepository db)
    {
       _db = db;
    }

    [HttpPost]
    public ActionResult CreateUser(User user){
        
        _db.CreateUser(user);
        return Created("http://192.168.4.90/api/users/id", user);
    }
    
}