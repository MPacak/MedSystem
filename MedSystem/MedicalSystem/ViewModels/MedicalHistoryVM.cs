using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    public class MedicalHistoryVM
    {
        public int Id { get; set; }

        [Required]
        public string PatientOIB { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<CheckupVM> Checkups { get; set; } = new List<CheckupVM>();
        public List<PrescriptionVM> Prescriptions { get; set; } = new List<PrescriptionVM>();
    }
}
