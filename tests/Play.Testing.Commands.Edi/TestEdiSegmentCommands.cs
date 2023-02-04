using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Edi;

[CollectionDefinition("Test Edi Model Commands", DisableParallelization = true)]
[TestCaseOrderer("Play.Testing.Setup.Runner.PriorityOrderer", "Play.Testing.Setup.Runner")]
public class TestEdiSegmentCommands
{
    private readonly IEdiModelRepository _ediModelRepository;
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;
    private readonly IEdiSegmentRepository _ediSegmentRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public TestEdiSegmentCommands()
    {
        var services = SetupRunner.Setup();
        _mediatorHandler = services.GetService<IMediatorHandler>() ??
                           throw new ArgumentNullException(nameof(IMediatorHandler));
        _ediModelRepository = services.GetService<IEdiModelRepository>() ??
                              throw new ArgumentNullException(nameof(IEdiModelRepository));
        _ediOrganizationRepository = services.GetService<IEdiOrganizationRepository>() ??
                                     throw new ArgumentNullException(nameof(IEdiOrganizationRepository));
        _ediSegmentRepository = services.GetService<IEdiSegmentRepository>() ??
                                throw new ArgumentNullException(nameof(IEdiSegmentRepository));
        _ediSegmentRepository.RemoveAll();
    }

    [Fact]
    [TestPriority(27)]
    public async Task RegisterSegment_WithValidCommand_ShouldRegisterSegment()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo Temp", "fo2121oo@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetByEmailAsync("fo2121oo@example.com");
        var orgId = org.Id;
        var command = new RegisterEdiModelCommand(orgId, "Test M2odel", '!', '@', '%', true);
        await _mediatorHandler.SendCommand(command);
        //get model id to make segment
        var model = await _ediModelRepository.GetByTitleAsync("Test M2odel");
        var modelId = model.Id;
        var segmentCommand = new RegisterEdiSegmentCommand(Guid.NewGuid(), modelId, "Test Segment", "Test Description");

        // Act
        var resSegCommand = await _mediatorHandler.SendCommand(segmentCommand);

        // Assert
        _ediSegmentRepository.Flush();
        var segment = await _ediSegmentRepository.GetByModelIdAsync(modelId);
        segment.Should().NotBeNull();
        segment.Should().BeOfType<EdiSegment>();
        segment.Model_Id.Should().Be(segmentCommand.Model_Id);
        segment.Title.Should().Be(segmentCommand.Title);
        segment.Description.Should().Be(segmentCommand.Description);


