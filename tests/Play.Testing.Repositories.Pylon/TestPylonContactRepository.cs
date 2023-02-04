using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Pylon;

public class TestPylonContactRepository
{
    private readonly IPylonTempContactRepository _pylonTempContactRepository;

    public TestPylonContactRepository()
    {
        var services = SetupRunner.Setup();

        _pylonTempContactRepository = services.GetRequiredService<IPylonTempContactRepository>() ??
                                      throw new Exception("Could not resolve IPylonTempContactRepository");
    }

    private PylonContact CreateContact(Guid? id = null, Guid? heid = null, string? code = null, string? name = null,
        string? firstName = null, string? lastName = null, string? tin = null, string? emails = null,
        string? phones = null, string? address = null, DateTime? createdDate = null)
    {
        return new PylonContact(
            id ?? Guid.NewGuid(),
            heid ?? Guid.NewGuid(),
            code ?? "code",
            name ?? "name",
            firstName ?? "firstName",
            lastName ?? "lastName",
            tin ?? "tin",
            emails ?? "emails",
            phones ?? "phones",
            address ?? "address",
            createdDate ?? DateTime.Now);
    }

    private List<PylonContact> CreateContacts(int count)
    {
        var contacts = new List<PylonContact>();
        for (var i = 0; i < count; i++) contacts.Add(CreateContact());
        return contacts;
    }

    [Fact]
    public async Task GetAll_WithNoContacts_ReturnsEmptyList()
    {
        var contacts = await _pylonTempContactRepository.GetAll(1, 100);
        contacts.Should().BeEmpty();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetAll_WithContacts_ReturnsAllContacts(int count)
    {
        var contacts = CreateContacts(count);
        await _pylonTempContactRepository.AddRange(contacts);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        var result = await _pylonTempContactRepository.GetAll(1, count);

        result.Should().HaveCount(count);

        //cleanup
        _pylonTempContactRepository.RemoveRange(contacts);
        await _pylonTempContactRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetById_WithNoContacts_ReturnsNull()
    {
        var contact = await _pylonTempContactRepository.GetById(Guid.NewGuid());
        contact.Should().BeNull();
    }

    [Fact]
    public async Task GetById_WithContact_ReturnsContact()
    {
        var contact = CreateContact();
        await _pylonTempContactRepository.Add(contact);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        var result = await _pylonTempContactRepository.GetById(contact.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(contact);

        //cleanup
        _pylonTempContactRepository.Remove(contact);
        await _pylonTempContactRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Add_WithContact_AddsContact()
    {
        var contact = CreateContact();
        await _pylonTempContactRepository.Add(contact);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        var result = await _pylonTempContactRepository.GetById(contact.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(contact);

        //cleanup
        _pylonTempContactRepository.Remove(contact);
        await _pylonTempContactRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task AddRange_WithContacts_AddsContacts()
    {
        var contacts = CreateContacts(100);
        await _pylonTempContactRepository.AddRange(contacts);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        var result = await _pylonTempContactRepository.GetAll(1, 1020);

        result.Should().HaveCount(100);

        //cleanup
        _pylonTempContactRepository.RemoveRange(contacts);
        await _pylonTempContactRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Remove_WithContact_RemovesContact()
    {
        var contact = CreateContact();
        await _pylonTempContactRepository.Add(contact);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        var result = await _pylonTempContactRepository.GetById(contact.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(contact);

        _pylonTempContactRepository.Remove(contact);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        result = await _pylonTempContactRepository.GetById(contact.Id);

        result.Should().BeNull();
    }

    [Fact]
    public async Task RemoveRange_WithContacts_RemovesContacts()
    {
        var contacts = CreateContacts(100);
        await _pylonTempContactRepository.AddRange(contacts);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        var result = await _pylonTempContactRepository.GetAll(1, 1020);

        result.Should().HaveCount(100);

        _pylonTempContactRepository.RemoveRange(contacts);
        await _pylonTempContactRepository.UnitOfWork.Commit();

        result = await _pylonTempContactRepository.GetAll(1, 1020);

        result.Should().BeEmpty();
    }
}