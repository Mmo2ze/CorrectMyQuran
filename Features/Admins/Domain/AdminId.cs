using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Admins.Domain;

public record AdminId(string Value) : ValueObjectId<AdminId>(Value)
{

}