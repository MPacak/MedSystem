using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Patient
    {
        [Key]
        [Required]
        public string OIB { get; set; } 

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
