using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Repositories.Pylon;

[Collection("Pylon Invoice Repository Tests")]
public class TestPylonInvoicesRepository
{
    private readonly IPylonInvoiceRepository _pylonInvoiceRepository;

    public TestPylonInvoicesRepository()
    {
        var services = SetupRunner.Setup();
        _pylonInvoiceRepository = services.GetService<IPylonInvoiceRepository>() ??
                                  throw new ArgumentNullException(nameof(IPylonInvoiceRepository));
    }

    private PylonInvoice CreateInvoice(Guid? id = null, string? invoiceNumber = null, string? invoiceCode = null,
        DateTime? invoiceDate = null, string? paymentMethod = null,
        decimal? totalAmountNoTax = null, decimal? totalAmountWithTax = null, decimal? totalVat = null,
        string? customerTin = null, string? customerName = null,
        string? vatRegime = null, string? eipUrl = null, List<PylonInvoiceLine>? invoiceLines = null)
    {
        return new PylonInvoice(
            id ?? Guid.NewGuid(),
            invoiceNumber ?? "123456789",
            invoiceCode ?? "123456789",
            invoiceDate ?? DateTime.Now,
            paymentMethod ?? "Cash",
            totalAmountNoTax ?? 100,
            totalAmountWithTax ?? 100,
            totalVat ?? 100,
            customerTin ?? "123456789",
            customerName ?? "Test Customer",
            vatRegime ?? "VAT",
            eipUrl ?? "https://www.google.com",
            invoiceLines ?? new List<PylonInvoiceLine>());
    }

    private List<PylonInvoice> CreateInvoices(int count)
    {
        var invoices = new List<PylonInvoice>();
        for (var i = 0; i < count; i++) invoices.Add(CreateInvoice());
        return invoices;
    }


    [Fact]
    public async Task GetAll_WithNoInvoices_ReturnsEmptyList()
    {
        var invoices = await _pylonInvoiceRepository.GetAll(1, 100);
        invoices.Should().BeEmpty();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetAll_WithInvoices_ReturnsAllInvoices(int count)
    {
        var invoices = CreateInvoices(count);
        _pylonInvoiceRepository.AddRange(invoices);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetAll(1, count);

        result.Should().HaveCount(count);

        //cleanup
        _pylonInvoiceRepository.RemoveRange(invoices);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetById_WithNoInvoices_ReturnsNull()
    {
        var invoice = await _pylonInvoiceRepository.GetById(Guid.NewGuid());
        invoice.Should().BeNull();
    }

    [Fact]
    public async Task GetById_WithInvoices_ReturnsInvoice()
    {
        var invoice = CreateInvoice();
        _pylonInvoiceRepository.Add(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetById(invoice.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(invoice);

        //cleanup
        _pylonInvoiceRepository.Remove(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task GetByCustomerTin_WithNoInvoices_ReturnsEmptyList()
    {
        var invoices = await _pylonInvoiceRepository.GetByCustomerTin("123456789");
        invoices.Should().BeEmpty();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetByCustomerTin_WithInvoices_ReturnsInvoices(int count)
    {
        var invoices = CreateInvoices(count);
        _pylonInvoiceRepository.AddRange(invoices);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetByCustomerTin(invoices.First().CustomerTin);

        result.Should().HaveCount(count);

        //cleanup
        _pylonInvoiceRepository.RemoveRange(invoices);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Add_WithNoInvoices_AddsInvoice()
    {
        var invoice = CreateInvoice();
        _pylonInvoiceRepository.Add(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetById(invoice.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(invoice);

        //cleanup
        _pylonInvoiceRepository.Remove(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task AddRange_WithNoInvoices_AddsInvoices()
    {
        var invoices = CreateInvoices(100);
        _pylonInvoiceRepository.AddRange(invoices);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetAll(1, 100);

        result.Should().HaveCount(100);

        //cleanup
        _pylonInvoiceRepository.RemoveRange(invoices);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Update_WithNoInvoices_ReturnsNull()
    {
        var invoice = CreateInvoice();
        _pylonInvoiceRepository.Add(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
        invoice.CustomerTin = "987654321";
        _pylonInvoiceRepository.Update(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetById(invoice.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(invoice);

        //cleanup
        _pylonInvoiceRepository.Remove(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
    }

    [Fact]
    public async Task Remove_WithNoInvoices_ReturnsNull()
    {
        var invoice = CreateInvoice();
        _pylonInvoiceRepository.Add(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();
        _pylonInvoiceRepository.Remove(invoice);
        await _pylonInvoiceRepository.UnitOfWork.Commit();

        var result = await _pylonInvoiceRepository.GetById(invoice.Id);

        result.Should().BeNull();
    }
}