        //Cleanup
        _ediSegmentRepository.Delete(segment);
        await _ediSegmentRepository.UnitOfWork.Commit();

        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(28)]
    public async Task RegisterSegment_WithNoExistingModel_ShouldNotRegisterSegment()
    {
        // Arrange
        var command = new RegisterEdiSegmentCommand(Guid.NewGuid(), Guid.NewGuid(), "Test Segment", "Test Description");

        // Act
        var resSegCommand = await _mediatorHandler.SendCommand(command);

        // Assert
        resSegCommand.IsValid.Should().BeFalse();
        resSegCommand.Errors.Should().ContainSingle(error => error.ErrorMessage == "Model not found");
    }

    [Fact]
    [TestPriority(29)]
    public async Task RemoveSegment_WithValidCommand_ShouldRemoveSegment()
    {
        // Arrange
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo Temp", "fooo2@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetByEmailAsync("fooo2@example.com");
        var orgId = org.Id;
        var command = new RegisterEdiModelCommand(orgId, "Test 32Model", '!', '@', '%', true);
        await _mediatorHandler.SendCommand(command);
        //get model id to make segment
        var model = await _ediModelRepository.GetByTitleAsync("Test 32Model");
        var modelId = model.Id;
        var segmentCommand = new RegisterEdiSegmentCommand(Guid.NewGuid(), modelId, "Test Segment", "Test Description");
        await _mediatorHandler.SendCommand(segmentCommand);

        //get segment id to remove
        var segments = await _ediSegmentRepository.GetAllAsync();
        var segmentId = segments.FirstOrDefault()?.Id ?? Guid.Empty;

        // Act
        var delSegCommand = new RemoveEdiSegmentCommand(segmentId);
        var resDelSegCommand = await _mediatorHandler.SendCommand(delSegCommand);

        // Assert
        _ediSegmentRepository.Flush();
        var ediSegments = await _ediSegmentRepository.GetAllAsync();
        resDelSegCommand.IsValid.Should().BeTrue();
        ediSegments.Should().HaveCount(0);

        //Cleanup

        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();


        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(30)]
    public async Task RemoveSegment_WithNoExistingSegment_ShouldFail()
    {
        // Arrange
        var command = new RemoveEdiSegmentCommand(Guid.NewGuid());

        // Act
        var resSegCommand = await _mediatorHandler.SendCommand(command);

        // Assert
        resSegCommand.IsValid.Should().BeFalse();
        resSegCommand.Errors.Should().ContainSingle(error => error.ErrorMessage == "Segment not found");
    }

    [Fact]
    [TestPriority(31)]
    public async Task UpdateSegment_WithValidCommand_ShouldUpdateSegment()
    {
        // create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo Temp", "fo5151515151o@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetByEmailAsync("fo5151515151o@example.com");
        var orgId = org.Id;
        var command = new RegisterEdiModelCommand(orgId, "Test M21541354151odel", '!', '@', '%', true);
        await _mediatorHandler.SendCommand(command);
        //get model id to make segment
        var model = await _ediModelRepository.GetByTitleAsync("Test M21541354151odel");
        var modelId = model.Id;
        var segmentCommand = new RegisterEdiSegmentCommand(Guid.NewGuid(), modelId, "Test Segment", "Test Description");
        await _mediatorHandler.SendCommand(segmentCommand);

        //get segment id to update
        var segments = await _ediSegmentRepository.GetAllAsync();
        var segmentId = segments.FirstOrDefault()?.Id ?? Guid.Empty;

        // Act
        var updateSegCommand =
            new UpdateEdiSegmentCommand(segmentId, modelId, "Test Segment Updated", "Test Description Updated");
        var resUpdateSegCommand = await _mediatorHandler.SendCommand(updateSegCommand);

        // Assert
        _ediSegmentRepository.Flush();
        var ediSegments = await _ediSegmentRepository.GetByModelIdAsync(modelId);
        resUpdateSegCommand.IsValid.Should().BeTrue();
        ediSegments.Should().NotBeNull();
        ediSegments.Should().BeOfType<EdiSegment>();

        //Cleanup
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        _ediModelRepository.Remove(model);
        await _ediModelRepository.UnitOfWork.Commit();

        _ediSegmentRepository.Delete(ediSegments);
        await _ediSegmentRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(32)]
    public async Task UpdateSegment_WithInvalidModel_ShouldFail()
    {
        //create org first
        var orgCommand = new RegisterEdiOrganizationCommand("Foo Temp", "foo@example.com");
        await _mediatorHandler.SendCommand(orgCommand);
        //get org id
        var org = await _ediOrganizationRepository.GetAllAsync();
        var orgId = org.FirstOrDefault()?.Id ?? Guid.Empty;
        var command = new RegisterEdiModelCommand(orgId, "Test Model", '!', '@', '%', true);
        await _mediatorHandler.SendCommand(command);
        //get model id to make segment
        var model = await _ediModelRepository.GetAllAsync();
        var modelId = model.FirstOrDefault()?.Id ?? Guid.Empty;
        var segmentCommand = new RegisterEdiSegmentCommand(Guid.NewGuid(), modelId, "Test Segment", "Test Description");
        await _mediatorHandler.SendCommand(segmentCommand);

        //get segment id to update
        var segments = await _ediSegmentRepository.GetAllAsync();
        var segmentId = segments.FirstOrDefault()?.Id ?? Guid.Empty;

        // Act
        _ediSegmentRepository.Flush();
        var updateSegCommand =
            new UpdateEdiSegmentCommand(segmentId, Guid.NewGuid(), "Test Segment Updated", "Test Description Updated");
        var resUpdateSegCommand = await _mediatorHandler.SendCommand(updateSegCommand);

        // Assert
        resUpdateSegCommand.IsValid.Should().BeFalse();
        resUpdateSegCommand.Errors.Should().ContainSingle(error => error.ErrorMessage == "Model not found");

        //Cleanup
        _ediOrganizationRepository.Remove(org.First());
        await _ediOrganizationRepository.UnitOfWork.Commit();

        _ediModelRepository.Remove(model.First());
        await _ediModelRepository.UnitOfWork.Commit();

        _ediSegmentRepository.Delete(segments.First());
        await _ediSegmentRepository.UnitOfWork.Commit();
    }
}