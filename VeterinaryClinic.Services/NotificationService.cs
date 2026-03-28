using System;
using System.Net;
using System.Net.Mail;

namespace VeterinaryClinic.Services
{
    public class NotificationService
    {
        public void SendEmail(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("your-email@example.com", "From Name");
            var toAddress = new MailAddress(to);
            const string fromPassword = "your-email-password";

            var smtp = new SmtpClient
            {
                Host = "smtp.example.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public void SendSMS(string phoneNumber, string message)
        {
            // Assuming using some SMS gateway API
            Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
            // Here you would integrate SMS sending logic using an SMS service provider
        }
    }
}