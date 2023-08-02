using App.Data.Entity;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace App.Web.Mvc.Utils
{
    public class EmailSend
    {
        public static async Task SendMailAsync(User user)
        {
            SmtpClient smtpClient = new();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("alanyaliburak0@gmail.com", "Asd1234.");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Siteden","alanyaliburak0@gmail.com"));
            message.To.Add(new MailboxAddress(user.Name,user.Email));
            message.Subject = "Şifre Sıfırlama Talebi";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text= $"Kullanıcı Bilgileri : <hr /> " +
                $"Ad : {user.Name} <hr /> " +
                $"Email : {user.Email} <hr /> " +
                $"Bilgilerine sahip kullanıcı şifre sıfırlama talebiniz alınmıştır.  <hr />" +
                $"Devam etmek için <a href='https://localhost:7276/Auth/UpdatePassword?newPassword={user.Id}' >Buraya</a> tıklayınız."
            };

            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
