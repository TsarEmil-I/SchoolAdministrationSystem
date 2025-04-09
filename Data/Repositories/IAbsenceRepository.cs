using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Utils;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface IAbsenceRepository
    {
        Task<List<Absence>> GetAllAbsencesAsync();
        Task<PaginatedListUtil<Absence>> GetPagedAbsencesAsync(int pageNumber, int pageSize);
        Task<List<Absence>> GetAllAbsencesByStudentIdAsync(int studentId);
        Task<List<Absence>> GetAllAbsencesByClassIdAsync(int classId);
        Task<List<Absence>> GetAllAbsencesByClassIdPeriodAsync(int classId, DateTime start, DateTime end);
        Task<Absence?> GetAbsenceByIdAsync(int id);
        Task<int> CreateAbsenceAsync(Absence absence);
        Task<bool> DeleteAbsenceAsync(int id);
        Task<bool> UpdateAbsenceAsync(Absence absence);
    }
}
