using AprilisJam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprilisJam.Services
{
    public interface IConfirmationEmailSender
    {
        Task SendConfirmationEmailAsync(RegistrationForm registrationForm);
    }
}
