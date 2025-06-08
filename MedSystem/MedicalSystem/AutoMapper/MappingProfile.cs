using AutoMapper;
using BL.DTO;
using MedicalSystem.ViewModels;

namespace MedicalSystem.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PatientDto, PatientVM>().ReverseMap();
            CreateMap<CheckupDto, CheckupVM>().ReverseMap();
            CreateMap<MedicalHistoryDto, MedicalHistoryVM>().ReverseMap();
            CreateMap<PrescriptionDto, PrescriptionVM>().ReverseMap();
        }
    }
}
