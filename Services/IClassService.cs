using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface IClassService
    {
        public Task<IEnumerable<ClassResponseDTO>> GetAllClassesAsync();
        public Task<ClassResponseDTO> GetClassByIdAsync(int id);
        public Task<ClassResponseDTO> CreateClassAsync(ClassRequestDTO classDto);
        public Task<ClassResponseDTO> UpdateClassAsync(int id, ClassRequestDTO classDto);
        public Task<bool> DeleteClassAsync(int id);


    }
}
