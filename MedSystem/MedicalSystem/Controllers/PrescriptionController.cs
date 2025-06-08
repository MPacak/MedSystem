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
        private readonly IMapper _mapper;

        public PrescriptionController(IPrescriptionService prescriptionService, IMapper mapper)
        {
            _prescriptionService = prescriptionService;
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

            return View(new PrescriptionVM { PatientOIB = oib });
        }

        [HttpPost]
        public IActionResult Create(PrescriptionVM prescriptionVM)
        {
            if (!ModelState.IsValid)
            {
                var drugNames = _prescriptionService.GetAllDrugNames();
                ViewBag.Drugs = new SelectList(drugNames);
                return View(prescriptionVM);
            }

            var prescriptionDto = _mapper.Map<PrescriptionDto>(prescriptionVM);
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
