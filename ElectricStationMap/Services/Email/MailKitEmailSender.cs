using MailKitSimplified.Sender.Models;
using MailKitSimplified.Sender.Services;
using Microsoft.Extensions.Configuration;

namespace ElectricStationMap.Services.Email
{
	public class MailKitEmailSender : IEmailSender
	{
		public async Task SendEmailAsync(string email, string title, string message)
		{
			var confuguration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			var emailSenderOptions = confuguration
				.GetRequiredSection(EmailSenderOptions.SectionName)
				.Get<EmailSenderOptions>();

			var smtpSender = SmtpSender.Create(emailSenderOptions);

			await smtpSender.WriteEmail
				.From("gorbachevAndrey0202@yandex.ru")
				.Bcc("gorbachevAndrey0202@yandex.ru")
				.Subject(title)
				.BodyHtml($"<p>{message}</p>")
				.To(email)
				.SendAsync();
		}
	}
}
