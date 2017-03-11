using System.ComponentModel.DataAnnotations;

namespace AprilisJam.Models
{
    public class RegistrationForm
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Nie wypełniłeś pola koniecznego proszę stąd iść, albo się poprawić.")]
        [Display(Name = "Imię*")]
        public string Name { get; set; }
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
    }
}

