using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiProfileRepository
{
    private readonly IEdiProfileRepository _ediProfileRepository;

    public TestEdiProfileRepository()
    {
        var services = SetupRunner.Setup();
        _ediProfileRepository = services.GetRequiredService<IEdiProfileRepository>();
    }

    private static EdiProfile CreateEdiProfile(Guid? id = null, Guid? customerId = null, Guid? modelId = null,
        string? title = null, string? payload = null, bool? enabled = null)
    {
        return new EdiProfile(
            id ?? Guid.NewGuid(),
            customerId ?? Guid.Empty,
            modelId ?? Guid.Empty,
            title ?? "",
            payload ?? "",
            enabled ?? false
        );
    }


    [Fact]
    public async Task GetById_GivenExistingProfile_ShouldReturnExpectedResult()
    {
        // Arrange
        var id = Guid.Parse("D809FE0E-5340-42E4-AB7E-4DF837EB9663");
        var expectedResult = CreateEdiProfile(id, Guid.NewGuid(), Guid.NewGuid(),
            "Profile 1", "payload1", true);
        _ediProfileRepository.Add(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediProfileRepository.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediProfileRepository.Remove(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetAll_Should_ReturnExpectedResult()
    {
        // Arrange
        var expectedResult = new List<EdiProfile>
        {
            CreateEdiProfile(customerId: Guid.NewGuid(), modelId: Guid.NewGuid(), title: "Profile 1",
                payload: "payload1", enabled: true),
            CreateEdiProfile(customerId: Guid.NewGuid(), modelId: Guid.NewGuid(), title: "Profile 2",
                payload: "payload2", enabled: false)
        };
        _ediProfileRepository.Add(expectedResult[0]);
        _ediProfileRepository.Add(expectedResult[1]);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediProfileRepository.GetAllAsync();

        // Assert
        result.Should().NotBeNull()
            .And.BeAssignableTo<IEnumerable<EdiProfile>>();

        // Cleanup
        _ediProfileRepository.Remove(expectedResult[0]);
        _ediProfileRepository.Remove(expectedResult[1]);
        await _ediProfileRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetAllByCustomerId_GivenExistingProfiles_ShouldReturnExpectedResult()
    {
        // Arrange
        var customerId = Guid.Parse("D2B23712-0D54-46EE-8DA3-A8AF3DFD6ED6");
        var expectedResult = new List<EdiProfile>
        {
            CreateEdiProfile(customerId: customerId, modelId: Guid.NewGuid(), title: "Profile 1", payload: "payload1",
                enabled: true),
            CreateEdiProfile(customerId: customerId, modelId: Guid.NewGuid(), title: "Profile 2", payload: "payload2",
                enabled: false)
        };
        _ediProfileRepository.Add(expectedResult[0]);
        _ediProfileRepository.Add(expectedResult[1]);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediProfileRepository.GetAllByCustomerIdAsync(customerId);

        // Assert
        result.Should().NotBeNull()
            .And.BeAssignableTo<IEnumerable<EdiProfile>>();

        // Cleanup
        _ediProfileRepository.Remove(expectedResult[0]);
        _ediProfileRepository.Remove(expectedResult[1]);
        await _ediProfileRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByModelId_GivenExistingProfile_ShouldReturnExpectedResult()
    {
        // Arrange
        var modelId = Guid.Parse("B0D8FD93-CB5B-4DEE-9E1B-78E8F31E4429");
        var expectedResult = CreateEdiProfile(customerId: Guid.NewGuid(), modelId: modelId, title: "Profile 1",
            payload: "payload1", enabled: true);
        _ediProfileRepository.Add(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediProfileRepository.GetByModelIdAsync(modelId);

        // Assert
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediProfileRepository.Remove(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByUserId_GivenExistingProfiles_ShouldReturnExpectedResult()
    {
        // Arrange
        var userId = Guid.Parse("D2B23712-0D54-46EE-8DA3-A8AF3DFD6ED6");
        var expectedResult = new List<EdiProfile>
        {
            CreateEdiProfile(customerId: userId, modelId: Guid.NewGuid(), title: "Profile 1", payload: "payload1",
                enabled: true),
            CreateEdiProfile(customerId: userId, modelId: Guid.NewGuid(), title: "Profile 2", payload: "payload2",
                enabled: false)
        };
        _ediProfileRepository.Add(expectedResult[0]);
        _ediProfileRepository.Add(expectedResult[1]);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediProfileRepository.GetByUserIdAsync(userId);

        // Assert
        result.Should().NotBeNull()
            .And.BeAssignableTo<IEnumerable<EdiProfile>>();

        // Cleanup
        _ediProfileRepository.Remove(expectedResult[0]);
        _ediProfileRepository.Remove(expectedResult[1]);
        await _ediProfileRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Add_GivenNewProfile_ShouldAddExpectedResult()
    {
        // Arrange
        var expectedResult = CreateEdiProfile(customerId: Guid.NewGuid(), modelId: Guid.NewGuid(), title: "Profile 1",
            payload: "payload1", enabled: true);

        // Act
        _ediProfileRepository.Add(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Assert
        var result = await _ediProfileRepository.GetByIdAsync(expectedResult.Id);
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediProfileRepository.Remove(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Update_GivenExistingProfile_ShouldUpdateExpectedResult()
    {
        // Arrange
        var expectedResult = CreateEdiProfile(customerId: Guid.NewGuid(), modelId: Guid.NewGuid(), title: "Profile 1",
            payload: "payload1", enabled: true);
        _ediProfileRepository.Add(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        expectedResult.Title = "Profile 2";
        _ediProfileRepository.Update(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Assert
        var result = await _ediProfileRepository.GetByIdAsync(expectedResult.Id);
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediProfileRepository.Remove(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Remove_GivenExistingProfile_ShouldRemoveExpectedResult()
    {
        // Arrange
        var expectedResult = CreateEdiProfile(customerId: Guid.NewGuid(), modelId: Guid.NewGuid(), title: "Profile 1",
            payload: "payload1", enabled: true);
        _ediProfileRepository.Add(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Act
        _ediProfileRepository.Remove(expectedResult);
        await _ediProfileRepository.UnitOfWork.Commit();

        // Assert
        var result = await _ediProfileRepository.GetByIdAsync(expectedResult.Id);
        result.Should().BeNull();
    }
}