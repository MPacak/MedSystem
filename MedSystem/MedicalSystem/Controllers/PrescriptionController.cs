using AutoMapper;
using BL.DTO;
using BL.IServices;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalSystem.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IMedicalHistoryService _historyService;
        private readonly IMapper _mapper;

        public PrescriptionController(IPrescriptionService prescriptionService, IMedicalHistoryService historyService, IMapper mapper)
        {
            _prescriptionService = prescriptionService;
            _historyService = historyService;
            _mapper = mapper;
        }

        public IActionResult List(string oib)
        {
            var prescriptions = _prescriptionService.GetPrescriptionsByPatient(oib);
            var prescriptionVMs = _mapper.Map<IEnumerable<PrescriptionVM>>(prescriptions);
          
            ViewBag.PatientOIB = oib;
            return View(prescriptionVMs);
        }

        public IActionResult Create(string oib)
        {
            if (string.IsNullOrEmpty(oib))
            {
                return BadRequest("Invalid patient OIB.");
            }
          
            var drugNames = _prescriptionService.GetAllDrugNames();
            ViewBag.Drugs = new SelectList(drugNames);
            var perscriptionVm = CreateVM(oib);
            return View(perscriptionVm);
        }

        private CreatePrescriptionVM CreateVM(string oib)
        {
            var histories = _historyService
         .GetMedicalHistoryByPatient(oib)
         .Select(h => new SelectListItem
         {
             Value = h.Id.ToString(),
             Text = h.DiseaseName
         });
            return new CreatePrescriptionVM
            {
                PatientOIB = oib,
                Date = DateTime.Now,
                MedicalHistories = histories
            };
        }

        [HttpPost]
        public IActionResult Create(CreatePrescriptionVM prescriptionVM)
        {
            if (!ModelState.IsValid)
            {
                var drugNames = _prescriptionService.GetAllDrugNames();
                ViewBag.Drugs = new SelectList(drugNames);
                return View(CreateVM(prescriptionVM.PatientOIB));
            }

            var prescriptionDto = _mapper.Map<CreatePrescriptionDto>(prescriptionVM);
            _prescriptionService.AddPrescription(prescriptionDto);

            return RedirectToAction("List", new { oib = prescriptionVM.PatientOIB });
        }
        [HttpGet]
        public JsonResult GetDoseType(string drugName)
        {
            var doseType = _prescriptionService.GetDoseType(drugName); 
            return Json(doseType.ToString());
        }
    }
}
