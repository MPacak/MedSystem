using AutoMapper;
using BL.DTO;
using BL.IServices;
using BL.Services;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc.Core;
using FluentValidation;

namespace MedicalSystem.Controllers
{
    public class PatientController : Controller
    {
        // GET: PatientController
        private readonly IPatientService _patientService;
        private readonly IGenderService _genderService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IGenderService genderService, IMapper mapper)
        {
            _patientService = patientService;
            _genderService = genderService;
            _mapper = mapper;
        }

        public IActionResult Index(string searchTerm, int? page, int pageSize = 10)
        {
            int pageNumber = page ?? 1;

            var patients = _patientService.SearchPatients(searchTerm);
            var patientVMs = _mapper.Map<IEnumerable<PatientVM>>(patients);

            ViewBag.PageSize = pageSize;
            ViewBag.SearchTerm = searchTerm; 

            return View(patientVMs.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Details(string oib)
        {
            var patient = _patientService.GetPatientByOIB(oib);
            if (patient == null) return NotFound();

            var patientVM = _mapper.Map<PatientVM>(patient);
            return View(patientVM);
        }

        public IActionResult Create()
        {
            ViewBag.Genders = GetGenderSelectList();
            return View();
        }

        private List<SelectListItem> GetGenderSelectList()
        {
            var genders = _genderService.GetGenderTypeList();
            return genders.Select(g => new SelectListItem { Value = g.Type, Text = g.Type }).ToList();
        }

        [HttpPost]
        public IActionResult Create(PatientVM patientVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genders = GetGenderSelectList();
                return View(patientVM);
            }
            var patientDto = _mapper.Map<PatientDto>(patientVM);
            try
            {
                _patientService.AddPatient(patientDto);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex) 
            {
                foreach (var err in ex.Errors)
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Genders = GetGenderSelectList();
            return View(patientVM);
        }

        public IActionResult Edit(string oib)
        {
            var patient = _patientService.GetPatientByOIB(oib);
            if (patient == null) return NotFound();

            var patientVM = _mapper.Map<PatientVM>(patient);
            ViewBag.Genders = GetGenderSelectList();
            return View(patientVM);
        }

        [HttpPost]
        public IActionResult Edit(string oib, PatientVM patientVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genders = GetGenderSelectList();
                return View(patientVM);
            }

            var patientDto = _mapper.Map<PatientDto>(patientVM);
            try
            {
                _patientService.UpdatePatient(oib, patientDto);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                foreach (var err in ex.Errors)
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Genders = GetGenderSelectList();
            return View(patientVM);
        }
    }
}

