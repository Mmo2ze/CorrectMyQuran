namespace CorectMyQuran.Application.Variables;

public static class Roles
{
    public const string CodeSent = "CodeSent";

    public static class Admin
    {
        public const string AdminNonRegister = "AdminNonRegister";
        public const string AdminCodeSent = "AdminCodeSent";
        public const string Role = "Admin";
    }
    public static class Student
    {
        public const string StudentNonRegister = "StudentNonRegister";
        public const string StudentCodeSent = "StudentCodeSent";
        public const string Role = "Student";
    }
    public static class Teacher
    {
        public const string TeacherNonRegister = "TeacherNonRegister";
        public const string Role = "Teacher";
        public const string TeacherCodeSent = "TeacherCodeSent";

    }
		
    public static class Parent
    {
        public const string ParentNonRegister = "ParentNonRegister";
        public  const string ParentCodeSent = "ParentCodeSent";
        public const string Role = "Parent";
    }
    public static class Assistant
    {
        public const string AddStudent = "AddStudent";
        public const string AddGroup = "AddSession";
        public const string RecordPayment = "RegisterPayment";
        public const string ViewPayments = "ViewPayments";
        public const string RecordAttendance = "RecordAttendance";
        public const string AddCardOrder ="AddCardOrder";
        public const string Role = "Assistant";
        public const string AddQuiz = "AddQuiz";
        public const string AddHoliday = "AddHoliday";
        public const string ScheduleAttendance = "ScheduleAttendance";
    }
		
}