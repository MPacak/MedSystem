﻿using AutoMapper;
using BL.DTO;
using BL.IServices;
using BL.Services;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalSystem.Controllers
{
    public class MedicalHistoryController : Controller
    {
        private readonly IMedicalHistoryService _medicalHistoryService;
        private readonly IMapper _mapper;
        private readonly IDiseaseService _diseaseService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService, IMapper mapper, IDiseaseService diseaseService)
        {
            _medicalHistoryService = medicalHistoryService;
            _mapper = mapper;
            _diseaseService = diseaseService;
        }
        [Authorize]
        public IActionResult List(string oib)
        {
            var history = _medicalHistoryService.GetMedicalHistoryByPatient(oib);
            var historyVMs = _mapper.Map<IEnumerable<MedicalHistoryVM>>(history);
      
            ViewBag.PatientOIB = oib;
         
            return View(historyVMs);
        }
        [Authorize]
        public IActionResult Create(string oib)
        {

           
            if (string.IsNullOrEmpty(oib))
            {
                return BadRequest("Invalid patient OIB.");
            }
            ViewBag.Diseases = GetDiseaseSelectList();
            return View(new MedicalHistoryVM { PatientOIB = oib });

        }

        private List<SelectListItem> GetDiseaseSelectList()
        {

            var diseases = _diseaseService.GetDiseaseNameList();
            return diseases.Select(g => new SelectListItem { Value = g.Name, Text = g.Name }).ToList();
        
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(MedicalHistoryVM historyVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Diseases = GetDiseaseSelectList();
                return View(historyVM);
            }
            
            var historyDto = _mapper.Map<MedicalHistoryDto>(historyVM);
            _medicalHistoryService.AddMedicalHistory(historyDto);

            return RedirectToAction("List", new { oib = historyVM.PatientOIB });
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var history = _medicalHistoryService.GetMedicalHistoryById(id);
            if (history == null) return NotFound();

            var historyVM = _mapper.Map<MedicalHistoryVM>(history);
            return View(historyVM);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MedicalHistoryVM historyVM)
        {
            if (!ModelState.IsValid) return View(historyVM);

            var historyDto = _mapper.Map<MedicalHistoryDto>(historyVM);
            _medicalHistoryService.UpdateMedicalHistory(id, historyDto);

            return RedirectToAction("List", new { oib = historyVM.PatientOIB });
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var historyDto = _medicalHistoryService.GetMedicalHistoryById(id);
            if (historyDto == null)
                return NotFound();

            var vm = _mapper.Map<MedicalHistoryVM>(historyDto);
            vm.Checkups = vm.Checkups
                             .OrderByDescending(c => c.DateTime)
                             .ToList();
            vm.Prescriptions = vm.Prescriptions.OrderByDescending(p => p.Date).ToList();

            return View(vm);
        }
    }
}
