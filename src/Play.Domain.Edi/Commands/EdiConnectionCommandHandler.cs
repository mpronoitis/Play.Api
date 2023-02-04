using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Edi.Events;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Commands;

public class EdiConnectionCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiConnectionCommand, ValidationResult>,
    IRequestHandler<UpdateEdiConnectionCommand, ValidationResult>,
    IRequestHandler<RemoveEdiConnectionCommand, ValidationResult>
{
    private readonly IEdiConnectionRepository _ediConnectionRepository;

    public EdiConnectionCommandHandler(IEdiConnectionRepository ediConnectionRepository)
    {
        _ediConnectionRepository = ediConnectionRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiConnectionCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //create new instance of edi connection
        //@todo don't create if model or customer doesn't exist
        var ediConnection = new EdiConnection(Guid.NewGuid(), request.Customer_Id, request.Model_Id, request.Org_Id,
            request.Profile_Id, request.Ftp_Hostname, request.Ftp_Username, request.Ftp_Password, request.Ftp_Port,
            request.File_Type);

        //add domain event
        ediConnection.AddDomainEvent(new EdiConnectionRegisteredEvent(ediConnection.Id, ediConnection.Customer_Id,
            ediConnection.Model_Id, ediConnection.Org_Id,
            ediConnection.Profile_Id, ediConnection.Ftp_Hostname, ediConnection.Ftp_Username,
            ediConnection.Ftp_Password, ediConnection.Ftp_Port, ediConnection.File_Type));
        //pass to repository
        _ediConnectionRepository.Add(ediConnection);
        //commit unit of work
        return await Commit(_ediConnectionRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiConnectionCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //get edi connection from repository
        var ediConnection = await _ediConnectionRepository.GetByIdAsync(request.Id);
        //if not found return not found
        if (ediConnection is null)
        {
            AddError("edi connection not found");
            return ValidationResult;
        }

        //remove edi connection
        ediConnection.AddDomainEvent(new EdiConnectionRemovedEvent(ediConnection.Id));
        //pass to repository
        _ediConnectionRepository.Remove(ediConnection);
        //commit unit of work
        return await Commit(_ediConnectionRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiConnectionCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //get edi connection from repository
        var ediConnection = await _ediConnectionRepository.GetByIdAsync(request.Id);
        //if not found return not found
        if (ediConnection is null)
        {
            AddError("edi connection not found");
            return ValidationResult;
        }

        ediConnection.Customer_Id = request.Customer_Id;
        ediConnection.Ftp_Hostname = request.Ftp_Hostname;
        ediConnection.Ftp_Password = request.Ftp_Password;
        ediConnection.Ftp_Username = request.Ftp_Username;
        ediConnection.Model_Id = request.Model_Id;
        ediConnection.Org_Id = request.Org_Id;
        ediConnection.Profile_Id = request.Profile_Id;
        ediConnection.Ftp_Port = request.Ftp_Port;
        ediConnection.File_Type = request.File_Type;

        //update edi connection
        ediConnection.AddDomainEvent(new EdiConnectionUpdatedEvent(ediConnection.Id, ediConnection.Customer_Id,
            ediConnection.Model_Id, ediConnection.Org_Id,
            ediConnection.Profile_Id, ediConnection.Ftp_Hostname, ediConnection.Ftp_Username,
            ediConnection.Ftp_Password, ediConnection.Ftp_Port, ediConnection.File_Type));
        //pass to repository
        _ediConnectionRepository.Update(ediConnection);
        //commit unit of work
        return await Commit(_ediConnectionRepository.UnitOfWork);
    }
}