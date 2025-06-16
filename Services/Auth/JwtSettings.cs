namespace CorectMyQuran.Services.Auth;

public class JwtSettings
{
	
	public static string Issuer  = DotNetEnv.Env.GetString("ISSUER");
	public static string Secret  = DotNetEnv.Env.GetString("SECRET");
	public static string Audience  = DotNetEnv.Env.GetString("AUDIENCE");
	public static int ExpireDays  = 30;
	public static int ExpireMinutes  = 60;
}