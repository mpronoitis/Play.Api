using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Pylon;

[Collection("Pylon Item Repository")]
public class TestPylonItemRepository
{
    private readonly IPylonItemRepository _pylonItemRepository;

    public TestPylonItemRepository()
    {
        var services = SetupRunner.Setup();
        _pylonItemRepository = services.GetRequiredService<IPylonItemRepository>() ??
                               throw new ArgumentNullException(nameof(_pylonItemRepository));
    }

    private PylonItem CreatePylonItem(Guid? id, Guid? heid, string? code, string? name, string? description,
        string? factoryCode,
        string? auxiliaryCode, string? comments, DateTime? createdAt)
    {
        return new PylonItem(
            id ?? Guid.NewGuid(),
            heid ?? Guid.NewGuid(),
            code ?? "code",
            name ?? "name",
            description ?? "description",
            factoryCode ?? "factoryCode",
            auxiliaryCode ?? "auxiliaryCode",
            comments ?? "comments",
            createdAt ?? DateTime.Now);
    }

    private List<PylonItem> CreatePylonItems(int count)
    {
        var pylonItems = new List<PylonItem>();
        for (var i = 0; i < count; i++)
            pylonItems.Add(CreatePylonItem(null, null, null, null, null, null, null, null, null));
        return pylonItems;
    }

    [Fact]
    public async Task GetAll_WithNoItems_ReturnsEmptyList()
    {
        var items = await _pylonItemRepository.GetAll(1, 10);
        items.Should().BeEmpty();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(500)]
    public async Task GetAll_WithMultipleItems_ReturnsAllItems(int numItems)
    {
        // Arrange
        var pylonItems = CreatePylonItems(numItems);
        _pylonItemRepository.AddRange(pylonItems);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Act
        var items = await _pylonItemRepository.GetAll(1, numItems);

        // Assert
        items.Should().HaveCount(numItems);

        // Cleanup
        _pylonItemRepository.RemoveRange(pylonItems);
        await _pylonItemRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetById_WithNoItems_ReturnsNull()
    {
        var item = await _pylonItemRepository.GetById(Guid.NewGuid());
        item.Should().BeNull();
    }

    [Fact]
    public async Task GetById_WithItem_ReturnsItem()
    {
        // Arrange
        var pylonItem = CreatePylonItem(null, null, null, null, null, null, null, null, null);
        _pylonItemRepository.Add(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Act
        var item = await _pylonItemRepository.GetById(pylonItem.Id);

        // Assert
        item.Should().NotBeNull();
        item.Should().BeEquivalentTo(pylonItem);

        // Cleanup
        _pylonItemRepository.Remove(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Add_WithItem_AddsItem()
    {
        // Arrange
        var pylonItem = CreatePylonItem(null, null, null, null, null, null, null, null, null);

        // Act
        _pylonItemRepository.Add(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Assert
        var item = await _pylonItemRepository.GetById(pylonItem.Id);
        item.Should().NotBeNull();
        item.Should().BeEquivalentTo(pylonItem);

        // Cleanup
        _pylonItemRepository.Remove(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task AddRange_WithItems_AddsItems()
    {
        // Arrange
        var pylonItems = CreatePylonItems(10);

        // Act
        _pylonItemRepository.AddRange(pylonItems);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Assert
        var items = await _pylonItemRepository.GetAll(1, 10);
        items.Should().HaveCount(10);

        // Cleanup
        _pylonItemRepository.RemoveRange(pylonItems);
        await _pylonItemRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Remove_WithItem_RemovesItem()
    {
        // Arrange
        var pylonItem = CreatePylonItem(null, null, null, null, null, null, null, null, null);
        _pylonItemRepository.Add(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Act
        _pylonItemRepository.Remove(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Assert
        var item = await _pylonItemRepository.GetById(pylonItem.Id);
        item.Should().BeNull();
    }

    [Fact]
    public async Task GetByCode_WithNoItems_ReturnsNull()
    {
        var item = await _pylonItemRepository.GetByCode("code");
        item.Should().BeNull();
    }

    [Fact]
    public async Task GetByCode_WithItem_ReturnsItem()
    {
        // Arrange
        var pylonItem = CreatePylonItem(null, null, "code", null, null, null, null, null, null);
        _pylonItemRepository.Add(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Act
        var item = await _pylonItemRepository.GetByCode("code");

        // Assert
        item.Should().NotBeNull();
        item.Should().BeEquivalentTo(pylonItem);

        // Cleanup
        _pylonItemRepository.Remove(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByName_WithNoItems_ReturnsEmptyList()
    {
        var items = await _pylonItemRepository.GetByName("name");
        items.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByName_WithItem_ReturnsItem()
    {
        // Arrange
        var pylonItem = CreatePylonItem(null, null, null, "name", null, null, null, null, null);
        _pylonItemRepository.Add(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Act
        var item = await _pylonItemRepository.GetByName("name");

        // Assert
        item.Should().NotBeNull();
        item.Should().Equal(pylonItem);

        // Cleanup
        _pylonItemRepository.Remove(pylonItem);
        await _pylonItemRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetCount_WithNoItems_ReturnsZero()
    {
        var count = await _pylonItemRepository.GetCount();
        count.Should().Be(0);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(500)]
    public async Task GetCount_WithMultipleItems_ReturnsCount(int numItems)
    {
        // Arrange
        var pylonItems = CreatePylonItems(numItems);
        _pylonItemRepository.AddRange(pylonItems);
        await _pylonItemRepository.UnitOfWork.Commit();

        // Act
        var count = await _pylonItemRepository.GetCount();

        // Assert
        count.Should().Be(numItems);

        // Cleanup
        _pylonItemRepository.RemoveRange(pylonItems);
        await _pylonItemRepository.UnitOfWork.Commit();
    }
}