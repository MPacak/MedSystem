using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public string PatientOIB { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public ICollection<Checkup> Checkups { get; set; } = new List<Checkup>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
