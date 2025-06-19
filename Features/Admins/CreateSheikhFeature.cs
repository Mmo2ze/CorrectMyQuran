using CorectMyQuran.DateBase;
using CorectMyQuran.Features.Auth.Common;
using CorectMyQuran.Features.Sheikh.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CorectMyQuran.Features.Admins;

public record CreateSheikhCommand(
    string Name,
    string Phone) : IRequest<ErrorOr<CreateSheikhResult>>;

public record CreateSheikhResult(string Name,string Phone,string Password , SheikhId Id);
public class CreateSheikhCommandHandler(MainContext context) : IRequestHandler<CreateSheikhCommand, ErrorOr<CreateSheikhResult>>
{


    public async Task<ErrorOr<CreateSheikhResult>> Handle(CreateSheikhCommand request, CancellationToken cancellationToken)
    {
        var result = Sheikh.Domain.Sheikh.CreateSheikh(request.Name, request.Phone);
        context.Sheikhs.Add(result.Item1);
        
        return new CreateSheikhResult(result.Item1.Name, result.Item1.Phone, result.password, result.Item1.Id);
    }
}