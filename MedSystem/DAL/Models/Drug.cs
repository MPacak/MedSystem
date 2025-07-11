﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Drug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
         [Required]
         [Column(TypeName = "text")]

        public DoseType DoseType { get; set; }
    }

}
