using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;

namespace BL.Services
{
    public class CheckupService : ICheckupService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckupService(IUnitofWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CheckupDto AddCheckup(CreateCheckupDto dto)
        {
            var checkupRepo = _unitOfWork.GetRepository<Checkup>();
            var checkup = new Checkup
            {
                PatientOIB = dto.PatientOIB,
                Type = dto.Type,
                DateTime = dto.DateTime,
                MedicalHistoryId = dto.MedicalHistoryId,
            };
          //  var checkup = _mapper.Map<Checkup>(checkupDto);
            checkupRepo.Add(checkup);
            _unitOfWork.Save();

            return _mapper.Map<CheckupDto>(checkup);
        }

        public CheckupDto UpdateCheckup(int checkupId, CheckupDto checkupDto)
        {
            var checkupRepo = _unitOfWork.GetRepository<Checkup>();
            var existingCheckup = checkupRepo.FindOne(c => c.Id == checkupId);

            if (existingCheckup == null)
                throw new KeyNotFoundException("Checkup not found.");

            _mapper.Map(checkupDto, existingCheckup);
            checkupRepo.Update(existingCheckup);
            _unitOfWork.Save();

            return _mapper.Map<CheckupDto>(existingCheckup);
        }

        public IEnumerable<CheckupDto> GetCheckupsByPatient(string patientOIB)
        {
            var checkupRepo = _unitOfWork.GetRepository<Checkup>();
            var checkups = checkupRepo.Find(c => c.PatientOIB == patientOIB);

            return _mapper.Map<IEnumerable<CheckupDto>>(checkups);
        }

        public CheckupDto GetCheckupById(int checkupId)
        {
            var checkupRepo = _unitOfWork.GetRepository<Checkup>();
            var checkup = checkupRepo.FindOne(c => c.Id == checkupId);

            if (checkup == null)
                throw new KeyNotFoundException("Checkup not found.");

            return _mapper.Map<CheckupDto>(checkup);
        }
    }
}
