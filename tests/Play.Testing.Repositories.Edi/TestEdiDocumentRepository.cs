using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Edi;

public class TestEdiDocumentRepository
{
    private readonly IEdiDocumentRepository _ediDocumentRepository;

    public TestEdiDocumentRepository()
    {
        var services = SetupRunner.Setup();
        _ediDocumentRepository = services.GetRequiredService<IEdiDocumentRepository>();
    }

    private static EdiDocument CreateFooEdiDocument(Guid? id = null, Guid? customer_Id = null, string? title = null,
        string? ediPayload = null, string? documentPayload = null,
        string? hedentid = null, bool? isProcessed = false, bool? isSent = false, DateTime? created_At = null)
    {
        // Create a new FooEdiDocument object with the specified or fallback values
        var fooEdiDocument = new EdiDocument(
            id ?? Guid.NewGuid(),
            customer_Id ?? Guid.NewGuid(),
            title ?? Guid.NewGuid().ToString(),
            ediPayload ?? Guid.NewGuid().ToString(),
            documentPayload ?? Guid.NewGuid().ToString(),
            hedentid ?? Guid.NewGuid().ToString(),
            isProcessed ?? false,
            isSent ?? false,
            created_At ?? DateTime.UtcNow
        );

        return fooEdiDocument;
    }


    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectObject_ForValidId()
    {
        // Create a new FooEdiDocument object with a specified ID
        var fooEdiDocument = CreateFooEdiDocument(new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified ID
        var result = await _ediDocumentRepository.GetByIdAsync(fooEdiDocument.Id);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Id.Should().Be(fooEdiDocument.Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
    }


    [Fact]
    public async Task GetByTitleAsync_ReturnsCorrectObjects_ForValidTitle()
    {
        // Create a new FooEdiDocument object with a specified title
        var fooEdiDocument = CreateFooEdiDocument(title: "ΤΙΠ-0000019815");

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified title
        var result = await _ediDocumentRepository.GetByTitleAsync(fooEdiDocument.Title);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Title.Should().Be(fooEdiDocument.Title);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }


    [Fact]
    public async Task GetByHedentid_ReturnsCorrectObjects_ForValidHedentid()
    {
        // Create a new FooEdiDocument object with a specified hedentid
        var fooEdiDocument = CreateFooEdiDocument(hedentid: "ΤΙΠ-0000019815");

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified hedentid
        var result = await _ediDocumentRepository.GetByHedentidAsync(fooEdiDocument.Hedentid);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Hedentid.Should().Be(fooEdiDocument.Hedentid);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByIsProcessed_ReturnsCorrectObjects_ForValidIsProcessed()
    {
        // Create a new FooEdiDocument object with a specified isProcessed
        var fooEdiDocument = CreateFooEdiDocument(isProcessed: true);

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified isProcessed
        var result = await _ediDocumentRepository.GetByIsProcessedAsync(fooEdiDocument.IsProcessed);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().IsProcessed.Should().Be(fooEdiDocument.IsProcessed);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByIsProccesedAndCustomerId_ReturnsCorrectObjects_ForValidIsProcessedAndCustomerId()
    {
        // Create a new FooEdiDocument object with a specified isProcessed and customer_Id
        var fooEdiDocument = CreateFooEdiDocument(isProcessed: true,
            customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified isProcessed and customer_Id
        var result =
            await _ediDocumentRepository.GetByIsProcessedAndCustomerAsync(fooEdiDocument.IsProcessed,
                fooEdiDocument.Customer_Id);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().IsProcessed.Should().Be(fooEdiDocument.IsProcessed);
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByIsSent_ReturnsCorrectObjects_ForValidIsSent()
    {
        // Create a new FooEdiDocument object with a specified isSent
        var fooEdiDocument = CreateFooEdiDocument(isSent: true);

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified isSent
        var result = await _ediDocumentRepository.GetByIsSentAsync(fooEdiDocument.IsSent);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().IsSent.Should().Be(fooEdiDocument.IsSent);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByIsSentAndCustomerId_ReturnsCorrectObjects_ForValidIsSentAndCustomerId()
    {
        // Create a new FooEdiDocument object with a specified isSent and customer_Id
        var fooEdiDocument =
            CreateFooEdiDocument(isSent: true, customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified isSent and customer_Id
        var result =
            await _ediDocumentRepository.GetByIsSentAndCustomerIdAsync(fooEdiDocument.IsSent,
                fooEdiDocument.Customer_Id);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().IsSent.Should().Be(fooEdiDocument.IsSent);
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByCustomerId_ReturnsCorrectObjects_ForValidCustomerId()
    {
        // Create a new FooEdiDocument object with a specified customer_Id
        var fooEdiDocument = CreateFooEdiDocument(customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified customer_Id
        var result = await _ediDocumentRepository.GetByCustomerIdAsync(fooEdiDocument.Customer_Id);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetAllWithPaginationAndCustomerId_ReturnsCorrectObjects_ForValidCustomerId()
    {
        // Create a new FooEdiDocument object with a specified customer_Id
        var fooEdiDocument = CreateFooEdiDocument(customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified customer_Id
        var result = await _ediDocumentRepository.GetAllWithPaginationByCustomerIdAsync(fooEdiDocument.Customer_Id);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetAllWithPagination_ReturnsCorrectObjects_ForValidCustomerId()
    {
        // Create a new FooEdiDocument object with a specified customer_Id
        var fooEdiDocument = CreateFooEdiDocument(customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified customer_Id
        var result = await _ediDocumentRepository.GetAllWithPaginationAsync();

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetAllWithDateRange_ReturnsCorrectObjects_ForValidDateRange()
    {
        // Create a new FooEdiDocument object with a specified customer_Id
        var fooEdiDocument = CreateFooEdiDocument(customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified customer_Id
        var result =
            await _ediDocumentRepository.GetAllWithDateRangeAsync(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetAllWithDateRangeAndCustomerId_ReturnsCorrectObjects_ForValidDateRange()
    {
        // Create a new FooEdiDocument object with a specified customer_Id
        var fooEdiDocument = CreateFooEdiDocument(customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified customer_Id
        var result = await _ediDocumentRepository.GetAllWithDateRangeAndCustomerIdAsync(DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1), fooEdiDocument.Customer_Id);

        // Use fluent assertions to verify the returned object
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Customer_Id.Should().Be(fooEdiDocument.Customer_Id);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetTotalCountByCustomerId_ReturnsCorrectObjects_ForValidCustomerId()
    {
        // Create a new FooEdiDocument object with a specified customer_Id
        var fooEdiDocument = CreateFooEdiDocument(customer_Id: new Guid("DEA1AD7A-CC3C-4666-AF7D-00A96F49280A"));

        // Add the FooEdiDocument object to the EdiDocumentRepository
        _ediDocumentRepository.Register(fooEdiDocument);
        //commit changes to the database
        await _ediDocumentRepository.UnitOfWork.Commit();

        // Get the FooEdiDocument object from the EdiDocumentRepository using the specified customer_Id
        var result = await _ediDocumentRepository.GetTotalCountByCustomerIdAsync(fooEdiDocument.Customer_Id);

        // Use fluent assertions to verify the returned object
        result.Should().Be(1);

        // Clean up
        _ediDocumentRepository.Remove(fooEdiDocument);
        await _ediDocumentRepository.UnitOfWork.Commit();
    }
}