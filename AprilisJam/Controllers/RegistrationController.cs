using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Data;

namespace AprilisJam.Controllers
{
    public class RegistrationController : Controller
    {
        private GameJamContext _context { get; }

        public RegistrationController(GameJamContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Succeed()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,Phone,City,School,AprilisQuestion,AdditionalNotes")] UserApplication userApplication)
        {
            if (ModelState.IsValid)
            {
                int memberCount = await _context.UserApplications.CountAsync();

                string emailContent = "";
                if (memberCount > 30)
                    emailContent = "Na chwilê obecn¹ mamy komplet ludzi. Je¿eli zwolni siê jakieœ miejsce zostanieœ o tym poinformowany :)";
                else
                    emailContent = "Widzimy siê na miejscu!";

                await Services.EmailSender.SendEmailAsync(
                    userApplication.Name,
                    userApplication.Surname,
                    userApplication.Email,
                    "Aprilis Jam - Rejestracja",
                    emailContent);

                _context.Add(userApplication);
                await _context.SaveChangesAsync();

                return RedirectToAction("Succeed");
            }

            return View(userApplication);
        }
    }
}
