using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.Models;
using MedicalSystem.Utils;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.Controllers
{
    public class CheckupController : Controller
    {
        private readonly ICheckupService _checkupService;
        private readonly IMapper _mapper;

        public CheckupController(ICheckupService checkupService, IMapper mapper)
        {
            _checkupService = checkupService;
            _mapper = mapper;
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
            ViewBag.CheckupTypes = EnumHelper.GetEnumSelectList<CheckupType>();

            return View(new CheckupVM { PatientOIB = oib });
        }

        [HttpPost]
        public IActionResult Create(CheckupVM checkupVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CheckupTypes = EnumHelper.GetEnumSelectList<CheckupType>();
                return View(checkupVM);
            }

            var checkupDto = _mapper.Map<CheckupDto>(checkupVM);
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
        public IActionResult Edit(int id, IFormFile image)
        {
            var checkup = _checkupService.GetCheckupById(id);
            if (checkup == null) return NotFound();

            if (image != null && image.Length > 0)
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

                _checkupService.UpdateCheckup(id, checkup);
                return RedirectToAction("List", new { oib = checkup.PatientOIB });
            } else
            {
                var checkupVM = _mapper.Map<CheckupVM>(checkup);
                return View(checkupVM);
            }

            
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
