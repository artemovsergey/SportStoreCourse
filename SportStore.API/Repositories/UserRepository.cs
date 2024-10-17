using System.Data.SqlTypes;
using SportStore.API.Data;
using SportStore.API.Entities;
using SportStore.API.Interfaces;
using SportStore.API.Migrations;

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
          throw new Exception($"Ошибка сохранения: {ex.Message}");
       }
    }

    // another methods interface

    public bool DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public User EditUser(User user, int id)
    {
        throw new NotImplementedException();
    }

    public User FindUserById(int id)
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }
}