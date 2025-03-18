using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetAllClassesAsync();
        Task<Class?> GetClassByIdAsync(int id);
        Task<int> CreateClassAsync(Class classItem);
        Task<Class> UpdateClassAsync(int id, Class classItem);
        Task<bool> DeleteClassAsync(int id);

    }
}
