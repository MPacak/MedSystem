using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string PatientOIB { get; set; }
        public string DrugName { get; set; }
       
        public DateTime Date { get; set; }
      
        public int Dose { get; set; }

        public DoseType DoseType { get; set; }
    }
}
