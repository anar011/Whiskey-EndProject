﻿using EndProject.Helpers;
using EndProject.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace EndProject.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public void Send(string to, string subject, string html, string from = null)
        {
            var a = _emailSettings;
            // create message

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.FromAddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };


            // send email
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);





           
        }




    }
}
