using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

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
