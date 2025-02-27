using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface IAbsenceRepository
    {
        Task<List<Absence>> GetAllAbsencesAsync();
        Task<Absence?> GetAbsenceByIdAsync(int id);
        Task<int> CreateAbsenceAsync(Absence absence);
        Task<bool> DeleteAbsenceAsync(int id);
        Task<bool> UpdateAbsenceAsync(Absence absence);

    }
}
