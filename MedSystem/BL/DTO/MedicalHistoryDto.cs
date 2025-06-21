using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class MedicalHistoryDto
    {
        public int Id { get; set; }
        public string PatientOIB { get; set; }
        public string DiseaseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<CheckupDto> Checkups { get; set; } = new List<CheckupDto>();
        public List<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
    }
}
