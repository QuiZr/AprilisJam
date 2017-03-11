using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Data;
using AprilisJam.Models;
using AprilisJam.Services;

namespace AprilisJam.Controllers
{
    public class RegistrationController : Controller
    {
        private AprilisJamRegistrationContext _context { get; }
        private IConfirmationEmailSender _confirmationEmailSender { get; }

        public RegistrationController(AprilisJamRegistrationContext context, IConfirmationEmailSender confirmationEmailSender)
        {
            _context = context;
            _confirmationEmailSender = confirmationEmailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Succeeded()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] RegistrationForm registrationForm)
        {
            if (ModelState.IsValid)
            {
                await _confirmationEmailSender.SendConfirmationEmailAsync(registrationForm);
                return RedirectToAction("Succeeded");
            }

            return View(registrationForm);
        }
    }
}
