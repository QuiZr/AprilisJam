using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AprilisJam.Services
{
    public static class EmailSender
    {
        public static async Task SendEmailAsync(string name, string surname, string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", ""));
            emailMessage.To.Add(new MailboxAddress($"{name} {surname}", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("", 6969, true).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync("", "").ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
