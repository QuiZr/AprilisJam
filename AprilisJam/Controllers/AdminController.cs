using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AprilisJam.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Models;
using AprilisJam.ViewModels;
using AprilisJam.Services;

namespace AprilisJam.Controllers
{
    public class AdminController : Controller
    {
        private AprilisJamRegistrationContext _context { get; }
        private AppSettings _appSettings { get; }
        private IEmailSender _emailSender;

        public AdminController(AprilisJamRegistrationContext context, IOptions<AppSettings> appSettings, IEmailSender emailSender)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAuthorized())
                return RedirectToLogin();
            return View(await _context.RegistrationForms.ToListAsync());
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (IsAuthorized())
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            if (_appSettings.Password == login.Password)
                Response.Cookies.Append("pw", login.Password);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("pw");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            if (id == null)
                return NotFound();

            var userApplication = await _context
                .RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);

            if (userApplication == null)
                return NotFound();

            return View(userApplication);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            if (id == null)
                return NotFound();

            var userApplication = await _context
                .RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);

            if (userApplication == null)
                return NotFound();
            return View(userApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] RegistrationForm userApplication)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            if (id != userApplication.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserApplicationExists(userApplication.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(userApplication);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            if (id == null)
                return NotFound();

            var userApplication = await _context
                .RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);

            if (userApplication == null)
                return NotFound();

            return View(userApplication);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            var userApplication = await _context
                .RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);

            _context.RegistrationForms.Remove(userApplication);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail(int? id)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            if (id == null)
                return NotFound();

            var userApplication = await _context
                .RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);

            if (userApplication == null)
                return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(int id, [Bind("Title,Content")] SendEmail sendEmail)
        {
            if (!IsAuthorized())
                return RedirectToLogin();

            if (ModelState.IsValid)
            {
                var userApplication = await _context
                    .RegistrationForms
                    .SingleOrDefaultAsync(m => m.ID == id);

                if (userApplication == null)
                    return NotFound();

                await _emailSender.SendEmailAsync(
                    userApplication.Name,
                    userApplication.Surname,
                    userApplication.Email,
                    sendEmail.Title,
                    sendEmail.Content
                   );

                return Ok($"Email do {userApplication.Email} poszed³!");
            }
            return View();
        }

        private bool UserApplicationExists(int id)
        {
            return _context
                .RegistrationForms
                .Any(e => e.ID == id);
        }

        private bool IsAuthorized()
        {
            return (Request.Cookies["pw"] == _appSettings.Password);
        }

        private RedirectToActionResult RedirectToLogin()
        {
            return RedirectToAction("Login");
        }
    }
}