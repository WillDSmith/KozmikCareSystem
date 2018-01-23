using System.Net.Mail;

namespace BusinessLogicLayer
{
    public class Utilities
    {
        public static bool SendEmail(string toEmail, string subject, string body)
        {
            var client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("williedevonsmith@gmail.com", "Escalade27")
            };

            var mm = new MailMessage("williedevonsmith@gmail.com", toEmail, subject, body);

            client.Send(mm);
            return true;
        }
    }
}
