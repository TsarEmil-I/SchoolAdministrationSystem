using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface IClassService
    {
        public Task<IEnumerable<ClassDTO>> GetAllClassesAsync();
        public Task<ClassDTO> GetClassByIdAsync(int id);
        public Task<ClassDTO> GetClassByClassName(string className);
        public Task<ClassDTO> CreateClassAsync(ClassDTO classDto);
        public Task<ClassDTO> UpdateClassAsync(int id, ClassDTO classDto);
        public Task<bool> DeleteClassAsync(int id);
    }
}
