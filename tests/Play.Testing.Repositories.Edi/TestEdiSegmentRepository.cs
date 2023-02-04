using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiSegmentRepository
{
    private readonly IEdiSegmentRepository _ediSegmentRepository;

    public TestEdiSegmentRepository()
    {
        var services = SetupRunner.Setup();
        _ediSegmentRepository = services.GetRequiredService<IEdiSegmentRepository>();
    }

    private EdiSegment CreateEdiSegment(
        Guid? id = null,
        Guid? modelId = null,
        string? title = null,
        string? description = null)
    {
        return new EdiSegment(
            id ?? Guid.NewGuid(),
            modelId ?? Guid.NewGuid(),
            title ?? "Title",
            description ?? "Description");
    }

    [Fact]
    public async Task GetEdiSegmentById_GivenExistingSegment_ShouldReturnExpectedResult()
    {
        // Arrange
        var id = Guid.Parse("5BCD80C0-77EE-4784-B480-325C51B2206C");
        var expectedResult = CreateEdiSegment(id, Guid.NewGuid(), "Segment 1", "Description 1");
        _ediSegmentRepository.Add(expectedResult);
        await _ediSegmentRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediSegmentRepository.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediSegmentRepository.Delete(expectedResult);
        await _ediSegmentRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByModelId_GivenExistingSegment_ShouldReturnExpectedResult()
    {
        // Arrange
        var model_id = Guid.Parse("B0D8FD93-CB5B-4DEE-9E1B-78E8F31E4429");
        var expectedResult = CreateEdiSegment(Guid.NewGuid(), model_id, "Segment 1", "Description 1");
        _ediSegmentRepository.Add(expectedResult);
        await _ediSegmentRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediSegmentRepository.GetByModelIdAsync(model_id);

        // Assert
        result.Should().NotBeNull()
            .And.BeEquivalentTo(expectedResult);

        // Cleanup
        _ediSegmentRepository.Delete(expectedResult);
        await _ediSegmentRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetAll_ShouldReturnExpectedResult()
    {
        // Arrange
        var expectedResult = new List<EdiSegment>
        {
            CreateEdiSegment(Guid.NewGuid(), Guid.NewGuid(), "Segment 1", "Description 1"),
            CreateEdiSegment(Guid.NewGuid(), Guid.NewGuid(), "Segment 2", "Description 2"),
            CreateEdiSegment(Guid.NewGuid(), Guid.NewGuid(), "Segment 3", "Description 3")
        };
        _ediSegmentRepository.AddRange(expectedResult);
        await _ediSegmentRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediSegmentRepository.GetAllAsync();

        // Assert
        result.Should().NotBeNull()
            .And.BeEquivalentTo(expectedResult);

        // Cleanup
        _ediSegmentRepository.DeleteRange(expectedResult);
        await _ediSegmentRepository.UnitOfWork.Commit();
    }


    [Fact]
    public void Add_GivenNewSegment_ShouldAddExpectedResult()
    {
        // Arrange
        var expectedResult = CreateEdiSegment(Guid.NewGuid(), Guid.NewGuid(), "Segment 1", "Description 1");

        // Act
        _ediSegmentRepository.Add(expectedResult);
        _ediSegmentRepository.UnitOfWork.Commit();

        // Assert
        var result = _ediSegmentRepository.GetByIdAsync(expectedResult.Id).Result;
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediSegmentRepository.Delete(expectedResult);
        _ediSegmentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public void Update_GivenExistingSegment_ShouldUpdateExpectedResult()
    {
        // Arrange
        var expectedResult = CreateEdiSegment(Guid.NewGuid(), Guid.NewGuid(), "Segment 1", "Description 1");
        _ediSegmentRepository.Add(expectedResult);
        _ediSegmentRepository.UnitOfWork.Commit();

        // Act
        expectedResult.Title = "Segment 2";
        _ediSegmentRepository.Update(expectedResult);
        _ediSegmentRepository.UnitOfWork.Commit();

        // Assert
        var result = _ediSegmentRepository.GetByIdAsync(expectedResult.Id).Result;
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediSegmentRepository.Delete(expectedResult);
        _ediSegmentRepository.UnitOfWork.Commit();
    }
}