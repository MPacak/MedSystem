using DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CreatePrescriptionVM
    {
        [Required]
        public string PatientOIB { get; set; }
        [Required]
        public string DrugName { get; set; }
        [Required]
        [Range(0, 5000)]
        public int Dose { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DoseType DoseType { get; set; }
        [Required]
        public int MedicalHistoryId { get; set; }
        public IEnumerable<SelectListItem> MedicalHistories { get; set; }
= new List<SelectListItem>();
    }
}
