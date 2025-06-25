using AutoMapper;
using BL.IServices;
using MedicalSystem.Utils;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace MedicalSystem.Controllers
{
    public class PatientDashboardController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ICheckupService _checkupService;
        private readonly IMedicalHistoryService _medicalHistoryService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IMapper _mapper;

        public PatientDashboardController(IPatientService patientService, ICheckupService checkupService,
                                          IMedicalHistoryService medicalHistoryService, IPrescriptionService prescriptionService,
                                          IMapper mapper)
        {
            _patientService = patientService;
            _checkupService = checkupService;
            _medicalHistoryService = medicalHistoryService;
            _prescriptionService = prescriptionService;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index(string oib)
        {
            var patient = _patientService.GetPatientByOIB(oib);
            if (patient == null) return NotFound();
            var checkupVMs = _mapper.Map<List<CheckupVM>>(_checkupService.GetCheckupsByPatient(oib)).Take(3).ToList();
            var dashboardVM = new PatientDashboardVM
            {
                Patient = _mapper.Map<PatientVM>(patient),
                MedicalHistorySummary = _mapper.Map<List<MedicalHistoryVM>>(_medicalHistoryService.GetMedicalHistoryByPatient(oib)).Take(3).ToList(),
                PrescriptionSummary = _mapper.Map<List<PrescriptionVM>>(_prescriptionService.GetPrescriptionsByPatient(oib)).Take(3).ToList(),
                CheckupSummary = checkupVMs

            };

            return View(dashboardVM);
        }
        public IActionResult ExportPatientData(string oib)
        {
            var patient = _patientService.GetPatientByOIB(oib);
            if (patient == null) return NotFound();

            var histories = _medicalHistoryService.GetMedicalHistoryByPatient(oib);

            var csv = new StringBuilder();
            csv.Append('\uFEFF');
            csv.AppendLine("OIB,First Name,Last Name,Date of Birth,Gender," +
                           "Disease,Start Date,End Date,Checkups,Prescriptions");
            if (!histories.Any())
            {
               
                csv.AppendLine(
                    $"{patient.OIB}," +
                    $"{patient.FirstName}," +
                    $"{patient.LastName}," +
                    $"{patient.DateOfBirth:yyyy-MM-dd}," +
                    $"{patient.Gender}," +
                    ",,,,"
                );
            } else
            {
                foreach (var h in histories)
                {
                    var checkupData = string.Join(" | ",
                        h.Checkups.Select(c =>
                            $"{EnumHelper.GetDescription(c.Type)} on {c.DateTime:yyyy-MM-dd}"));

                    var prescriptionData = string.Join(" | ",
                        h.Prescriptions.Select(p =>
                            $"{p.DrugName} – {p.Dose} {p.DoseType}"));

                    csv.AppendLine(
                        $"{patient.OIB}," +
                        $"{patient.FirstName}," +
                        $"{patient.LastName}," +
                        $"{patient.DateOfBirth:yyyy-MM-dd}," +
                        $"{patient.Gender}," +
                        $"{h.DiseaseName}," +
                        $"{h.StartDate:yyyy-MM-dd}," +
                        $"{(h.EndDate.HasValue ? h.EndDate.Value.ToString("yyyy-MM-dd") : "")}," +
                        $"\"{checkupData}\"," +
                        $"\"{prescriptionData}\""
                    );
                }
            }
          

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"Patient_{oib}_Data.csv");
        }

    }
}
