using Company.G03.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.G03.PL.Helpers
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("routec41v02@gmail.com", "hqqfzhuptaqfhzix");

			client.Send("routec41v02@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
