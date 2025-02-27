using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDTO>();
            CreateMap<ClassDTO, Class>();
        }
    }
}
