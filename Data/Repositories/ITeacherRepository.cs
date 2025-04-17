using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<Teacher> GetTeacherByUserIdAsync(string id);
        Task<int> CreateTeacherAsync(Teacher teacher);
        Task<Teacher> UpdateTeacherAsync(Teacher teacher);
        Task<List<Teacher>> GetAllTeachersWithoutClassesAsync();
        Task<bool> DeleteTeacherAsync(int id);
        Task CreateTeachersFromRangeAsync(List<Teacher> teachers);
    }
}
