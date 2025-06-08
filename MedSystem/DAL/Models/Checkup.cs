using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Checkup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public CheckupType Type { get; set; } 

        [ForeignKey("Patient")]
        public string PatientOIB { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string? PicturePath { get; set; } 
    }
}
