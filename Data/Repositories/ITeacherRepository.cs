using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<int> CreateTeacherAsync(Teacher teacher);
        Task<List<Teacher>> GetAllTeachersWithoutClassesAsync();

    }
}
