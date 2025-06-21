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

            CreateMap<Checkup, CheckupDto>()
            .ForMember(dst => dst.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dst => dst.DiseaseName, opt => opt.MapFrom(src => src.MedicalHistory.Disease.Name));
            CreateMap<CheckupDto, Checkup>()
          .ForMember(dst => dst.MedicalHistory, opt => opt.Ignore());
            CreateMap<MedicalHistory, MedicalHistoryDto>()
            .ForMember(dst => dst.DiseaseName, opt => opt.MapFrom(src => src.Disease.Name))
            .ForMember(dst => dst.Checkups, opt => opt.MapFrom(src => src.Checkups))
            .ForMember(dst => dst.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));
           
            CreateMap<MedicalHistoryDto, MedicalHistory>()
                .ForMember(dst => dst.Checkups, opt => opt.Ignore())
                .ForMember(dst => dst.Disease, opt => opt.Ignore())
                .ForMember(dst => dst.Prescriptions, opt => opt.Ignore());

            CreateMap<Prescription, PrescriptionDto>()
                .ForMember(dst => dst.DoseType, src => src.MapFrom(opt => opt.Drug.DoseType))
                .ForMember(dst => dst.DrugName, opt => opt.MapFrom(src => src.Drug.Name))
                 .ForMember(dst => dst.DiseaseName, opt => opt.MapFrom(src => src.MedicalHistory.Disease.Name));
            CreateMap<PrescriptionDto, Prescription>()
            .ForMember(dst => dst.MedicalHistory, opt => opt.Ignore())
            .ForMember(dst => dst.Drug, opt => opt.Ignore());
            CreateMap<CreatePrescriptionDto, Prescription>()
            .ForMember(dst => dst.PatientOIB, opt => opt.MapFrom(src => src.PatientOIB))
            .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dst => dst.Dose, opt => opt.MapFrom(src => src.Dose))
            .ForMember(dst => dst.MedicalHistoryId, opt => opt.MapFrom(src => src.MedicalHistoryId))
            .ForMember(dst => dst.DrugId, opt => opt.Ignore())
            .ForMember(dst => dst.Patient, opt => opt.Ignore())
            .ForMember(dst => dst.Drug, opt => opt.Ignore())
            .ForMember(dst => dst.MedicalHistory, opt => opt.Ignore());


            CreateMap<Drug, DrugDto>().ReverseMap();
                

        }
    }
}
