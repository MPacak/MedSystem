using BL.DTO;
using DAL.Models;

namespace BL.IServices
{
    public interface IPrescriptionService
    {
        PrescriptionDto AddPrescription(PrescriptionDto prescriptionDto);
        PrescriptionDto UpdatePrescription(int prescriptionId, PrescriptionDto prescriptionDto);
        IEnumerable<PrescriptionDto> GetPrescriptionsByPatient(string patientOIB);
        IList<string> GetAllDrugNames();
        DoseType GetDoseType(string name);
    }
}
