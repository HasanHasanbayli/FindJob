using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace FindJob.Service;

public class EmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        MimeMessage emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("FiorelloC", "c.cavid123.d@mail.ru"));
        emailMessage.To.Add(new MailboxAddress(string.Empty, email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };

        using (SmtpClient client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync("c.cavid123.d@mail.ru", "IA2UoIuory6_");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}