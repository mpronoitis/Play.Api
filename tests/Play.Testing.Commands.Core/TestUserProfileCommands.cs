using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Interfaces;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Core;

public class TestUserProfileCommands
{
    private readonly IMediatorHandler _mediator;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUserRepository _userRepository;

    public TestUserProfileCommands()
    {
        var services = SetupRunner.Setup();
        _userRepository = services.GetService<IUserRepository>() ??
                          throw new ArgumentNullException(nameof(_userRepository));
        _userProfileRepository = services.GetService<IUserProfileRepository>() ??
                                 throw new ArgumentNullException(nameof(_userProfileRepository));
        _mediator = services.GetService<IMediatorHandler>() ?? throw new ArgumentNullException(nameof(_mediator));
    }

    [Fact]
    public async Task RegisterUserProfile_WithValidCommand_ShouldCreateUserProfile()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");

        // Act
        await _mediator.SendCommand(command);

        // Assert
        var userProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        userProfile.Should().NotBeNull();
        userProfile.User_Id.Should().Be(command.User_Id);
    }

    [Fact]
    public async Task RegisterUserProfile_WithInvalidFirstname_ShouldThrowException()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "a", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "FirstName must have between 2 and 100 characters");
    }

    [Fact]
    public async Task RegisterUserProfile_WithInvalidLastname_ShouldThrowException()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "a", DateTime.Now,
            "CompanyName", "dark", "801061785");

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "LastName must have between 2 and 100 characters");
    }

    [Fact]
    public async Task RegisterUserProfile_WithInvalidCompanyName_ShouldThrowException()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "a", "dark", "801061785");

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "CompanyName must have between 2 and 100 characters");
    }

    [Fact]
    public async void UpdateUserProfile_WithValidCommand_ShouldUpdateUserProfile()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");
        await _mediator.SendCommand(command);

        var userProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        var updateCommand = new UpdateUserProfileCommand(userProfile.Id, userProfile.User_Id, "Firstname2", "Lastname2",
            DateTime.Now,
            "CompanyName2", "en", "dark2", "8010617852");

        // Act
        await _mediator.SendCommand(updateCommand);

        // Assert
        var updatedUserProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        updatedUserProfile.Should().NotBeNull();
        updatedUserProfile.User_Id.Should().Be(command.User_Id);
        updatedUserProfile.FirstName.Should().Be(updateCommand.FirstName);
        updatedUserProfile.LastName.Should().Be(updateCommand.LastName);
    }

    [Fact]
    public async void UpdateUserProfile_WithInvalidFirstname_ShouldThrowException()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");
        await _mediator.SendCommand(command);

        var userProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        var updateCommand = new UpdateUserProfileCommand(userProfile.Id, userProfile.User_Id, "a", "Lastname2",
            DateTime.Now,
            "CompanyName2", "en", "dark2", "8010617852");

        // Act
        var res = await _mediator.SendCommand(updateCommand);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "FirstName must have between 2 and 100 characters");
    }

    [Fact]
    public async void UpdateUserProfile_WithInvalidLastname_ShouldThrowException()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");
        await _mediator.SendCommand(command);

        var userProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        var updateCommand = new UpdateUserProfileCommand(userProfile.Id, userProfile.User_Id, "Firstname2", "a",
            DateTime.Now,
            "CompanyName2", "en", "dark2", "8010617852");

        // Act
        var res = await _mediator.SendCommand(updateCommand);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "LastName must have between 2 and 100 characters");
    }

    [Fact]
    public async void RemoveUserProfile_WithValidCommand_ShouldRemoveUserProfile()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");
        await _mediator.SendCommand(command);

        var userProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        var removeCommand = new RemoveUserProfileCommand(userProfile.Id);

        // Act
        await _mediator.SendCommand(removeCommand);

        // Assert
        var removedUserProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        removedUserProfile.Should().BeNull();
    }

    [Fact]
    public async void RemoveUserProfile_WithInvalidId_ShouldFail()
    {
        // Arrange
        var command = new RegisterUserProfileCommand(Guid.NewGuid(), "Firstname", "Lastname", DateTime.Now,
            "CompanyName", "dark", "801061785");
        await _mediator.SendCommand(command);

        var userProfile = await _userProfileRepository.GetByUserId(command.User_Id);

        var removeCommand = new RemoveUserProfileCommand(Guid.NewGuid());

        // Act
        var res = await _mediator.SendCommand(removeCommand);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "The userProfile does not exist");
    }
}