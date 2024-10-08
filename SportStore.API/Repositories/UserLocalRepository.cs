using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.API.Repositories;

public class UserLocalRepository : IUserRepository
{
    public IList<User> Users { get; set; } = new List<User>();  

    public User CreateUser(User user)
    {
       Users.Add(user);
       return user;
    }

    public bool DeleteUser(int id)
    {
        var result = FindUserById(id);
        Users.Remove(result);
        return true;
    }


    /// <summary>
    /// Редактирование пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public User EditUser(User user, int id)
    {
       var result = FindUserById(id);
       // update
       result.Name = user.Name;
       return result;
    }

    public User FindUserById(int id)
    {
        var result = Users.Where(u => u.Id == id).FirstOrDefault();

       if(result == null){
         throw new Exception($"Нет пользователя с id = {id}");
       }

       return result;
    }

    public List<User> GetUsers()
    {
       return (List<User>)Users;
    }
}