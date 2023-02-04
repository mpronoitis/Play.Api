using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Core;

public class TestUserRepository
{
    private static readonly Random _random = new();
    private readonly IUserRepository _userRepository;

    public TestUserRepository()
    {
        var services = SetupRunner.Setup();

        _userRepository = services.GetRequiredService<IUserRepository>();
    }

    public static User CreateFooUser(Guid id = default, string? email = null, string? passwordHash = null,
        string? salt = null, string? username = null, string? role = null,
        int loginAttempts = 0, int failedLoginAttempts = 0, DateTime? lastLogin = null,
        string? otpSecret = null)
    {
        // Use the _random field to generate random values for the email, passwordHash, salt, username, and otpSecret
        // properties if they are not provided as arguments.
        email ??= $"{_random.Next(100000, 999999)}@test.com";
        passwordHash ??= $"{_random.Next(100000, 999999)}";
        salt ??= $"{_random.Next(100000, 999999)}";
        username ??= $"{_random.Next(100000, 999999)}";
        role ??= $"{_random.Next(100000, 999999)}";
        otpSecret ??= $"{_random.Next(100000, 999999)}";

        return new User
        {
            Id = id != default ? id : Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash,
            Salt = salt,
            Username = username,
            Role = role,
            LoginAttempts = loginAttempts,
            FailedLoginAttempts = failedLoginAttempts,
            LastLogin = lastLogin,
            OtpSecret = otpSecret,
            CreatedAt = DateTime.UtcNow
        };
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public async Task GetAllUsers_DatabaseContainsMultipleUsers_ReturnsAllUsers(int num)
    {
        //Arrange
        //create 5 users
        var users = new List<User>();
        for (var i = 0; i < num; i++) users.Add(CreateFooUser());

        foreach (var user in users) _userRepository.Add(user);

        //commit units of work
        await _userRepository.UnitOfWork.Commit();
        //Act
        var userConnection = await _userRepository.GetAllAsync();
        //Assert
        userConnection.Should().NotBeNull()
            .And.BeOfType<List<User>>()
            .And.HaveCount(users.Count);

        //cleanup
        foreach (var user in users) _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetUserById_DatabaseContainsUser_ReturnsUser()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        var userConnection = await _userRepository.GetByIdAsync(user.Id);
        //Assert
        userConnection.Should().NotBeNull()
            .And.BeOfType<User>()
            .And.Subject.As<User>().Id.Should().Be(user.Id);
        //cleanup
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetUserById_DatabaseDoesNotContainUser_ReturnsNull()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        var userConnection = await _userRepository.GetByIdAsync(Guid.NewGuid());
        //Assert
        userConnection.Should().BeNull();
        //cleanup
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetUserByEmail_DatabaseContainsUser_ReturnsUser()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        var userConnection = await _userRepository.GetByEmailAsync(user.Email);
        //Assert
        userConnection.Should().NotBeNull()
            .And.BeOfType<User>()
            .And.Subject.As<User>().Email.Should().Be(user.Email);
        //cleanup
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetUserByEmail_DatabaseDoesNotContainUser_ReturnsNull()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        var userConnection = await _userRepository.GetByEmailAsync($"{_random.Next(100000, 999999)}@test.com");
        //Assert
        userConnection.Should().BeNull();
        //cleanup
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task UpdateUser_DatabaseContainsUser_ReturnsUser()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        user.Email = $"{_random.Next(100000, 999999)}@test.com";
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.Commit();
        var userConnection = await _userRepository.GetByIdAsync(user.Id);
        //Assert
        userConnection.Should().NotBeNull()
            .And.BeOfType<User>()
            .And.Subject.As<User>().Email.Should().Be(user.Email);
        //cleanup
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task UpdateUser_DatabaseDoesNotContainUser_ReturnsNull()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        user.Email = $"{_random.Next(100000, 999999)}@test.com";
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.Commit();
        var userConnection = await _userRepository.GetByIdAsync(Guid.NewGuid());
        //Assert
        userConnection.Should().BeNull();
        //cleanup
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task DeleteUser_DatabaseContainsUser_ReturnsUser()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
        var userConnection = await _userRepository.GetByIdAsync(user.Id);
        //Assert
        userConnection.Should().BeNull();
    }

    [Fact]
    public async Task DeleteUser_DatabaseDoesNotContainUser_ReturnsNull()
    {
        //Arrange
        var user = CreateFooUser();
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        //Act
        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
        var userConnection = await _userRepository.GetByIdAsync(Guid.NewGuid());
        //Assert
        userConnection.Should().BeNull();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    public async Task GetTotalCount_ReturnsCount(int count)
    {
        //Arrange
        var users = new List<User>();
        for (var i = 0; i < count; i++)
        {
            var user = CreateFooUser();
            users.Add(user);
            _userRepository.Add(user);
        }

        await _userRepository.UnitOfWork.Commit();
        //Act
        var userConnection = await _userRepository.GetTotalCount();
        //Assert
        userConnection.Should().Be(count);
        //cleanup
        foreach (var user in users) _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetTotalCount_ReturnsZero()
    {
        //Arrange
        //Act
        var userConnection = await _userRepository.GetTotalCount();
        //Assert
        userConnection.Should().Be(0);
    }
}