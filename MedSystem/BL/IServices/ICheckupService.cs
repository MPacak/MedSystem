using BL.DTO;

namespace BL.IServices
{
    public interface ICheckupService
    {
        CheckupDto AddCheckup(CheckupDto checkupDto);
        CheckupDto UpdateCheckup(int checkupId, CheckupDto checkupDto);
        IEnumerable<CheckupDto> GetCheckupsByPatient(string patientOIB);
        CheckupDto GetCheckupById(int checkupId);
    }
}
