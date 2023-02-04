using FluentValidation;

namespace Play.Domain.Contracting.Commands.Validation;

public class ContractValidation<T> : AbstractValidator<T> where T : ContractCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }

    //clientName
    protected void ValidateClientName()
    {
        RuleFor(c => c.Contract.ClientName)
            .NotEmpty()
            .Length(2, 100);
    }

    //clientTin
    protected void ValidateClientTin()
    {
        RuleFor(c => c.Contract.ClientTin)
            .NotEmpty()
            .Length(10, 12);
    }

    //item name
    protected void ValidateItemName()
    {
        RuleFor(c => c.Contract.ItemName)
            .NotEmpty()
            .Length(2, 100);
    }

    //status
    protected void ValidateStatus()
    {
        RuleFor(c => c.Contract.Status)
            .NotEmpty()
            .Length(2, 100);
    }

    //startDate
    protected void ValidateStartDate()
    {
        RuleFor(c => c.Contract.StartDate)
            .NotEmpty();
    }

    //endDate
    protected void ValidateEndDate()
    {
        RuleFor(c => c.Contract.EndDate)
            .NotEmpty();
    }

    //clientid
    protected void ValidateClientId()
    {
        RuleFor(c => c.Contract.ClientId)
            .NotEqual(Guid.Empty);
    }

    //item id
    protected void ValidateItemId()
    {
        RuleFor(c => c.Contract.ItemId)
            .NotEqual(Guid.Empty);
    }
}

public class RegisterNewContractCommandValidation : ContractValidation<RegisterContractCommand>
{
    public RegisterNewContractCommandValidation()
    {
        ValidateClientName();
        ValidateClientTin();
        ValidateItemName();
        ValidateStatus();
        ValidateStartDate();
        ValidateEndDate();
        ValidateClientId();
        ValidateItemId();
    }
}

public class UpdateContractCommandValidation : ContractValidation<UpdateContractCommand>
{
    public UpdateContractCommandValidation()
    {
        ValidateId();
        ValidateClientName();
        ValidateClientTin();
        ValidateItemName();
        ValidateStatus();
        ValidateStartDate();
        ValidateEndDate();
        ValidateClientId();
        ValidateItemId();
    }
}

public class RemoveContractCommandValidation : ContractValidation<RemoveContractCommand>
{
    public RemoveContractCommandValidation()
    {
        ValidateId();
    }
}