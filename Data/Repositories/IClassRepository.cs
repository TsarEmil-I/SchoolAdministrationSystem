using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetAllClassesAsync();
        Task<Class?> GetClassByIdAsync(int id);
        Task<int> CreateClassAsync(Class classItem);
    }
}
