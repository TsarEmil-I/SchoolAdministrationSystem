using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<int> CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
        Task<List<Student>> GetStudentsByClassIdAsync(int classId);
    }
}
