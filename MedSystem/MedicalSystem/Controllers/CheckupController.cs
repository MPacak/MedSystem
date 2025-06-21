using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.Models;
using MedicalSystem.Utils;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalSystem.Controllers
{
    public class CheckupController : Controller
    {
        private readonly ICheckupService _checkupService;
        IMedicalHistoryService _historyService;
        private readonly IMapper _mapper;

        public CheckupController(ICheckupService checkupService, IMapper mapper, IMedicalHistoryService historyService)
        {
            _checkupService = checkupService;
            _mapper = mapper;
            _historyService = historyService;
        }

        public IActionResult List(string oib)
        {
            var checkups = _checkupService.GetCheckupsByPatient(oib);
            var checkupVMs = _mapper.Map<IEnumerable<CheckupVM>>(checkups);
            ViewBag.PatientOIB = oib;
            return View(checkupVMs);
        }


        public IActionResult Create(string oib)
        {
            if (string.IsNullOrEmpty(oib))
            {
                return BadRequest("Invalid patient OIB.");
            }
          
            var vm = CreateCheckup(oib);
           
            ViewBag.CheckupTypes = EnumHelper.GetEnumSelectList<CheckupType>();

            return View(vm);
        }

        private CreateCheckupVM CreateCheckup(string oib)
        {
            var histories = _historyService.GetMedicalHistoryByPatient(oib);
            var vm = new CreateCheckupVM
            {
                PatientOIB = oib,
                DateTime = DateTime.Now,
                MedicalHistories = histories
               .Select(h => new SelectListItem
               {
                   Value = h.Id.ToString(),
                   Text = h.DiseaseName
               })
               .ToList()
            };
            return vm;
        }

        [HttpPost]
        public IActionResult Create(CreateCheckupVM checkupVM)
        {
            if (!ModelState.IsValid)
            {
                var vm = CreateCheckup(checkupVM.PatientOIB);
                ViewBag.CheckupTypes = EnumHelper.GetEnumSelectList<CheckupType>();
                return View(checkupVM);
            }

            var checkupDto = _mapper.Map<CreateCheckupDto>(checkupVM);
            _checkupService.AddCheckup(checkupDto);

            return RedirectToAction("List", new { oib = checkupVM.PatientOIB });
        }
        public IActionResult Edit(int id)
        {
            var checkup = _checkupService.GetCheckupById(id);
            if (checkup == null) return NotFound();


            // return View(GetTypeDescription(checkup));
            var checkupVM = _mapper.Map<CheckupVM>(checkup);
            return View(checkupVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, CheckupVM vm, IFormFile image)
        {
            var checkup = _checkupService.GetCheckupById(id);
            if (checkup == null) return NotFound();
            bool hasNotes = !string.IsNullOrWhiteSpace(vm.Notes);
            bool hasImage = image != null && image.Length > 0;
            if (!hasNotes && !hasImage)
            {
                ViewBag.CheckupTypes = EnumHelper.GetEnumSelectList<CheckupType>(); 
                return View(vm);
            }

            if (hasImage)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                checkup.PicturePath = $"/uploads/{uniqueFileName}";

            }
            checkup.Notes = vm.Notes;
            _checkupService.UpdateCheckup(id, checkup);
            return RedirectToAction("List", new { oib = checkup.PatientOIB });
        }

        public IActionResult Details(int id)
        {
            var checkup = _checkupService.GetCheckupById(id);
            if (checkup == null) return NotFound();
            var checkupVM = _mapper.Map<CheckupVM>(checkup);
            return View(checkupVM);
        }
    
     
    }
}
