using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Profiles
{
    public class AbsenceProfile : Profile
    {
        public AbsenceProfile()
        {
            CreateMap<Absence, AbsenceResponseDTO>();
            CreateMap<AbsenceRequestDTO, Absence>();
        }
    }
}
