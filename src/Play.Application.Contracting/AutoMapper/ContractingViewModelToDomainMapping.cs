using AutoMapper;
using Play.Application.Contracting.ViewModels;
using Play.Domain.Contracting.Commands;
using Play.Domain.Contracting.Models;

namespace Play.Application.Contracting.AutoMapper;

public class ContractingViewModelToDomainMapping
{
    public static void RegisterMappings(Profile profile)
    {
        profile.CreateMap<ContractViewModel, RegisterContractCommand>()
            .ConstructUsing(c => new RegisterContractCommand(new Contract(Guid.NewGuid(), "", c.ClientName, c.ClientTin,
                c.ItemName, c.Status, c.StartDate, c.EndDate, DateTime.Now, c.ClientId, c.ItemId)));
        profile.CreateMap<UpdateContractViewModel, UpdateContractCommand>()
            .ConstructUsing(c => new UpdateContractCommand(new Contract(c.Id, c.ContractCode, c.ClientName, c.ClientTin,
                c.ItemName,
                c.Status, c.StartDate, c.EndDate, c.CreatedAt, c.ClientId, c.ItemId)));
    }
}