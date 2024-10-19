using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Entities;

namespace SportStore.API.Interfaces;

public interface IUserRepository
{
   User CreateUser(User user);
   List<User> GetUsers();
   User EditUser(User user, int id);
   bool DeleteUser(int id);
   User FindUserById(int id);
   User FindUser(string login);
}