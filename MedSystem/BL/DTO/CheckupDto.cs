using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class CheckupDto
    {
        public int Id { get; set; }
        public string PatientOIB { get; set; }
        public CheckupType Type { get; set; }
        public DateTime DateTime { get; set; }
        public string? PicturePath { get; set; }
    }
}
