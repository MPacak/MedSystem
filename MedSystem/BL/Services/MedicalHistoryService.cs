using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;

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
     
            return GetDiseasesForAll(histories);
        }
        public MedicalHistoryDto GetMedicalHistoryById(int id)
        {
            var historyRepo = _unitOfWork.GetRepository<MedicalHistory>();
            var histories = historyRepo.FindOne(h => h.Id == id);
            var historiesDto = _mapper.Map<MedicalHistoryDto>(histories);
            historiesDto.DiseaseName = _service.GetDiseaseNameById(histories.DiseaseId);
            return historiesDto;
        }
       private IEnumerable<MedicalHistoryDto> GetDiseasesForAll(IQueryable<MedicalHistory> histories)
        {
            var historiesDto = _mapper.Map<IEnumerable<MedicalHistoryDto>>(histories);
            foreach (var dto in historiesDto)
            {
                dto.DiseaseName = _service.GetDiseaseNameById(histories.First(h => h.Id == dto.Id).DiseaseId);
                    

            }
            return historiesDto;
        }
    }
}
