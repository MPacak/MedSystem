using AutoMapper;
using BL.IServices;
using MedicalSystem.Utils;
using MedicalSystem.ViewModels;
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

            var medicalHistory = _medicalHistoryService.GetMedicalHistoryByPatient(oib);
            var checkups = _checkupService.GetCheckupsByPatient(oib);
            var prescriptions = _prescriptionService.GetPrescriptionsByPatient(oib);

            var csv = new StringBuilder();
            csv.AppendLine("OIB,First Name,Last Name,Date of Birth,Gender,Medical History,Checkups,Prescriptions");

            var medicalHistoryData = string.Join(" | ", medicalHistory.Select(m => $"{m.DiseaseName}: {m.StartDate:yyyy-MM-dd} - {m.EndDate:yyyy-MM-dd}"));
            var checkupData = string.Join(" | ", checkups.Select(c => $"{EnumHelper.GetDescription(c.Type)} on {c.DateTime:yyyy-MM-dd}"));
            var prescriptionData = string.Join(" | ", prescriptions.Select(p => $"{p.DrugName} - {p.Dose} {p.DoseType}"));

            csv.AppendLine($"{patient.OIB},{patient.FirstName},{patient.LastName}," +
                           $"{patient.DateOfBirth.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}," +
                           $"{patient.Gender},{medicalHistoryData},{checkupData},{prescriptionData}");

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", $"Patient_{oib}_Data.csv");
        }

    }
}
