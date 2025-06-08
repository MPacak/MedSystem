

namespace MedicalSystem.ViewModels
{
    public class PatientDashboardVM
    {
        public PatientVM Patient { get; set; }
        public List<MedicalHistoryVM> MedicalHistorySummary { get; set; } = new();
        public List<PrescriptionVM> PrescriptionSummary { get; set; } = new();
        public List<CheckupVM> CheckupSummary { get; set; } = new();
    }
}
