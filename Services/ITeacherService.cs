using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface ITeacherService
    {
        public Task<IEnumerable<TeacherResponseDTO>> GetAllTeachersAsync();
        public Task<IEnumerable<TeacherResponseDTO>> GetAllTeachersWithoutClassesAsync();
        public Task<TeacherResponseDTO> GetTeacherByIdAsync(int id);
        public Task<TeacherResponseDTO> CreateTeacherAsync(TeacherRequestDTO teacherDto);
        public Task<TeacherResponseDTO> UpdateTeacherAsync(int id, TeacherRequestDTO teacherDto);
        public Task<bool> DeleteTeacherAsync(int id);
    }
}
