namespace CorectMyQuran.Application.Services;

public interface IWhatsappSender
{
	Task<bool> IsValidNumber(string number);
	Task<ErrorOr<string>> Send(string number, string message);
	
	
}