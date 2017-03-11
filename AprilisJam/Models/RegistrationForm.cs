using AprilisJam.Data;
using AprilisJam.Services;
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
        [Display(Name = "Email*"), EmailAddress(ErrorMessage = "Jesteś pewien, że wiesz jak wygląda poprawny email?")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Numer kontaktowy*"), DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}", ErrorMessage = "To nie jest poprawny numer telefonu.")]
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
    }
}

