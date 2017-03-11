using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Data;
using AprilisJam.Models;

namespace AprilisJam.Controllers
{
    public class RegistrationController : Controller
    {
        private AprilisJamRegistrationContext _context { get; }

        public RegistrationController(AprilisJamRegistrationContext context)
        {
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
                bool result = await registrationForm.RegisterUserWithEmailConfirmation(_context);
                if (result)
                    return RedirectToAction("Succeeded");
            }

            return View(registrationForm);
        }
    }
}
