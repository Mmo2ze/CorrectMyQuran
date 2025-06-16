using MediatR;

namespace CorectMyQuran.DateBase.Common.Models;

/**
 * This is DomainEvent Rolls
 * Any Aggregate Root should implement this interface
 * it is used to store the domain events that are raised by the aggregate root
 * DomainEvents are used to communicate between aggregates
 * If DomainEvent Failed to be processed, the Aggregate should be rolled back
 * DomainEvent should be processed in the same transaction as the Aggregate
 **/
public  record DomainEvent(Guid Id = new() ):INotification;