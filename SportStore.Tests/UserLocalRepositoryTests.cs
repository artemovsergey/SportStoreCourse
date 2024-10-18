using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SportStore.API.Entities;
using SportStore.API.Repositories;

namespace SportStore.Tests;

public class UserLocalRepositoryTests
{
    private readonly UserLocalRepository _userLocalRepository;
    public HMACSHA512 hmac = new HMACSHA512();
    public UserLocalRepositoryTests()
    {
        _userLocalRepository = new UserLocalRepository();
    }

    [Fact]
    public void CreateUser_ShouldReturnNewUserWithGeneratedId()
    {
        // Arrange
        var newUser = new User {Name = "Test User",
                                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                PasswordSalt = hmac.Key};
        // Act
        var createdUser = _userLocalRepository.CreateUser(newUser);
        // Assert
        Assert.NotNull(createdUser);
        // Assert.NotEqual(Guid.Empty, createdUser.Id);
        Assert.Equal(newUser.Name, createdUser.Name);
    }

    [Fact]
    public void DeleteUser_ShouldReturnTrueAndRemoveUser()
    {
        // Arrange
        var UserLocalRepository = new UserLocalRepository();
        var testUser = new User { Id = 1,
                                  Name = "Test User",
                                  PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                  PasswordSalt = hmac.Key };

        UserLocalRepository.Users.Add(testUser);

        // Act
        bool result = UserLocalRepository.DeleteUser(testUser.Id);

        // Assert
        Assert.True(result);
        Assert.Empty(UserLocalRepository.Users);
    }

    [Fact]
    public void EditUser_ShouldUpdateExistingUser()
    {
        // Arrange
        var UserLocalRepository = new UserLocalRepository();
        var originalUser = new User { Id = 1,
                                      Name = "Original User",
                                      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                      PasswordSalt = hmac.Key };

        UserLocalRepository.Users.Add(originalUser);

        // Act
        var editedUser = new User { Id = originalUser.Id,
                                    Name = "Edited User",
                                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                    PasswordSalt = hmac.Key };

        var result = UserLocalRepository.EditUser(editedUser, originalUser.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Edited User", result.Name);
        Assert.Single(UserLocalRepository.Users);
    }

    [Fact]
    public void FindUserById_ShouldReturnUserByValidId()
    {
        // Arrange
        var UserLocalRepository = new UserLocalRepository();
        var testUser = new User { Id = 1,
                                  Name = "Test User",
                                  PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                  PasswordSalt = hmac.Key };

        UserLocalRepository.Users.Add(testUser);

        // Act
        var foundUser = UserLocalRepository.FindUserById(testUser.Id);

        // Assert
        Assert.NotNull(foundUser);
        Assert.Equal(testUser.Id, foundUser.Id);
        Assert.Equal(testUser.Name, foundUser.Name);
    }

    [Fact]
    public void FindUserById_ShouldThrowExceptionForInvalidId()
    {
        // Arrange
        var UserLocalRepository = new UserLocalRepository();

        // Act & Assert
        Assert.Throws<Exception>(() => UserLocalRepository.FindUserById(1));
    }

    [Fact]
    public void GetUsers_ShouldReturnAllUsers()
    {
        // Arrange
        var UserLocalRepository = new UserLocalRepository();
        var testUser1 = new User { Id = 1, Name = "User 1",
                                   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                   PasswordSalt = hmac.Key };
        var testUser2 = new User { Id = 1, Name = "User 2",
                                   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password")),
                                   PasswordSalt = hmac.Key };
                                   
        UserLocalRepository.Users.Add(testUser1);
        UserLocalRepository.Users.Add(testUser2);

        // Act
        var users = UserLocalRepository.GetUsers();

        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count);
        Assert.Contains(testUser1, users);
        Assert.Contains(testUser2, users);
    }

    [Fact]
    public void FindUserById_ShouldThrowExceptionForNonExistentId()
    {
        // Arrange
        var UserLocalRepository = new UserLocalRepository();

        // Act & Assert
        Assert.Throws<Exception>(() => UserLocalRepository.FindUserById(1));
    }
}