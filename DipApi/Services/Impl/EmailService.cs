using System.Net;
using System.Net.Mail;


namespace DipApi.Services.Impl
{
	public class EmailService : IEmailService
	{
		public async void SendEmailAsync(string email, string body)
		{
			var from = new MailAddress("testingemailsender@mail.ru");
			var to = new MailAddress("s3rg0sh4@gmail.com"); //email
			var m = new MailMessage(from, to)
			{
				Subject = "Тест",
				Body = body
			};
			var smtp = new SmtpClient("smtp.mail.ru", 587)
			{
				Credentials = new NetworkCredential("testingemailsender@mail.ru", "TKLTv5272v2cZsZxJgwE"),
				EnableSsl = true
			};
			await smtp.SendMailAsync(m);
		}
	}
}
