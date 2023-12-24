using Microsoft.Extensions.Configuration;
using MiniETicaretAPI.Application.Abstactions.Services;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MiniETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private string _mailUserName;
        private string _mailPassword;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailUserName = Environment.GetEnvironmentVariable(_configuration["Mail:UserName"]) ?? "";
            _mailPassword = Environment.GetEnvironmentVariable(_configuration["Mail:Password"]) ?? "";
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var item in tos)
            {
                mail.To.Add(item);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(_mailUserName, "Mini E-Ticaret", Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_mailUserName, _mailPassword);
            smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.Host = _configuration["Mail:Server"];
            await smtp.SendMailAsync(mail);
        }
    }
}
