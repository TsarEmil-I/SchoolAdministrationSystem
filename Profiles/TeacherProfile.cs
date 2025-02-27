using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<TeacherDTO, Teacher>();
        }
    }
}
