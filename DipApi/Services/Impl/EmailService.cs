using System.Net;
using System.Net.Mail;


namespace DipApi.Services.Impl
{
	public class EmailService : IEmailService
	{
		private static async Task SendEmailAsync()
		{
			MailAddress from = new MailAddress("somemail@gmail.com", "Tom");
			MailAddress to = new MailAddress("somemail@yandex.ru");
			MailMessage m = new MailMessage(from, to);
			m.Subject = "Тест";
			m.Body = "Письмо-тест 2 работы smtp-клиента";
			SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
			smtp.Credentials = new NetworkCredential("somemail@gmail.com", "mypassword");
			smtp.EnableSsl = true;
			await smtp.SendMailAsync(m);
			Console.WriteLine("Письмо отправлено");
		}
	}
}
