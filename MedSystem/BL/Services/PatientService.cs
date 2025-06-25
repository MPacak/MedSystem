using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;
using FluentValidation;
using System.Data.Entity;

namespace BL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenderService _genderService;
        private readonly IValidator<PatientDto> _dtoValidator;
        public PatientService(IUnitofWork unitOfWork, IMapper mapper, IGenderService genderService, IValidator<PatientDto> dtoValidator)
        {
            _dtoValidator = dtoValidator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _genderService = genderService;
        }

        public PatientDto GetPatientByOIB(string oib)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            var patient = patientRepo.FindOne(p => p.OIB == oib);


            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            var patientDto = _mapper.Map<PatientDto>(patient);
            patientDto.Gender = _genderService.GetGenderById(patient.GenderId);
            return patientDto;
        }

        public IEnumerable<PatientDto> GetPatientsByLastName(string lastName)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            var patients = patientRepo.Find(p => p.LastName.Contains(lastName));
            var patientsDto = GetGenderForAll(patients);
            return patientsDto;
        }

        private IEnumerable<PatientDto> GetGenderForAll(IQueryable<Patient> patients)
        {
            var patientsDto = _mapper.Map<IEnumerable<PatientDto>>(patients);
            foreach (var dto in patientsDto)
            {
                dto.Gender = _genderService.GetGenderById(patients.First(p => p.OIB == dto.OIB).GenderId);

            }
            return patientsDto;
        }

        public IEnumerable<PatientDto> GetAllPatients()
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            var patients = patientRepo.GetAll();

            return GetGenderForAll(patients);
        }
        public IEnumerable<PatientDto> SearchPatients(string query)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();

            IQueryable<Patient> patients;

            if (string.IsNullOrEmpty(query))
            {
                patients = patientRepo.GetAll();
            }
            else
            {
                patients = patientRepo.Find(p => p.OIB.Contains(query) || p.LastName.ToLower().Contains(query.ToLower()));
            }

            return GetGenderForAll(patients);
        }

        public PatientDto AddPatient(PatientDto patientDto)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            _dtoValidator.ValidateAndThrow(patientDto);
            if (patientRepo.Any(p => p.OIB == patientDto.OIB))
                throw new InvalidOperationException("Patient with the given OIB already exists.");
          
            var patient = _mapper.Map<Patient>(patientDto);
            patient.GenderId = _genderService.GetGenderIdByName(patientDto.Gender);
            patientRepo.Add(patient);
            _unitOfWork.Save();

            return _mapper.Map<PatientDto>(patient);
        }

        public PatientDto UpdatePatient(string oib, PatientDto patientDto)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            _dtoValidator.ValidateAndThrow(patientDto);
            var existingPatient = patientRepo.FindOne(p => p.OIB == oib);

            if (existingPatient == null)
                throw new KeyNotFoundException("Patient not found.");
         
            _mapper.Map(patientDto, existingPatient);
            existingPatient.GenderId = _genderService.GetGenderIdByName(patientDto.Gender);
            patientRepo.Update(existingPatient);
            _unitOfWork.Save();

            return _mapper.Map<PatientDto>(existingPatient);
        }

        public void DeletePatient(string oib)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            var patient = patientRepo.FindOne(p => p.OIB == oib);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            patientRepo.Delete(patient);
            _unitOfWork.Save();
        }

        public IEnumerable<CheckupDto> GetPatientCheckups(string oib)
        {
            var checkupRepo = _unitOfWork.GetRepository<Checkup>();
            var checkups = checkupRepo.Find(c => c.PatientOIB == oib);

            return _mapper.Map<IEnumerable<CheckupDto>>(checkups);
        }

        public IEnumerable<MedicalHistoryDto> GetPatientMedicalHistory(string oib)
        {
            var historyRepo = _unitOfWork.GetRepository<MedicalHistory>();
            var medicalHistory = historyRepo.Find(mh => mh.PatientOIB == oib);

            return _mapper.Map<IEnumerable<MedicalHistoryDto>>(medicalHistory);
        }

        public IEnumerable<PrescriptionDto> GetPatientPrescriptions(string oib)
        {
            var prescriptionRepo = _unitOfWork.GetRepository<Prescription>();
            var prescriptions = prescriptionRepo.Find(p => p.PatientOIB == oib);

            return _mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions);
        }
       

    }
}
