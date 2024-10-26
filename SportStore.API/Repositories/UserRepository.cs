using System.Data.SqlTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportStore.API.Data;
using SportStore.API.Entities;
using SportStore.API.Exceptions;
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
        catch (SqlTypeException ex)
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

    public User FindUser(string login)
    {
       var user = _db.Users.Where(u => u.Login == login).FirstOrDefault<User>();
       return  user != null ? user : throw new NotFoundException("Пользователь не найден"); 
    }

    public User FindUserById(int id)
    {
        var user = _db.Users.Where(u => u.Id == id).FirstOrDefault<User>();
       return  user != null ? user : throw new NotFoundException($"Пользователь c id = {id} не найден"); 
    }

    public List<User> GetUsers()
    {
        return _db.Users.ToList();
    }
}