using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface IStudentService
    {
        public Task<List<StudentResponseDTO>> GetAllStudentsAsync();
        public Task<IEnumerable<StudentResponseDTO>> GetAllStudentsByClassIdAsync(int classId);
        public Task<StudentResponseDTO> CreateStudentAsync(StudentRequestDTO studentDto);
        public Task<StudentResponseDTO> UpdateStudentAsync(int id, StudentRequestDTO studentDto);
        public Task<bool> DeleteStudentAsync(int id);
    }
}
