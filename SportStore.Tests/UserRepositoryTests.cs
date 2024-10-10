using SportStore.API.Entities;
using SportStore.API.Interfaces;
using SportStore.API.Repositories;

namespace SportStore.Tests;

public class UserRepositoryTests
{
    private readonly UserLocalRepository _userRepository;
    public UserRepositoryTests()
    {
        _userRepository = new UserLocalRepository();
    }

    [Fact]
    public void CreateUser_ShouldReturnNewUserWithGeneratedId()
    {
        // Arrange
        var newUser = new User { Name = "Test User" };
        // Act
        var createdUser = _userRepository.CreateUser(newUser);
        // Assert
        Assert.NotNull(createdUser);
   
        Assert.Equal(newUser.Name, createdUser.Name);
    }

    [Fact]
    public void DeleteUser_ShouldReturnTrueAndRemoveUser()
    {
        // Arrange
        var userRepository = new UserLocalRepository();
        var testUser = new User { Id = 2, Name = "Test User" };
        userRepository.Users.Add(testUser);

        // Act
        bool result = userRepository.DeleteUser(testUser.Id);

        // Assert
        Assert.True(result);
        Assert.Empty(userRepository.Users);
    }

    [Fact]
    public void EditUser_ShouldUpdateExistingUser()
    {
        // Arrange
        var userRepository = new UserLocalRepository();
        var originalUser = new User { Id = 1, Name = "Original User" };
        userRepository.Users.Add(originalUser);

        // Act
        var editedUser = new User { Id = originalUser.Id, Name = "Edited User" };
        var result = userRepository.EditUser(editedUser, originalUser.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Edited User", result.Name);
        Assert.Single(userRepository.Users);
    }

    [Fact]
    public void FindUserById_ShouldReturnUserByValidId()
    {
        // Arrange
        var userRepository = new UserLocalRepository();
        var testUser = new User { Id = 2, Name = "Test User" };
        userRepository.Users.Add(testUser);

        // Act
        var foundUser = userRepository.FindUserById(testUser.Id);

        // Assert
        Assert.NotNull(foundUser);
        Assert.Equal(testUser.Id, foundUser.Id);
        Assert.Equal(testUser.Name, foundUser.Name);
    }

    [Fact]
    public void FindUserById_ShouldThrowExceptionForInvalidId()
    {
        // Arrange
        var userRepository = new UserLocalRepository();

        // Act & Assert
        Assert.Throws<Exception>(() => userRepository.FindUserById(1));
    }

    [Fact]
    public void GetUsers_ShouldReturnAllUsers()
    {
        // Arrange
        var userRepository = new UserLocalRepository();
        var testUser1 = new User { Id = 1, Name = "User 1" };
        var testUser2 = new User { Id = 1, Name = "User 2" };
        userRepository.Users.Add(testUser1);
        userRepository.Users.Add(testUser2);

        // Act
        var users = userRepository.GetUsers();

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
        var userRepository = new UserLocalRepository();

        // Act & Assert
        Assert.Throws<Exception>(() => userRepository.FindUserById(1));
    }
}