using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Data;
using Microsoft.Extensions.Options;

namespace AprilisJam.Controllers
{
    public class UserApplicationsController : Controller
    {
        private GameJamContext _context { get; }
        private AppSettings _appSettings { get; }

        public UserApplicationsController(GameJamContext context, IOptions<AppSettings> appSettings)
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

        [Route("")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("")]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] UserApplication userApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userApplication);

                await _context.SaveChangesAsync();

                int number = await _context.UserApplications.CountAsync();

                string message = "";
                if (number > 30)
                {
                    message = "Na chwilê obecn¹ mamy komplet ludzi. Je¿eli zwolni siê jakieœ miejsce zostanieœ o tym poinformowany :)";
                }
                else
                {
                    message = "Widzimy siê na miejscu!";
                }

                await Services.EmailSender.SendEmailAsync(
                    userApplication.Name,
                    userApplication.Surname,
                    userApplication.Email,
                    "Aprilis Jam",
                    message);

                return RedirectToAction("RegistrationCompleted");
            }
            return View(userApplication);
        }

        [Route("RegistrationCompleted")]
        public IActionResult RegistrationCompleted()
        {
            return View();
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] UserApplication userApplication)
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
