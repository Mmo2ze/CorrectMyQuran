using CorectMyQuran.DateBase;
using MediatR;

namespace CorectMyQuran.Application.Behaviors;

public class UnitOfWorkBehavior<TRequest,TResponse>: 
    IPipelineBehavior<TRequest,TResponse> 
    where TRequest : notnull 
{
    private readonly MainContext _mainContext;
    public UnitOfWorkBehavior(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(IsNotCommand())
        {
            return await next();
        }
        var response = await next();
        await _mainContext.SaveChangesAsync(cancellationToken);
        return response;
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}