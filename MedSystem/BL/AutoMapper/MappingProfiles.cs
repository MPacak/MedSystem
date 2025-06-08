using AutoMapper;
using BL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>();
               
            CreateMap<PatientDto, Patient>()
                .ForMember(dst => dst.GenderId, src => src.Ignore())
                .ForMember(dst => dst.Gender, src => src.Ignore());

            CreateMap<Checkup, CheckupDto>().ReverseMap();

            CreateMap<MedicalHistory, MedicalHistoryDto>().ReverseMap();

            CreateMap<Prescription, PrescriptionDto>()
                .ForMember(dst => dst.DoseType, src => src.MapFrom(opt => opt.Drug.DoseType));
            CreateMap<PrescriptionDto, Prescription>();
            CreateMap<Drug, DrugDto>().ReverseMap();
                

        }
    }
}
