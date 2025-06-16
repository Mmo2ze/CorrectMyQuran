using System.ComponentModel.DataAnnotations.Schema;

namespace CorectMyQuran.DateBase.Common.Models;

public abstract class Aggregate<TId> : IAggregate
{
    private readonly List<DomainEvent> _domainEvents = [];

    protected Aggregate(TId id)
    {
        Id = id;
    }

    protected Aggregate()
    {
    }

    public TId Id { get; set; }

    [NotMapped] public IEnumerable<DomainEvent> DomainEvents => _domainEvents;


    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RaiseDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}