using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AprilisJam.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Models;

namespace AprilisJam.Controllers
{
    public class AdminController : Controller
    {
        private AprilisJamRegistrationContext _context { get; }
        private AppSettings _appSettings { get; }

        public AdminController(AprilisJamRegistrationContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Index(string pw)
        {
            if (pw == _appSettings.Password)
            {
                Response.Cookies.Append("pw", pw);
                return View(await _context.RegistrationForms.ToListAsync());
            }
            if (!IsAuthorized())
                return RedirectToDefaultRoute();

            return View(await _context.RegistrationForms.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAuthorized())
                return RedirectToDefaultRoute();

            if (id == null)
            {
                return NotFound();
            }

            var userApplication = await _context.RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);
            if (userApplication == null)
            {
                return NotFound();
            }

            return View(userApplication);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAuthorized())
                return RedirectToDefaultRoute();

            if (id == null)
            {
                return NotFound();
            }

            var userApplication = await _context.RegistrationForms.SingleOrDefaultAsync(m => m.ID == id);
            if (userApplication == null)
            {
                return NotFound();
            }
            return View(userApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] RegistrationForm userApplication)
        {
            if (!IsAuthorized())
                return RedirectToDefaultRoute();

            if (id != userApplication.ID)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAuthorized())
                return RedirectToDefaultRoute();

            if (id == null)
            {
                return NotFound();
            }

            var userApplication = await _context.RegistrationForms
                .SingleOrDefaultAsync(m => m.ID == id);
            if (userApplication == null)
            {
                return NotFound();
            }

            return View(userApplication);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAuthorized())
                return RedirectToDefaultRoute();

            var userApplication = await _context.RegistrationForms.SingleOrDefaultAsync(m => m.ID == id);
            _context.RegistrationForms.Remove(userApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserApplicationExists(int id)
        {
            return _context.RegistrationForms.Any(e => e.ID == id);
        }

        private bool IsAuthorized()
        {
            if (Request.Cookies["pw"] == _appSettings.Password)
                return true;
            return false;
        }

        private RedirectToRouteResult RedirectToDefaultRoute()
        {
            return RedirectToRoute(new
            {
                controller = "Registration",
                action = "Index",
            });
        }
    }
}