using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class DrugDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public DoseType DoseType { get; set; }
    }
}
