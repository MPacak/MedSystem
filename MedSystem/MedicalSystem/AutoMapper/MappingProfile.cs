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
            CreateMap<CreateCheckupVM, CreateCheckupDto>();
            CreateMap<CreateCheckupDto, CreateCheckupVM>()
                .ForMember(dest => dest.MedicalHistories, opt => opt.Ignore());
            CreateMap<CreatePrescriptionDto, CreatePrescriptionVM>()
                .ForMember(dest => dest.MedicalHistories, opt => opt.Ignore());
            CreateMap<CreatePrescriptionVM, CreatePrescriptionDto>();
            CreateMap<MedicalHistoryDto, MedicalHistoryVM>().ReverseMap();
            CreateMap<PrescriptionDto, PrescriptionVM>().ReverseMap();
        }
    }
}
