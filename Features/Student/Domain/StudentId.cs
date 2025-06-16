using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Student.Domain;

public record StudentId(string Value) : ValueObjectId<StudentId>(Value);