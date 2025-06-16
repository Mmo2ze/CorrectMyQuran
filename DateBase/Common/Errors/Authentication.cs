namespace CorectMyQuran.DateBase.Common.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error InvalidCredentials => Error.Unauthorized("Auth.InvalidCredentials", "Invalid credentials");
        public static Error InvalidCode => Error.Unauthorized("Auth.InvalidCode", "Invalid code");

        public static Error CodeExpired(DateTime date) =>
            Error.Unauthorized("Auth.CodeExpired", $"Code expired at {date}");

        public static Error UnauthorizedToBeAdmin =>
            Error.Unauthorized("Auth.UnauthorizedTobeAdmin", "You are not authorized to be an admin");

        public static Error ToManyTry => Error.Unauthorized("Auth.ToManyTry", "You have tried too many times");
        public static Error PhoneAlreadyExits => Error.Conflict("Auth.PhoneAlreadyExits", "Phone already exists");
        public static Error EmailAlreadyExists => Error.Conflict("Auth.EmailAlreadyExists", "Email already exists");
        public static Error YouAreNotAdmin => Error.Unauthorized("Auth.YouAreNotAdmin", "You are not an admin");

        public static Error NotTeacherOrAssistant =>
            Error.Unauthorized("Auth.NotTeacherOrAssistant", "You are not a teacher or assistant");
    }


    public static class Quiz
    {
        public static Error NotFound => Error.NotFound("Quiz.NotFound", "Quiz not found");
    }

    public static class Parnet
    {
        public static Error NotFound => Error.NotFound("Parent.NotFound", "Parent not found");
        public static Error  PhoneAlreadyExists => Error.Conflict("Parent.PhoneAlreadyExists", "Phone already exists by other parent");
    }

    public static class Assistant
    {
        public static Error NotFound => Error.NotFound("Assistant.NotFound", "Assistant not found");
        public static Error PhoneAlreadyExists => Error.Conflict("Assistant.PhoneAlreadyExists", "Phone already exists by other assistant");
        public static Error EmailAlreadyExists => Error.Conflict("Assistant.EmailAlreadyExists", "Email already exists by other assistant");
    }

    public class Admin
    {
        public static Error NameIsAlreadyInUse => Error.Conflict("Admin.NameIsAlreadyInUse", "Name is already in use");
        public static Error EmailIsAlreadyInUse => Error.Conflict("Admin.EmailIsAlreadyInUse", "Email is already in use");
    }

    public class Bus
    {
        public static Error NotFound => Error.NotFound("Bus.NotFound", "Bus not found");
        public static Error ThisEndTimeIsAlreadyTaken => Error.Conflict("Bus.ThisEndTimeIsAlreadyTaken", "This end time is already taken");

        public static Error NoSeatsLeft => Error.Conflict("Bus.NoSeatsLeft", "No seats left");
    }

    public class Trip
    {
        public static Error BusIsNotAvailable => Error.Conflict("Trip.BusIsNotAvailable", "Bus is not available");
        public static Error CantBookTwice => Error.Conflict("Trip.CantBookTwice", "You can't book twice");
        public static Error NotFound => Error.NotFound("Trip.NotFound", "Trip not found");
        public static Error CantCancel => Error.Conflict("Trip.CantCancel", "You can't cancel this trip");
        public static Error YouCanCancelOnlyOneTripPerDay => Error.Conflict("Trip.YouCanCancelOnlyOneTripPerDay", "You can cancel only one trip per day");
        public static Error BusIsFull => Error.Conflict("Trip.BusIsFull", "Bus is full");
        public static Error InvalidStatus => Error.Conflict("Trip.InvalidStatus", "Invalid status");
        public static Error SameBus => Error.Conflict("Trip.SameBus", "You can't change to the same bus");
        public static Error CantRequestForInvalidTrip => Error.Conflict("Trip.CantRequestForInvalidTrip", "You can't request for invalid trip");
        public static Error CantRequestForSameBus => Error.Conflict("Trip.CantRequestForSameBus", "You can't request for the same bus");
        public static Error CantRequestForExpiredBus => Error.Conflict("Trip.CantRequestForExpiredBus", "You can't request for expired bus");
    }

    public class Station
    {
        public static Error ThisNameIsAlreadyTakenAndTime => Error.Conflict("Station.ThisNameIsAlreadyTakenAndTime", "This name is already taken and time");
        public static Error NotFound => Error.NotFound("Station.NotFound", "Station not found");
    }
}