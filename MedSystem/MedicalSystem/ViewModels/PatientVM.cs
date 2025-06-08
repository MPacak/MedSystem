using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    public class PatientVM
    {
        [Required]
        [StringLength(11)]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "OIB has to have 11 digits.")]

        public string OIB { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }
    }
}
