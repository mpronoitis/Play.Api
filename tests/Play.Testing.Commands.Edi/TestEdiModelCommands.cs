using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Edi;

//run one by one
[TestCaseOrderer("Play.Testing.Setup.Runner.PriorityOrderer", "Play.Testing.Setup.Runner")]
[CollectionDefinition("Test Edi Model Commands", DisableParallelization = true)]
public class TestEdiModelCommands
{
    private readonly IEdiModelRepository _ediModelRepository;
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public TestEdiModelCommands()
    {
        var services = SetupRunner.Setup();
        _mediatorHandler = services.GetService<IMediatorHandler>() ??
                           throw new ArgumentNullException(nameof(IMediatorHandler));
        _ediModelRepository = services.GetService<IEdiModelRepository>() ??
                              throw new ArgumentNullException(nameof(IEdiModelRepository));
        _ediOrganizationRepository = services.GetService<IEdiOrganizationRepository>() ??
                                     throw new ArgumentNullException(nameof(IEdiOrganizationRepository));
        _ediModelRepository.RemoveAll();
    }

    [Fact]
    [TestPriority(8)]
    public async Task RegisterModel_WithValidCommand_ShouldCreateModel()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo", "fo21421412421o@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetByEmailAsync("fo21421412421o@example.com");
        var orgId = org.Id;
        var command = new RegisterEdiModelCommand(orgId, "Test 3213123131Model", '!', '@', '%', true);

        // Act
        var res = await _mediatorHandler.SendCommand(command);

        // Assert (fluent assertions)
        _ediModelRepository.Flush();
        var model = await _ediModelRepository.GetByTitleAsync("Test 3213123131Model");
        res.IsValid.Should().BeTrue();
        model.Should().NotBeNull();
        model?.Title.Should().Be("Test 3213123131Model");

        //cleanup
        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(9)]
    public async Task RegisterModel_WithExistingTitle_ShouldNotCreateModel()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo", "foo1515121511@email.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetByEmailAsync("foo1515121511@email.com");
        var orgId = org.Id;
        var command = new RegisterEdiModelCommand(orgId, "Test 25152151Model", '!', '@', '%', true);
        var res = await _mediatorHandler.SendCommand(command);
        var command2 = new RegisterEdiModelCommand(orgId, "Test 25152151Model", '!', '@', '%', true);

        // Act
        var res2 = await _mediatorHandler.SendCommand(command2);

        // Assert that validation result contains error "There is already a model with this Title"
        res2.IsValid.Should().BeFalse();
        res2.Errors.Should().ContainSingle(e => e.ErrorMessage == "There is already a model with this Title");

        //cleanup
        _ediModelRepository.Flush();
        //get model
        var model = await _ediModelRepository.GetByTitleAsync("Test 25152151Model");
        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(10)]
    public async Task RegisterModel_WithNonExistingOrg_ShouldNotCreateModel()
    {
        // Arrange
        var command = new RegisterEdiModelCommand(Guid.NewGuid(), "Test Model", '!', '@', '%', true);

        // Act
        var res = await _mediatorHandler.SendCommand(command);

        // Assert that validation result contains error "There is no organization with this Id"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "The organization does not exist");
    }

    [Fact]
    [TestPriority(11)]
    public async Task UpdateModel_WithValidCommand_ShouldUpdateModel()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo", "foo521521521542131@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetByEmailAsync("foo521521521542131@example.com");
        var orgId = org.Id;
        var command = new RegisterEdiModelCommand(orgId, "Test M12421421421421odel", '!', '@', '%', true);
        var res = await _mediatorHandler.SendCommand(command);
        //get model id
        var model = await _ediModelRepository.GetByTitleAsync("Test M12421421421421odel");
        var command2 = new UpdateEdiModelCommand(model.Id, model.Org_Id, "Test Model 3213212", '!', '@', '%', true);

        // Act
        var res2 = await _mediatorHandler.SendCommand(command2);

        // Assert
        res2.IsValid.Should().BeTrue();
        _ediModelRepository.Flush();
        var models2 = await _ediModelRepository.GetByTitleAsync("Test Model 3213212");
        models2.Should().NotBeNull();

        //cleanup
        _ediModelRepository.Remove(models2);
        await _ediModelRepository.UnitOfWork.Commit();
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(12)]
    public async Task UpdateModel_WithNonExistingModel_ShouldNotUpdateModel()
    {
        // Arrange
        var command = new UpdateEdiModelCommand(Guid.NewGuid(), Guid.NewGuid(), "Test Model", '!', '@', '%', true);

        // Act
        var res = await _mediatorHandler.SendCommand(command);

        // Assert that validation result contains error "There is no model with this Id"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "The model does not exist");
    }

    [Fact]
    [TestPriority(13)]
    public async Task UpdateModel_WithNonExistingOrg_ShouldNotUpdateModel()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("foo", "foo@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetAllAsync();
        var orgId = org.FirstOrDefault()?.Id ?? Guid.Empty;
        var command = new RegisterEdiModelCommand(orgId, "Test Model", '!', '@', '%', true);
        var res = await _mediatorHandler.SendCommand(command);
        //get model id
        var models = await _ediModelRepository.GetAllAsync();
        var ediModels = models as EdiModel[] ?? models.ToArray();
        var model = ediModels.FirstOrDefault() ?? throw new ArgumentNullException(nameof(ediModels));
        var command2 = new UpdateEdiModelCommand(model.Id, Guid.NewGuid(), "Test Model 2", '!', '@', '%', true);

        // Act
        var res2 = await _mediatorHandler.SendCommand(command2);

        // Assert that validation result contains error "There is no organization with this Id"
        res2.IsValid.Should().BeFalse();
        res2.Errors.Should().ContainSingle(e => e.ErrorMessage == "The organization does not exist");
    }

    [Fact]
    [TestPriority(14)]
    public async Task RemoveModel_WithValidCommand_ShouldRemoveModel()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo", "em@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetAllAsync();
        var orgId = org.FirstOrDefault()?.Id ?? Guid.Empty;
        var command = new RegisterEdiModelCommand(orgId, "Test Model", '!', '@', '%', true);
        var res = await _mediatorHandler.SendCommand(command);
        //get model id
        var models = await _ediModelRepository.GetAllAsync();
        var ediModels = models as EdiModel[] ?? models.ToArray();
        var model = ediModels.FirstOrDefault() ?? throw new ArgumentNullException(nameof(ediModels));
        var command2 = new RemoveEdiModelCommand(model.Id);

        // Act
        var res2 = await _mediatorHandler.SendCommand(command2);

        // Assert
        res2.IsValid.Should().BeTrue();
        _ediModelRepository.Flush();
        var models2 = await _ediModelRepository.GetAllAsync();
        var ediModels2 = models2 as EdiModel[] ?? models2.ToArray();
        ediModels2.Should().BeEmpty();
    }


    [Fact]
    [TestPriority(15)]
    public async Task RemoveModel_WithNonExistingModel_ShouldNotRemoveModel()
    {
        // Arrange
        var command = new RemoveEdiModelCommand(Guid.NewGuid());

        // Act
        var res = await _mediatorHandler.SendCommand(command);

        // Assert that validation result contains error "There is no model with this Id"
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "The model does not exist");
    }
}