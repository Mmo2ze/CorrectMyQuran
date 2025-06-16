using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Sheikh.Domain;


public record SheikhId(string Value) : ValueObjectId<SheikhId>(Value)
{

}