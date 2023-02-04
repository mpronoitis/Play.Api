using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiModelRepository
{
    private readonly IEdiModelRepository _ediModelRepository;

    public TestEdiModelRepository()
    {
        var services = SetupRunner.Setup();

        _ediModelRepository = services.GetRequiredService<IEdiModelRepository>();
    }

    private static EdiModel GenerateEdiModel(Guid? id = null, Guid? orgId = null, string? title = null,
        bool? enabled = null)
    {
        return new EdiModel(
            id ?? Guid.NewGuid(),
            orgId ?? Guid.Empty,
            title ?? "",
            '~',
            '*',
            ':',
            enabled ?? false
        );
    }


    [Fact]
    public async Task GetAllModels_ReturnsExpectedResult()
    {
        // Arrange
        var expectedModels = new List<EdiModel>
        {
            GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: "Model 1", enabled: true),
            GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000002"), title: "Model 2", enabled: false)
        };

        _ediModelRepository.Add(expectedModels[0]);
        _ediModelRepository.Add(expectedModels[1]);

        //commit uow
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var models = await _ediModelRepository.GetAllAsync();

        // Assert
        models.Should().NotBeNull()
            .And.BeAssignableTo<IEnumerable<EdiModel>>()
            .And.Equal(expectedModels);

        //cleanup
        _ediModelRepository.Remove(expectedModels[0]);
        _ediModelRepository.Remove(expectedModels[1]);
        await _ediModelRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetModelById_ReturnsExpectedResult()
    {
        // Arrange
        var id = Guid.Parse("B0D8FD93-CB5B-4DEE-9E1B-78E8F31E4429");
        var expectedModel = GenerateEdiModel(id, title: "Model 1", enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByIdAsync(id);

        // Assert
        model.Should().NotBeNull()
            .And.BeAssignableTo<EdiModel>()
            .And.Be(expectedModel);

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByTitle_ReturnsExpectedResult()
    {
        // Arrange
        var title = "INVOIC";
        var expectedModel = GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: title,
            enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByTitleAsync(title);

        // Assert
        model.Should().NotBeNull()
            .And.BeAssignableTo<EdiModel>()
            .And.Be(expectedModel);

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    //failures

    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleDoesNotExist()
    {
        // Arrange
        var title = "INVOIC";
        var expectedModel = GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: title,
            enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByTitleAsync("NOT INVOIC");

        // Assert
        model.Should().BeNull();

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleIsNull()
    {
        // Arrange
        var title = "INVOIC";
        var expectedModel = GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: title,
            enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByTitleAsync(null);

        // Assert
        model.Should().BeNull();

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleIsEmpty()
    {
        // Arrange
        var title = "INVOIC";
        var expectedModel = GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: title,
            enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByTitleAsync("");

        // Assert
        model.Should().BeNull();

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleIsWhitespace()
    {
        // Arrange
        var title = "INVOIC";
        var expectedModel = GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: title,
            enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByTitleAsync(" ");

        // Assert
        model.Should().BeNull();

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleIsWhitespaceAndNull()
    {
        // Arrange
        var title = "INVOIC";
        var expectedModel = GenerateEdiModel(orgId: new Guid("00000000-0000-0000-0000-000000000001"), title: title,
            enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByTitleAsync(" ");

        // Assert
        model.Should().BeNull();

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenIdDoesNotExist()
    {
        // Arrange
        var id = Guid.Parse("B0D8FD93-CB5B-4DEE-9E1B-78E8F31E4429");
        var expectedModel = GenerateEdiModel(id, title: "Model 1", enabled: true);
        _ediModelRepository.Add(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();

        // Act
        var model = await _ediModelRepository.GetByIdAsync(Guid.NewGuid());

        // Assert
        model.Should().BeNull();

        // Cleanup
        _ediModelRepository.Remove(expectedModel);
        await _ediModelRepository.UnitOfWork.Commit();
    }
}