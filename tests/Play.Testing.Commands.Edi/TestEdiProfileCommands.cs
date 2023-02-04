using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Edi;

[CollectionDefinition("Test Edi Model Commands", DisableParallelization = true)]
[TestCaseOrderer("Play.Testing.Setup.Runner.PriorityOrderer", "Play.Testing.Setup.Runner")]
public class TestEdiProfileCommands
{
    private readonly IEdiModelRepository _ediModelRepository;
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;
    private readonly IEdiProfileRepository _ediProfileRepository;
    private readonly IMediatorHandler _mediator;

    public TestEdiProfileCommands()
    {
        var services = SetupRunner.Setup();
        _ediProfileRepository = services.GetService<IEdiProfileRepository>() ??
                                throw new ArgumentNullException(nameof(IEdiProfileRepository));
        _ediModelRepository = services.GetService<IEdiModelRepository>() ??
                              throw new ArgumentNullException(nameof(IEdiModelRepository));
        _mediator = services.GetService<IMediatorHandler>() ??
                    throw new ArgumentNullException(nameof(IMediatorHandler));
        _ediOrganizationRepository = services.GetService<IEdiOrganizationRepository>() ??
                                     throw new ArgumentNullException(nameof(IEdiOrganizationRepository));

        _ediProfileRepository.RemoveAll();
    }

    [Fact]
    [TestPriority(21)]
    public async Task RegisterProfile_WithValidCommand_ShouldCreateProfile()
    {
        //create org -> model -> profile
        var orgCommand = new RegisterEdiOrganizationCommand("foo", "foo@example.com");
        var orgRes = await _mediator.SendCommand(orgCommand);
        _ediOrganizationRepository.Flush();
        var org = await _ediOrganizationRepository.GetByEmailAsync("foo@example.com");
        //create model
        var modelCommand = new RegisterEdiModelCommand(org.Id, "test", '`', '~', '%', true);
        var modelRes = await _mediator.SendCommand(modelCommand);
        _ediModelRepository.Flush();
        var model = await _ediModelRepository.GetByTitleAsync("test");
        //create profile
        var profileCommand = new RegisterEdiProfileCommand(Guid.NewGuid(), model.Id, "foo", "foo", true);

        //act
        var res = await _mediator.SendCommand(profileCommand);
        _ediProfileRepository.Flush();
        var profile = await _ediProfileRepository.GetByModelIdAsync(model.Id);

        //assert
        profile.Should().NotBeNull();
        profile.Id.Should().Be(profile.Id);
        profile.Model_Id.Should().Be(model.Id);

        //cleanup
        _ediProfileRepository.Remove(profile);
        await _ediProfileRepository.UnitOfWork.Commit();
        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(22)]
    public async Task RegisterProfile_WithInvalidModelId_ShouldThrowValidationError()
    {
        //attempt to create profile with random model id
        var profileCommand = new RegisterEdiProfileCommand(Guid.NewGuid(), Guid.NewGuid(), "foo", "foo", true);

        //act
        var res = await _mediator.SendCommand(profileCommand);

        //assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "Model not found");
    }

