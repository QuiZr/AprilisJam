using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprilisJam.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string name, string surname, string email, string subject, string message);
    }
}
