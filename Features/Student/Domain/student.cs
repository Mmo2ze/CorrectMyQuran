using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Student.Domain;

public class Student:Aggregate<StudentId>
{
    public StudentId Id { get; private set; }

    public string Name { get; private set; }

    public string Phone { get; private set; }
    
    public string Password { get; private set; }

    private Student() { } // Required for EF Core
    public Student(StudentId id, string name, string password, string phone):base(id)
    {
        Name = name;
        Password = password;
        Phone = phone;
    }
    
}