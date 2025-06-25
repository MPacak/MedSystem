using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Doctor
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string? SecurityToken { get; set; } 
        public string? PwdHash { get; set; } 
        public string? PwdSalt { get; set; }
    }
}
