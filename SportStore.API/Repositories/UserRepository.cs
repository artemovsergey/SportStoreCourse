using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.API.Repositories;

public clasUserRepository : IUserRepository
{
    public IList<User> Users { get; set; } = new List<User>();  

    public User CreateUser(User user)
    {
       Users.Add(user);
       return user;
    }

    public List<User> GetUsers()
    {
        return (List<User>)Users;
    }
}