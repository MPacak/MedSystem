using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public string PatientOIB { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public Drug Drug { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Dose { get; set; }
        [Required]
        [ForeignKey("MedicalHistory")]
        public int MedicalHistoryId { get; set; }
        public MedicalHistory MedicalHistory { get; set; }

    }
}
