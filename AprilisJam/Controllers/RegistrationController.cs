using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private EmailContent _emailContent { get; }

        public RegistrationController(IConfirmationEmailSender confirmationEmailSender, AprilisJamRegistrationContext context, IOptions<EmailContent> emailContent)
        {
            _confirmationEmailSender = confirmationEmailSender;
            _context = context;
            _emailContent = emailContent.Value;
        }

        public async Task<IActionResult> Index()
        {
            int memberCount = await _context.RegistrationForms.CountAsync();
            if (memberCount >= _emailContent.MemberThreshold)
                ViewData["MemberCountMessage"] = $"Niestety, wszystkie miejsca s¹ ju¿ zajête :( <br />Mo¿esz wpisaæ siê na listê rezerwow¹, bêdziesz {memberCount + 1 - _emailContent.MemberThreshold} w kolejce.";
            else
                ViewData["MemberCountMessage"] = $"Iloœæ wolnych miejsc: {_emailContent.MemberThreshold - memberCount}";

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
