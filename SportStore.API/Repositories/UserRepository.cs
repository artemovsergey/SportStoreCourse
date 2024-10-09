using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Data;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SportStoreContext _db;
    public UserRepository(SportStoreContext db)
    {
        _db = db;
    }

    public User CreateUser(User user)
    {
       try
       {
         _db.Add(user);
         _db.SaveChanges();
         return user;
       }
       catch(SqlTypeException ex)
       {
         throw new SqlTypeException($"Ошибка SQL: {ex.Message}");
       }
       catch (Exception ex)
       {
         throw new Exception($"Ошибка: {ex.Message}");
       }
    }

    public bool DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public User EditUser(User user, Guid id)
    {
        throw new NotImplementedException();
    }

    public User FindUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }
}