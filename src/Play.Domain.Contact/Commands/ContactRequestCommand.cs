using NetDevPack.Messaging;
using Play.Domain.Contact.Commands.Validations;

namespace Play.Domain.Contact.Commands;

/// <summary>
///     Contact request command is used for sending contact requests to the sales team.
/// </summary>
public class ContactRequestCommand : Command
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}

public class RegisterContactRequestCommand : ContactRequestCommand
{
    public RegisterContactRequestCommand(string email, string subject, string message, string phoneNumber)
    {
        Id = Guid.NewGuid();
        Email = email;
        Subject = subject;
        Message = message;
        PhoneNumber = phoneNumber;
        CreatedOn = DateTime.Now;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterContactRequestValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}