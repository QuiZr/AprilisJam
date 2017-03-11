using AprilisJam.Data;
using AprilisJam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprilisJam.Services
{
    public class ConfirmationEmailSender : IConfirmationEmailSender
    {
        private AprilisJamRegistrationContext _context { get; }
        private IEmailSender _emailSender { get; }
        private EmailContent _emailContent { get; }

        public ConfirmationEmailSender(AprilisJamRegistrationContext context, IEmailSender emailSender, IOptions<EmailContent> emailContent)
        {
            _context = context;
            _emailSender = emailSender;
            _emailContent = emailContent.Value;
        }

        public async Task SendConfirmationEmailAsync(RegistrationForm registrationForm)
        {
            int memberCount = await _context.RegistrationForms.CountAsync();

            string emailContent = "";
            if (memberCount > _emailContent.MemberThreshold)
                emailContent = _emailContent.ContentIfOver;
            else
                emailContent = _emailContent.ContentIfUnder;

            await _emailSender.SendEmailAsync(
                registrationForm.Name,
                registrationForm.Surname,
                registrationForm.Email,
                _emailContent.Title,
                emailContent);
        }
    }
}
