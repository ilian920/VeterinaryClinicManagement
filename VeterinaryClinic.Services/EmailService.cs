using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace VeterinaryClinic.Services
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string host, int port, string user, string pass)
        {
            _smtpHost = host;
            _smtpPort = port;
            _smtpUser = user;
            _smtpPass = pass;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_smtpUser);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    client.EnableSsl = true;
                    await client.SendMailAsync(message);
                }
            }
        }
    }
}