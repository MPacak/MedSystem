using DAL.Models;
using MedicalSystem.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    public class CheckupVM
    {
        public int Id { get; set; }

        public string PatientOIB { get; set; }

        [Required]
        public CheckupType Type { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
       public string? PicturePath { get; set; }
        public string TypeDescription
      => EnumHelper.GetDescription(Type);
        public string DiseaseName { get; set; }
        public string? Notes { get; set; }
    }
    public class CreateCheckupVM
    {
        [Required]
        public string PatientOIB { get; set; }
        [Required]
        public CheckupType Type { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int MedicalHistoryId { get; set; }
        public IEnumerable<SelectListItem> MedicalHistories { get; set; }
  = new List<SelectListItem>();
    }
}
