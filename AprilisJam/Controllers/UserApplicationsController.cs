using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Data;

namespace AprilisJam.Controllers
{
    public class UserApplicationsController : Controller
    {
        private GameJamContext _context { get; }

        public UserApplicationsController(GameJamContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
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
                    "Aprilis Jam - Rejestracja",
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
    }
}
