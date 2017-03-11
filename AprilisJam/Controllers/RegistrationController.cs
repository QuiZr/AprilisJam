using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Data;
using AprilisJam.Models;
using AprilisJam.Services;
using System;

namespace AprilisJam.Controllers
{
    public class RegistrationController : Controller
    {
        private IConfirmationEmailSender _confirmationEmailSender { get; }
        private AprilisJamRegistrationContext _context { get; }

        public RegistrationController(IConfirmationEmailSender confirmationEmailSender, AprilisJamRegistrationContext context)
        {
            _confirmationEmailSender = confirmationEmailSender;
            _context = context;
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

                registrationForm.RegistrationDate = DateTime.Now;

                _context.Add(registrationForm);
                await _context.SaveChangesAsync();

                return RedirectToAction("Succeeded");
            }

            return View(registrationForm);
        }
    }
}
