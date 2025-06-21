using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class MedicalHistoryService : IMedicalHistoryService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDiseaseService _service;

        public MedicalHistoryService(IUnitofWork unitOfWork, IMapper mapper, IDiseaseService service)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
        }

        public MedicalHistoryDto AddMedicalHistory(MedicalHistoryDto medicalHistoryDto)
        {
            var historyRepo = _unitOfWork.GetRepository<MedicalHistory>();
            var medicalHistory = _mapper.Map<MedicalHistory>(medicalHistoryDto);
            medicalHistory.DiseaseId = _service.GetDIseaseIdByName(medicalHistoryDto.DiseaseName);
            historyRepo.Add(medicalHistory);
          
            _unitOfWork.Save();

            return _mapper.Map<MedicalHistoryDto>(medicalHistory);
        }

        public MedicalHistoryDto UpdateMedicalHistory(int historyId, MedicalHistoryDto medicalHistoryDto)
        {
            var historyRepo = _unitOfWork.GetRepository<MedicalHistory>();
            var existingHistory = historyRepo.FindOne(h => h.Id == historyId);

            if (existingHistory == null)
                throw new KeyNotFoundException("Medical history not found.");

            _mapper.Map(medicalHistoryDto, existingHistory);
            existingHistory.DiseaseId = _service.GetDIseaseIdByName(medicalHistoryDto.DiseaseName);
            historyRepo.Update(existingHistory);
            _unitOfWork.Save();

            return _mapper.Map<MedicalHistoryDto>(existingHistory);
        }

        public IEnumerable<MedicalHistoryDto> GetMedicalHistoryByPatient(string patientOIB)
        {
            var historyRepo = _unitOfWork.GetRepository<MedicalHistory>();
            var histories = historyRepo.Find(h => h.PatientOIB == patientOIB);

            var query = _unitOfWork
             .GetRepository<MedicalHistory>()
             .Find(h => h.PatientOIB == patientOIB)
             .Include(h => h.Disease)
             .Include(h => h.Checkups)
             .Include(h => h.Prescriptions)
              .ThenInclude(p => p.Drug);
            return _mapper.Map<IEnumerable<MedicalHistoryDto>>(query);
               
        }
        public MedicalHistoryDto GetMedicalHistoryById(int id)
        {

               var query = _unitOfWork
            .GetRepository<MedicalHistory>()
            .Find(h => h.Id == id)                  
            .Include(h => h.Disease)
            .Include(h => h.Checkups)
            .Include(h => h.Prescriptions)
             .ThenInclude(p => p.Drug);
            var historiesDto = _mapper.Map<MedicalHistoryDto>(query.FirstOrDefault());
            return historiesDto;
        }
    
    }
}
