using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Core.Events;

/**
 * Current we only handle the Update event since the other events are not relevant
 */
public class UserProfileEventHandler : INotificationHandler<UserProfileUpdatedEvent>
{
    public Task Handle(UserProfileUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class UserProfileUpdatedEvent : Event
{
    public UserProfileUpdatedEvent(Guid id, Guid user_Id, string firstName, string lastName, DateTime dateOfBirth,
        string companyName)
    {
        Id = id;
        User_Id = user_Id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CompanyName = companyName;
    }

    public Guid Id { get; }
    public Guid User_Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateOfBirth { get; }
    public string CompanyName { get; }
}