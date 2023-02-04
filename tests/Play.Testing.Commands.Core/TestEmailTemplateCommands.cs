using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;
using Play.Testing.Setup.Runner;
using Xunit;

namespace Play.Testing.Commands.Core;

public class TestEmailTemplateCommands
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IMediatorHandler _mediator;

    public TestEmailTemplateCommands()
    {
        var services = SetupRunner.Setup();
        _emailTemplateRepository = services.GetService<IEmailTemplateRepository>() ??
                                   throw new ArgumentNullException(nameof(IEmailTemplateRepository));
        _mediator = services.GetService<IMediatorHandler>() ??
                    throw new ArgumentNullException(nameof(IMediatorHandler));
    }

    [Fact]
    public async Task RegisterEmailTemplate_WithValidCommand_CreatesTemplate()
    {
        // Arrange
        var command =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "foosubhect", "foobdy"));

        // Act
        await _mediator.SendCommand(command);

        // Assert
        var template = await _emailTemplateRepository.FindByIdAsync(command.EmailTemplate.Id);

        template.Should()
            .NotBeNull()
            .And
            .BeOfType<EmailTemplate>()
            .And
            .Match<EmailTemplate>(t => t.Id == command.EmailTemplate.Id);
    }

    [Fact]
    public async Task RegisterEmailTemplate_WithInvalidName_ThrowsValidationError()
    {
        // Arrange
        var command =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "s", "foosubhect", "foobdy"));

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.Should()
            .NotBeNull()
            .And
            .BeOfType<ValidationResult>()
            .And
            .Match<ValidationResult>(r =>
                r.Errors.Any(e => e.ErrorMessage == "Name must be between 2 and 100 characters"));
    }

    [Fact]
    public async Task RegisterEmailTemplate_WithInvalidSubject_ThrowsValidationError()
    {
        // Arrange
        var command = new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "s", "foobdy"));

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.Should()
            .NotBeNull()
            .And
            .BeOfType<ValidationResult>()
            .And
            .Match<ValidationResult>(r =>
                r.Errors.Any(e => e.ErrorMessage == "Subject must be between 2 and 100 characters"));
    }

    [Fact]
    public async Task RegisterEmailTemplate_WithInvalidBody_ThrowsValidationError()
    {
        // Arrange
        var command =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "foosubhect", ""));

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.Should()
            .NotBeNull()
            .And
            .BeOfType<ValidationResult>()
            .And
            .Match<ValidationResult>(r => r.Errors.Any(e => e.ErrorMessage == "Body is required"));
    }

    [Fact]
    public async Task UpdateEmailTemplate_WithValidCommand_UpdatesTemplate()
    {
        // Arrange
        var regCommand =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "foosubhect", "foobdy"));
        var command =
            new UpdateEmailTemplateCommand(new EmailTemplate(regCommand.EmailTemplate.Id, "fooname2", "foosubhect2",
                "foobdy2"));

        // Act
        await _mediator.SendCommand(command);

        // Assert
        var updatedTemplate = await _emailTemplateRepository.FindByIdAsync(command.EmailTemplate.Id);

        updatedTemplate.Should()
            .NotBeNull()
            .And
            .BeOfType<EmailTemplate>()
            .And
            .Match<EmailTemplate>(t => t.Id == command.EmailTemplate.Id)
            .And
            .Match<EmailTemplate>(t => t.Name == "fooname2")
            .And
            .Match<EmailTemplate>(t => t.Subject == "foosubhect2")
            .And
            .Match<EmailTemplate>(t => t.Body == "foobdy2");
    }

    [Fact]
    public async Task UpdateEmailTemplate_WithInvalidName_ThrowsValidationError()
    {
        // Arrange
        var regCommand =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "foosubhect", "foobdy"));
        var command =
            new UpdateEmailTemplateCommand(
                new EmailTemplate(regCommand.EmailTemplate.Id, "s", "foosubhect2", "foobdy2"));

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.Should()
            .NotBeNull()
            .And
            .BeOfType<ValidationResult>()
            .And
            .Match<ValidationResult>(r =>
                r.Errors.Any(e => e.ErrorMessage == "Name must be between 2 and 100 characters"));
    }

    [Fact]
    public async Task UpdateEmailTemplate_WithInvalidSubject_ThrowsValidationError()
    {
        // Arrange
        var regCommand =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "foosubhect", "foobdy"));
        var command =
            new UpdateEmailTemplateCommand(new EmailTemplate(regCommand.EmailTemplate.Id, "fooname2", "s", "foobdy2"));

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.Should()
            .NotBeNull()
            .And
            .BeOfType<ValidationResult>()
            .And
            .Match<ValidationResult>(r =>
                r.Errors.Any(e => e.ErrorMessage == "Subject must be between 2 and 100 characters"));
    }

    [Fact]
    public async Task RemoveEmailTemplate_WithValidCommand_RemovesTemplate()
    {
        // Arrange
        var regCommand =
            new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), "fooname", "foosubhect", "foobdy"));
        var command = new RemoveEmailTemplateCommand(regCommand.EmailTemplate.Id);

        // Act
        await _mediator.SendCommand(command);

        // Assert
        var template = await _emailTemplateRepository.FindByIdAsync(command.Id);

        template.Should()
            .BeNull();
    }

    [Fact]
    public async Task RemoveEmailTemplate_WithInvalidId_ThrowsValidationError()
    {
        // Arrange
        var command = new RemoveEmailTemplateCommand(Guid.NewGuid());

        // Act
        var res = await _mediator.SendCommand(command);

        // Assert
        res.Should()
            .NotBeNull()
            .And
            .BeOfType<ValidationResult>()
            .And
            .Match<ValidationResult>(r => r.Errors.Any(e => e.ErrorMessage == "Email template not found"));
    }
}