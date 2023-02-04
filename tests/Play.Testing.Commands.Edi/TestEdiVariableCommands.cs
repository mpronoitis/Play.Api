using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Edi;

public class TestEdiVariableCommands
{
    private readonly IEdiVariableRepository _ediVariableRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public TestEdiVariableCommands()
    {
        var services = SetupRunner.Setup();
        _ediVariableRepository = services.GetService<IEdiVariableRepository>() ??
                                 throw new ArgumentNullException(nameof(IEdiVariableRepository));
        _mediatorHandler = services.GetService<IMediatorHandler>() ??
                           throw new ArgumentNullException(nameof(IMediatorHandler));
        _ediVariableRepository.RemoveAll();
    }

    [Fact]
    public async Task RegisterVariable_WithValidCommand_ShouldRegisterVariable()
    {
        var variableCommand =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValue", "TestDescription");
        // Act
        var resVarCommand = await _mediatorHandler.SendCommand(variableCommand);
        // Assert
        _ediVariableRepository.Flush();
        var variables = await _ediVariableRepository.GetAllAsync();
        var ediVariables = variables as EdiVariable[] ?? variables.ToArray();
        ediVariables.Should().NotBeEmpty();
        ediVariables.Should().HaveCount(1);
        ediVariables.Should()
            .ContainSingle(x => x.Placeholder == variableCommand.Placeholder && x.Id == ediVariables.First().Id);
        resVarCommand.IsValid.Should().BeTrue();

        //Cleanup
        _ediVariableRepository.Remove(ediVariables.First());
        await _ediVariableRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task RegisterVariable_WithSamePlaceHolder_ShouldFail()
    {
        var variableCommand1 =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValue", "TestDescription");
        await _mediatorHandler.SendCommand(variableCommand1);
        //Act
        var variableCommand2 =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValue", "TestDescription");
        var resVarCommand2 = await _mediatorHandler.SendCommand(variableCommand2);

        //Assert
        _ediVariableRepository.Flush();
        resVarCommand2.IsValid.Should().BeFalse();
        resVarCommand2.Errors.Should().ContainSingle(e =>
            e.ErrorMessage == $"Placeholder {variableCommand2.Placeholder} is already in use");

        //Cleanup
        var variables = await _ediVariableRepository.GetAllAsync();
        var ediVariables = variables as EdiVariable[] ?? variables.ToArray();
        _ediVariableRepository.Remove(ediVariables.First());
        await _ediVariableRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task RegisterVariable_WithNoTitle_ShouldFail()
    {
        var variableCommand = new RegisterEdiVariableCommand(Guid.NewGuid(), "", "TestValue", "TestDescription");
        //Act
        var resVarCommand = await _mediatorHandler.SendCommand(variableCommand);

        //Assert
        resVarCommand.IsValid.Should().BeFalse();
        resVarCommand.Errors.Should().ContainSingle(e => e.ErrorMessage == "Title is required");
    }

    [Fact]
    public async Task RemoveVariable_WithValidCommand_ShouldRemoveVariable()
    {
        var variableCommand =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValue", "TestDescription");
        await _mediatorHandler.SendCommand(variableCommand);
        //Act, Get the variable to remove
        _ediVariableRepository.Flush();
        var variables = await _ediVariableRepository.GetAllAsync();
        var ediVariables = variables as EdiVariable[] ?? variables.ToArray();
        var removeCommand = new RemoveEdiVariableCommand(ediVariables.First().Id);
        var resRemoveCommand = await _mediatorHandler.SendCommand(removeCommand);

        //Assert
        resRemoveCommand.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task RemoveVariable_WithInvalidId_ShouldFail()
    {
        var removeCommand = new RemoveEdiVariableCommand(Guid.NewGuid());
        //Act
        var resRemoveCommand = await _mediatorHandler.SendCommand(removeCommand);

        //Assert
        resRemoveCommand.IsValid.Should().BeFalse();
        resRemoveCommand.Errors.Should().ContainSingle(e => e.ErrorMessage == "EdiVariable not found");
    }

    [Fact]
    public async Task UpdateVariable_WithValidCommand_ShouldUpdateVariable()
    {
        var variableCommand =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValue", "TestDescription");
        await _mediatorHandler.SendCommand(variableCommand);
        //Act, Get the variable to update
        _ediVariableRepository.Flush();
        var variables = await _ediVariableRepository.GetAllAsync();
        var ediVariables = variables as EdiVariable[] ?? variables.ToArray();
        var updateCommand = new UpdateEdiVariableCommand(ediVariables.First().Id, "TestVariable", "TestValueUpdated",
            "TestDescriptionUpdated");
        var resUpdateCommand = await _mediatorHandler.SendCommand(updateCommand);

        //Assert
        resUpdateCommand.IsValid.Should().BeTrue();
        _ediVariableRepository.Flush();
        var variablesAfterUpdate = await _ediVariableRepository.GetAllAsync();
        variablesAfterUpdate.Should().ContainSingle(x => x.Placeholder == "TestDescriptionUpdated");

        //Cleanup
        _ediVariableRepository.Remove(variablesAfterUpdate.First());
        await _ediVariableRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task UpdateVariable_WithInvalidId_ShouldFail()
    {
        var updateCommand = new UpdateEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValueUpdated",
            "TestDescriptionUpdated");
        //Act
        var resUpdateCommand = await _mediatorHandler.SendCommand(updateCommand);

        //Assert
        resUpdateCommand.IsValid.Should().BeFalse();
        resUpdateCommand.Errors.Should().ContainSingle(e => e.ErrorMessage == "EdiVariable not found");

        _ediVariableRepository.RemoveAll();
    }

    [Fact]
    public async Task UpdateVariable_WithExistingPlaceHolder_ShouldFail()
    {
        var variableCommand1 =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable", "TestValue", "TestDescription");
        await _mediatorHandler.SendCommand(variableCommand1);
        var variableCommand2 =
            new RegisterEdiVariableCommand(Guid.NewGuid(), "TestVariable2", "TestValue", "TestDescriptionVariable2");
        await _mediatorHandler.SendCommand(variableCommand2);
        //Act, Get the variable to update
        _ediVariableRepository.Flush();
        var variable = await _ediVariableRepository.GetByPlaceholderAsync("TestDescriptionVariable2");

        var updateCommand = new UpdateEdiVariableCommand(variable.FirstOrDefault().Id, "TestVariable2",
            "TestValueUpdated", "TestDescription");
        var resUpdateCommand = await _mediatorHandler.SendCommand(updateCommand);

        //Assert
        resUpdateCommand.IsValid.Should().BeFalse();
        resUpdateCommand.Errors.Should().ContainSingle(e =>
            e.ErrorMessage == $"Placeholder {updateCommand.Placeholder} is already in use");

        //Cleanup
        _ediVariableRepository.Remove(variable.FirstOrDefault());
        await _ediVariableRepository.UnitOfWork.Commit();
    }
}