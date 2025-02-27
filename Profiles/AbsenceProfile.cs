using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Profiles
{
    public class AbsenceProfile : Profile
    {
        public AbsenceProfile()
        {
            CreateMap<Absence, AbsenceDTO>();
            CreateMap<AbsenceDTO, Absence>();
        }
    }
}
