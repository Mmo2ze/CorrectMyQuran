using CorectMyQuran.Application.Mapping;
using CorectMyQuran.DateBase;
using CorectMyQuran.DateBase.Common.Models;
using MediatR;

namespace CorectMyQuran.Features.Admins;

public record GetSheikhsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<ErrorOr<PaginatedList<Sheikh.Domain.Sheikh>>>;

public class GetSheikhsQueryHandler(MainContext context)
    : IRequestHandler<GetSheikhsQuery, ErrorOr<PaginatedList<Sheikh.Domain.Sheikh>>>
{
    public async Task<ErrorOr<PaginatedList<Sheikh.Domain.Sheikh>>> Handle(GetSheikhsQuery request, CancellationToken cancellationToken)
    {
        var query = context.Sheikhs.AsQueryable();

       var result = await query.PaginatedListAsync(request.PageNumber, request.PageSize);

       return result;
    }
}