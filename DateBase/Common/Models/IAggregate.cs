namespace CorectMyQuran.DateBase.Common.Models;

public interface IAggregate
{
    IEnumerable<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}