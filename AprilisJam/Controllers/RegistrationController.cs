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
                    emailContent = "Na chwil� obecn� mamy komplet ludzi. Je�eli zwolni si� jakie� miejsce zostanie� o tym poinformowany :)";
                else
                    emailContent = "Widzimy si� na miejscu!";

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
