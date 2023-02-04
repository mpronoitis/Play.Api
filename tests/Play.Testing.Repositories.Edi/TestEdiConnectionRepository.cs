using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiConnectionRepository
{
    private readonly IEdiConnectionRepository _ediConnectionRepository;


    public TestEdiConnectionRepository()
    {
        var services = SetupRunner.Setup();

        _ediConnectionRepository = services.GetRequiredService<IEdiConnectionRepository>();
    }

    private static EdiConnection CreateFooEdiConnection(Guid id = default, Guid customer_id = default,
        Guid model_id = default, Guid org_id = default, Guid profile_id = default, string? ftp_hostname = default,
        string? ftp_username = default, string? ftp_password = default, int? ftp_port = default, string? file_type = default)
    {
        if (id == default) id = Guid.NewGuid();
        if (customer_id == default) customer_id = Guid.NewGuid();
        if (model_id == default) model_id = Guid.NewGuid();
        if (org_id == default) org_id = Guid.NewGuid();
        if (profile_id == default) profile_id = Guid.NewGuid();
        if (ftp_hostname == default) ftp_hostname = $"ftp.{id}.com";
        if (ftp_username == default) ftp_username = $"{id}_username";
        if (ftp_password == default) ftp_password = $"{id}_password";
        if (ftp_port == default) ftp_port = 21;
        if (file_type == default) file_type = "csv";

        return new EdiConnection(id, customer_id, model_id, org_id, profile_id, ftp_hostname, ftp_username,
            ftp_password, 21, file_type);
    }

    private IEnumerable<EdiConnection> CreateFooEdiConnections(int num)
    {
        var connections = new List<EdiConnection>();
        for (var i = 0; i < num; i++)
            connections.Add(new EdiConnection(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), "foo", "foo", "foo", 21, "csv"));

        return connections;
    }


    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public async Task GetAllEdiConnections_DatabaseContainsMultipleEdiConnections_ReturnsAllEdiConnections(int num)
    {
        //Arrange
        //create some EdiConnections
        var ediConnections = new List<EdiConnection>();
        for (var i = 0; i < num; i++) ediConnections.Add(CreateFooEdiConnection());

        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        //commit units of work
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetAllAsync();

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<List<EdiConnection>>()
            .And.HaveCount(ediConnections.Count);

        //cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetEdiConnectionById_DatabaseContainsEdiConnection_ReturnsEdiConnection()
    {
        //Arrange
        var guid = Guid.Parse("c86b260f-9954-4f51-a357-2958b984dd2a");
        var ediConnection = CreateFooEdiConnection(guid);
        _ediConnectionRepository.Add(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetByIdAsync(guid);

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<EdiConnection>()
            .And.BeEquivalentTo(ediConnection);

        //cleanup
        _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetEdiConnectionsByModelId_DatabaseContainsEdiConnections_ReturnsEdiConnections()
    {
        //Arrange
        var guid = Guid.Parse("B0D8FD93-CB5B-4DEE-9E1B-78E8F31E4429");
        var ediConnections = new List<EdiConnection>();
        for (var i = 0; i < 5; i++) ediConnections.Add(CreateFooEdiConnection(model_id: guid));

        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        //commit units of work
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetByModelIdAsync(guid);

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<List<EdiConnection>>()
            .And.HaveCount(ediConnections.Count)
            .And.Contain(ediConnections);

        //cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetEdiConnectionsByOrganizationId_DatabaseContainsEdiConnections_ReturnsEdiConnections()
    {
        //Arrange
        var guid = Guid.Parse("BA282AE5-C423-4F0A-9443-FBED0C62C0FD");
        var ediConnections = new List<EdiConnection>();
        for (var i = 0; i < 5; i++) ediConnections.Add(CreateFooEdiConnection(org_id: guid));

        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        //commit units of work
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetByOrganizationIdAsync(guid);

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<List<EdiConnection>>()
            .And.HaveCount(ediConnections.Count)
            .And.Contain(ediConnections);

        //cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetEdiConnectionsByProfileId_DatabaseContainsEdiConnections_ReturnsEdiConnections()
    {
        //Arrange
        var guid = Guid.Parse("D809FE0E-5340-42E4-AB7E-4DF837EB9663");
        var ediConnections = new List<EdiConnection>();
        for (var i = 0; i < 5; i++) ediConnections.Add(CreateFooEdiConnection(profile_id: guid));

        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        //commit units of work
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetByProfileIdAsync(guid);

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<List<EdiConnection>>()
            .And.HaveCount(ediConnections.Count)
            .And.Contain(ediConnections);

        //cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetEdiConnectionsByCustomerId_DatabaseContainsEdiConnections_ReturnsEdiConnections()
    {
        //Arrange
        var guid = Guid.Parse("D2B23712-0D54-46EE-8DA3-A8AF3DFD6ED6");
        var ediConnections = new List<EdiConnection>();
        for (var i = 0; i < 5; i++) ediConnections.Add(CreateFooEdiConnection(customer_id: guid));

        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        //commit units of work
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetByCustomerIdAsync(guid);

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<List<EdiConnection>>()
            .And.HaveCount(ediConnections.Count)
            .And.Contain(ediConnections);

        //cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetAllEdiConnectionsByCustomerId_DatabaseContainsEdiConnections_ReturnsEdiConnections()
    {
        //Arrange
        var guid = Guid.Parse("D2B23712-0D54-46EE-8DA3-A8AF3DFD6ED6");
        var ediConnections = new List<EdiConnection>();
        for (var i = 0; i < 5; i++) ediConnections.Add(CreateFooEdiConnection(customer_id: guid));

        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        //commit units of work
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var ediConnectionResult = await _ediConnectionRepository.GetAllByCustomerIdAsync(guid);

        //Assert
        ediConnectionResult.Should().NotBeNull()
            .And.BeOfType<List<EdiConnection>>()
            .And.HaveCount(ediConnections.Count)
            .And.Contain(ediConnections);

        //cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task AddEdiConnection_NewEdiConnection_AddsEdiConnection()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var ediConnection = new EdiConnection(guid, guid, guid, guid, guid, "foo", "foo", "foo", 21, "edi");

        //Act
        _ediConnectionRepository.Add(ediConnection);
        var result = await _ediConnectionRepository.GetByIdAsync(guid);

        //Assert
        result.Should().NotBeNull()
            .And.BeOfType<EdiConnection>()
            .And.BeEquivalentTo(ediConnection);

        //Cleanup
        _ediConnectionRepository.Remove(result);
    }


    [Fact]
    public async Task RemoveEdiConnection_ExistingEdiConnection_RemovesEdiConnection()
    {
        //Arrange
        var ediConnection = CreateFooEdiConnection();
        _ediConnectionRepository.Add(ediConnection);

        //Act
        _ediConnectionRepository.Remove(ediConnection);
        var result = await _ediConnectionRepository.GetByIdAsync(ediConnection.Id);

        //Assert
        result.Should().BeNull();
    }


    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public async Task TestGetAllEdiConnections(int num)
    {
        // Create the edi connections
        var ediConnections = CreateFooEdiConnections(num);

        // Add the edi connections to the repository
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Add(ediConnection);

        // Commit the changes
        await _ediConnectionRepository.UnitOfWork.Commit();

        //Act
        var result = await _ediConnectionRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<EdiConnection>>(result);
        Assert.Equal(num, result.Count());

        //Cleanup
        foreach (var ediConnection in ediConnections) _ediConnectionRepository.Remove(ediConnection);
        await _ediConnectionRepository.UnitOfWork.Commit();
    }
}