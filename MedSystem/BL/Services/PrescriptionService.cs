using AutoMapper;
using BL.DTO;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;

namespace BL.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrescriptionService(IUnitofWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PrescriptionDto AddPrescription(PrescriptionDto prescriptionDto)
        {
            var prescriptionRepo = _unitOfWork.GetRepository<Prescription>();
            var drugRepo = _unitOfWork.GetRepository<Drug>();
            var prescription = _mapper.Map<Prescription>(prescriptionDto);
            prescription.DrugId = drugRepo.FindOne(d => d.Name.Equals(prescriptionDto.DrugName)).Id;
            prescriptionRepo.Add(prescription);
            _unitOfWork.Save();

            return _mapper.Map<PrescriptionDto>(prescription);
        }

        public PrescriptionDto UpdatePrescription(int prescriptionId, PrescriptionDto prescriptionDto)
        {
            var prescriptionRepo = _unitOfWork.GetRepository<Prescription>();
            var existingPrescription = prescriptionRepo.FindOne(p => p.Id == prescriptionId);

            if (existingPrescription == null)
                throw new KeyNotFoundException("Prescription not found.");

            _mapper.Map(prescriptionDto, existingPrescription);
            prescriptionRepo.Update(existingPrescription);
            _unitOfWork.Save();

            return _mapper.Map<PrescriptionDto>(existingPrescription);
        }

        public IEnumerable<PrescriptionDto> GetPrescriptionsByPatient(string patientOIB)
        {
            var prescriptionRepo = _unitOfWork.GetRepository<Prescription>();
            var prescriptions = prescriptionRepo.Find(p => p.PatientOIB == patientOIB);
            var prescriptionDto = _mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions);
            foreach (var p in prescriptionDto)
            {
                p.DrugName = GetDrugByID(p.Id);
            }
            return prescriptionDto;
        }
        private IEnumerable<DrugDto> GetAllDrugs()
        {
            var drugRepo = _unitOfWork.GetRepository<Drug>();
            var drugs = drugRepo.GetAll();
            return _mapper.Map<IEnumerable<DrugDto>>(drugs);

        }
        private string GetDrugByID(int id)
        {
            var drugs = GetAllDrugs();
            return drugs.FirstOrDefault(d => d.Id == id).Name;

        }

        public IList<string> GetAllDrugNames()
        {
            var drugs = GetAllDrugs();
            return drugs.Select(d => d.Name).ToList();
        }

        public DoseType GetDoseType(string name)
        {
            var drugs = GetAllDrugs();
            return drugs.FirstOrDefault(d => d.Name == name).DoseType;
        }
    }
}
