using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Core;

public class TestUserCommands
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly Random _random = new();
    private readonly IUserRepository _userRepository;

    public TestUserCommands()
    {
        var services = SetupRunner.Setup();

        _mediatorHandler = services.GetService<IMediatorHandler>() ??
                           throw new InvalidOperationException("IMediatorHandler not found");
        _userRepository = services.GetService<IUserRepository>() ??
                          throw new InvalidOperationException("IUserRepository not found");
    }


    [Fact]
    public async Task RegisterUser_WithValidCommand_ShouldCreateUser()
    {
        // Arrange
        var command = new RegisterUserCommand(_random.Next(1000, 9999) + "@example.com", "214214142");

        // Act
        await _mediatorHandler.SendCommand(command);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        var user = await _userRepository.ExistsAsync(command.Email);
        user.Should().BeTrue();
    }

    [Fact]
    public async Task RegisterUser_WithInvalidEmail_ShouldNotCreateUser()
    {
        // Arrange
        var command = new RegisterUserCommand("invalid_email", "214214142");

        // Act
        var res = await _mediatorHandler.SendCommand(command);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        var user = await _userRepository.ExistsAsync(command.Email);
        user.Should().BeFalse();
        //should contain validation error "Invalid email"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "Invalid Email");
    }

    [Fact]
    public async Task RegisterUser_WithInvalidPassword_ShouldNotCreateUser()
    {
        // Arrange
        var command = new RegisterUserCommand(_random.Next(1000, 9999) + "@example.com", "123");

        // Act
        var res = await _mediatorHandler.SendCommand(command);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        var user = await _userRepository.ExistsAsync(command.Email);
        user.Should().BeFalse();
        //should contain validation error "Invalid password"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "Password must be at least 6 characters");
    }

    [Fact]
    public async Task RegisterUser_WithExistingEmail_ShouldNotCreateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var command = new RegisterUserCommand(email, "214214142");
        await _mediatorHandler.SendCommand(command);
        //again
        var res = await _mediatorHandler.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "Email already exists");
    }

    [Fact]
    public async Task UpdateUser_WithValidCommand_ShouldUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var newEmail = _random.Next(1000, 9999) + "@example.com";
        var command = new RegisterUserCommand(email, "214214142");
        await _mediatorHandler.SendCommand(command);
        await _userRepository.UnitOfWork.Commit();
        var user = await _userRepository.GetByEmailAsync(email);
        var updateCommand = new UpdateUserCommand(user.Id, newEmail, "new_password");

        // Act
        await _mediatorHandler.SendCommand(updateCommand);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        var updatedUser = await _userRepository.GetByEmailAsync(newEmail);
        updatedUser.Email.Should().Be(newEmail);
    }

    [Fact]
    public async Task UpdateUser_WithInvalidEmail_ShouldNotUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var command = new RegisterUserCommand(email, "214214142");
        await _mediatorHandler.SendCommand(command);
        await _userRepository.UnitOfWork.Commit();
        var user = await _userRepository.GetByEmailAsync(email);
        var updateCommand = new UpdateUserCommand(user.Id, "invalid_email", "new_password");

        // Act
        var res = await _mediatorHandler.SendCommand(updateCommand);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        var updatedUser = await _userRepository.GetByEmailAsync(email);
        updatedUser.Email.Should().Be(email);
        //should contain validation error "Invalid email"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "Invalid Email");
    }

    [Fact]
    public async Task ForgotPassword_WithValidCommand_ShouldUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var user = new User(Guid.NewGuid(), email, "feafaeeafea", "214214142", "214214142", "Customer", 22, 2,
            DateTime.Now, "", DateTime.Now);
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        _userRepository.Flush();
        var forgotPasswordCommand = new ForgotPasswordCommand(email);

        // Act
        await _mediatorHandler.SendCommand(forgotPasswordCommand);

        // Assert
        _userRepository.Flush();
        var status = await _userRepository.HasOtpSecretAsync(email);
        status.Should().BeTrue();
    }

    [Fact]
    public async Task ForgotPassword_WithInvalidEmail_ShouldNotUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var forgotPasswordCommand = new ForgotPasswordCommand(email);

        // Act
        var res = await _mediatorHandler.SendCommand(forgotPasswordCommand);

        // Assert
        //should contain validation error "Invalid email"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "User not found");
    }

    [Fact]
    public async Task UpdateRole_WitValidCommand_ShouldUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var user = new User(Guid.NewGuid(), email, "feafaeeafea", "214214142", "214214142", "Customer", 22, 2,
            DateTime.Now, "", DateTime.Now);
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        _userRepository.Flush();
        var updateRoleCommand = new UpdateUserRoleCommand(user.Id, user.Email, "PlayAdmin");

        // Act
        await _mediatorHandler.SendCommand(updateRoleCommand);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        _userRepository.Flush();
        var updatedUser = await _userRepository.GetByEmailAsync(email);
        updatedUser.Role.Should().Be("PlayAdmin");
    }

    [Fact]
    public async Task UpdateRole_WithInvalidEmail_ShouldNotUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var updateRoleCommand = new UpdateUserRoleCommand(Guid.NewGuid(), email, "PlayAdmin");

        // Act
        var res = await _mediatorHandler.SendCommand(updateRoleCommand);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        //should contain validation error "Invalid email"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "User not found");
    }

    [Fact]
    public async Task UpdateRole_WithInvalidRole_ShouldNotUpdateUser()
    {
        // Arrange
        var email = _random.Next(1000, 9999) + "@example.com";
        var user = new User(Guid.NewGuid(), email, "feafaeeafea", "214214142", "214214142", "Customer", 22, 2,
            DateTime.Now, "", DateTime.Now);
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.Commit();
        _userRepository.Flush();
        var updateRoleCommand = new UpdateUserRoleCommand(user.Id, user.Email, "InvalidRole");

        // Act
        var res = await _mediatorHandler.SendCommand(updateRoleCommand);
        await _userRepository.UnitOfWork.Commit();

        // Assert
        //should contain validation error "Invalid role"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "Invalid Role");
    }
}