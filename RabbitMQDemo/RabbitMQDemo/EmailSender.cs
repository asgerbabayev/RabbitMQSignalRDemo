using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace RabbitMQDemo;

public static class EmailSender
{
    public static void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine("Email sending...");
        MimeMessage email = new MimeMessage();
        email.Sender = MailboxAddress.Parse("Your email address");
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = $@"<h1 style='text-align:center;'>{subject}</h1></br>
                              <p style='text-align:center;color:blue;'>{body}</p>";
        email.Body = builder.ToMessageBody();
        using (SmtpClient smtp = new())
        {
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("Your email address", "Your password");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        Console.WriteLine("Email sent");
    }
}

