namespace DipApi.Services
{
	public interface IEmailService
	{
		public void SendEmailAsync(string email, string body);
	}
}
