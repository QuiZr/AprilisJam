using AprilisJam.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AprilisJam.Models
{
    public class RegistrationForm
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Imię*")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Nazwisko*")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Email*"), DataType(DataType.EmailAddress, ErrorMessage = "Jesteś pewien, że wiesz jak wygląda poprawny email?")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Numer kontaktowy*"), DataType(DataType.PhoneNumber, ErrorMessage = "To nie jest poprawny numer telefonu.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Miasto*")]
        public string City { get; set; }

        [Display(Name = "Uczelnia")]
        public string School { get; set; }

        [Display(Name = "Co oprócz żartów kojarzy ci się z Prima Aprilis?")]
        public string AprilisQuestion { get; set; }

        [Display(Name = "Czy masz jakieś dodatkowe uwagi? ")]
        public string AdditionalNotes { get; set; }

        public RegistrationForm() { }

        public async Task<bool> RegisterUserWithEmailConfirmation(AprilisJamRegistrationContext context)
        {
            int memberCount = await context.RegistrationForms.CountAsync();

            string emailContent = "";
            if (memberCount > 30)
                emailContent = "Na chwilę obecną mamy komplet ludzi. Jeżeli zwolni się jakieś miejsce zostanieś o tym poinformowany :)";
            else
                emailContent = "Widzimy się na miejscu!";

            await Services.EmailSender.SendEmailAsync(
                this.Name,
                this.Surname,
                this.Email,
                "Aprilis Jam - Rejestracja",
                emailContent);

            context.Add(this);
            await context.SaveChangesAsync();

            return true;
        }
    }
}

