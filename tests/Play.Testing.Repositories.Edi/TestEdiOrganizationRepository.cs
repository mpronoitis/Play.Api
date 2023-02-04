using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiOrganizationRepository
{
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;

    public TestEdiOrganizationRepository()
    {
        var services = SetupRunner.Setup();
        _ediOrganizationRepository = services.GetRequiredService<IEdiOrganizationRepository>();
    }

    private static EdiOrganization CreateEdiOrg(Guid? id = null, string? name = null, string? email = null)
    {
        return new EdiOrganization(
            id ?? Guid.NewGuid(),
            name ?? "",
            email ?? ""
        );
    }

    [Fact]
    public async Task GetById_GivenExistingOrganization_ShouldReturnExpectedResult()
    {
        // Arrange
        var id = Guid.Parse("BA282AE5-C423-4F0A-9443-FBED0C62C0FD");
        var expectedResult = CreateEdiOrg(id, "Organization 1", "org1@example.com");
        _ediOrganizationRepository.Add(expectedResult);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediOrganizationRepository.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediOrganizationRepository.Remove(expectedResult);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByEmail_GivenExistingOrganization_ShouldReturnExpectedResult()
    {
        // Arrange
        var email = "edi.support@lidl.com";
        var expectedResult = CreateEdiOrg(name: "Organization 1", email: email);
        _ediOrganizationRepository.Add(expectedResult);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediOrganizationRepository.GetByEmailAsync(email);

        // Assert
        result.Should().NotBeNull()
            .And.Be(expectedResult);

        // Cleanup
        _ediOrganizationRepository.Remove(expectedResult);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetAll_Should_ReturnExpectedResult()
    {
        // Arrange
        var expectedResult = new List<EdiOrganization>
        {
            CreateEdiOrg(name: "Organization 1", email: "org1@example.com"),
            CreateEdiOrg(name: "Organization 2", email: "org2@example.com")
        };
        _ediOrganizationRepository.Add(expectedResult[0]);
        _ediOrganizationRepository.Add(expectedResult[1]);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        // Act
        var result = await _ediOrganizationRepository.GetAllAsync();

        // Assert
        result.Should().NotBeNull()
            .And.BeAssignableTo<IEnumerable<EdiOrganization>>()
            .And.Equal(expectedResult);

        // Cleanup
        _ediOrganizationRepository.Remove(expectedResult[0]);
        _ediOrganizationRepository.Remove(expectedResult[1]);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Add_Should_AddOrganization()
    {
        // Arrange
        var organization = CreateEdiOrg(name: "Organization 1", email: "fii@example.com");

        // Act
        _ediOrganizationRepository.Add(organization);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        // Assert
        var result = await _ediOrganizationRepository.GetByIdAsync(organization.Id);
        result.Should().NotBeNull()
            .And.Be(organization);

        // Cleanup
        _ediOrganizationRepository.Remove(organization);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Update_Should_UpdateOrganization()
    {
        // Arrange
        var organization = CreateEdiOrg(name: "Organization 1", email: "faefwa@fii.com");
        _ediOrganizationRepository.Add(organization);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        // Act
        organization.Name = "Organization 2";
        _ediOrganizationRepository.Update(organization);
        await _ediOrganizationRepository.UnitOfWork.Commit();

        // Assert
        var result = await _ediOrganizationRepository.GetByIdAsync(organization.Id);
        result.Should().NotBeNull()
            .And.Be(organization);

        // Cleanup
        _ediOrganizationRepository.Remove(organization);
        await _ediOrganizationRepository.UnitOfWork.Commit();
    }
}