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
public class TestEdiOrganizationCommands
{
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;
    private readonly IMediatorHandler _mediator;

    public TestEdiOrganizationCommands()
    {
        var services = SetupRunner.Setup();
        _mediator = services.GetService<IMediatorHandler>() ??
                    throw new ArgumentNullException(nameof(IMediatorHandler));
        _ediOrganizationRepository = services.GetService<IEdiOrganizationRepository>() ??
                                     throw new ArgumentNullException(nameof(IEdiOrganizationRepository));
    }

    [Fact]
    [TestPriority(15)]
    public async Task RegisterOrganization_WithValidCommand_ShouldCreateOrganization()
    {
        // Arrange
        var command = new RegisterEdiOrganizationCommand("foo", "ba@example.com");

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeTrue();
        _ediOrganizationRepository.Flush();
        var org = await _ediOrganizationRepository.GetByEmailAsync("ba@example.com");
        org.Should().NotBeNull();
        org.Name.Should().Be("foo");

        // Cleanup
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(16)]
    public async Task RegisterOrganization_WithExistingEmail_ShouldFail()
    {
        // Arrange
        var command = new RegisterEdiOrganizationCommand("foo", "bar@example.com");
        await _mediator.SendCommand(command);
        var command2 = new RegisterEdiOrganizationCommand("foo2", "bar@example.com");

        // Act
        var res = await _mediator.SendCommand(command2);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "Email exists on another organization");

        // Cleanup
        _ediOrganizationRepository.Flush();
        var org = await _ediOrganizationRepository.GetByEmailAsync("bar@example.com");
        _ediOrganizationRepository.Remove(org);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(17)]
    public async Task UpdateOrganization_WithValidCommand_ShouldUpdateOrganization()
    {
        // Arrange
        var command = new RegisterEdiOrganizationCommand("foo", "eafea@example.com");
        await _mediator.SendCommand(command);
        var org = await _ediOrganizationRepository.GetByEmailAsync("eafea@example.com");
        var command2 = new UpdateEdiOrganizationCommand(org.Id, "foo2", "1337@example.com");

        // Act
        var res = await _mediator.SendCommand(command2);

        // Assert
        res.IsValid.Should().BeTrue();
        _ediOrganizationRepository.Flush();
        var org2 = await _ediOrganizationRepository.GetByEmailAsync("1337@example.com");
        org2.Should().NotBeNull();
        org2.Name.Should().Be("foo2");

        // Cleanup
        _ediOrganizationRepository.Remove(org2);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(18)]
    public async Task UpdateOrganization_WithInvalidCommand_ShouldFail()
    {
        // Arrange
        var command = new UpdateEdiOrganizationCommand(Guid.NewGuid(), "foo2", "31251@example.com");

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "Organization not found");
    }

    [Fact]
    [TestPriority(19)]
    public async Task RemoveOrganization_WithValidCommand_ShouldRemoveOrganization()
    {
        // Arrange
        var command = new RegisterEdiOrganizationCommand("foo", "1337@example.com");
        await _mediator.SendCommand(command);
        var org = await _ediOrganizationRepository.GetByEmailAsync("1337@example.com");
        var command2 = new RemoveEdiOrganizationCommand(org.Id);

        // Act
        var res = await _mediator.SendCommand(command2);

        // Assert
        res.IsValid.Should().BeTrue();
        _ediOrganizationRepository.Flush();
        var org2 = await _ediOrganizationRepository.GetByEmailAsync("1337@example.com");
        org2.Should().BeNull();
    }

    [Fact]
    [TestPriority(20)]
    public async Task RemoveOrganization_WithInvalidCommand_ShouldFail()
    {
        // Arrange
        var command = new RemoveEdiOrganizationCommand(Guid.NewGuid());

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().ContainSingle(e => e.ErrorMessage == "Organization not found");
    }
}