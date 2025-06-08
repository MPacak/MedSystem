using BL.DTO;

namespace BL.IServices
{
    public interface IMedicalHistoryService
    {
        MedicalHistoryDto AddMedicalHistory(MedicalHistoryDto medicalHistoryDto);
        MedicalHistoryDto UpdateMedicalHistory(int historyId, MedicalHistoryDto medicalHistoryDto);
        IEnumerable<MedicalHistoryDto> GetMedicalHistoryByPatient(string patientOIB);
        MedicalHistoryDto GetMedicalHistoryById(int id);
    }
}
