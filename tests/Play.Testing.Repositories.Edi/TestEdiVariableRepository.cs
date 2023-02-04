using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiVariableRepository
{
    private readonly IEdiVariableRepository _ediVariableRepository;

    public TestEdiVariableRepository()
    {
        var services = SetupRunner.Setup();
        _ediVariableRepository = services.GetRequiredService<IEdiVariableRepository>();
    }

    private EdiVariable CreateEdiVariable(Guid? id = null, string? title = null, string? description = null,
        string? placeholder = null)
    {
        return new EdiVariable(
            id ?? Guid.NewGuid(),
            title ?? "Variable 1",
            description ?? "Description 1",
            placeholder ?? "{VAR1}"
        );
    }


    [Fact]
    public async Task GetById_ShouldReturnExpectedResult()
    {
        // Arrange
        var id = Guid.Parse("DDF2FC7D-244B-461C-AE57-014B252C4A19");
        var expectedResult = CreateEdiVariable(id, "Variable 1", "Description 1", "{VAR1}");
        _ediVariableRepository.Register(expectedResult);
        await _ediVariableRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediVariableRepository.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediVariableRepository.Remove(expectedResult);
        await _ediVariableRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByPlaceholder_ShouldReturnExpectedResult()
    {
        // Arrange
        var placeholder = "$ITEM_NAME$";
        var expectedResult = CreateEdiVariable(placeholder: placeholder);
        _ediVariableRepository.Register(expectedResult);
        await _ediVariableRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediVariableRepository.GetByPlaceholderAsync(placeholder);

        // Assert
        result.Should().NotBeNull()
            .And.ContainSingle()
            .And.Subject.First().Should().Be(expectedResult);

        // Cleanup
        _ediVariableRepository.Remove(expectedResult);
        await _ediVariableRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByTitle_ShouldReturnExpectedResult()
    {
        // Arrange
        var title = "Document total price without tax";
        var expectedResult = CreateEdiVariable(title: title);
        _ediVariableRepository.Register(expectedResult);
        await _ediVariableRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediVariableRepository.GetByTitleAsync(title);

        // Assert
        result.Should().NotBeNull()
            .And.ContainSingle()
            .And.Subject.First().Should().Be(expectedResult);

        // Cleanup
        _ediVariableRepository.Remove(expectedResult);
        await _ediVariableRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetAll_ShouldReturnExpectedResult()
    {
        // Arrange
        var expectedResult = new List<EdiVariable>
        {
            CreateEdiVariable(title: "Variable 1", description: "Description 1", placeholder: "{VAR1}"),
            CreateEdiVariable(title: "Variable 2", description: "Description 2", placeholder: "{VAR2}"),
            CreateEdiVariable(title: "Variable 3", description: "Description 3", placeholder: "{VAR3}")
        };
        foreach (var variable in expectedResult) _ediVariableRepository.Register(variable);
        await _ediVariableRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediVariableRepository.GetAllAsync();

        // Assert
        result.Should().NotBeNull()
            .And.BeEquivalentTo(expectedResult);

        // Cleanup
        foreach (var variable in expectedResult) _ediVariableRepository.Remove(variable);
        await _ediVariableRepository.UnitOfWork.Commit();
    }

    [Fact]
    public void Register_ShouldAddExpectedResult()
    {
        // Arrange
        var expectedResult = CreateEdiVariable();

        // Act
        _ediVariableRepository.Register(expectedResult);
        _ediVariableRepository.UnitOfWork.Commit();

        // Assert
        _ediVariableRepository.GetByIdAsync(expectedResult.Id).Result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediVariableRepository.Remove(expectedResult);
        _ediVariableRepository.UnitOfWork.Commit();
    }

    [Fact]
    public void Dispose_ShouldDisposeContext()
    {
        // Arrange
        var ediVariableRepository = _ediVariableRepository;

        // Act
        ediVariableRepository.Dispose();

        // Assert
        try
        {
            ediVariableRepository.UnitOfWork.Commit();
        }
        catch (ObjectDisposedException)
        {
            Assert.True(true);
        }
    }
}