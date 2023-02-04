using System.Text;
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
public class TestEdiDocumentsCommands
{
    private readonly IEdiDocumentRepository _ediDocumentRepository;
    private readonly IMediatorHandler _mediator;

    public TestEdiDocumentsCommands()
    {
        var services = SetupRunner.Setup();
        _ediDocumentRepository = services.GetService<IEdiDocumentRepository>() ??
                                 throw new ArgumentNullException(nameof(IEdiDocumentRepository));
        _mediator = services.GetService<IMediatorHandler>() ??
                    throw new ArgumentNullException(nameof(IMediatorHandler));
    }

    [Fact]
    [TestPriority(1)]
    public async Task RegisterDocument_WithValidCommand_ShouldCreateDocument()
    {
        var customerId = Guid.NewGuid();
        var ediDocCommand = new RegisterEdiDocumentCommand(Guid.NewGuid(), customerId, "Testing Documnet", "EdiPayload",
            "DocumentPayload", "Hedentid", true, false);
        //Act
        var res = await _mediator.SendCommand(ediDocCommand);
        _ediDocumentRepository.Flush();

        //Assert
        res.IsValid.Should().BeTrue();
        var ediDocument = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);
        ediDocument.Should().NotBeNull();

        //Cleanup
        _ediDocumentRepository.Remove(ediDocument.FirstOrDefault());
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(2)]
    public async Task RegisterDocument_WithInvalidCustomerID_ShouldNotCreateDocument()
    {
        var ediDocCommand = new RegisterEdiDocumentCommand(Guid.NewGuid(), Guid.Empty, "Testing Documnet", "EdiPayload",
            "DocumentPayload", "Hedentid", true, false);
        //Act
        var res = await _mediator.SendCommand(ediDocCommand);
        _ediDocumentRepository.Flush();

        //Assert
        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain(x => x.ErrorMessage == "CustomerId is required");
    }

    [Fact]
    [TestPriority(3)]
    public async Task ReceivedDocument_WithValidCommand_ShouldReceiveDocument()
    {
        var title = Encoding.UTF8.GetBytes("Testin");
        var docPayload = Encoding.UTF8.GetBytes("EdiPayload");
        var customerId = Guid.NewGuid();
        var ediReceivedCommand = new ReceivedEdiDocumentCommand(Guid.NewGuid(), customerId,
            Convert.ToBase64String(title), "EdiPayload", Convert.ToBase64String(docPayload), "Hedentid", true, false);
        //Act
        var res = await _mediator.SendCommand(ediReceivedCommand);
        _ediDocumentRepository.Flush();

        //Assert
        res.IsValid.Should().BeTrue();
        var ediDocument = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);
        ediDocument.FirstOrDefault().Customer_Id.Should().Be(customerId);
        //Cleanup
        _ediDocumentRepository.Remove(ediDocument.FirstOrDefault());
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(4)]
    public async Task UpdateDocument_WithValidCommand_ShouldUpdateDocument()
    {
        //Create Document
        var customerId = Guid.NewGuid();
        var ediDocCommand = new RegisterEdiDocumentCommand(Guid.NewGuid(), customerId, "Testing Document", "EdiPayload",
            "DocumentPayload", "Hedentid", true, false);
        var res = await _mediator.SendCommand(ediDocCommand);
        _ediDocumentRepository.Flush();

        //Act
        var ediDocument = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);
        var ediUpdateCommand = new UpdateEdiDocumentCommand(ediDocument.FirstOrDefault().Id, customerId,
            "Testing Document Updated", "EdiPayload", "DocumentPayload", "Hedentid", true, false);
        var resUpdate = await _mediator.SendCommand(ediUpdateCommand);
        _ediDocumentRepository.Flush();

        //Assert
        resUpdate.IsValid.Should().BeTrue();
        var ediDocumentUpdated = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);
        ediDocumentUpdated.FirstOrDefault().Title.Should().Be("Testing Document Updated");

        //Cleanup
        _ediDocumentRepository.Remove(ediDocument.FirstOrDefault());
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    [TestPriority(5)]
    public async Task UpdateDocument_WithInvalidId_ShouldFail()
    {
        //Create Document
        var customerId = Guid.NewGuid();
        var ediDocCommand = new RegisterEdiDocumentCommand(Guid.NewGuid(), customerId, "Testing Document", "EdiPayload",
            "DocumentPayload", "Hedentid", true, false);
        var res = await _mediator.SendCommand(ediDocCommand);
        _ediDocumentRepository.Flush();

        //Act
        var ediDocument = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);
        var ediUpdateCommand = new UpdateEdiDocumentCommand(Guid.NewGuid(), customerId, "Testing Document Updated",
            "EdiPayload", "DocumentPayload", "Hedentid", true, false);
        var resUpdate = await _mediator.SendCommand(ediUpdateCommand);

        //Assert
        resUpdate.IsValid.Should().BeFalse();
        resUpdate.Errors.Should().ContainSingle(e => e.ErrorMessage == "Document with hedentid : Hedentid not found");
    }

    [Fact]
    [TestPriority(6)]
    public async Task RemoveDocument_WithValidCommand_ShouldRemoveDocument()
    {
        //Create Documenet to test remove command
        var customerId = Guid.NewGuid();
        var ediDocCommand = new RegisterEdiDocumentCommand(Guid.NewGuid(), customerId, "Testing Documnet", "EdiPayload",
            "DocumentPayload", "Hedentid", true, false);
        var res = await _mediator.SendCommand(ediDocCommand);
        _ediDocumentRepository.Flush();
        var ediDocument = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);

        //Act
        var removeCommand = new RemoveEdiDocumentCommand(ediDocument.FirstOrDefault().Id);
        var removeRes = await _mediator.SendCommand(removeCommand);
        _ediDocumentRepository.Flush();

        //Assert
        removeRes.IsValid.Should().BeTrue();
        var removedDocument = await _ediDocumentRepository.GetByCustomerIdAsync(customerId);
        removedDocument.Should().BeEmpty();
    }

    [Fact]
    [TestPriority(7)]
    public async Task RemoveDocument_WithInvalidCommand_ShouldFail()
    {
        // Arrange
        var command = new RemoveEdiDocumentCommand(Guid.NewGuid());

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.IsValid.Should().BeFalse();
    }
}