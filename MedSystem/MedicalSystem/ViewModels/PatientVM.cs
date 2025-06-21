using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    public class PatientVM
    {
        [Required]
        [StringLength(11)]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "OIB has to have 11 digits.")]

        public string OIB { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
    }
}
