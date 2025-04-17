using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface IStudentService
    {
        public Task<List<StudentDTO>> GetAllStudentsAsync();
        public Task<StudentDTO> GetStudentByIdAsync(int id);
        public Task<IEnumerable<StudentDTO>> GetAllStudentsByClassIdAsync(int classId);
        public Task<StudentDTO> CreateStudentAsync(StudentDTO studentDto);
        public Task<StudentDTO> UpdateStudentAsync(int id, StudentDTO studentDto);
        public Task<bool> DeleteStudentAsync(int id);
        public Task CreateStudentsFromRangeAsync(List<StudentDTO> students);
    }
}
