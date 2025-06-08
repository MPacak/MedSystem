using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.IServices
{
    public interface IPatientService
    {
        PatientDto GetPatientByOIB(string oib);
        IEnumerable<PatientDto> GetPatientsByLastName(string lastName);
        IEnumerable<PatientDto> GetAllPatients();
        IEnumerable<PatientDto> SearchPatients(string query);
        PatientDto AddPatient(PatientDto patientDto);
        PatientDto UpdatePatient(string oib, PatientDto patientDto);
        void DeletePatient(string oib);

        IEnumerable<CheckupDto> GetPatientCheckups(string oib);
        IEnumerable<MedicalHistoryDto> GetPatientMedicalHistory(string oib);
        IEnumerable<PrescriptionDto> GetPatientPrescriptions(string oib);

        // byte[] ExportPatientsToCsv();
    }
}