    [Fact]
    [TestPriority(23)]
    public async Task UpdateProfile_WithValidCommand_ShouldUpdateProfile()
    {
        //create org -> model -> profile
        var orgCommand = new RegisterEdiOrganizationCommand("foo", "fafa@example.com");
        var orgRes = await _mediator.SendCommand(orgCommand);
        _ediOrganizationRepository.Flush();
        var org = await _ediOrganizationRepository.GetByEmailAsync("fafa@example.com");
        //create model
        var modelCommand = new RegisterEdiModelCommand(org.Id, "test", '`', '~', '%', true);
        var modelRes = await _mediator.SendCommand(modelCommand);
        _ediModelRepository.Flush();
        var model = await _ediModelRepository.GetByTitleAsync("test");
        //create profile
        var profileCommand = new RegisterEdiProfileCommand(Guid.NewGuid(), model.Id, "foo", "foo", true);
        var profileRes = await _mediator.SendCommand(profileCommand);
        _ediProfileRepository.Flush();
        var profile = await _ediProfileRepository.GetByModelIdAsync(model.Id);
        var updateCommand = new UpdateEdiProfileCommand(profile.Id, Guid.NewGuid(), model.Id, "bar", "bar", true);

        //act
        var res = await _mediator.SendCommand(updateCommand);
        _ediProfileRepository.Flush();

        //assert
        res.IsValid.Should().BeTrue();
        profile = await _ediProfileRepository.GetByModelIdAsync(model.Id);
        profile.Should().NotBeNull();
        profile.Id.Should().Be(profile.Id);
        profile.Model_Id.Should().Be(model.Id);
        profile.Title.Should().Be("bar");

        //cleanup
        _ediProfileRepository.Remove(profile);
        await _ediProfileRepository.UnitOfWork.Commit();
        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(24)]
    public async Task UpdateProfile_WithInvalidModelId_ShouldThrowValidationError()
    {
        //create org -> model -> profile
        var orgCommand = new RegisterEdiOrganizationCommand("foo", "fa@example.com");
        var orgRes = await _mediator.SendCommand(orgCommand);
        _ediOrganizationRepository.Flush();
        var org = await _ediOrganizationRepository.GetByEmailAsync("fa@example.com");
        //create model
        var modelCommand = new RegisterEdiModelCommand(org.Id, "test", '`', '~', '%', true);
        var modelRes = await _mediator.SendCommand(modelCommand);
        _ediModelRepository.Flush();
        var model = await _ediModelRepository.GetByTitleAsync("test");
        //create profile
        var profileCommand = new RegisterEdiProfileCommand(Guid.NewGuid(), model.Id, "foo", "foo", true);
        var profileRes = await _mediator.SendCommand(profileCommand);
        _ediProfileRepository.Flush();
        var profile = await _ediProfileRepository.GetByModelIdAsync(model.Id);
        var updateCommand = new UpdateEdiProfileCommand(profile.Id, Guid.NewGuid(), Guid.NewGuid(), "bar", "bar", true);

        //act
        var res = await _mediator.SendCommand(updateCommand);

        //assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "Model not found");

        //cleanup
        _ediProfileRepository.Remove(profile);
        await _ediProfileRepository.UnitOfWork.Commit();
        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(25)]
    public async Task RemoveProfile_WithValidCommand_ShouldRemoveProfile()
    {
        //create org -> model -> profile
        var orgCommand = new RegisterEdiOrganizationCommand("foo", "fa@example.com");
        var orgRes = await _mediator.SendCommand(orgCommand);

        var org = await _ediOrganizationRepository.GetByEmailAsync("fa@example.com");
        //create model
        var modelCommand = new RegisterEdiModelCommand(org.Id, "test", '`', '~', '%', true);
        var modelRes = await _mediator.SendCommand(modelCommand);

        var model = await _ediModelRepository.GetByTitleAsync("test");
        //create profile
        var profileCommand = new RegisterEdiProfileCommand(Guid.NewGuid(), model.Id, "foo", "foo", true);
        var profileRes = await _mediator.SendCommand(profileCommand);

        var profile = await _ediProfileRepository.GetByModelIdAsync(model.Id);
        var removeCommand = new RemoveEdiProfileCommand(profile.Id);

        //act
        var res = await _mediator.SendCommand(removeCommand);
        _ediProfileRepository.Flush();

        //assert
        res.IsValid.Should().BeTrue();
        profile = await _ediProfileRepository.GetByModelIdAsync(model.Id);
        profile.Should().BeNull();

        //cleanup
        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(26)]
    public async Task RemoveProfile_WithInvalidProfileId_ShouldThrowValidationError()
    {
        var removeCommand = new RemoveEdiProfileCommand(Guid.NewGuid());

        //act
        var res = await _mediator.SendCommand(removeCommand);

        //assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "EdiProfile not found");
    }
}