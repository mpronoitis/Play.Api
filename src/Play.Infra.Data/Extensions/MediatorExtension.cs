namespace Play.Infra.Data.Extensions;

public static class MediatorExtension
{
    /// <summary>
    ///     This method is an extension method for the IMediatorHandler interface that is used to publish a list of domain
    ///     events using an IMediatorHandler.
    ///     The method takes a single argument, ctx, which is a context object that is used to track changes to entities in a
    ///     database.
    ///     The method starts by querying the change tracker for all entities that have domain events associated with them
    ///     using the Entries
    ///     <Entity>
    ///         method and the Where clause.
    ///         It then flattens the list of domain events into a single list using the SelectMany method, and clears the
    ///         domain events from the entities using the ClearDomainEvents method.
    ///         Finally, the method uses the Select method to create a list of tasks that represent the asynchronous publishing
    ///         of each domain event using the PublishEvent method of the IMediatorHandler interface.
    ///         The method then waits for all of these tasks to complete using the Task.WhenAll method before returning.
    ///         Overall, this method provides a way to asynchronously publish a list of domain events using an IMediatorHandler
    ///         and clear the domain events from the entities after they have been published.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="ctx">The context.</param>
    /// <typeparam name="T">The type of the context.</typeparam>
    public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async domainEvent => { await mediator.PublishEvent(domainEvent); });

        await Task.WhenAll(tasks);
    }
}