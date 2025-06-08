using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    public class PrescriptionVM
    {
        public int Id { get; set; } 

        [Required]
        public string PatientOIB { get; set; } 

        [Required]
        public string DrugName { get; set; } 

        [Required]
        [Range(0, 5000)]
        public int Dose { get; set; }


        [Required]
        public DateTime Date { get; set; }
        public DoseType DoseType { get; set; }
    }
}
