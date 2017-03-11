using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AprilisJam.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace AprilisJam.Controllers
{
    public class AdminController : Controller
    {
        private GameJamContext _context { get; }
        private AppSettings _appSettings { get; }

        public AdminController(GameJamContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [Route("AdminOverwiev")]
        public async Task<IActionResult> Index(string pw)
        {
            if (pw == _appSettings.Password)
            {
                Response.Cookies.Append("pw", pw);
                return View(await _context.UserApplications.ToListAsync());
            }
            if (Request.Cookies["pw"] == _appSettings.Password)
            {
                return View(await _context.UserApplications.ToListAsync());
            }

            return RedirectToAction("Create");
        }

        [Route("AdminDetails/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (Request.Cookies["pw"] != _appSettings.Password)
            {
                return RedirectToAction("Create");
            }

            if (id == null)
            {
                return NotFound();
            }

            var userApplication = await _context.UserApplications
                .SingleOrDefaultAsync(m => m.ID == id);
            if (userApplication == null)
            {
                return NotFound();
            }

            return View(userApplication);
        }

        [Route("AdminEdit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (Request.Cookies["pw"] != _appSettings.Password)
            {
                return RedirectToAction("Create");
            }

            if (id == null)
            {
                return NotFound();
            }

            var userApplication = await _context.UserApplications.SingleOrDefaultAsync(m => m.ID == id);
            if (userApplication == null)
            {
                return NotFound();
            }
            return View(userApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AdminEdit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] UserApplication userApplication)
        {
            if (Request.Cookies["pw"] != _appSettings.Password)
            {
                return RedirectToAction("Create");
            }

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

        [Route("AdminDelete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (Request.Cookies["pw"] != _appSettings.Password)
            {
                return RedirectToAction("Create");
            }

            if (id == null)
            {
                return NotFound();
            }

            var userApplication = await _context.UserApplications
                .SingleOrDefaultAsync(m => m.ID == id);
            if (userApplication == null)
            {
                return NotFound();
            }

            return View(userApplication);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("AdminDelete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Request.Cookies["pw"] != _appSettings.Password)
            {
                return RedirectToAction("Create");
            }

            var userApplication = await _context.UserApplications.SingleOrDefaultAsync(m => m.ID == id);
            _context.UserApplications.Remove(userApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserApplicationExists(int id)
        {
            return _context.UserApplications.Any(e => e.ID == id);
        }
    }
}