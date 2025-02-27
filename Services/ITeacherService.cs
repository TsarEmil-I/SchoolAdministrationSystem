using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface ITeacherService
    {
        public Task<IEnumerable<TeacherDTO>> GetAllTeachersAsync();
        public Task<IEnumerable<TeacherDTO>> GetAllTeachersWithoutClassesAsync();
        public Task<TeacherDTO> GetTeacherByIdAsync(int id);
        public Task<TeacherDTO> CreateTeacherAsync(TeacherDTO teacherDto);
        public Task<TeacherDTO> UpdateTeacherAsync(int id, TeacherDTO teacherDto);
        public Task<bool> DeleteTeacherAsync(int id);
    }
}
