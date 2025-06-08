using DAL.Models;
using MedicalSystem.Utils;
using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    public class CheckupVM
    {
        public int Id { get; set; }
        public string PatientOIB { get; set; }

        [Required]
        public CheckupType Type { get; set; }

        public DateTime DateTime { get; set; }
       public string? PicturePath { get; set; }
        public string TypeDescription
      => EnumHelper.GetDescription(Type);

    }
}
